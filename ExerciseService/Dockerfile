FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY Core/*.csproj ./Core/
COPY Storage.Sql/*.csproj ./Storage.Sql/
COPY Web/*.csproj ./Web/

RUN dotnet restore

# copy everything else and build app
COPY Core/. ./Core/
COPY Storage.Sql/. ./Storage.Sql/
COPY Web/. ./Web/

WORKDIR /source/Web
RUN dotnet publish -c release -o out

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
# COPY --from=build /app ./
COPY --from=build /source/Web/out ./
ENTRYPOINT ["dotnet", "Web.dll"]