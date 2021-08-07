$webapp = "mywebapp127"
$rgname = "webapps-dev-rg"
$location = 'northeurope'

New-AzResourceGroup -Name $rgname -Location $location

New-AzAppServicePlan -Name $webappname -Location $location -ResourceGroupName $rgname -Tier f1

New-AzWebApp -Name $webapp -Location $location -AppServicePlan $webapp -ResourceGroupName $rgname