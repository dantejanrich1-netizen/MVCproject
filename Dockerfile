FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY JanrichDanteMVC_Project/JanrichDanteMVC_Project.csproj JanrichDanteMVC_Project/
RUN dotnet restore JanrichDanteMVC_Project/JanrichDanteMVC_Project.csproj

COPY JanrichDanteMVC_Project/ JanrichDanteMVC_Project/
WORKDIR /src/JanrichDanteMVC_Project
RUN dotnet publish JanrichDanteMVC_Project.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 8080

ENTRYPOINT ["sh", "-c", "ASPNETCORE_HTTP_PORTS=${PORT:-8080} dotnet JanrichDanteMVC_Project.dll"]
