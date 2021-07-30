az login
az account set --subscription "Visual Studio Enterprise Subscription"

az group list --output table

az group create --name "psdemo-rg" --location "northeurope"

az vm create --resource-group "psdemo-rg" --name "psdemo-win-cli" --image "win2019datacenter" --admin-username "vku" --admin-password "pwd"

az vm open-port --resource-group "psdemo-rg" --name "psdemo-win-cli" --port "3389"

az vm list-ip-addresses --resource-group "psdemo-rg" --name "psdemo-win-cli" --output table

az deployment group create --name mydeployment --resource-group "psdemo-rg" --template-file "C:\Users\vitalii.kulykivskyi\Downloads\template\template.json" --parameters "C:\Users\vitalii.kulykivskyi\Downloads\template\parameters.json"