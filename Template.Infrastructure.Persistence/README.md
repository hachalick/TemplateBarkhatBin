# start

```bash
cd ~/Template
```

```bash
dotnet ef dbcontext scaffold "Server=.;Database=TemplateBarkhatBin;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models/Entities/Template --context-dir Context/Template --context ApplicationDbContextSqlServerTemplate -f --project "./Template.Infrastructure.Persistence/Template.Infrastructure.Persistence.csproj"
```