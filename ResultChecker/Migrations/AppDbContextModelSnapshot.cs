// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ResultChecker.Services;

namespace ResultChecker.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrencystamp");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("name");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalizedname");

                    b.HasKey("Id")
                        .HasName("pk_aspnetroles");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claimtype");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claimvalue");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("roleid");

                    b.HasKey("Id")
                        .HasName("pk_aspnetroleclaims");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_aspnetroleclaims_roleid");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claimtype");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claimvalue");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("userid");

                    b.HasKey("Id")
                        .HasName("pk_aspnetuserclaims");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_aspnetuserclaims_userid");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnName("loginprovider");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text")
                        .HasColumnName("providerkey");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text")
                        .HasColumnName("providerdisplayname");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("userid");

                    b.HasKey("LoginProvider", "ProviderKey")
                        .HasName("pk_aspnetuserlogins");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_aspnetuserlogins_userid");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text")
                        .HasColumnName("userid");

                    b.Property<string>("RoleId")
                        .HasColumnType("text")
                        .HasColumnName("roleid");

                    b.HasKey("UserId", "RoleId")
                        .HasName("pk_aspnetuserroles");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_aspnetuserroles_roleid");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text")
                        .HasColumnName("userid");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnName("loginprovider");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Value")
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("UserId", "LoginProvider", "Name")
                        .HasName("pk_aspnetusertokens");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ResultChecker.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer")
                        .HasColumnName("accessfailedcount");

                    b.Property<string>("Address")
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrencystamp");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("dob");

                    b.Property<DateTime>("DateSubmitted")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("datesubmitted");

                    b.Property<string>("Department")
                        .HasColumnType("text")
                        .HasColumnName("department");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("email");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("emailconfirmed");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("firstname");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("gender");

                    b.Property<string>("Hobbies")
                        .HasColumnType("text")
                        .HasColumnName("hobbies");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("isactive");

                    b.Property<bool>("IsStaff")
                        .HasColumnType("boolean")
                        .HasColumnName("isstaff");

                    b.Property<string>("LGA")
                        .HasColumnType("text")
                        .HasColumnName("lga");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("lastname");

                    b.Property<string>("Level")
                        .HasColumnType("text")
                        .HasColumnName("level");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("lockoutenabled");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("lockoutend");

                    b.Property<string>("MiddleName")
                        .HasColumnType("text")
                        .HasColumnName("middlename");

                    b.Property<string>("NOKAddress")
                        .HasColumnType("text")
                        .HasColumnName("nokaddress");

                    b.Property<string>("NOKEmail")
                        .HasColumnType("text")
                        .HasColumnName("nokemail");

                    b.Property<string>("NOKName")
                        .HasColumnType("text")
                        .HasColumnName("nokname");

                    b.Property<string>("NOKPhone")
                        .HasColumnType("text")
                        .HasColumnName("nokphone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalizedemail");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalizedusername");

                    b.Property<string>("Passport")
                        .HasColumnType("text")
                        .HasColumnName("passport");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text")
                        .HasColumnName("passwordhash");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text")
                        .HasColumnName("phonenumber");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("phonenumberconfirmed");

                    b.Property<string>("Qualification")
                        .HasColumnType("text")
                        .HasColumnName("qualification");

                    b.Property<string>("Religion")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("religion");

                    b.Property<string>("Role")
                        .HasColumnType("text")
                        .HasColumnName("role");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text")
                        .HasColumnName("securitystamp");

                    b.Property<string>("StaffID")
                        .HasColumnType("text")
                        .HasColumnName("staffid");

                    b.Property<string>("State")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("state");

                    b.Property<string>("Tribe")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("tribe");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("twofactorenabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pk_aspnetusers");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("ResultChecker.Models.CourseLecturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Course")
                        .HasColumnType("text")
                        .HasColumnName("course");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("datecreated");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("isactive");

                    b.Property<string>("Session")
                        .HasColumnType("text")
                        .HasColumnName("session");

                    b.Property<string>("SubmittedBy")
                        .HasColumnType("text")
                        .HasColumnName("submittedby");

                    b.Property<string>("UserId")
                        .HasColumnType("text")
                        .HasColumnName("userid");

                    b.HasKey("Id")
                        .HasName("pk_courselecturers");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_courselecturers_userid");

                    b.ToTable("courselecturers");
                });

            modelBuilder.Entity("ResultChecker.Models.CourseReg", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("CourseCode")
                        .HasColumnType("text")
                        .HasColumnName("coursecode");

                    b.Property<DateTime>("DateSubmitted")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("datesubmitted");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("boolean")
                        .HasColumnName("isapproved");

                    b.Property<string>("Semester")
                        .HasColumnType("text")
                        .HasColumnName("semester");

                    b.Property<string>("Session")
                        .HasColumnType("text")
                        .HasColumnName("session");

                    b.Property<string>("SubmittedBy")
                        .HasColumnType("text")
                        .HasColumnName("submittedby");

                    b.Property<string>("UserId")
                        .HasColumnType("text")
                        .HasColumnName("userid");

                    b.Property<string>("Year")
                        .HasColumnType("text")
                        .HasColumnName("year");

                    b.HasKey("Id")
                        .HasName("pk_courseregs");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_courseregs_userid");

                    b.ToTable("courseregs");
                });

            modelBuilder.Entity("ResultChecker.Models.Courses", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("CourseCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("coursecode");

                    b.Property<string>("CourseTitle")
                        .HasColumnType("text")
                        .HasColumnName("coursetitle");

                    b.Property<string>("CourseUnit")
                        .HasColumnType("text")
                        .HasColumnName("courseunit");

                    b.Property<DateTime>("DateSubmitted")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("datesubmitted");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("isactive");

                    b.Property<string>("IsCore")
                        .HasColumnType("text")
                        .HasColumnName("iscore");

                    b.Property<string>("Semester")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("semester");

                    b.Property<string>("Session")
                        .HasColumnType("text")
                        .HasColumnName("session");

                    b.Property<string>("SubmittedBy")
                        .HasColumnType("text")
                        .HasColumnName("submittedby");

                    b.HasKey("Id")
                        .HasName("pk_courses");

                    b.ToTable("courses");
                });

            modelBuilder.Entity("ResultChecker.Models.ResultUpload", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("CourseCode")
                        .HasColumnType("text")
                        .HasColumnName("coursecode");

                    b.Property<string>("GPA")
                        .HasColumnType("text")
                        .HasColumnName("gpa");

                    b.Property<string>("Grade")
                        .HasColumnType("text")
                        .HasColumnName("grade");

                    b.Property<string>("MatNo")
                        .HasColumnType("text")
                        .HasColumnName("matno");

                    b.Property<int>("Points")
                        .HasColumnType("integer")
                        .HasColumnName("points");

                    b.Property<int>("Score")
                        .HasColumnType("integer")
                        .HasColumnName("score");

                    b.Property<string>("Semester")
                        .HasColumnType("text")
                        .HasColumnName("semester");

                    b.Property<string>("Session")
                        .HasColumnType("text")
                        .HasColumnName("session");

                    b.Property<int>("Unit")
                        .HasColumnType("integer")
                        .HasColumnName("unit");

                    b.Property<string>("UploadId")
                        .HasColumnType("text")
                        .HasColumnName("uploadid");

                    b.HasKey("Id")
                        .HasName("pk_resultuploads");

                    b.ToTable("resultuploads");
                });

            modelBuilder.Entity("ResultChecker.Models.SessionSemester", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("datecreated");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("isactive");

                    b.Property<string>("Session")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("session");

                    b.Property<string>("SubmittedBy")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("submittedby");

                    b.HasKey("Id")
                        .HasName("pk_sessionsemesters");

                    b.ToTable("sessionsemesters");
                });

            modelBuilder.Entity("ResultChecker.Models.StateLga", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("LGA")
                        .HasColumnType("text")
                        .HasColumnName("lga");

                    b.Property<string>("State")
                        .HasColumnType("text")
                        .HasColumnName("state");

                    b.HasKey("Id")
                        .HasName("pk_statelgas");

                    b.ToTable("statelgas");
                });

            modelBuilder.Entity("ResultChecker.Models.vwUploadedResults", b =>
                {
                    b.Property<string>("CourseCode")
                        .HasColumnType("text")
                        .HasColumnName("coursecode");

                    b.Property<string>("CourseTitle")
                        .HasColumnType("text")
                        .HasColumnName("coursetitle");

                    b.Property<string>("GPA")
                        .HasColumnType("text")
                        .HasColumnName("gpa");

                    b.Property<string>("Grade")
                        .HasColumnType("text")
                        .HasColumnName("grade");

                    b.Property<string>("MatNo")
                        .HasColumnType("text")
                        .HasColumnName("matno");

                    b.Property<int>("Points")
                        .HasColumnType("integer")
                        .HasColumnName("points");

                    b.Property<int>("Score")
                        .HasColumnType("integer")
                        .HasColumnName("score");

                    b.Property<string>("Semester")
                        .HasColumnType("text")
                        .HasColumnName("semester");

                    b.Property<string>("Session")
                        .HasColumnType("text")
                        .HasColumnName("session");

                    b.Property<int>("Unit")
                        .HasColumnType("integer")
                        .HasColumnName("unit");

                    b.Property<string>("UploadId")
                        .HasColumnType("text")
                        .HasColumnName("uploadid");

                    b.ToView("vwUploadedResults");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("fk_aspnetroleclaims_aspnetroles_roleid")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ResultChecker.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_aspnetuserclaims_aspnetusers_userid")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ResultChecker.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_aspnetuserlogins_aspnetusers_userid")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("fk_aspnetuserroles_aspnetroles_roleid")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ResultChecker.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_aspnetuserroles_aspnetusers_userid")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ResultChecker.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_aspnetusertokens_aspnetusers_userid")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("ResultChecker.Models.CourseLecturer", b =>
                {
                    b.HasOne("ResultChecker.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_courselecturers_users_userid")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("ResultChecker.Models.CourseReg", b =>
                {
                    b.HasOne("ResultChecker.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("fk_courseregs_users_userid")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("ApplicationUser");
                });
#pragma warning restore 612, 618
        }
    }
}
