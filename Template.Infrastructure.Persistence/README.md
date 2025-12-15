# start

```bash
Scaffold-DbContext "Server=.;Database=MyDb;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Infrastructure/Persistence/Models -ContextDir Infrastructure/Persistence -Context MyDbContext -Force
```

```bash
dotnet ef dbcontext scaffold "Server=.;Database=TemplateBarkhatBin;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models/Entities -f --project "./Template.Infrastructure.Persistence/Template.Infrastructure.Persistence.csproj"
```