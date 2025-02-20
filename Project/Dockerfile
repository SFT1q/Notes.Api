#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Project/Notes.WebApi.csproj", "Project/"]
COPY ["Notes.Domain/Notes.Domain.csproj", "Notes.Domain/"]
COPY ["Notes.Interfaces/Notes.Interfaces.csproj", "Notes.Interfaces/"]
COPY ["Notes.Persistence/Notes.DataBase.csproj", "Notes.Persistence/"]
COPY ["Notes.UseCases/Notes.UseCases.csproj", "Notes.UseCases/"]
RUN dotnet restore "./Project/Notes.WebApi.csproj"
COPY . .
WORKDIR "/src/Project"
RUN dotnet build "./Notes.WebApi.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Notes.WebApi.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notes.WebApi.dll"]