FROM mcr.microsoft.com/dotnet/aspnet:5.0
COPY bin/Release/net5.0/publish/ App/
COPY assets/ App/assets/
WORKDIR /App
EXPOSE 80
ENTRYPOINT ["dotnet", "api.dll"]