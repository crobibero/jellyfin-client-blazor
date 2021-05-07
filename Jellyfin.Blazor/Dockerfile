FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Jellyfin.Blazor/Jellyfin.Blazor.csproj", "Jellyfin.Blazor/"]
RUN dotnet restore "Jellyfin.Blazor/Jellyfin.Blazor.csproj"
COPY . .
WORKDIR "/src/Jellyfin.Blazor"
RUN dotnet build "Jellyfin.Blazor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Jellyfin.Blazor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Jellyfin.Blazor.dll"]
