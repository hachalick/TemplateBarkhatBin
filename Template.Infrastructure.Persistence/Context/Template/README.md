
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
            Payload = JsonSerializer.Serialize(evt),
            OccurredOnUtc = DateTime.UtcNow,
            Error = "",
            OutboxStatus = (byte)EOutboxStatus.Pending,
            ProcessedOnUtc = DateTime.UtcNow,
            RetryCount = 0,
        });
    }

    return await base.SaveChangesAsync(cancellationToken);
}
```