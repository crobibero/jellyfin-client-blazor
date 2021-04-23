# Experimental Blazor Jellyfin Client
###https://jellyfin-blazor.pages.dev


CloudFlare build script:
```bash
curl -sSL https://dot.net/v1/dotnet-install.sh > dotnet-install.sh;
chmod +x dotnet-install.sh;
./dotnet-install.sh -c release/6.0.1xx-preview3 -InstallDir ./dotnet6;
./dotnet6/dotnet --version;
npm ci --prefix src/Jellyfin.Blazor/wwwroot;
./dotnet6/dotnet publish -c Release -o output src/Jellyfin.Blazor.sln;
```