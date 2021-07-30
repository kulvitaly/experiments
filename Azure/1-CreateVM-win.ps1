Connect-AzAccount -SubscriptionName 'Visual Studio Enterprise Subscription'

Set-AzContext -SubscriptionName 'Visual Studio Enterprise Subscription'

New-AzResourceGroup -Name "psdemo-rg" -Location "NorthEurope"

$username = 'vku'
$password = ConvertTo-SecureString 'Kulykivsky89' -AsPlainText -Force
$WindowsCred = New-Object System.Management.Automation.PsCredential ($username, $password)


New-AzVM `
    -ResourceGroupName 'psdemo-rg' `
    -Name 'psdemo-win-az' `
    -Image 'Win2019Datacenter' `
    -Credential $WindowsCred `
    -OpenPorts 3389

Get-AzPublicIpAddress `
    -ResourceGroupName 'psdemo-rg' `
    -Name 'psdemo-win-az' | Select-Object IpAddress

Remove-AzResourceGroup -Name 'psdemo-rg'
    
