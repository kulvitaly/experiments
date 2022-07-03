param ([string] $Path = './config.json')

$configJson = Get-Content $Path | Out-String | ConvertFrom-Json

return $configJson.LogLevel