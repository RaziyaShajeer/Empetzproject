using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public partial class PetsCategory
{
	[Key]
	[Required]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid Id { get; set; } =  Guid.NewGuid();

    public string Name { get; set; } = null!;
 
    public byte[]? Image { get; set; } = null!;
    public string? ImagePath { get; set; }
    public virtual ICollection<Breed> Breeds { get; set; } = new List<Breed>();

    public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();
}
