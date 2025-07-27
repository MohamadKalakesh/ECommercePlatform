FROM mcr.microsoft.com/dotnet/sdk:8.0

WORKDIR /app

#COPY . .	//Avoid copy as you will specify the volume in the docker-compose file

EXPOSE 8080

CMD ["dotnet", "watch", "run", "--project", "AuthService.API.csproj", "--launch-profile", "Docker"]
