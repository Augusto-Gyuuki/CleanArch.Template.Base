#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/CleanArch.Base.Template.Web/CleanArch.Base.Template.Web.csproj", "src/CleanArch.Base.Template.Web/"]
COPY ["src/CleanArch.Base.Template.Application/CleanArch.Base.Template.Application.csproj", "src/CleanArch.Base.Template.Application/"]
COPY ["src/CleanArch.Base.Template.Domain/CleanArch.Base.Template.Domain.csproj", "src/CleanArch.Base.Template.Domain/"]
COPY ["src/CleanArch.Base.Template.Infrastructure/CleanArch.Base.Template.Infrastructure.csproj", "src/CleanArch.Base.Template.Infrastructure/"]
COPY ["src/CleanArch.Base.Template.Persistence/CleanArch.Base.Template.Persistence.csproj", "src/CleanArch.Base.Template.Persistence/"]
COPY ["src/CleanArch.Base.Template.Presentation/CleanArch.Base.Template.Presentation.csproj", "src/CleanArch.Base.Template.Presentation/"]
RUN dotnet restore "src/CleanArch.Base.Template.Web/CleanArch.Base.Template.Web.csproj"
COPY . .
WORKDIR "/src/src/CleanArch.Base.Template.Web"
RUN dotnet build "CleanArch.Base.Template.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CleanArch.Base.Template.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleanArch.Base.Template.Web.dll"]