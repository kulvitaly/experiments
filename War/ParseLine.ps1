param([string] $line)

. .\Logging.ps1

LogDebug "parsing: $($line)"

$parts = $($line.Trim(' ', '/')).Split(' ', [System.StringSplitOptions]::RemoveEmptyEntries)
LogVerbose "Splitted parts: $($parts)"
if ($parts.count -eq 1) {
    LogVerbose "Return url: $($parts)"
    return $parts;
}

LogVerbose "Process parts: $($parts)"

$urls = [System.Collections.Generic.List[string]]::new()
for ($i = 1; $i -lt $parts.count; $i++) {
    LogVerbose "$($i)  $($parts.count) Process part $($parts[$i])"
    $url =  $parts[$i].Trim('(', ' ', ')', ',')
    LogVerbose "Trimmed $($url)"
    $url = "$($parts[0]):$($url)"
    LogDebug "Parsed url: $($url)"
    $urls.Add($url)
}

return [array]$urls;