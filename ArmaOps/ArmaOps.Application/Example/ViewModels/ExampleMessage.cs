using ArmaOps.Common;

namespace ArmaOps.Application.Example.ViewModels
{
    [Preserve(AllMembers = true)]
    public class ExampleMessage
    {
        public string Title { get; }
        public string Description { get; }
        public string ButtonText { get; }

        public ExampleMessage(string title, string description, string buttonText)
        {
            Title = title;
            Description = description;
            ButtonText = buttonText;
        }
    }
}
