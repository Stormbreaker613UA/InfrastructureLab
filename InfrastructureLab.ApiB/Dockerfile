FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["InfrastructureLab.ApiB/InfrastructureLab.ApiB.csproj", "InfrastructureLab.ApiB/"]
RUN dotnet restore "InfrastructureLab.ApiB/InfrastructureLab.ApiB.csproj"
COPY . .
WORKDIR "/src/InfrastructureLab.ApiB"
RUN dotnet build "./InfrastructureLab.ApiB.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./InfrastructureLab.ApiB.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InfrastructureLab.ApiB.dll"]
