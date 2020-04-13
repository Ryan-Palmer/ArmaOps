using ArmaOps.Common;
using ArmaOps.Domain.Example;
using GalaSoft.MvvmLight.Command;
using static ArmaOps.Caching.CachingTypes;

namespace ArmaOps.Application.Example.ViewModels
{
    public interface IExampleCellViewModel
    {
        string Title { get; }
        string Description { get; }
        string ImageUrl { get; }
        RelayCommand Select { get; }
    }

    [Preserve(AllMembers = true)]
    public class ExampleCellViewModel : IExampleCellViewModel
    {
        readonly IMessenger<ExampleMessage> _cellMessenger;
        public delegate IExampleCellViewModel Factory(
           ExampleModel model);

        public ExampleCellViewModel(
            ExampleModel model,
            IMessenger<ExampleMessage> cellMessenger)
        {
            Title = model.Title;
            Description = model.Description;
            ImageUrl = model.ImageUrl;
            _cellMessenger = cellMessenger;
        }

        public string Title { get; }
        public string Description { get; }
        public string ImageUrl { get; }

        private RelayCommand? _select;
        public RelayCommand Select => _select ?? (_select = new RelayCommand(DoSelect));

        async void DoSelect()
        {
            // Do something when clicked - in this case push a message to the parent

            var messageForParentVm =
                MessageCommand<ExampleMessage>.NewSend(
                    new ExampleMessage($"Cell {Title} Clicked", Description, "Thanks"));
            
            await _cellMessenger.Post(messageForParentVm);
        }
    }
}
