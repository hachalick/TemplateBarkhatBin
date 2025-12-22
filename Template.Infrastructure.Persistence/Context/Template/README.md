
```c#
public override async Task<int> SaveChangesAsync(
    CancellationToken cancellationToken = default)
{
    var domainEvents = ChangeTracker
        .Entries<AggregateRoot>()
        .SelectMany(x => x.Entity.PullDomainEvents())
        .ToList();

    foreach (var evt in domainEvents)
    {
        OutboxMessages.Add(new OutboxMessage
        {
            Id = Guid.NewGuid(),
            Type = evt.GetType().Name,
            Content = JsonSerializer.Serialize(evt),
            OccurredOnUtc = DateTime.UtcNow
        });
    }

    return await base.SaveChangesAsync(cancellationToken);
}
```