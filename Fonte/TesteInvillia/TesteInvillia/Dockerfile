FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY TesteInvillia/TesteInvillia.csproj TesteInvillia/
COPY Infra/Infra.csproj Infra/
COPY Infra.Interfaces/Infra.Interfaces.csproj Infra.Interfaces/
COPY Dominio.Entities/Dominio.Entidades.csproj Dominio.Entities/
COPY DTO/DTO.csproj DTO/
COPY Dominio/Dominio.csproj Dominio/
COPY Dominio.Interfaces/Dominio.Interfaces.csproj Dominio.Interfaces/
RUN dotnet restore TesteInvillia/TesteInvillia.csproj
COPY . .
WORKDIR /src/TesteInvillia
RUN dotnet build TesteInvillia.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish TesteInvillia.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TesteInvillia.dll"]
