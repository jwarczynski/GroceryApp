﻿FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["GroceryApp_Architecture/GroceryApp_Architecture.csproj", "GroceryApp_Architecture/"]
RUN dotnet restore "GroceryApp_Architecture/GroceryApp_Architecture.csproj"
COPY . .
WORKDIR "/src/GroceryApp_Architecture"
RUN dotnet build "GroceryApp_Architecture.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GroceryApp_Architecture.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GroceryApp_Architecture.dll"]
