namespace Empetz_API.API.Pets.RequestObject
{
    public class PostedPetRequest
    {
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public string Gender { get; set; } = null!;
        public string Discription { get; set; } = null!;
        public byte[] Image { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public string BreedName { get; set; }
        public string UserFirstName { get; set; }
        public string LocationName { get; set; } = null!;
        public string ?TimeAgo { get; set; }
        public DateTime? PetPosted { get; set; }
    }
}
