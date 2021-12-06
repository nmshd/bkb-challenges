# Build
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /src
COPY . .
RUN dotnet restore "Challenges.Jobs.Cleanup/Challenges.Jobs.Cleanup.csproj"

WORKDIR "/src/Challenges.Jobs.Cleanup"
RUN dotnet publish --configuration Release --output /app --no-restore

# Run	
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim
WORKDIR /app
COPY --from=build-env /app .

LABEL org.opencontainers.image.source = "https://github.com/nmshd/bkb-challenges"

ENTRYPOINT ["dotnet", "Challenges.Jobs.Cleanup.dll"]
