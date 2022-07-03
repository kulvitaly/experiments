param(
    [Alias('Source')][string] $LinesPath = "./lines.txt",
    [Alias('Destination')][string] $TargetUrlsPath = "./urls.txt",
    [int] $AgentCount = 100,
    [string] $ExecutionTime = "60s",
    [string] $HttpMethod = 'http1',
    [int] $FinishTimeout = 100
)

. .\Logging.ps1

LogInfo "==== Start PingSites ===="

if (Test-Path $TargetUrlsPath) {
    LogInfo "Remove old urls file: $TargetUrlsPath"
    Remove-Item $TargetUrlsPath
}

$lines = [System.IO.File]::ReadLines($LinesPath)

$location = .\GetRandomLocation

$resourceGroup = "rg-rw-ping-$(Get-Date -Format "MM-dd")-$(Get-Random)"
az group create --location $location --name $resourceGroup

$id = 1
$containerUrlMap = @{}

ForEach ($line in $lines) {
    if ([string]::IsNullOrWhiteSpace($line)) {
        LogDebug "Ignore empty line"
        continue 
    }

    $urls = .\ParseLine $line 
    ForEach ($url in $urls) {
        $id++
        
        $name = "ruswar" + $i++
        $location = .\GetRandomLocation

        LogInfo "ResouceGroup: ${resourceGroup} Container: ${name} location: ${location} url: ${url}";

        az container create --resource-group $resourceGroup --name $name --image alpine/bombardier:latest --restart-policy OnFailure --command-line "/gopath/bin/bombardier -c 100 -d 60s --http1 -l ${url}" --location $location
    
        $containerUrlMap.Add($name, $url)

        LogDebug "Add to dictionary: ${name}: ${url}"
    }
}

LogInfo "Waiting $FinishTimeout sec for all containers to finish.";
        
Start-Sleep -Seconds $FinishTimeout

# Collect & Analyze Logs
ForEach ($key in $containerUrlMap.keys) {
    $url = $containerUrlMap[$key]
    LogInfo "ResouceGroup: ${resourceGroup} Container: ${key} url: ${url}";

    $logs = az container logs --resource-group $resourceGroup --name $key
    LogInfo "Container: ${key}, url: ${url} Output: $logs"

    # Filter out unsuccessful 
    $match = $logs -match '1xx - 0, 2xx - 0, 3xx - 0, 4xx - 0, 5xx - 0'
    LogVerbose "Match: $match"
    if (![string]::IsNullOrEmpty($logs) -and $logs -match 'Bombarding' -and !($logs -match '1xx - 0, 2xx - 0, 3xx - 0, 4xx - 0, 5xx - 0')) {
        LogInfo "Ping ${url}. Result: SUCCESS"
        if (!(Test-Path $TargetUrlsPath)) {
            New-Item -Path $TargetUrlsPath -ItemType "file"
        }

        Add-content $TargetUrlsPath -value ${url}
    } 
    else {
        LogInfo "Ping ${url}. Result: FAIL"
    }
}

az group delete --resource-group $resourceGroup --yes
LogInfo "Deleted resource group ${resourceGroup}";

LogInfo "==== Finish PingSites ===="
[console]::beep(500,1000)