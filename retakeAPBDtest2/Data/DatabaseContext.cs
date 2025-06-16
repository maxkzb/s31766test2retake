using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using retakeAPBDtest2.Models;

namespace retakeAPBDtest2.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Character> Characters { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Item> Backpacks { get; set; }
    public DbSet<Title> Titles { get; set; }
    public DbSet<CharacterTitle> CharacterTitles { get; set; }

    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>(a =>
            {
                a.ToTable("Item");
                a.HasKey(x => x.ItemId);
                a.Property(x => x.Name).HasMaxLength(100);
                a.Property(x => x.Weight);
            });

        modelBuilder.Entity<Title>(a =>
        {
            a.ToTable("Title");
            a.HasKey(x => x.TitleId);
            a.Property(x => x.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Character>(a =>
        {
            a.ToTable("Character");
            a.HasKey(x => x.CharacterId);
            a.Property(x => x.FirstName).HasMaxLength(50);
            a.Property(x => x.LastName).HasMaxLength(120);
            a.Property(x => x.CurrentWeight);
            a.Property(x => x.MaxWeight);
        });

        modelBuilder.Entity<Backpack>(a =>
        {
            a.ToTable("Backpack");
            a.HasKey(x => new { x.ItemId, x.CharacterId });
            a.HasOne(x => x.Item).WithMany(x => x.Backpacks).HasForeignKey(x => x.ItemId);
            a.HasOne(x => x.Character).WithMany(x => x.Backpacks).HasForeignKey(x => x.CharacterId);
            a.Property(x => x.Amount);
        });

        modelBuilder.Entity<CharacterTitle>(a =>
        {
            a.ToTable("CharacterTitle");
            a.HasKey(x => new { x.CharacterId, x.TitleId });
            a.HasOne(x => x.Title).WithMany(x => x.CharacterTitles).HasForeignKey(x => x.TitleId);
            a.HasOne(x => x.Character).WithMany(x => x.CharacterTitles).HasForeignKey(x => x.CharacterId);
            a.Property(x => x.AcquiredAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Title>().HasData(
            new Title
            {
                TitleId = 1,
                Name = "Alexander"
            },
            new Title
            {
                TitleId = 2,
                Name = "James"
            });
        
        modelBuilder.Entity<Item>().HasData(
            new Item
            {
                ItemId = 1,
                Name = "Sword",
                Weight = 15
            },
            new Item
            {
                ItemId = 2,
                Name = "Axe",
                Weight = 10
            });

        modelBuilder.Entity<Character>().HasData(
            new Character
            {
                CharacterId = 1,
                FirstName = "Joe",
                LastName = "Smith",
                CurrentWeight = 75,
                MaxWeight = 85,
            },
            new Character
            {
                CharacterId = 2,
                FirstName = "Jack",
                LastName = "Jones",
                CurrentWeight = 95,
                MaxWeight = 95
            });

        modelBuilder.Entity<Backpack>().HasData(
            new Backpack
            {
                ItemId = 1,
                CharacterId = 1,
                Amount = 10
            },
            new Backpack
            {
                ItemId = 2,
                CharacterId = 2,
                Amount = 15
            });

        modelBuilder.Entity<CharacterTitle>().HasData(
            new CharacterTitle
            {
                CharacterId = 1,
                TitleId = 1,
                AcquiredAt = new DateTime(2018, 6, 17)
            },
            new CharacterTitle
            {
                CharacterId = 2,
                TitleId = 2,
                AcquiredAt = new DateTime(2024, 9, 25)
            });
    }
}