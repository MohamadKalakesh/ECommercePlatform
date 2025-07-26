# Use .NET SDK image for dotnet watch
FROM mcr.microsoft.com/dotnet/sdk:8.0

# Set working directory inside the container
WORKDIR /app

# Copy everything (your local code will be mounted over this)
COPY . .

# Expose the app's port
EXPOSE 8080

# Start with dotnet watch
CMD ["dotnet", "watch", "run", "--project", "ApiGateway.csproj"]
