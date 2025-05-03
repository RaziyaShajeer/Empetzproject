namespace Empetz_API.API.Category.RequestObject
{
    public class CategoryUpdateRequest
    {
      

        public string Name { get; set; } = null!;
        public IFormFile? ImageFile { get; set; }
    }
}
