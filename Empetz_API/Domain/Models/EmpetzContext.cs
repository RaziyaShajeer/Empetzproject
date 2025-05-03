using System;
using System.Collections.Generic;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public partial class EmpetzContext : DbContext
{
    public EmpetzContext()
    {
    }

    public EmpetzContext(DbContextOptions<EmpetzContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Breed> Breeds { get; set; }

    public virtual DbSet<Favourite> Favourites { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Pet> Pets { get; set; }

    public virtual DbSet<PetsCategory> PetsCategories { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<ContactUs> ContactUs { get; set; }

    public virtual DbSet<Reason> Reasons { get; set; }

    public virtual DbSet<ReportedPost> ReportedPosts { get; set; }
    public virtual DbSet<Message> Messages { get; set; }
    public virtual DbSet<MessageGroup> MessageGroups { get; set; }
    public virtual DbSet<GroupMember> GroupMember { get; set; }




    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Breed>(entity =>
        {
            entity.ToTable("Breed");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.CategoryNavigation).WithMany(p => p.Breeds)
                .HasForeignKey(d => d.Category)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Breed_PetsCategory");
        });

        modelBuilder.Entity<Favourite>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.PetNavigation).WithMany(p => p.Favourites)
                .HasForeignKey(d => d.Pet)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Favourites_Pet");

            entity.HasOne(d => d.UserNavigation).WithMany(p => p.Favourites)
                .HasForeignKey(d => d.User)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Favourites_PublicUser");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.ToTable("Location");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Pet>(entity =>
        {
            entity.ToTable("Pet");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.Image).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);


            //entity.HasOne(d => d.Breed).WithMany(p => p.Pets)
            //    .HasForeignKey(d => d.Breed)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_Pet_Breed");

            //entity.HasOne(d => d.Category).WithMany(p => p.Pets)
            //    .HasForeignKey(d => d.Category)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_Pet_PetsCategory");

            //entity.HasOne(d => d.Location).WithMany(p => p.Pets)
            //    .HasForeignKey(d => d.Location)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_Pet_Location");

            //entity.HasOne(d => d.User).WithMany(p => p.Pets)
            //    .HasForeignKey(d => d.User)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_Pet_PublicUser");

          
        });

        modelBuilder.Entity<PetsCategory>(entity =>
        {
            entity.ToTable("PetsCategory");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Profile");

            entity.Property(e => e.Discription).HasMaxLength(50);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Image).HasMaxLength(50);
            entity.Property(e => e.ProfileName).HasMaxLength(50);

            entity.HasOne(d => d.UserNavigation).WithMany()
                .HasForeignKey(d => d.User)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Profile_PublicUser");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(20);
        });

        modelBuilder.Entity<Reason>(entity =>
        {
            entity.ToTable("Reason");
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Discription).HasMaxLength(100);
        });
       
        modelBuilder.Entity<ReportedPost>(entity =>
        {
            entity.ToTable("ReportedPost");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Reason).HasMaxLength(200);

            entity.HasOne(d => d.PetNavigation).WithMany(p => p.ReportedPosts)
                .HasForeignKey(d => d.Pet)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReportedPost_Pet");

            entity.HasOne(d => d.UserNavigation).WithMany(p => p.ReportedPosts)
                .HasForeignKey(d => d.User)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReportedPost_PublicUser");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
