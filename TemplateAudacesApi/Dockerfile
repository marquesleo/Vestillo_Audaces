#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


ENV ConnectionStrings__MyConnectionString="Server=localhost;port=3306;user id=root;password=thug$2Pac;database=vestillo_cupom;pooling=true"

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TemplateAudacesApi/TemplateAudacesApi.csproj", "TemplateAudacesApi/"]
RUN dotnet restore "TemplateAudacesApi.csproj"
COPY . .
WORKDIR "/src/TemplateAudacesApi"
RUN dotnet build "TemplateAudacesApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TemplateAudacesApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TemplateAudacesApi.dll"]