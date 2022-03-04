# provision a Key Vault
New-AzKeyVault -VaultName 'vku-kv-az204' -ResourceGroupName 'test-security1' -Location eastus

# enable soft-delete on an existing Vault
($resource = Get-AzResource -ResourceId (Get-AzKeyVault -VaultName 'vku-kv-az204').ResourceId).Properties | Add-Member -MemberType "NoteProperty" -Name "enableSoftDelete" -Value "true"
Set-AzResource -resourceid $resource.ResourceId -Properties $resource.Properties

# enable purge protection on an existing Vault (once enabled, it can not be disabled)
($resource = Get-AzResource -ResourceId (Get-AzKeyVault -VaultName 'vku-kv-az204').ResourceId).Properties | Add-Member -MemberType "NoteProperty" -Name "enablePurgeProtection" -Value "true"
Set-AzResource -resourceid $resource.ResourceId -Properties $resource.Properties
