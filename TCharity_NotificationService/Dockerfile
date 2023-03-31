FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TCharity_NotificationService/TCharity_NotificationService.csproj", "TCharity_NotificationService/"]
RUN dotnet restore "TCharity_NotificationService/TCharity_NotificationService.csproj"
COPY . .
WORKDIR "/src/TCharity_NotificationService"
RUN dotnet build "TCharity_NotificationService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TCharity_NotificationService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TCharity_NotificationService.dll"]
