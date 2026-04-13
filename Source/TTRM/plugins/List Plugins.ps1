# Lista los campos Name y Url de todos los archivos JSON encontrados recursivamente.

$basePath = "."

$items = New-Object System.Collections.ArrayList

# Recorrer todos los JSON
Get-ChildItem -Path $basePath -Filter "*.json" -Recurse | ForEach-Object {
    try {
        $json = Get-Content $_.FullName -Raw | ConvertFrom-Json
        if ($json -isnot [System.Collections.IEnumerable]) { $json = @($json) }

        foreach ($item in $json) {
                $null = $items.Add([PSCustomObject]@{
                    Count = (++$i).ToString("00")
                    Name = $item.Name
                    LoginUrl  = $item.UrlLogin
                })
        }
    } catch {
        Write-Warning "Error procesando $($_.FullName)"
    }
}

$items | Format-Table -AutoSize Count, Name, LoginUrl

Write-Host ""
Pause
Exit /B 0
