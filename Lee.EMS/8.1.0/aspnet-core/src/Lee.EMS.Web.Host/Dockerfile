FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["src/Lee.EMS.Web.Host/Lee.EMS.Web.Host.csproj", "src/Lee.EMS.Web.Host/"]
COPY ["src/Lee.EMS.Web.Core/Lee.EMS.Web.Core.csproj", "src/Lee.EMS.Web.Core/"]
COPY ["src/Lee.EMS.Application/Lee.EMS.Application.csproj", "src/Lee.EMS.Application/"]
COPY ["src/Lee.EMS.Core/Lee.EMS.Core.csproj", "src/Lee.EMS.Core/"]
COPY ["src/Lee.EMS.EntityFrameworkCore/Lee.EMS.EntityFrameworkCore.csproj", "src/Lee.EMS.EntityFrameworkCore/"]
WORKDIR "/src/src/Lee.EMS.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/Lee.EMS.Web.Host", "src/Lee.EMS.Web.Host"]
COPY ["src/Lee.EMS.Web.Core", "src/Lee.EMS.Web.Core"]
COPY ["src/Lee.EMS.Application", "src/Lee.EMS.Application"]
COPY ["src/Lee.EMS.Core", "src/Lee.EMS.Core"]
COPY ["src/Lee.EMS.EntityFrameworkCore", "src/Lee.EMS.EntityFrameworkCore"]
WORKDIR "/src/src/Lee.EMS.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "Lee.EMS.Web.Host.dll"]
