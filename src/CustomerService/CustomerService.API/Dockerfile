# Use .NET SDK image with dotnet-watch available
FROM mcr.microsoft.com/dotnet/sdk:8.0

# Set working directory inside the container
WORKDIR /app

# Copy the entire source code
COPY . .

# Expose the port your app listens on
EXPOSE 8080

# Run the application with dotnet watch
CMD ["dotnet", "watch", "run", "--project", "CustomerService.API/CustomerService.API.csproj"]
