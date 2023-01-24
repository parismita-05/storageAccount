FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["StorageCrud.csproj", "./"]
RUN dotnet restore "StorageCrud.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "StorageCrud.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StorageCrud.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StorageCrud.dll"]
