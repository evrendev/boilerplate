FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
#
COPY *.sln .
WORKDIR /src
COPY src/. ./
#
RUN dotnet build "./Api/Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./Api/Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]
# #
# RUN apt-get update && \
#     apt-get install -y --allow-unauthenticated libgdiplus libc6-dev libx11-dev
# #
# COPY entrypoint.sh /usr/local/bin/
# RUN ln -s /usr/local/bin/entrypoint.sh /