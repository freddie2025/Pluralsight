FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["PsWebApi/PsWebApi.csproj", "PsWebApi/"]
RUN dotnet restore "PsWebApi/PsWebApi.csproj"
COPY . .
WORKDIR "/src/PsWebApi"
RUN dotnet build "PsWebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "PsWebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PsWebApi.dll"]