FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base

LABEL author="Malek Atwiz" email="hello@malekatwiz.net"

# Environment variables:
      #- IssuerUri
      #- StorageAccount__FileUri
      #- StorageAccount__Key
      #- StorageAccount__Name
      #- Certificate__Filename
      #- Certificate__Password
      #- ApiResources__0__DisplayName
      #- ApiResources__0__Name
      #- ApiClients__0__Id
      #- ApiClients__0__Scopes
      #- ApiClients__0__Secret

WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Skybot.Auth/Skybot.Auth.csproj", "Skybot.Auth/"]
RUN dotnet restore "Skybot.Auth/Skybot.Auth.csproj"
COPY . .
WORKDIR "/src/Skybot.Auth"
RUN dotnet build "Skybot.Auth.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Skybot.Auth.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Skybot.Auth.dll"]
