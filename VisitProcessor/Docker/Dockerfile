#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["VisitProcessor/MiniLink.VisitProcessor.csproj", "VisitProcessor/"]
COPY ["MiniLinkLogic/MiniLinkLogic.csproj", "MiniLinkLogic/"]
COPY ["Shared/MiniLink.Shared.csproj", "Shared/"]
RUN dotnet restore "VisitProcessor/MiniLink.VisitProcessor.csproj"
COPY . .
WORKDIR "/src/VisitProcessor"
RUN dotnet build "MiniLink.VisitProcessor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MiniLink.VisitProcessor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MiniLink.VisitProcessor.dll"]