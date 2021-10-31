FROM mcr.microsoft.com/dotnet/core/runtime:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/OpetNet.EventListeners/OpetNet.EventListeners.csproj", "src/OpetNet.EventListeners/"]
COPY ["src/OpetNet.Application/OpetNet.Application.csproj", "src/OpetNet.Application/"]
COPY ["src/OpetNet.Domain/OpetNet.Domain.csproj", "src/OpetNet.Domain/"]
COPY ["src/OpetNet.Domain.Core/OpetNet.Domain.Core.csproj", "src/OpetNet.Domain.Core/"]
COPY ["src/OpetNet.Infra.CrossCutting.Bus/OpetNet.Infra.CrossCutting.Bus.csproj", "src/OpetNet.Infra.CrossCutting.Bus/"]
COPY ["src/OpetNet.Infra.Data/OpetNet.Infra.Data.csproj", "src/OpetNet.Infra.Data/"]
COPY ["src/OpetNet.Infra.CrossCutting.IoC/OpetNet.Infra.CrossCutting.IoC.csproj", "src/OpetNet.Infra.CrossCutting.IoC/"]
COPY ["src/OpetNet.Infra.CrossCutting.Identity/OpetNet.Infra.CrossCutting.Identity.csproj", "src/OpetNet.Infra.CrossCutting.Identity/"]
RUN dotnet restore "OpetNet.EventListeners/OpetNet.EventListeners.csproj"
COPY . .
WORKDIR "/src/OpetNet.EventListeners"
RUN dotnet build "OpetNet.EventListeners.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OpetNet.EventListeners.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OpetNet.EventListeners.dll"]
