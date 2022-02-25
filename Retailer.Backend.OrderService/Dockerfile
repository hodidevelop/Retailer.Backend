#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Retailer.Backend.OrderService/Retailer.Backend.OrderService.csproj", "Retailer.Backend.OrderService/"]
RUN dotnet restore "Retailer.Backend.OrderService/Retailer.Backend.OrderService.csproj"
COPY . .
WORKDIR "/src/Retailer.Backend.OrderService"
RUN dotnet build "Retailer.Backend.OrderService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Retailer.Backend.OrderService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Retailer.Backend.OrderService.dll"]