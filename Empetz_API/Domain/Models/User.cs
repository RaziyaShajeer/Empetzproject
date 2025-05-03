using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

[Index(nameof(UserName), IsUnique = true)]
public partial class User
{
	[Key]
	[Required]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid Id { get; set; }= Guid.NewGuid();

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }
    public string  UserName { get; set; }
    [EmailAddress]
    public string? Email { get; set; } = null!;
	
	public string Phone { get; set; } = null!;
    public string Password { get; set; } = null!;
    public byte[]? Image { get; set; } = null!;
    public string? ConnectionId { get; set; } 
    public bool IsOnline { get; set; } =false;
    public string ? Address { get; set; }
    public Role? Role { get; set; }
    public Status? Status { get; set; }
    public DateTime? Accountcreated { get; set; }
	public bool IsAnyNotifications { get; set; } = false;

	public virtual ICollection<Favourite> Favourites { get; set; } = new List<Favourite>();

    public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();

    public virtual ICollection<ReportedPost> ReportedPosts { get; set; } = new List<ReportedPost>();
}
