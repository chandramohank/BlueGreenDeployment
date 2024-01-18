FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5269

ENV ASPNETCORE_URLS=http://+:5269

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
#RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
#USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG configuration=Release
# Nginx
RUN apt update && \
    apt install -y --no-install-recommends nginx && \
    rm -rf /var/lib/apt/lists/* && \
    apt clean && \
    rm /etc/nginx/nginx.conf
COPY content /usr/share/nginx/html
COPY conf /etc/nginx
RUN mkdir -p /usr/share/nginx/html/.well-known/apple-developer-merchantid-domain-association
COPY cert/apple-developer-merchantid-domain-association /usr/share/nginx/html/.well-known/apple-developer-merchantid-domain-association
WORKDIR /src
COPY ["WebAPI.csproj", "./"]
RUN dotnet restore "WebAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "WebAPI.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "WebAPI.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAPI.dll"]
