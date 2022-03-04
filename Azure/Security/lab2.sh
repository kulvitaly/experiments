# create key
az keyvault key create --name "key1" --vault-name pssacs1372w87bchlr4l

#  create a secret
az keyvault secret set --name "SQLPassword" --value "hVFkk965BuUv" --vault-name pssacs1372w87bchlr4l