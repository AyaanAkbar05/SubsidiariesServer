﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ParentCompanyModel;

#nullable disable

namespace ParentCompanyModel.Migrations
{
    [DbContext(typeof(ParentCompanySourceContext))]
    [Migration("20240330210355_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ParentCompanyModel.ParentCompany", b =>
                {
                    b.Property<int>("ParentCompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ParentCompanyId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.HasKey("ParentCompanyId")
                        .HasName("PK__Table__026F1F3D9BABDD8E");

                    b.ToTable("ParentCompany");
                });

            modelBuilder.Entity("ParentCompanyModel.Subsidiary", b =>
                {
                    b.Property<int>("SubsidiaryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubsidiaryId"));

                    b.Property<string>("Location")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<int>("ParentCompanyId")
                        .HasColumnType("int");

                    b.Property<decimal>("Revenue")
                        .HasColumnType("numeric(18, 2)");

                    b.HasKey("SubsidiaryId")
                        .HasName("PK__Table__5E27442B633F7B59");

                    b.HasIndex("ParentCompanyId");

                    b.ToTable("Subsidiary");
                });

            modelBuilder.Entity("ParentCompanyModel.Subsidiary", b =>
                {
                    b.HasOne("ParentCompanyModel.ParentCompany", "ParentCompany")
                        .WithMany("Subsidiaries")
                        .HasForeignKey("ParentCompanyId")
                        .IsRequired()
                        .HasConstraintName("FK_Subsidiary_ParentCompany");

                    b.Navigation("ParentCompany");
                });

            modelBuilder.Entity("ParentCompanyModel.ParentCompany", b =>
                {
                    b.Navigation("Subsidiaries");
                });
#pragma warning restore 612, 618
        }
    }
}
