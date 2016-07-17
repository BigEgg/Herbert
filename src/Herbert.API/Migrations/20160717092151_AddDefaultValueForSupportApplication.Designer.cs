using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Herbert.DAL;

namespace Herbert.API.Migrations
{
    [DbContext(typeof(HerbertContext))]
    [Migration("20160717092151_AddDefaultValueForSupportApplication")]
    partial class AddDefaultValueForSupportApplication
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Herbert.Models.Access.SupportApplication", b =>
                {
                    b.Property<int>("ApplicationType")
                        .IsConcurrencyToken();

                    b.Property<Guid>("AppId")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AppSecret")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 64);

                    b.Property<DateTime>("CreatedTime")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("LastUpdated");

                    b.HasKey("ApplicationType");

                    b.HasIndex("AppId", "AppSecret");

                    b.ToTable("SupportApplications");
                });

            modelBuilder.Entity("Herbert.Models.UserInfo.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedTime")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasAnnotation("MaxLength", 256);

                    b.Property<DateTime>("LastUpdated");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 64);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 256);

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
