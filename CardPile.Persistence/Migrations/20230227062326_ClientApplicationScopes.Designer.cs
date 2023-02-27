﻿// <auto-generated />
using System;
using CardPile.Persistence.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CardPile.Persistence.Migrations
{
    [DbContext(typeof(PersistenceContext))]
    [Migration("20230227062326_ClientApplicationScopes")]
    partial class ClientApplicationScopes
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CardPile.Domain.Entities.Account", b =>
                {
                    b.Property<long>("AccountID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("AccountID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("AccountID"));

                    b.Property<DateTime>("CreatedOnUTC")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<Guid?>("GuestToken")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("AccountID");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("UQ_Account_Email")
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("UserName")
                        .IsUnique()
                        .HasDatabaseName("UQ_Account_UserName");

                    b.ToTable("Account", (string)null);
                });

            modelBuilder.Entity("CardPile.Domain.Entities.Card", b =>
                {
                    b.Property<long>("CardID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("CardID"));

                    b.Property<long?>("DeckListID")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CardID");

                    b.HasIndex("DeckListID");

                    b.ToTable("Card", (string)null);
                });

            modelBuilder.Entity("CardPile.Domain.Entities.ClientApplication", b =>
                {
                    b.Property<long>("ClientApplicationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("ClientApplicationID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ClientApplicationID"));

                    b.Property<string>("AccessToken")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Secret")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("ClientApplicationID");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("UQ_ClientApplication_Name");

                    b.ToTable("ClientApplication", (string)null);
                });

            modelBuilder.Entity("CardPile.Domain.Entities.ClientApplicationScope", b =>
                {
                    b.Property<long>("ClientApplicationID")
                        .HasColumnType("bigint")
                        .HasColumnName("ClientApplicationID");

                    b.Property<long>("ScopeID")
                        .HasColumnType("bigint")
                        .HasColumnName("ScopeID");

                    b.HasKey("ClientApplicationID", "ScopeID");

                    b.HasIndex("ScopeID");

                    b.ToTable("ClientApplicationScope", (string)null);
                });

            modelBuilder.Entity("CardPile.Domain.Entities.DeckList", b =>
                {
                    b.Property<long>("DeckListID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("DeckListID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DeckListID");

                    b.ToTable("DeckList", (string)null);
                });

            modelBuilder.Entity("CardPile.Domain.Entities.Scope", b =>
                {
                    b.Property<long>("ScopeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ScopeID"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ScopeID");

                    b.ToTable("Scope");
                });

            modelBuilder.Entity("CardPile.Domain.Entities.UserToken", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Username");

                    b.Property<string>("AccessToken")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("ExpiresIn")
                        .HasColumnType("int");

                    b.Property<string>("RefreshToken")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("TokenType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Username");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasDatabaseName("UQ_UserToken");

                    b.ToTable("UserToken", (string)null);
                });

            modelBuilder.Entity("CardPile.Domain.Entities.Account", b =>
                {
                    b.OwnsOne("CardPile.Domain.ValueObjects.Password", "Password", b1 =>
                        {
                            b1.Property<long>("AccountID")
                                .HasColumnType("bigint");

                            b1.Property<string>("m_Password")
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("Password");

                            b1.HasKey("AccountID");

                            b1.ToTable("Account");

                            b1.WithOwner()
                                .HasForeignKey("AccountID");
                        });

                    b.Navigation("Password");
                });

            modelBuilder.Entity("CardPile.Domain.Entities.Card", b =>
                {
                    b.HasOne("CardPile.Domain.Entities.DeckList", null)
                        .WithMany("Cards")
                        .HasForeignKey("DeckListID");
                });

            modelBuilder.Entity("CardPile.Domain.Entities.ClientApplicationScope", b =>
                {
                    b.HasOne("CardPile.Domain.Entities.ClientApplication", "ClientApplication")
                        .WithMany("ClientApplicationScope")
                        .HasForeignKey("ClientApplicationID")
                        .IsRequired()
                        .HasConstraintName("FK_ClientApplicationScope_ClientApplication");

                    b.HasOne("CardPile.Domain.Entities.Scope", "Scope")
                        .WithMany("ClientApplicationScopes")
                        .HasForeignKey("ScopeID")
                        .IsRequired()
                        .HasConstraintName("FK_ClientApplicationScope_Scope");

                    b.Navigation("ClientApplication");

                    b.Navigation("Scope");
                });

            modelBuilder.Entity("CardPile.Domain.Entities.ClientApplication", b =>
                {
                    b.Navigation("ClientApplicationScope");
                });

            modelBuilder.Entity("CardPile.Domain.Entities.DeckList", b =>
                {
                    b.Navigation("Cards");
                });

            modelBuilder.Entity("CardPile.Domain.Entities.Scope", b =>
                {
                    b.Navigation("ClientApplicationScopes");
                });
#pragma warning restore 612, 618
        }
    }
}
