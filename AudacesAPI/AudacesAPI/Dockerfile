#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ConnectionStrings__MyConnectionString="Server=localhost;port=3306;user id=root;password=thug$2Pac;database=vestillo_cupom;pooling=true"

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AudacesAPI/AudacesAPI.csproj", "AudacesAPI/"]
RUN dotnet restore "AudacesAPI/AudacesAPI.csproj"
COPY . .
WORKDIR "/src/AudacesAPI"
RUN dotnet build "AudacesAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AudacesAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AudacesAPI.dll"]
