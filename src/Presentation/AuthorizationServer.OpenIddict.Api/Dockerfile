#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Presentation/AuthorizationServer.OpenIddict.Api/AuthorizationServer.OpenIddict.Api.csproj", "src/Presentation/AuthorizationServer.OpenIddict.Api/"]
COPY ["src/Core/AuthorizationServer.OpenIddict.Application/AuthorizationServer.OpenIddict.Application.csproj", "src/Core/AuthorizationServer.OpenIddict.Application/"]
COPY ["src/Shared/AuthorizationServer.OpenIddict.Shared/AuthorizationServer.OpenIddict.Shared.csproj", "src/Shared/AuthorizationServer.OpenIddict.Shared/"]
COPY ["src/Core/AuthorizationServer.OpenIddict.Domain/AuthorizationServer.OpenIddict.Domain.csproj", "src/Core/AuthorizationServer.OpenIddict.Domain/"]
COPY ["src/Infrastructure/AuthorizationServer.OpenIddict.Infrastructure/AuthorizationServer.OpenIddict.Infrastructure.csproj", "src/Infrastructure/AuthorizationServer.OpenIddict.Infrastructure/"]
COPY ["src/Infrastructure/AuthorizationServer.OpenIddict.Persistence/AuthorizationServer.OpenIddict.Persistence.csproj", "src/Infrastructure/AuthorizationServer.OpenIddict.Persistence/"]
RUN dotnet restore "src/Presentation/AuthorizationServer.OpenIddict.Api/AuthorizationServer.OpenIddict.Api.csproj"
COPY . .
WORKDIR "/src/src/Presentation/AuthorizationServer.OpenIddict.Api"
RUN dotnet build "AuthorizationServer.OpenIddict.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthorizationServer.OpenIddict.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthorizationServer.OpenIddict.Api.dll"]