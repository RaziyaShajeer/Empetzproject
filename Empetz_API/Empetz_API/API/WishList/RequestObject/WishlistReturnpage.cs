namespace Empetz_API.API.WishList.RequestObject
{
    public class WishlistReturnpage
    {
        public Guid id { get; set; }
        public required string UserNavigationFirstName { get; set; }
        public required string PetNavigationName { get; set; }
        //public string Name { get; set; }
        public required string PetNavigationBreedName { get; set; }
        public int PetNavigationAge { get; set; }
        public required string PetNavigationGender { get; set; }
        public required string PetNavigationDiscription { get; set; }
        public required byte[] PetNavigationImage { get; set; }
        public required string PetImagePath { get; set; }
        public required string PetNavigationCategoryName { get; set; }
    }
}
