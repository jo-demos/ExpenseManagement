# Stage 1 - Base, Runtime
#FROM mcr.microsoft.com/dotnet/core/runtime:3.1 AS base
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 as base
RUN mkdir /app
WORKDIR /app
EXPOSE 80/tcp

# Stage 2 - Build, SDK
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS publish
WORKDIR /app
COPY ExpenseManagement.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o dist

# Stage 3 - Final, release app
FROM base AS final
WORKDIR /dist
COPY --from=publish /app/dist .
ENTRYPOINT ["dotnet", "ExpenseManagement.dll"]