using Android.Content;
using Android.OS;
using Android.Views;
using AndroidX.AppCompat.App;
using AndroidX.Core.Content;
using AndroidX.RecyclerView.Widget;
using AndroidX.SwipeRefreshLayout.Widget;
using ArmaOps.Application.Example.ViewModels;
using ArmaOps.Common;
using ArmaOps.Droid.Common.Fragments;
using GalaSoft.MvvmLight.Helpers;
using System;
using System.Collections.Generic;
using AndroidX.AppCompat.Widget;
#nullable enable

namespace ArmaOps.Droid.Example
{
    public class ExampleFragment : LifetimeFragment
    {
        readonly IList<Binding> _bindings = new List<Binding>();

        [InjectOnCreate]
        IExampleViewModel? _viewModel;

        [InjectOnCreateView]
        ExampleRecyclerViewAdapter? _adapter;

        RecyclerView? _recyclerView;
        SwipeRefreshLayout? _swipeLayout;
        Toolbar? _toolbar;

        public static ExampleFragment NewInstance()
        {
            var fragment = new ExampleFragment();
            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.fragment_example, container, false);

            FindSubViews(view);
            SetupUi();
            SetBindings();

            _viewModel?.Refresh.Execute(null);

            return view;
        }

        public override void OnDestroyView()
        {
            DetachBindings();

            base.OnDestroyView();
        }

        public override void OnStart()
        {
            base.OnStart();
            ConnectHandlers();
        }

        public override void OnStop()
        {
            base.OnStop();
            DisconnectHandlers();
        }

        void HandleShowDialog(object sender, DialogEventArgs e)
        {
#nullable disable warnings
            new AlertDialog.Builder(Activity)
                .SetTitle(e.Title)
                .SetMessage(e.Message)
                .SetPositiveButton(e.PositiveButtonTitle, (IDialogInterfaceOnClickListener)null)
                .Create()
                .Show();
#nullable enable warnings
        }

        void HandleRefresh(object sender, EventArgs e)
        {
            _viewModel?.Refresh.Execute(null);
        }

        void HandleLoadingChanged()
        {
            if (_viewModel != null && _swipeLayout != null)
                _swipeLayout.Refreshing = _viewModel.Loading;
        }

        void HandleDataUpdated()
        {
            if (_adapter != null && _viewModel != null)
                _adapter.ReloadData(_viewModel.Data);
        }

        void SetBindings()
        {
            if (_viewModel != null && _toolbar != null)
            {
                _bindings.Add(this.SetBinding(() => _viewModel.Title, () => _toolbar.Title));
                _bindings.Add(this.SetBinding(() => _viewModel.Loading).WhenSourceChanges(HandleLoadingChanged));
                _bindings.Add(this.SetBinding(() => _viewModel.Data).WhenSourceChanges(HandleDataUpdated));
            }
        }

        void DetachBindings()
        {
            foreach (var b in _bindings)
            {
                b.Detach();
            }
        }

        void FindSubViews(View view)
        {
            _recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            _swipeLayout = view.FindViewById<SwipeRefreshLayout>(Resource.Id.swipeLayout);
            _toolbar = view.FindViewById<Toolbar>(Resource.Id.toolbar);
        }

        void ConnectHandlers()
        {
            if (_swipeLayout != null) _swipeLayout.Refresh += HandleRefresh;
            if (_viewModel != null)
            {
                _viewModel.ShowDialog += HandleShowDialog;
                _viewModel.Connect.Execute(null);
            }
        }

        void DisconnectHandlers()
        {
            if (_viewModel != null)
            {
                _viewModel.Disconnect.Execute(null);
                _viewModel.ShowDialog -= HandleShowDialog;
            }
            if (_swipeLayout != null) _swipeLayout.Refresh -= HandleRefresh;
        }

        void SetupUi()
        {
            if (_toolbar != null) _toolbar.Title = string.Empty; // Stops app title overwriting VM title binding
            (Activity as AppCompatActivity)?.SetSupportActionBar(_toolbar);

            DividerItemDecoration dividerItemDecoration = new DividerItemDecoration(Activity, LinearLayoutManager.Vertical);
            dividerItemDecoration.Drawable = ContextCompat.GetDrawable(Activity, Resource.Drawable.divider);
            _recyclerView?.AddItemDecoration(dividerItemDecoration);
            _recyclerView?.SetAdapter(_adapter);
        }
    }
}