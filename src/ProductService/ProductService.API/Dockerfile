FROM mcr.microsoft.com/dotnet/sdk:8.0

WORKDIR /app

#COPY . .	//Avoid copy as you will specify the volume in the docker-compose file

# Install any required tools here (optional)
#RUN apt-get update && apt-get install -y netcat-openbsd

EXPOSE 8080

CMD ["dotnet", "watch", "run", "--project", "ProductService.API/ProductService.API.csproj", "--launch-profile", "Docker", "--environment", "Docker"]
