@echo off
echo Refreshing Citations for main project...
cd C:\Users\T.R.X\Documents\GitHub\Citations\Citations
dotnet ef dbcontext scaffold "Host=localhost;Database=citations;Username=citation_user;Password=citation_user" Npgsql.EntityFrameworkCore.PostgreSQL --schema core -o Models -c CitationContext -f
echo Done
