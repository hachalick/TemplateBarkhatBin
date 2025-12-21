# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Template.Api/Template.Api.csproj", "Template.Api/"]
COPY ["Template.Application/Template.Application.csproj", "Template.Application/"]
COPY ["Template.Contracts/Template.Contracts.csproj", "Template.Contracts/"]
COPY ["Template.Domain/Template.Domain.csproj", "Template.Domain/"]
COPY ["Template.Infrastructure.Catch/Template.Infrastructure.Catch.csproj", "Template.Infrastructure.Catch/"]
COPY ["Template.Infrastructure.Logging/Template.Infrastructure.Logging.csproj", "Template.Infrastructure.Logging/"]
COPY ["Template.Infrastructure.Messaging.Kafka/Template.Infrastructure.Messaging.Kafka.csproj", "Template.Infrastructure.Messaging.Kafka/"]
COPY ["Template.Infrastructure.Messaging.Rabbit/Template.Infrastructure.Messaging.Rabbit.csproj", "Template.Infrastructure.Messaging.Rabbit/"]
COPY ["Template.Infrastructure.Messaging/Template.Infrastructure.Messaging.csproj", "Template.Infrastructure.Messaging/"]
COPY ["Template.Infrastructure.Persistence/Template.Infrastructure.Persistence.csproj", "Template.Infrastructure.Persistence/"]
COPY ["Template.SignalR/Template.SignalR.csproj", "Template.SignalR/"]
RUN dotnet restore "./Template.Api/Template.Api.csproj"
COPY . .
WORKDIR "/src/Template.Api"
RUN dotnet build "./Template.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Template.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Template.Api.dll"]