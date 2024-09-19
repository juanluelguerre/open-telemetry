# Stage 1: Restore dependencies
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS restore
WORKDIR /source

# Copy project files
COPY MinimalApiWithTelemetry.csproj ./
COPY *.props ./
COPY *.targets ./

# Restore dependencies
RUN dotnet restore

# Stage 2: Build and publish
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copy restored dependencies
COPY --from=restore /root/.nuget /root/.nuget

# Copy the rest of the source code
COPY . ./

# Publish the application
RUN dotnet publish MinimalApiWithTelemetry.csproj -c Release -o /app

# Stage 3: Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy the published app
COPY --from=build /app ./

# Expose port 80
EXPOSE 8080

# Start the application
ENTRYPOINT ["dotnet", "MinimalApiWithTelemetry.dll"]
