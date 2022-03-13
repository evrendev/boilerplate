FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
#
COPY *.sln .
WORKDIR /src/
COPY src/. ./
#
RUN dotnet build "./Backend/PublicApi/PublicApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./Backend/PublicApi/PublicApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PublicApi.dll"]