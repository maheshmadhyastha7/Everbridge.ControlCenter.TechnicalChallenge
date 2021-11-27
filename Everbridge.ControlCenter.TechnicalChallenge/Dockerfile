#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
ARG buildConfiguration

# VS in fast mode only builds base - use release mode to use non-development server for web app
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 443
RUN apt-get update -y && apt-get install curl -yq
RUN curl -fsSL https://deb.nodesource.com/setup_15.x | bash -
RUN apt-get update -y && apt-get install nodejs -yq
RUN nodejs -v
RUN npm -v

FROM node:15.8-alpine AS clientbase
ARG skip_client_build=false 
WORKDIR /app 
COPY Everbridge.ControlCenter.TechnicalChallenge/ClientApp/package*.json .
RUN npm install

FROM clientbase AS client
COPY Everbridge.ControlCenter.TechnicalChallenge/ClientApp .
RUN npm run build

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Everbridge.ControlCenter.TechnicalChallenge/Everbridge.ControlCenter.TechnicalChallenge.csproj", "Everbridge.ControlCenter.TechnicalChallenge/"]
RUN dotnet restore "Everbridge.ControlCenter.TechnicalChallenge/Everbridge.ControlCenter.TechnicalChallenge.csproj"
COPY . .
WORKDIR "/src/Everbridge.ControlCenter.TechnicalChallenge"
RUN dotnet build "Everbridge.ControlCenter.TechnicalChallenge.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Everbridge.ControlCenter.TechnicalChallenge.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS final-release
WORKDIR /app
EXPOSE 443
COPY --from=publish /app/publish .
COPY --from=client /app/build ./ClientApp/build

FROM base AS final-debug
WORKDIR /app
EXPOSE 443
COPY --from=publish /app/publish .
COPY --from=client /app/build ./ClientApp/build
COPY Everbridge.ControlCenter.TechnicalChallenge/ClientApp .

FROM final-${buildConfiguration} AS final
WORKDIR /app
ARG buildConfiguration
RUN echo "Build Configuration: $buildConfiguration"
ENTRYPOINT ["dotnet", "Everbridge.ControlCenter.TechnicalChallenge.dll"]
