FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY   HyperJSONGenerator.csproj  .  
COPY   .  .
RUN dotnet restore HyperJSONGenerator.csproj
RUN dotnet build HyperJSONGenerator.csproj -c debug

CMD ["dotnet",  "run",  "HyperJSONGenerator.csproj"]

