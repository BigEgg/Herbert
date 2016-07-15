using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Herbert.Data;

namespace Herbert.API.Migrations
{
    [DbContext(typeof(HerbertContext))]
    [Migration("20160715123347_CreateApplicationUser")]
    partial class CreateApplicationUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Herbert.Model.UserInfo.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedTime")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.Property<DateTime>("LastUpdated")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("NickName")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<int>("RegisterSource");

                    b.Property<int>("Role");

                    b.HasKey("Id");

                    b.HasAlternateKey("Email");

                    b.HasIndex("Email");

                    b.ToTable("ApplicationUsers");
                });
        }
    }
}
