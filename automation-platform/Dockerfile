FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["automation_platform.csproj", "."]
RUN dotnet restore "automation_platform.csproj"

COPY . .
RUN dotnet publish "automation_platform.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "automation_platform.dll"]