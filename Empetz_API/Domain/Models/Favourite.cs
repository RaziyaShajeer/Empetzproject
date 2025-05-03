using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public partial class Favourite
{
	[Key]
	[Required]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid Id { get; set; }= Guid.NewGuid();

    public Guid User { get; set; }

    public Guid Pet { get; set; }

    public virtual Pet PetNavigation { get; set; } = null!;

    public virtual User UserNavigation { get; set; } = null!;
}
