FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

# Npgsql için gerekli kütüphane
RUN apt-get update && \
    apt-get install -y --no-install-recommends libgssapi-krb5-2 && \
    rm -rf /var/lib/apt/lists/*

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["src/PastryFlow.API/PastryFlow.API.csproj", "src/PastryFlow.API/"]
COPY ["src/PastryFlow.Application/PastryFlow.Application.csproj", "src/PastryFlow.Application/"]
COPY ["src/PastryFlow.Domain/PastryFlow.Domain.csproj", "src/PastryFlow.Domain/"]
COPY ["src/PastryFlow.Infrastructure/PastryFlow.Infrastructure.csproj", "src/PastryFlow.Infrastructure/"]

RUN dotnet restore "src/PastryFlow.API/PastryFlow.API.csproj"

COPY . .
WORKDIR "/src/src/PastryFlow.API"
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
RUN mkdir -p /app/uploads
ENTRYPOINT ["dotnet", "PastryFlow.API.dll"]