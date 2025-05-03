namespace Empetz_API.API.Category.RequestObject
{
    public class CategoryRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;


        public string? ImagePath { get; set; }

    }
}
