#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/DataCollector/Api/Api.csproj", "DataCollector/Api/"]
COPY ["src/DataCollector/Application/Application.csproj", "DataCollector/Application/"]
COPY ["src/DataCollector/Persistence/Persistence.csproj", "DataCollector/Persistence/"]

COPY ["src/Shared/Infrastructure/Infrastructure.csproj", "Shared/Infrastructure/"]
COPY ["src/Shared/Domain/Domain.csproj", "Shared/Domain/"]
COPY ["src/Shared/Common/Common.csproj", "Shared/Common/"]


RUN dotnet restore "DataCollector/Api/Api.csproj"
COPY "/src" .
WORKDIR "/src/DataCollector/Api"
RUN dotnet build "Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]