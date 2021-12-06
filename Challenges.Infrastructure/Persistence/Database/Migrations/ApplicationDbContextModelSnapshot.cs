﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Challenges.Infrastructure.Persistence.Database.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Challenges.Domain.Entities.Challenge", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("char(20)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("char(36)");

                    b.Property<string>("CreatedByDevice")
                        .HasColumnType("char(20)");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Challenges");
                });
#pragma warning restore 612, 618
        }
    }
}
