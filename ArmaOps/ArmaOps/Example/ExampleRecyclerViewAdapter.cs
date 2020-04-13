using Android.Runtime;
using Android.Views;
using AndroidX.RecyclerView.Widget;
using ArmaOps.Application.Example.ViewModels;
using ArmaOps.Domain.Example;
using Autofac;
using System.Collections.Generic;
using System.Linq;

namespace ArmaOps.Droid.Example
{
    [Preserve(AllMembers = true)]
    public class ExampleRecyclerViewAdapter : RecyclerView.Adapter
    {
        readonly LayoutInflater _inflater;
        readonly ExampleCellViewHolder.Factory _exampleCellViewHolderFactory;
        readonly ILifetimeScope _scope;

        ILifetimeScope? _itemsScope;
        IList<IExampleCellViewModel> _cellVms = new List<IExampleCellViewModel>();

        public ExampleRecyclerViewAdapter(
            ExampleCellViewHolder.Factory cellViewHolderFactory,
            LayoutInflater inflater,
            ILifetimeScope scope)
        {
            _exampleCellViewHolderFactory = cellViewHolderFactory;
            _inflater = inflater;
            _scope = scope;
        }

        public override int ItemCount => _cellVms?.Count() ?? 0;

        public override int GetItemViewType(int position)
        {
            return 0;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            return _exampleCellViewHolderFactory(
                        _inflater.Inflate(Resource.Layout.cell_example, parent, false));
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            switch (holder)
            {
                case ExampleCellViewHolder vh:
                    vh.RefreshBindings(_cellVms.ElementAt(position));
                    break;
            }
        }

        public override void OnViewAttachedToWindow(Java.Lang.Object holder)
        {
            base.OnViewAttachedToWindow(holder);

            switch (holder)
            {
                case ExampleCellViewHolder holderObject:
                    holderObject.OnAttach();
                    break;
            }
        }

        public override void OnViewDetachedFromWindow(Java.Lang.Object holder)
        {
            base.OnViewDetachedFromWindow(holder);

            switch (holder)
            {
                case ExampleCellViewHolder holderObject:
                    holderObject.OnDetach();
                    break;
            }
        }

        public void ReloadData(IEnumerable<ExampleModel> models)
        {
            _itemsScope?.Dispose();
            _itemsScope = _scope.BeginLifetimeScope();
            _cellVms.Clear();

            var vmFactory = _itemsScope.Resolve<ExampleCellViewModel.Factory>();
            foreach (var model in models)
            {
                _cellVms.Add(vmFactory(model));
            }

            NotifyDataSetChanged();
        }

        internal void Finish()
        {
            _itemsScope?.Dispose();
        }
    }
}