# Use .NET SDK image with dotnet-watch available
FROM mcr.microsoft.com/dotnet/sdk:8.0

# Set working directory inside the container
WORKDIR /app

# Copy the entire source code
COPY . .

# Install any required tools here (optional)
RUN apt-get update && apt-get install -y netcat-openbsd

# Expose the port your app listens on
EXPOSE 8080

# Run the application with dotnet watch
CMD ["dotnet", "watch", "run", "--project", "ProductService.API/ProductService.API.csproj"]
