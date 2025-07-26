# STAGE 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project file and restore
COPY ApiGateway.csproj .
RUN dotnet restore

# Copy everything and publish
COPY . .
RUN dotnet publish -c Release -o /app/publish

# STAGE 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Expose port and start
EXPOSE 8080
ENTRYPOINT ["dotnet", "ApiGateway.dll"]
