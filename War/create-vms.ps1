$resourceGroup = "rg-rus-war-18-01"

az group create --location westus --name $resourceGroup

for ($i = 0; $i -lt 4; $i++)
{
    $location = "northeurope", "westeurope", "southeastasia", "westus", "westeurope", "swedensouth", "ukwest", "southafricawest", "francesouth" | Get-Random
    $name = "ruswar" + $i
    echo "VM: ${name} location: ${location}";

    az vm create --resource-group $resourceGroup --name $name --image "win2019datacenter" --admin-username "retamosik" --admin-password "drRetamoso2019"

    az vm open-port --resource-group $resourceGroup --name $name --port "3389"
}