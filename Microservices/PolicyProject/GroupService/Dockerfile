#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["GroupService/GroupService.csproj", "GroupService/"]
RUN dotnet restore "GroupService/GroupService.csproj"
COPY . .
WORKDIR "/src/GroupService"
RUN dotnet build "GroupService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GroupService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GroupService.dll"]