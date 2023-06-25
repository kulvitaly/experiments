# **Build Project**
# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:8.0-preview-alpine AS build
EXPOSE 80

WORKDIR /src

# Copy fsproj and restore all dependencies
COPY src/TelegramBot/TelegramBot.fsproj ./
RUN dotnet restore ./TelegramBot.fsproj

# Copy source code and build / publish app and libraries
COPY . .
RUN dotnet publish -c release -o /app

# **Run project**
# Create new layer with runtime, copy app / libraries, then run dotnet
FROM mcr.microsoft.com/dotnet/sdk:8.0-preview-alpine 
#/aspnet:8.0-preview-alpine
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "TelegramBot.dll"]