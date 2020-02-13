FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY PsMovieStore/PsMovieStore.csproj PsMovieStore/
RUN dotnet restore PsMovieStore/PsMovieStore.csproj
COPY . .
WORKDIR /src/PsMovieStore
RUN dotnet build PsMovieStore.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish PsMovieStore.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PsMovieStore.dll"]
