#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Pizzaria/Pizzaria.csproj", "Pizzaria/"]
RUN dotnet restore "Pizzaria/Pizzaria.csproj"
COPY . .
WORKDIR "/src/Pizzaria"
RUN dotnet build "Pizzaria.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pizzaria.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pizzaria.dll"]