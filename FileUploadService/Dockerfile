# Etap 1: Budowanie aplikacji
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Skopiuj plik projektu i przywróæ zale¿noœci
COPY *.csproj ./
RUN dotnet restore

# Skopiuj resztê plików i zbuduj aplikacjê
COPY . ./
RUN dotnet build -c Release -o /out

# Publikowanie aplikacji
RUN dotnet publish -c Release -o /out

# Etap 2: Uruchamianie aplikacji
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out .

# Ustawienie punktu wejœcia
ENTRYPOINT ["dotnet", "FileUploadService.dll"]
