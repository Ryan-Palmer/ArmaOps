using ArmaOps.Application.Common;
using ArmaOps.Application.Example.Services;
using ArmaOps.Application.Example.ViewModels;
using ArmaOps.Common;
using ArmaOps.Domain.Example;
using GalaSoft.MvvmLight.Command;
using Microsoft.FSharp.Core;
using System;
using System.Collections.Generic;
using static ArmaOps.Caching.CachingTypes;

namespace ArmaOps.Application.Example.ViewModels
{
    public interface IExampleViewModel
    {
        string Title { get; }
        bool Loading { get; }
        IEnumerable<ExampleModel> Data { get; }
        RelayCommand Connect { get; }
        RelayCommand Disconnect { get; }
        RelayCommand Refresh { get; }

        event EventHandler<DialogEventArgs> ShowDialog;
    }

    [Preserve(AllMembers = true)]
    public class ExampleViewModel : DisposableViewModel, IExampleViewModel
    {
        readonly IExampleService _exampleService;
        readonly IMessenger<ExampleMessage> _cellMessenger;
        readonly IMainThreadDispatcher _mainThreadDispatcher;
        IDisposable? _cellMessageToken;

        public ExampleViewModel(
            IExampleService exampleService,
            IMessenger<ExampleMessage> cellMessenger,
            IMainThreadDispatcher mainThreadDispatcher)
        {
            _exampleService = exampleService;
            _cellMessenger = cellMessenger;
            _mainThreadDispatcher = mainThreadDispatcher;
        }

        public event EventHandler<DialogEventArgs>? ShowDialog;

        private bool _loading;
        public bool Loading
        {
            get => _loading;
            private set
            {
                Set(ref _loading, value);
            }
        }

        private IEnumerable<ExampleModel> _data = new List<ExampleModel>();
        public IEnumerable<ExampleModel> Data
        {
            get => _data;
            private set
            {
                Set(ref _data, value);
            }
        }

        public string Title => "Example List";

        private RelayCommand? _connect;
        public RelayCommand Connect => _connect ?? (_connect = new RelayCommand(DoConnect));

        async void DoConnect()
        {
            // This is a message handler used to process a push update
            void HandleCellMessage(ExampleMessage message)
            {
                // Make sure any messages that update the UI do so on the UI thread.
                // You do that by sending the action to the injected main thread dispatcher.
                // This just wraps Xamarin Essentials, which in turn handles whichever platform we are on.
                // We wrap essentials so that we can test this class in isolation,
                // rather than use the Essentials static methods directly..
                _mainThreadDispatcher.BeginInvoke(() => 
                    ShowDialog?.Invoke(
                        this, new DialogEventArgs(
                            message.Title, message.Description, message.ButtonText)));
            }

            // This is where we subscribe to push updates, passing the handler
            if (_cellMessageToken == null)
            {
                // Slightly funny syntax here to convert the C# handler method to an F# func.
                // The reason it is an F# library is that events in F# implement IObservable
                // out of the box, so no need to import Rx.Net etc to make a basic messenger / cache.
  
                var subscribeMessage = MessageCommand<ExampleMessage>.NewSubscribe(
                    FuncConvert.ToFSharpFunc<Tuple<object, ExampleMessage>>(t =>
                         HandleCellMessage(t.Item2))); 

                switch (await _cellMessenger.Post(subscribeMessage))
                {
                    case MessageResult.Subscribed token:
                        _cellMessageToken = token.Item;
                        break;
                }
            }
        }

        private RelayCommand? _disconnect;
        public RelayCommand Disconnect => _disconnect ?? (_disconnect = new RelayCommand(DoDisconnect));

        void DoDisconnect()
        {
            // This is where we unsubscribe from push updates
            _cellMessageToken?.Dispose();
            _cellMessageToken = null;
        }

        private RelayCommand? _refresh;
        public RelayCommand Refresh => _refresh ?? (_refresh = new RelayCommand(DoRefresh));

        async void DoRefresh()
        {
            // Must try/catch in an async void method otherwise crash will be swallowed
            // as if you aren't awaiting the task there is no context to throw on.
            try
            {
                Loading = true;

                // Just returns some dummy data and occasionally throws an exception instead
                Data = await _exampleService.GetSomeData(); 
            }
            catch (Exception e)
            {
                ShowDialog?.Invoke(this, new DialogEventArgs("Example Error", e.Message, "Ok"));
            }
            finally
            {
                Loading = false;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // No need for a safehandle on unmanaged resource here,
                // just using dispose to clean up subscriptions to observables
                DoDisconnect();
            }

            _disposed = true;
        }
    }
}
