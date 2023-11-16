FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

ARG NODE_VERSION=20.x
# Install NodeJs
RUN set -uex; \
    apt-get update; \
    apt-get install -y ca-certificates curl gnupg; \
    mkdir -p /etc/apt/keyrings; \
    curl -fsSL https://deb.nodesource.com/gpgkey/nodesource-repo.gpg.key \
     | gpg --dearmor -o /etc/apt/keyrings/nodesource.gpg; \
    NODE_MAJOR=20; \
    echo "deb [signed-by=/etc/apt/keyrings/nodesource.gpg] https://deb.nodesource.com/node_$NODE_MAJOR.x nodistro main" \
     > /etc/apt/sources.list.d/nodesource.list; \
    apt-get -qy update; \
    apt-get -qy install nodejs;

ARG TARGETARCH

WORKDIR /App

# Copy everything
COPY . ./

WORKDIR ./src/Profile
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish --use-current-runtime --self-contained false -o /app

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as final
WORKDIR /App
COPY --from=build-env /app .
ENTRYPOINT ["dotnet", "Profile.dll"]
