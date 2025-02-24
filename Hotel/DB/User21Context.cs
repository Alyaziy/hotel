using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Hotel.DB;

public partial class User21Context : DbContext
{
    public User21Context()
    {
    }

    public User21Context(DbContextOptions<User21Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookService> BookServices { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Guest> Guests { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceStaff> ServiceStaffs { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=192.168.200.35;user=user21;password=61605;database=user21;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("Book");

            entity.Property(e => e.DateIn).HasColumnType("date");
            entity.Property(e => e.DateOut).HasColumnType("date");

            entity.HasOne(d => d.Guest).WithMany(p => p.Books)
                .HasForeignKey(d => d.GuestId)
                .HasConstraintName("FK_Book_Guest");

            entity.HasOne(d => d.Room).WithMany(p => p.Books)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK_Book_Room");
        });

        modelBuilder.Entity<BookService>(entity =>
        {
            entity.ToTable("BookService");

            entity.Property(e => e.Date).HasColumnType("date");

            entity.HasOne(d => d.Book).WithMany(p => p.BookServices)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK_BookService_Book");

            entity.HasOne(d => d.Service).WithMany(p => p.BookServices)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK_BookService_Service");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");
        });

        modelBuilder.Entity<Guest>(entity =>
        {
            entity.ToTable("Guest");

            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.MiddleName).HasMaxLength(50);

            entity.HasOne(d => d.User).WithMany(p => p.Guests)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Guest_User");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.ToTable("Room");

            entity.HasOne(d => d.Category).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Room_Category");

            entity.HasOne(d => d.Status).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK_Room_Status");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.ToTable("Schedule");

            entity.Property(e => e.Date).HasColumnType("date");

            entity.HasOne(d => d.Staff).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK_Schedule_Staff");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.ToTable("Service");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<ServiceStaff>(entity =>
        {
            entity.ToTable("ServiceStaff");

            entity.HasOne(d => d.Service).WithMany(p => p.ServiceStaffs)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK_ServiceStaff_Service");

            entity.HasOne(d => d.Staff).WithMany(p => p.ServiceStaffs)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK_ServiceStaff_Staff");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.MiddleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("Status");

            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.LastAuth).HasColumnType("datetime");
            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_User_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
