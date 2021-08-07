$ACR_NAME = 'vkupsdemoacr'

az acr create --resource-group psdemo-rg --name $ACR_NAME --sku Basic

az acr login --name $ACR_NAME

$ACR_LOGINSERVER = $(az acr show --name $ACR_NAME --query loginServer --output tsv)
echo $ACR_LOGINSERVER

docker tag webappimage:v1 $ACR_LOGINSERVER/webappimage:v1

docker push $ACR_LOGINSERVER/webappimage:v1

az acr repository list --name $ACR_NAME --output table

az acr repository show-tags --name $ACR_NAME --repository webappimage --output table

az acr build --image "webappimage:v1-acr-task" --registry $ACR_NAME .

# create service principal
$ACR_REGISTRY_ID = $(az acr show --name $ACR_NAME --query id --output tsv)

$SP_NAME = acr-service-principal

$SP_PASSWD = $(az ad sp create-for-rbac --name http://$ACR_NAME-pull --scopes $ACR_REGISTRY_ID --role acrpull --query password --output tsv)

$SP_APPID = $(az ad sp show --id http://$ACR_NAME-pull --query appId --output tsv)

az container create --resource-group psdemo-rg --name psdemo-webapp-cli --dns-name-label psdemo-webapp-cli --ports 80 --image $ACR_LOGINSERVER/webappimage:v1 --registry-login-server $ACR_LOGINSERVER --registry-username $SP_APPID --registry-password $SP_PASSWD

az container show --resource-group 'psdemo-rg' --name 'psdemo-webapp-cli'

$URL=$(az container show --resource-group 'psdemo-rg' --name 'psdemo-webapp-cli' --query ipAddress.gqdn | tr -d)

az container show --resource-group psdemo-rg --name psdemo-webapp-cli

az container logs --resource-group psdemo-rg --name psdemo-webapp-cli
