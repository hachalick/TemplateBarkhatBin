using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Template.Application.Interfaces;
using Template.Domain.Common;
using Template.Infrastructure.Persistence.Models.Entities.Template;

namespace Template.Infrastructure.Persistence.Context.Template;

public partial class ApplicationDbContextSqlServerTemplate : DbContext
{
    private readonly IDomainEventDispatcher _dispatcher;

    public ApplicationDbContextSqlServerTemplate(DbContextOptions<ApplicationDbContextSqlServerTemplate> options, IDomainEventDispatcher dispatcher)
        : base(options)
    {
        _dispatcher = dispatcher;
    }

    public virtual DbSet<FileJob> FileJobs { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OutboxMessage> OutboxMessages { get; set; }

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

    public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        var domainEvents = ChangeTracker
            .Entries<AggregateRoot>()
            .SelectMany(e => e.Entity.PullDomainEvents())
            .ToList();

        var result = await base.SaveChangesAsync(cancellationToken);

        if (domainEvents.Any())
            await _dispatcher.DispatchAsync(domainEvents);

        return result;
    }
}
