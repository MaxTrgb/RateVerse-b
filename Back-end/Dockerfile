FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

COPY . /app

ENTRYPOINT ["dotnet", "DENMAP_SERVER.dll"]
