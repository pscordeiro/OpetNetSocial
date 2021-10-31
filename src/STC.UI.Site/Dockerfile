FROM microsoft/dotnet:2.2-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["STC.UI.Site/STC.UI.Site.csproj", "STC.UI.Site/"]
COPY ["STC.Infra.CrossCutting.Identity/STC.Infra.CrossCutting.Identity.csproj", "STC.Infra.CrossCutting.Identity/"]
COPY ["STC.Domain/STC.Domain.csproj", "STC.Domain/"]
COPY ["STC.Domain.Core/STC.Domain.Core.csproj", "aqui.Template.Domain.Core/"]
COPY ["STC.Infra.CrossCutting.Bus/STC.Infra.CrossCutting.Bus.csproj", "STC.Infra.CrossCutting.Bus/"]
COPY ["STC.Application/STC.Application.csproj", "STC.Application/"]
COPY ["STC.Infra.Data/STC.Infra.Data.csproj", "STC.Infra.Data/"]
COPY ["STC.Infra.CrossCutting.IoC/STC.Infra.CrossCutting.IoC.csproj", "aqui.Template.Infra.CrossCutting.IoC/"]
RUN dotnet restore "STC.UI.Site/STC.UI.Site.csproj"
COPY . .
WORKDIR "STC.UI.Site"
RUN dotnet build "STC.UI.Site.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "STC.UI.Site.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "STC.UI.Site.dll"]
