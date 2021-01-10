#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-nanoserver-1903 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1903 AS build
WORKDIR /src
COPY ["src/Services/Cars/Cars.API/Cars.API.csproj", "src/Services/Cars/Cars.API/"]
RUN dotnet restore "src/Services/Cars/Cars.API/Cars.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Cars/Cars.API"
RUN dotnet build "Cars.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cars.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cars.API.dll"]