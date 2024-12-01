# Starta från .NET SDK-bilden för att bygga applikationen
FROM mcr.microsoft.com/dotnet/sdk:8.0@sha256:35792ea4ad1db051981f62b313f1be3b46b1f45cadbaa3c288cd0d3056eefb83 AS build-env
WORKDIR /App

# Kopiera hela lösningen från rotmappen (WebShop och WebShopTests)
COPY . ./

# Återställ beroenden för hela lösningen
RUN dotnet restore WebShopSolution.sln

# Bygg och publicera applikationen
RUN dotnet publish WebShop/WebShop.csproj -c Release -o out

# Bygg runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0@sha256:6c4df091e4e531bb93bdbfe7e7f0998e7ced344f54426b7e874116a3dc3233ff
WORKDIR /App
COPY --from=build-env /App/out ./

# Exponera port och definiera entry point
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "WebShop.dll"]
