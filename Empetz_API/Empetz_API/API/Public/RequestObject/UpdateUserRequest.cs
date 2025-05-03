namespace Empetz_API.API.Public.RequestObject
{
    public class UpdateUserRequest
    {
      
        public string FirstName { get; set; } = null!;

        public string? LastName { get; set; }

        public string? Email { get; set; } = null!;


        public string Address { get; set; }
        public IFormFile?  ImageFile { get; set; } // Added for image upload
        public string Phone { get; set; } = null!;
     
    }
}
