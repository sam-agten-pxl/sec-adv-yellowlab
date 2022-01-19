#Build step
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /App

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

#Run console
FROM mcr.microsoft.com/dotnet/aspnet:5.0
#COPY bin/Release/net5.0/publish/ App/
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "console.dll"]