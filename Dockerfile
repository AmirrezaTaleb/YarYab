# Use the official .NET SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Set the working directory
WORKDIR /app

# Copy the .csproj files and restore dependencies
COPY **/*.csproj ./
RUN for file in $(find . -name '*.csproj'); do mkdir -p $(dirname $file); mv $file $(dirname $file); done
RUN dotnet restore

# Copy the rest of the application code
COPY . ./

# Build the application
RUN dotnet publish -c Release -o out

# Use the runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Set the entry point for the application
ENTRYPOINT ["dotnet", "YourMainProject.dll"]
