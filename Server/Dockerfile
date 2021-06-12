#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Server/MiniLink.Server.csproj", "Server/"]
COPY ["MiniLinkLogic/MiniLinkLogic.csproj", "MiniLinkLogic/"]
COPY ["Shared/MiniLink.Shared.csproj", "Shared/"]
COPY ["Client/MiniLink.Client.csproj", "Client/"]
RUN dotnet restore "Server/MiniLink.Server.csproj"
COPY . .
WORKDIR "/src/Server"
RUN dotnet build "MiniLink.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MiniLink.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MiniLink.Server.dll"]