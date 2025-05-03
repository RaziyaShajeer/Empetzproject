using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public partial class ReportedPost
{
	[Key]
	[Required]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid Id { get; set; }= Guid.NewGuid();

    public Guid Pet { get; set; }

    public Guid User { get; set; }

    public Guid Reason { get; set; } 

    public virtual Pet PetNavigation { get; set; } = null!;

  
    public virtual User UserNavigation { get; set; } = null!;
}
