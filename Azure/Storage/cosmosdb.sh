# create a sql qpo cosmos db account
az cosmosdb create --name pluralsight --resource-group pluralsight

# create a sql DB
az cosmosdb create sql database create --account-name pluralsight --name sampledb

# create a sql database container
az cosmosdb sql container create --resource-group pluralsight --account-name pluralsight
 --database-name sampledb --name samplecontainer --partition-key-path "/employeeid"
 