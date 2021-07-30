az login
az account set --subscription "Visual Studio Enterprise Subscription"

az group list --output table

az group create --name "psdemo-rg" --location "northeurope"

az vm create --resource-group "psdemo-rg" --name "psdemo-linux-cli" --image "UbuntuLTS" --admin-username "vku" --authentication-type "ssh" --ssh-key-value ~/.ssh/id-rsa.pub --generate-ssh-keys

az vm open-port --resource-group "psdemo-rg" --name "psdemo-linux-cli" --port "22"

az vm list-ip-addresses --resource-group "psdemo-rg" --name "psdemo-linux-cli" --output table

ssh vku@23.100.48.147

az vm show --resource-group "psdemo-rg" --name "psdemo-linux-cli" --query osProfile.linuxConfiguration.ssh.publicKeys[0].keyData -o tsv

az vm user update --resource-group "psdemo-rg" --name "psdemo-linux-cli" --username vku --ssh-key-value ~/.ssh/id-rsa1.pub