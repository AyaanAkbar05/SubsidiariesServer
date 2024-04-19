using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ParentCompanyModel;

public partial class ParentCompanySourceContext : IdentityDbContext<SubsdiariesUsers>
{
    public ParentCompanySourceContext()
    {
    }

    public ParentCompanySourceContext(DbContextOptions<ParentCompanySourceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ParentCompany> ParentCompanies { get; set; }

    public virtual DbSet<Subsidiary> Subsidiaries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
        var config = builder.Build();
        optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ParentCompany>(entity =>
        {
            entity.HasKey(e => e.ParentCompanyId).HasName("PK__Table__026F1F3D9BABDD8E");
        });

        modelBuilder.Entity<Subsidiary>(entity =>
        {
            entity.HasKey(e => e.SubsidiaryId).HasName("PK__Table__5E27442B633F7B59");

            entity.HasOne(d => d.ParentCompany).WithMany(p => p.Subsidiaries)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Subsidiary_ParentCompany");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
