#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

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




#FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
#WORKDIR /app

# copy csproj and restore as distinct layers
#COPY *.sln .
#COPY CarHistory/*.csproj ./CarHistory/
#RUN dotnet restore

# copy everything else and build app
#COPY CarHistory/. ./CarHistory/
#WORKDIR /app/CarHistory
#RUN dotnet publish -c Release -o out


#FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
#WORKDIR /app
#COPY --from=build /app/CarHistory/out ./
#ENTRYPOINT ["dotnet", "CarHistory.dll"]