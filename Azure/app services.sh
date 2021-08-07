az group create -n webapps-dev-rg --location "northeurope"

az appservice plan create --name webapp-dev-plan --resource-group webapps-dev-rg --sku f1 --is-linux

az webapp create -g webapps-dev-rg -p webapp-dev-plan -n mp10344884 --runtime "node|10.14"