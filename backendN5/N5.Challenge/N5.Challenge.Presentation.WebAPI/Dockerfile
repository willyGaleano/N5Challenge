#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

ENV N5_ENVIRONMENT=local

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["N5.Challenge.Presentation.WebAPI/N5.Challenge.Presentation.WebAPI.csproj", "N5.Challenge.Presentation.WebAPI/"]
COPY ["N5.Challenge.Infraestructure.Persistence/N5.Challenge.Infraestructure.Persistence.csproj", "N5.Challenge.Infraestructure.Persistence/"]
COPY ["N5.Challenge.Core.Application/N5.Challenge.Core.Application.csproj", "N5.Challenge.Core.Application/"]
COPY ["N5.Challenge.Core.Domain/N5.Challenge.Core.Domain.csproj", "N5.Challenge.Core.Domain/"]
COPY ["N5.Challenge.Infraestructure.Shared/N5.Challenge.Infraestructure.Shared.csproj", "N5.Challenge.Infraestructure.Shared/"]
RUN dotnet restore "N5.Challenge.Presentation.WebAPI/N5.Challenge.Presentation.WebAPI.csproj"
COPY . .
WORKDIR "/src/N5.Challenge.Presentation.WebAPI"
RUN dotnet build "N5.Challenge.Presentation.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "N5.Challenge.Presentation.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "N5.Challenge.Presentation.WebAPI.dll"]
