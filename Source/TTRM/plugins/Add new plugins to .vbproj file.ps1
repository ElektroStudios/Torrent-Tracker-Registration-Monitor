$projectPath = Join-Path $PSScriptRoot "..\TTRM.vbproj"
[xml]$xml = Get-Content $projectPath -Encoding UTF8

# Obtener colecciones de ItemGroup
$itemGroups = $xml.Project.ItemGroup

# Buscar (o crear) ItemGroup que contenga <None>
$noneGroup = $itemGroups | Where-Object { $_.None } | Select-Object -First 1
if (-not $noneGroup) {
    $noneGroup = $xml.CreateElement("ItemGroup")
    $xml.Project.AppendChild($noneGroup) | Out-Null
}

# Buscar (o crear) ItemGroup que contenga <Compile>
$compileGroup = $itemGroups | Where-Object { $_.Compile } | Select-Object -First 1
if (-not $compileGroup) {
    $compileGroup = $xml.CreateElement("ItemGroup")
    $xml.Project.AppendChild($compileGroup) | Out-Null
}

$baseDir = $PSScriptRoot
$baseName = Split-Path $baseDir -Leaf
$files = Get-ChildItem $baseDir -Recurse -File

# Recolectar listas de paths ya existentes (None y Compile)
$existingNone = @()
$existingCompile = @()

# Protegemos contra null cuando no hay nodos
if ($xml.Project.ItemGroup) {
    $xml.Project.ItemGroup | ForEach-Object {
        if ($_.None) { $null = @($_.None) | ForEach-Object { $existingNone += $_.Update } }
        if ($_.Compile) { $null = @($_.Compile) | ForEach-Object { $existingCompile += $_.Update } }
    }
}

# Union de existentes para comprobación rápida
$existingAll = @($existingNone + $existingCompile)

# Flag para saber si se añadió algún elemento
$changed = $false

# Lista para mostrar con formato de tabla
$tableData = @()

foreach ($file in $files) {
    $relativePart = $file.FullName.Substring($baseDir.Length + 1)
    $relPath = Join-Path $baseName $relativePart
    $relPath = $relPath -replace '\\','\' # normalizar separadores

    # No añadir si ya existe en ninguno de los dos tipos
    if ($existingAll -contains $relPath) { continue }

    $ext = $file.Extension.ToLower()
    # Valor CopyToOutputDirectory: .ps1 => Never, else => Always
    $copyValue = if ($ext -eq ".ps1") { "Never" } else { "Always" }

    if ($ext -eq ".vb") {
        # Crear nodo <Compile Update="...">
        $compileNode = $xml.CreateElement("Compile")
        $compileNode.SetAttribute("Update", $relPath)

        $copyNode = $xml.CreateElement("CopyToOutputDirectory")
        $copyNode.InnerText = $copyValue
        $compileNode.AppendChild($copyNode) | Out-Null

        $compileGroup.AppendChild($compileNode) | Out-Null

        # Agregar a existente para evitar duplicados posteriores en la misma ejecución
        $existingCompile += $relPath
        $existingAll += $relPath

        $tableData += [PSCustomObject]@{
            File = $relPath
            Type = "Compile"
            CopyToOutputDirectory = $copyValue
        }
        $changed = $true
    } else {
        # Crear nodo <None Update="...">
        $noneNode = $xml.CreateElement("None")
        $noneNode.SetAttribute("Update", $relPath)

        $copyNode = $xml.CreateElement("CopyToOutputDirectory")
        $copyNode.InnerText = $copyValue
        $noneNode.AppendChild($copyNode) | Out-Null

        $noneGroup.AppendChild($noneNode) | Out-Null

        $existingNone += $relPath
        $existingAll += $relPath

        $tableData += [PSCustomObject]@{
            File = $relPath
            Type = "None"
            CopyToOutputDirectory = $copyValue
        }
        $changed = $true
    }
}

if ($changed) {
    $settings = New-Object System.Xml.XmlWriterSettings
    $settings.Indent = $true
    $settings.Encoding = [System.Text.UTF8Encoding]::new($false)
    $writer = [System.Xml.XmlWriter]::Create($projectPath, $settings)
    $xml.Save($writer)
    $writer.Close()

    Write-Host "New files added:`n"
    $tableData | Format-Table -AutoSize | Out-String | Write-Host

    Write-Host "Project file updated!"
} else {
    Write-Host "No new files to add. Project file remains unchanged."
}

Write-Host ""
Pause
Exit /B 0
