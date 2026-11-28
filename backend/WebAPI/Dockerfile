FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG TARGETARCH
WORKDIR /src

COPY . .
RUN dotnet restore "LangScan/LangScan.csproj" -a $TARGETARCH
RUN dotnet publish "LangScan/LangScan.csproj" -a $TARGETARCH -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "LangScan.dll"]