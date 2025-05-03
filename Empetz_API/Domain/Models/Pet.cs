using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Models;

public partial class Pet
{
	[Key]
	[Required]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid Id { get; set; }= Guid.NewGuid();

    public string? Name { get; set; } = null!;
	[ForeignKey(nameof(Breed))]
	public Guid? BreedId { get; set; }

    public int? Age { get; set; }

    public string ?Gender { get; set; } = null!;

    public string ?Discription { get; set; } = null!;

    public string? ImagePath { get; set; }
    public byte[] Image { get; set; } = null!;
	[ForeignKey(nameof(Category))]
	public Guid CategoryId { get; set; }
	[ForeignKey(nameof(User))]
	public Guid UserId { get; set; }
	[ForeignKey(nameof(Location))]
	public Guid? LocationId { get; set; }
	public bool ?Vaccinated { get; set; }

    public DateTime? PetPosted { get; set; }
    public DateTime? VaccinationDate { get; set; }

    public bool? Certified { get; set; }
	public long ?Price { get; set; }
	public virtual Breed Breed { get; set; } = null!;

    public string ?height { get; set; }
    public string ?weight { get; set; }

    public string? Address { get; set; }
    public string Status { get; set; }

    public virtual PetsCategory Category { get; set; } = null!;
	

    public virtual ICollection<Favourite> Favourites { get; set; } = new List<Favourite>();

    public virtual Location Location { get; set; } = null!;

    public virtual ICollection<ReportedPost> ReportedPosts { get; set; } = new List<ReportedPost>();

    public virtual User User { get; set; } = null!;
	
	
}
