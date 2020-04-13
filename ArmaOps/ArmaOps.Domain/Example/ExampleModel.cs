using ArmaOps.Common;

namespace ArmaOps.Domain.Example
{
    [Preserve(AllMembers = true)]
    public class ExampleModel
    {
        public string Title { get; }
        public string Description { get; }
        public string ImageUrl { get; }

        public ExampleModel(string title, string description, string imageUrl)
        {
            Title = title;
            Description = description;
            ImageUrl = imageUrl;
        }
    }
}
