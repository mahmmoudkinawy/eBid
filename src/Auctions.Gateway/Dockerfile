FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Auctions.Gateway/Auctions.Gateway.csproj", "src/Auctions.Gateway/"]
RUN dotnet restore "./src/Auctions.Gateway/./Auctions.Gateway.csproj"
COPY . .
WORKDIR "/src/src/Auctions.Gateway"
RUN dotnet build "./Auctions.Gateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Auctions.Gateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Auctions.Gateway.dll"]