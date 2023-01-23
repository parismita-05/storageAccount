FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5011

ENV ASPNETCORE_URLS=http://+:5011

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["StorageCrud/StorageCrud.csproj", "StorageCrud/"]
RUN dotnet restore "StorageCrud\StorageCrud.csproj"
COPY . .
WORKDIR "/src/StorageCrud"
RUN dotnet build "StorageCrud.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StorageCrud.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StorageCrud.dll"]
