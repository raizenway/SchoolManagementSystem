FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /out .

ENV ASPNETCORE_ENVIRONMENT=Production

# Jalankan aplikasi
ENTRYPOINT ["dotnet", "SchoolManagementSystem.dll"]
