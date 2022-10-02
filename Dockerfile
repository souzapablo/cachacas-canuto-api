# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source
COPY . .
RUN dotnet test --logger:trx
RUN dotnet restore "./src/CachacasCanuto.API/CachacasCanuto.API.csproj" --disable-parallel
RUN dotnet publish "./src/CachacasCanuto.API/CachacasCanuto.API.csproj" -c release -o /app --no-restore

# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000
CMD ASPNETCORE_URLS=http://*:$PORT dotnet CachacasCanuto.API.dll