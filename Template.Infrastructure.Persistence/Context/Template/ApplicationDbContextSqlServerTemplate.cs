using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Template.Infrastructure.Persistence.Models.Entities.Template;

namespace Template.Infrastructure.Persistence.Context.Template;

public partial class ApplicationDbContextSqlServerTemplate : DbContext
{
    public ApplicationDbContextSqlServerTemplate()
    {
    }

    public ApplicationDbContextSqlServerTemplate(DbContextOptions<ApplicationDbContextSqlServerTemplate> options)
        : base(options)
    {
    }

    public virtual DbSet<FileJob> FileJobs { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OutboxMessage> OutboxMessages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=TemplateBarkhatBin;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FileJob>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())", "DF_FileJobs_Id");
            entity.Property(e => e.FilePath).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<OutboxMessage>(entity =>
        {
            entity.ToTable("OutboxMessage");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())", "DF_OutboxMessage_Id");
            entity.Property(e => e.Content).HasMaxLength(50);
            entity.Property(e => e.Error).HasMaxLength(50);
            entity.Property(e => e.OccurredOnUtc).HasColumnType("datetime");
            entity.Property(e => e.ProcessedOnUtc).HasColumnType("datetime");
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
