# ============================================
# DOCKERFILE - BlogPlantasDomesticas API
# ============================================
# Este Dockerfile usa "multi-stage build" - uma técnica que:
# 1. Reduz o tamanho final da imagem (só inclui o necessário)
# 2. Melhora segurança (não expõe ferramentas de build)
# 3. Acelera deploys (imagens menores = uploads mais rápidos)

# ============================================
# STAGE 1: BUILD
# ============================================
# Usamos a imagem SDK que contém todas as ferramentas para compilar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Define o diretório de trabalho dentro do container
WORKDIR /src

# IMPORTANTE: Copiamos primeiro APENAS os arquivos .csproj e .sln
# Por quê? Docker usa cache por camadas. Se copiarmos tudo de uma vez,
# qualquer mudança no código invalida o cache do "dotnet restore"
# Copiando só os arquivos de projeto primeiro, o restore só roda
# novamente se as dependências mudarem
COPY *.sln ./
COPY src/BlogPlantasDomesticas.Api/*.csproj ./src/BlogPlantasDomesticas.Api/

# Restaura as dependências (NuGet packages)
# Esta camada fica em cache até você mudar o .csproj
RUN dotnet restore

# Agora sim, copia todo o código fonte
COPY . .

# Compila em modo Release (otimizado para produção)
# -c Release = configuração Release
# -o /app/build = output para /app/build
RUN dotnet build -c Release -o /app/build

# ============================================
# STAGE 2: PUBLISH
# ============================================
# Continua do stage anterior
FROM build AS publish

# Publica a aplicação (gera os arquivos finais otimizados)
# --no-restore = já fizemos restore antes
# /p:UseAppHost=false = não gera executável nativo (usaremos dotnet CLI)
RUN dotnet publish src/BlogPlantasDomesticas.Api/BlogPlantasDomesticas.Api.csproj \
    -c Release \
    -o /app/publish \
    --no-restore \
    /p:UseAppHost=false

# ============================================
# STAGE 3: RUNTIME (imagem final)
# ============================================
# Usamos a imagem ASP.NET runtime - muito menor que o SDK!
# SDK ~800MB vs Runtime ~200MB
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Boa prática: não rodar como root
# Cria um usuário sem privilégios para segurança
USER app

# Diretório onde a aplicação vai rodar
WORKDIR /app

# Porta que a aplicação vai expor
# ASPNETCORE_HTTP_PORTS é a forma moderna de configurar (NET 8+)
ENV ASPNETCORE_HTTP_PORTS=8080

# Copia APENAS os arquivos publicados do stage anterior
# Isso significa que SDK, código fonte, etc. NÃO vão para a imagem final
COPY --from=publish /app/publish .

# Expõe a porta (documentação - não abre a porta automaticamente)
EXPOSE 8080

# Comando que executa quando o container inicia
# ENTRYPOINT vs CMD:
# - ENTRYPOINT: comando principal (difícil de sobrescrever)
# - CMD: argumentos ou comando padrão (fácil de sobrescrever)
ENTRYPOINT ["dotnet", "BlogPlantasDomesticas.Api.dll"]