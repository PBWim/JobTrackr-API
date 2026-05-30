FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/JobTrackr.API/JobTrackr.API.csproj", "src/JobTrackr.API/"]
COPY ["src/JobTrackr.Application/JobTrackr.Application.csproj", "src/JobTrackr.Application/"]
COPY ["src/JobTrackr.Domain/JobTrackr.Domain.csproj", "src/JobTrackr.Domain/"]
COPY ["src/JobTrackr.Infrastructure/JobTrackr.Infrastructure.csproj", "src/JobTrackr.Infrastructure/"]
RUN dotnet restore "src/JobTrackr.API/JobTrackr.API.csproj"
COPY . .
WORKDIR "/src/src/JobTrackr.API"
RUN dotnet build "JobTrackr.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JobTrackr.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JobTrackr.API.dll"]
