param([string] $line)

.\Log -l DBG -s 'ParseLine' "parsing: $($line)"

$parts = $($line.Trim(' ', '/')).Split(' ', [System.StringSplitOptions]::RemoveEmptyEntries)
.\Log -l VRB -s 'ParseLine' "Splitted parts: $($parts)"
if ($parts.count -eq 1) {
    .\Log -l VRB -s 'ParseLine' "Return url: $($parts)"
    return $parts;
}

.\Log -l VRB -s 'ParseLine' "Process parts: $($parts)"

$urls = [System.Collections.Generic.List[string]]::new()
for ($i = 1; $i -lt $parts.count; $i++) {
    .\Log -l VRB -s 'ParseLine' "$($i)  $($parts.count) Process part $($parts[$i])"
    $url =  $parts[$i].Trim('(', ' ', ')', ',')
    .\Log -l VRB -s 'ParseLine' "Trimmed $($url)"
    $url = $url.Replace('/tcp', '')
    .\Log -l VRB -s 'ParseLine' "Removed tcp $($url)"
    $url = "$($parts[0]):$($url)"
    .\Log -l VRB -s 'ParseLine' "Result: $($url)"
    $urls.Add($url)
}

return [array]$urls;