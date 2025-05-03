namespace Empetz_API.API.WishList.RequestObject
{
    public class WishListreturnRequest
    {
        public Guid id { get; set; }
        public Guid userid { get; set; }
        public Guid petId { get; set; }
        public string Name { get; set; }
        public string BreedName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Discription { get; set; }
        public byte[] Image { get; set; }

        public string CategoryName { get; set; }
    }
}
