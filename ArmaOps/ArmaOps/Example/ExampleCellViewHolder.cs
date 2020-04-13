using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using ArmaOps.Application.Example.ViewModels;
using ArmaOps.Droid.Common.Commands;
using FFImageLoading.Cross;
using GalaSoft.MvvmLight.Helpers;
using System.Collections.Generic;
# nullable enable

namespace ArmaOps.Droid.Example
{
    [Preserve(AllMembers = true)]
    public class ExampleCellViewHolder : RecyclerView.ViewHolder
    {
        public delegate ExampleCellViewHolder Factory(View view);
        readonly List<CommandDelegates> _commandDelegates = new List<CommandDelegates>();
        readonly List<Binding> _bindings = new List<Binding>();
        readonly TextView _tvTitle;
        readonly TextView _tvDescription;
        readonly MvxCachedImageView _ivThumb;

        IExampleCellViewModel? _viewModel;

        public ExampleCellViewHolder(View view) : base(view)
        {
            _tvTitle = view.FindViewById<TextView>(Resource.Id.tvTitle);
            _tvDescription = view.FindViewById<TextView>(Resource.Id.tvDescription);
            _ivThumb = view.FindViewById<MvxCachedImageView>(Resource.Id.ivThumb);
        }

        public void RefreshBindings(IExampleCellViewModel vm)
        {
            DetachBindings();
            AttachBindings(vm);
        }

        void AttachBindings(IExampleCellViewModel vm)
        {
            _viewModel = vm;
            _bindings.Add(this.SetBinding(() => _viewModel.Title, () => _tvTitle.Text));
            _bindings.Add(this.SetBinding(() => _viewModel.Description, () => _tvDescription.Text));
            _bindings.Add(this.SetBinding(() => _viewModel.ImageUrl, () => _ivThumb.ImagePath));
        }

        public void OnAttach()
        {
            if (_viewModel != null)
                _commandDelegates.Add(ItemView.SubscribeCommand("Click", _viewModel.Select));
        }

        public void OnDetach()
        {
            foreach (var commandDelegate in _commandDelegates)
            {
                commandDelegate.UnsubscribeCommand("Click");
            }
            _commandDelegates.Clear();
        }

        void DetachBindings()
        {
            foreach (var b in _bindings)
            {
                b.Detach();
            }

            _bindings.Clear();
        }

        internal void Finish()
        {
            DetachBindings();
            OnDetach();
        }
    }
}