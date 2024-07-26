# Use the official .NET SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Set the working directory
WORKDIR /src

# Copy all project files
COPY . .

# Find all .csproj files and restore dependencies
RUN find . -name '*.csproj' -exec sh -c 'cd $(dirname {}); dotnet restore' \;

# Set the working directory for the build
WORKDIR /src

# Build the application
RUN dotnet publish -c Release -o /app/out

# Use the runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Set the entry point for the application
ENTRYPOINT ["dotnet", "YarYab.API.dll"]
