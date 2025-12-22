
```c#
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
```