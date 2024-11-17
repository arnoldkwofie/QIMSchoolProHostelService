FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /app


COPY QIMSchoolProHostelService/QIMSchoolProHostelService.csproj .

RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV ASPNETCORE_URLS=http://+:80

WORKDIR /app
COPY --from=base /app/publish .
COPY cert.pfx /https/cert.pfx
ENTRYPOINT ["dotnet","QIMSchoolProHostelService.dll"]

