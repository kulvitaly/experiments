# show list of storage accounts
Get-AzStorageAccount -ResourceGroupName pluralsight-resource-group

# show list of containers in Storage account
Get-AzRmStorageContainer -ResourceGroupName pluralsight-resource-group -AccountName <account name>

# create variable holding storage account key
$GlobomanticsAccountKey = (Get-AzureRmStorageAccountKey -ResourceGroupName pluralsight-resource-group -Name <storage account>).Value[0]

# create variable holding actual context
$GlobomanticsContext = New-AzureStorageContext -StorageAccountName <storage account> -StorageAccountKey $GlobomanticsAccountKey

# list blobs in "archive" container
Get-AzStorageBlob -Container "archive" -context $GlobomanticsContext

# transition all blobs to archive tier
$Blobs = Get-AzureStorageBlob -Context $GlobomanticsContext -Container "archive"
Foreach($Blob in $Blobs) {
    $blob.icloudblob.SetStandardBlobTier("archive") 
}

# rehydrate blob (put back to hot tier). Note: it could take up to 15 hours
$Blob = Get-AzureStorageBlob -Context $GlobomanticsContext -Container "archive" -Blob "doc1.txt"
$blob.icloudblob.SetStandardBlobTier("hot","High")

# transition all blobs to cool tier
$Blobs = Get-AzureStorageBlob -Context $GlobomanticsContext -Container "public"
Foreach($Blob in $Blobs) {
    $blob.icloudblob.SetStandardBlobTier("cool") 
}

# show all blobs from all containers
$StorageContainers = Get-AzureStorageContainer -Context $GlobomanticsContext 
foreach($StorageContainer in $StorageContainers) { 
    Get-AzureStorageBlob -Context $GlobomanticsContext -Container ($StorageContainer).Name 
}

#####
# Set up Lifecycle Management Policies
#####

# list all management policies currently set up on the storage account
Get-AzStorageAccountManagementPolicy -ResourceGroupName pluralsight-resource-group -AccountName <storage account>

# create variable holding action to be taken as part of the policy we are setting up
$action = Add-AzStorageAccountManagementPolicyAction -BaseBlobAction TierToArchive -daysAfterModificationGreaterThan 90
$action = Add-AzStorageAccountManagementPolicyAction -InputObject $action -BaseBlobAction TierToCool -daysAfterModificationGreaterThan 30

# create variable holding a filter (which blobs the policy applies to)
$PublicFilter = New-AzStorageAccountManagementPolicyFilter -DefaultProfile $GlobomanticsContext -PrefixMatch public

# create variable hodling the rule witch will be executed as part of the policy you are setting up
$PublicLifecyleRule = New-AzStorageAccountManagementPolicyRule -Name PublicLifecyleRule -Action $action -Filter $PublicFilter

# list the policies that have been set up
Set-AzStorageAccountManagementPolicy -ResourceGroupName pluralsight-resource-group -StorageAccountName <storage account> -Rule $PublicLifecyleRule