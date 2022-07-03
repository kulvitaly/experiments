function ShouldLog([string] $logLevel) {
    $allLogLevels = @("CRI", "ERR", "WRN", "INF", "DBG", "VRB")
    $currentLogLevel = .\GetCurrentLogLevel
    $threashold = [array]::IndexOf($allLogLevels, $currentLogLevel)

    $levelId = [array]::IndexOf($allLogLevels, $logLevel)

    return $threashold -ge $levelId
}

function Log(
    [Alias('m')]
    [string] $message,
    [Alias('s')]
    [string] $source = $(Get-ChildItem $MyInvocation.PSCommandPath | Select-Object -Expand Name),
    [Alias('l')]
    [string] $level = "INF"
) {

    $Stamp = (Get-Date).toString("yyyy/MM/dd HH:mm:ss")
    $LogMessage = "$Stamp [${level}:${source}] $message"
    $Today =  (Get-Date).toString("yyyy_MM_dd")

    if (ShouldLog($level)) {
        if (!(Test-Path ".\Logs")) {
            New-Item -Path ".\" -Name "Logs" -ItemType "directory"
        }
    
        $Logfile = ".\Logs\proc_$Today.log"
        if (!(Test-Path $Logfile)) {
            New-Item -Path $Logfile -ItemType "file"
        }

        Add-content $LogFile -value $LogMessage
        Write-Host $LogMessage
    }
}

function LogDebug(
        [Alias('m')]
        [string] $message,
        [Alias('s')]
        [string] $source = $(Get-ChildItem $MyInvocation.PSCommandPath | Select-Object -Expand Name)
) {
    Log -l DBG -m $message -s $source
}

function LogVerbose(
        [Alias('m')]
        [string] $message,
        [Alias('s')]
        [string] $source = $(Get-ChildItem $MyInvocation.PSCommandPath | Select-Object -Expand Name)
) {
    Log -l VRB -m $message -s $source
}

function LogInfo(
        [Alias('m')]
        [string] $message,
        [Alias('s')]
        [string] $source = $(Get-ChildItem $MyInvocation.PSCommandPath | Select-Object -Expand Name)
) {
    Log -l INF -m $message -s $source
}

function LogWarning(
        [Alias('m')]
        [string] $message,
        [Alias('s')]
        [string] $source = $(Get-ChildItem $MyInvocation.PSCommandPath | Select-Object -Expand Name)
) {
    Log -l WRN -m $message -s $source
}

function LogError(
        [Alias('m')]
        [string] $message,
        [Alias('s')]
        [string] $source = $(Get-ChildItem $MyInvocation.PSCommandPath | Select-Object -Expand Name)
) {
    Log -l ERR -m $message -s $source
}

function LogCritical(
        [Alias('m')]
        [string] $message,
        [Alias('s')]
        [string] $source = $(Get-ChildItem $MyInvocation.PSCommandPath | Select-Object -Expand Name)
) {
    Log -l CRI -m $message -s $source
}