$dataHora = Get-Date
'Horario Inicial: ' + $dataHora

$dirPublicacao = "publicacao"
$DesktopPath = [Environment]::GetFolderPath("Desktop") + '\PastaPublicar'

# Usa o NuGet para restaurar as dependências e ferramentas específicas de projeto especificadas no arquivo de projeto
dotnet restore

# Compilar o projeto e, portanto, dá suporte a builds paralelos e incrementais.
dotnet build

# Driver de teste do .NET usado para executar testes de unidade.
dotnet test

# Força a exclusão de uma pasta para publicação criada anteriormente
if (Test-Path $dirPublicacao) {
    Remove-Item -Recurse -Force $dirPublicacao
}

# Realiza a publicação
dotnet publish -c release -o $dirPublicacao

# Remove um arquivo compactado com a publicação (caso o mesmo tenha sido
# criado anteriormente)
$arqPublicacao = "publicacao.zip"
if (Test-Path $arqPublicacao) {
    Remove-Item -Force $arqPublicacao
}

# Efetua a compressão da pasta com a publicação
Add-Type -assembly "system.io.compression.filesystem"
[io.compression.zipfile]::CreateFromDirectory($dirPublicacao, $arqPublicacao)

# Efetua a descompressão do .zip com a publicação
Add-Type -Assembly "System.IO.Compression.FileSystem"
[System.IO.Compression.ZipFile]::ExtractToDirectory($arqPublicacao, $DesktopPath)

$dataHora = Get-Date
'Horario Final: ' + $dataHora