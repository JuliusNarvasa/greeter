using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Greeter.Models;

public partial class GreeterDbContext : DbContext
{
    public GreeterDbContext()
    {
    }

    public GreeterDbContext(DbContextOptions<GreeterDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<Translation> Translations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=greeterDB;Username=postgres;Password=password");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.LanguageId).HasName("languages_pkey");

            entity.ToTable("languages");

            entity.Property(e => e.LanguageId)
                .ValueGeneratedNever()
                .HasColumnName("language_id");
            entity.Property(e => e.Language1).HasColumnName("language");
        });

        modelBuilder.Entity<Translation>(entity =>
        {
            entity.HasKey(e => e.TranslationId).HasName("translations_pkey");

            entity.ToTable("translations");

            entity.Property(e => e.TranslationId)
                .ValueGeneratedNever()
                .HasColumnName("translation_id");
            entity.Property(e => e.Afternoon).HasColumnName("afternoon");
            entity.Property(e => e.Evening).HasColumnName("evening");
            entity.Property(e => e.LanguageId).HasColumnName("language_id");
            entity.Property(e => e.Morning).HasColumnName("morning");

            entity.HasOne(d => d.Language).WithMany(p => p.Translations)
                .HasForeignKey(d => d.LanguageId)
                .HasConstraintName("language_id_FK");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
