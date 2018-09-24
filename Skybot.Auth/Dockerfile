FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
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
