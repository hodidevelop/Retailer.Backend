#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Retailer.Backend.InvoiceService/Retailer.Backend.InvoiceService.csproj", "Retailer.Backend.InvoiceService/"]
COPY ["Retailer.Backend.Bll.InvoiceService/Retailer.Backend.Bll.InvoiceService.csproj", "Retailer.Backend.Bll.InvoiceService/"]
COPY ["Retailer.Backend.Dal.InvoiceService/Retailer.Backend.Dal.InvoiceService.csproj", "Retailer.Backend.Dal.InvoiceService/"]
COPY ["Retailer.Backend.Core/Retailer.Backend.Core.csproj", "Retailer.Backend.Core/"]
RUN dotnet restore "Retailer.Backend.InvoiceService/Retailer.Backend.InvoiceService.csproj"
COPY . .
WORKDIR "/src/Retailer.Backend.InvoiceService"
RUN dotnet build "Retailer.Backend.InvoiceService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Retailer.Backend.InvoiceService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Retailer.Backend.InvoiceService.dll"]