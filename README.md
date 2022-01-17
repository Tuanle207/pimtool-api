## Development

Want to get your hand dirty? Great!

To run add-migration command (**from src/PIMTool working directory**):
```sh
dotnet ef migrations add <MIGRATION_NAME>  -c AppDbContext -p "../PIMTool.Db/PIMTool.Db.csproj" -o "../PIMTool.Db/Migrations"
```

To run remove-migration command:
```sh
dotnet ef migrations remove -c AppDbContext -p "../PIMTool.Db/PIMTool.Db.csproj" -o "../PIMTool.Db/Migrations"
```

To run update-database command:
```sh
dotnet ef database update -c AppDbContext -p "../PIMTool.Db/PIMTool.Db.csproj"
```

To run drop-database command:
```sh
dotnet ef database drop -c AppDbContext -p "../PIMTool.Db/PIMTool.Db.csproj"
```

## Author
**LEAT - ELCA VN**