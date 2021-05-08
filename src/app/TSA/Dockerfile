#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["SGRE.TSA.Api/SGRE.TSA.Api.csproj", "SGRE.TSA.Api/"]
COPY ["SGRE.TSA.ExternalServices/SGRE.TSA.ExternalServices.csproj", "SGRE.TSA.ExternalServices/"]
COPY ["SGRE.TSA.Models/SGRE.TSA.Models.csproj", "SGRE.TSA.Models/"]
COPY ["SGRE.TSA.Services/SGRE.TSA.Services.csproj", "SGRE.TSA.Services/"]
RUN dotnet restore "SGRE.TSA.Api/SGRE.TSA.Api.csproj"
RUN dotnet restore "SGRE.TSA.ExternalServices/SGRE.TSA.ExternalServices.csproj"
RUN dotnet restore "SGRE.TSA.Models/SGRE.TSA.Models.csproj"
RUN dotnet restore "SGRE.TSA.Services/SGRE.TSA.Services.csproj"
COPY . .
WORKDIR "/src/SGRE.TSA.Api"
RUN dotnet build "SGRE.TSA.Api.csproj" -c Release -o /app/build --no-restore

FROM build AS publish
RUN dotnet publish "SGRE.TSA.Api.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT "Development"
ENTRYPOINT ["dotnet", "SGRE.TSA.Api.dll"]