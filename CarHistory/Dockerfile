FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-nanoserver-1809 AS build
WORKDIR /src
COPY ["CarHistory/CarHistory.csproj", "CarHistory/"]
RUN dotnet restore "CarHistory/CarHistory.csproj"
COPY . .
WORKDIR "/src/CarHistory"
RUN dotnet build "CarHistory.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarHistory.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD dotnet CarHistory.dll

#ENTRYPOINT ["dotnet", "CarHistory.dll"]

