
param(
    [Alias('m')]
    [string] $message,
    [Alias('s')]
    [string] $source = "",
    [Alias('l')]
    [string] $level = "INF"
)
#$name  = $MyInvocation.PSCommandPath | Select -Expand Name
#Write-Host $name

$Stamp = (Get-Date).toString("yyyy/MM/dd HH:mm:ss")
$LogMessage = "$Stamp [${level}:${source}] $message"
$Today =  (Get-Date).toString("yyyy_MM_dd")

if (!(Test-Path ".\Logs")) {
    New-Item -Path ".\" -Name "Logs" -ItemType "directory"
}
    
$Logfile = ".\Logs\proc_$env:computername_$Today.log"
if (!(Test-Path $Logfile)) {
    New-Item -Path $Logfile -ItemType "file"
}

Add-content $LogFile -value $LogMessage
Write-Host $LogMessage



