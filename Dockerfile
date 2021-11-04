FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["src/AssetQuote.Api/AssetQuote.Api.csproj", "src/AssetQuote.Api/"]
COPY ["src/AssetQuote.Data/AssetQuote.Data.csproj", "src/AssetQuote.Api/"]
COPY ["src/AssetQuote.Domain/AssetQuote.Domain.csproj", "src/AssetQuote.Api/"]
COPY ["src/AssetQuote.Infrastructure/AssetQuote.Infrastructure.csproj", "src/AssetQuote.Api/"]

RUN dotnet restore "src/AssetQuote.Api/AssetQuote.Api.csproj"
COPY . .
WORKDIR "/src/src/AssetQuote.Api"
RUN dotnet build "AssetQuote.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AssetQuote.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AssetQuote.Api.dll"]
