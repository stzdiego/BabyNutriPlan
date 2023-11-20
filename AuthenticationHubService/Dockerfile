FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 6001

ENV ASPNETCORE_URLS=http://+:6001

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["AuthenticationHubService/AuthenticationHubService.csproj", "AuthenticationHubService/"]
RUN dotnet restore "AuthenticationHubService/AuthenticationHubService.csproj"
COPY . .
WORKDIR "/src/AuthenticationHubService"
RUN dotnet build "AuthenticationHubService.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "AuthenticationHubService.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthenticationHubService.dll"]
