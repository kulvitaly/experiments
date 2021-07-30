ACR_NAME = 'psdemoacr'

az acr create --resource-group psdemo-rg --name $ACR_NAME --sku Basic

az acr login --name $ACR_NAME

ACR_LOGINSERVER = $(az acr show --name $ACR_NAME --query loginServer --output tsv)

docker tag webappimage:v1 $ACR_LOGINSERVER/webappimage:v1

docker push $ACR_LOGINSERVER/webappimage:v1

az acr build --image "webappimage:v1-acr-task" --registry $ACR_NAME .