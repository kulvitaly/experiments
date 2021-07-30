## create resource group
az group create --name "psdemo-rg" --location "northeurope

## create VM
** Win **
> az vm create --resource-group "psdemo-rg" --name "psdemo-win-cli" --image "win2019datacenter" --admin-username "vku" --admin-password "pwd"
> az vm open-port --resource-group "psdemo-rg" --name "psdemo-won-cli" --port "3389"
> az vm list-ip-addresses --resource-group "psdemo-rg" --name "psdemo-win-cli"

** Linux **
> az vm create --resource-group "psdemo-rg" --name "psdemo-linux-cli" --image "UbuntuLTS" --admin-username "vku" --authentication-type "ssh" --ssh-key-value ~/.ssh/id-rsa.pub --generate-ssh-keys
> az vm open-port --resource-group "psdemo-rg" --name "psdemo-linux-cli" --port "22"
> az vm list-ip-addresses --resource-group "psdemo-rg" --name "psdemo-linux-cli"