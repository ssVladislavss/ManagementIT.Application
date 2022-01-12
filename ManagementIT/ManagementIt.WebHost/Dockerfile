#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ManagementIt.WebHost/ManagementIt.WebHost.csproj", "ManagementIt.WebHost/"]
COPY ["ManagementIt.DataAccess/ManagementIt.DataAccess.csproj", "ManagementIt.DataAccess/"]
COPY ["ManagementIt.Core/ManagementIt.Core.csproj", "ManagementIt.Core/"]
RUN dotnet restore "ManagementIt.WebHost/ManagementIt.WebHost.csproj"
COPY . .
WORKDIR "/src/ManagementIt.WebHost"
RUN dotnet build "ManagementIt.WebHost.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ManagementIt.WebHost.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ManagementIt.WebHost.dll"]