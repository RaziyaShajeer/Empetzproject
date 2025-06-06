﻿// <auto-generated />
using System;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Domain.Migrations
{
    [DbContext(typeof(EmpetzContext))]
    partial class EmpetzContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.Breed", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Category")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Category");

                    b.ToTable("Breed", (string)null);
                });

            modelBuilder.Entity("Domain.Models.ContactUs", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ContactUs");
                });

            modelBuilder.Entity("Domain.Models.Favourite", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Pet")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("User")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Pet");

                    b.HasIndex("User");

                    b.ToTable("Favourites");
                });

            modelBuilder.Entity("Domain.Models.GroupMember", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("MemberId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("MessageGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MessageGroupId");

                    b.ToTable("GroupMember");
                });

            modelBuilder.Entity("Domain.Models.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Location", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("From")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("FromUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GroupName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("MessageGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("SentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("To")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ToUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("MessageGroupId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Domain.Models.MessageGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsNewMessages")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("newCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("MessageGroups");
                });

            modelBuilder.Entity("Domain.Models.Pet", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Age")
                        .HasColumnType("int");

                    b.Property<Guid?>("BreedId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("Certified")
                        .HasColumnType("bit");

                    b.Property<string>("Discription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varbinary(50)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("LocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("PetPosted")
                        .HasColumnType("datetime2");

                    b.Property<long?>("Price")
                        .HasColumnType("bigint");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("Vaccinated")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("VaccinationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("height")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("weight")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BreedId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("LocationId");

                    b.HasIndex("UserId");

                    b.ToTable("Pet", (string)null);
                });

            modelBuilder.Entity("Domain.Models.PetsCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("PetsCategory", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Profile", b =>
                {
                    b.Property<string>("Discription")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .IsFixedLength();

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varbinary(50)");

                    b.Property<byte[]>("ProfileName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varbinary(50)");

                    b.Property<Guid>("User")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("User");

                    b.ToTable("Profile", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Reason", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discription")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Reason", (string)null);
                });

            modelBuilder.Entity("Domain.Models.ReportedPost", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Pet")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Reason")
                        .HasMaxLength(200)
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("User")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Pet");

                    b.HasIndex("User");

                    b.ToTable("ReportedPost", (string)null);
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Accountcreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConnectionId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("IsAnyNotifications")
                        .HasColumnType("bit");

                    b.Property<bool>("IsOnline")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("Role")
                        .HasColumnType("int");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Breed", b =>
                {
                    b.HasOne("Domain.Models.PetsCategory", "CategoryNavigation")
                        .WithMany("Breeds")
                        .HasForeignKey("Category")
                        .IsRequired()
                        .HasConstraintName("FK_Breed_PetsCategory");

                    b.Navigation("CategoryNavigation");
                });

            modelBuilder.Entity("Domain.Models.ContactUs", b =>
                {
                    b.HasOne("Domain.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Models.Favourite", b =>
                {
                    b.HasOne("Domain.Models.Pet", "PetNavigation")
                        .WithMany("Favourites")
                        .HasForeignKey("Pet")
                        .IsRequired()
                        .HasConstraintName("FK_Favourites_Pet");

                    b.HasOne("Domain.Models.User", "UserNavigation")
                        .WithMany("Favourites")
                        .HasForeignKey("User")
                        .IsRequired()
                        .HasConstraintName("FK_Favourites_PublicUser");

                    b.Navigation("PetNavigation");

                    b.Navigation("UserNavigation");
                });

            modelBuilder.Entity("Domain.Models.GroupMember", b =>
                {
                    b.HasOne("Domain.Models.MessageGroup", "MessageGroup")
                        .WithMany("GroupMembers")
                        .HasForeignKey("MessageGroupId");

                    b.Navigation("MessageGroup");
                });

            modelBuilder.Entity("Domain.Models.Message", b =>
                {
                    b.HasOne("Domain.Models.MessageGroup", "MessageGroup")
                        .WithMany("Messages")
                        .HasForeignKey("MessageGroupId");

                    b.Navigation("MessageGroup");
                });

            modelBuilder.Entity("Domain.Models.Pet", b =>
                {
                    b.HasOne("Domain.Models.Breed", "Breed")
                        .WithMany("Pets")
                        .HasForeignKey("BreedId");

                    b.HasOne("Domain.Models.PetsCategory", "Category")
                        .WithMany("Pets")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Location", "Location")
                        .WithMany("Pets")
                        .HasForeignKey("LocationId");

                    b.HasOne("Domain.Models.User", "User")
                        .WithMany("Pets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Breed");

                    b.Navigation("Category");

                    b.Navigation("Location");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Models.Profile", b =>
                {
                    b.HasOne("Domain.Models.User", "UserNavigation")
                        .WithMany()
                        .HasForeignKey("User")
                        .IsRequired()
                        .HasConstraintName("FK_Profile_PublicUser");

                    b.Navigation("UserNavigation");
                });

            modelBuilder.Entity("Domain.Models.ReportedPost", b =>
                {
                    b.HasOne("Domain.Models.Pet", "PetNavigation")
                        .WithMany("ReportedPosts")
                        .HasForeignKey("Pet")
                        .IsRequired()
                        .HasConstraintName("FK_ReportedPost_Pet");

                    b.HasOne("Domain.Models.User", "UserNavigation")
                        .WithMany("ReportedPosts")
                        .HasForeignKey("User")
                        .IsRequired()
                        .HasConstraintName("FK_ReportedPost_PublicUser");

                    b.Navigation("PetNavigation");

                    b.Navigation("UserNavigation");
                });

            modelBuilder.Entity("Domain.Models.Breed", b =>
                {
                    b.Navigation("Pets");
                });

            modelBuilder.Entity("Domain.Models.Location", b =>
                {
                    b.Navigation("Pets");
                });

            modelBuilder.Entity("Domain.Models.MessageGroup", b =>
                {
                    b.Navigation("GroupMembers");

                    b.Navigation("Messages");
                });

            modelBuilder.Entity("Domain.Models.Pet", b =>
                {
                    b.Navigation("Favourites");

                    b.Navigation("ReportedPosts");
                });

            modelBuilder.Entity("Domain.Models.PetsCategory", b =>
                {
                    b.Navigation("Breeds");

                    b.Navigation("Pets");
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.Navigation("Favourites");

                    b.Navigation("Pets");

                    b.Navigation("ReportedPosts");
                });
#pragma warning restore 612, 618
        }
    }
}
