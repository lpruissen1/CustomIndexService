[CmdletBinding()]
param (
	[Parameter()]
	[string]
	$ServicePath = $(Join-Path $PSScriptRoot "\..\Users.Service")
)

function CleanStagingDirectory {
	Write-Host "`nCleaning output directory"

	& Remove-Item $OutputDirectory
}

function StageApplicationFiles {
	Write-Host "`nStaging Files"

	xcopy "$PSScriptRoot\..\API\Users.Service\bin\net5.0" $"$OutputDirectory\bin\" /y /I /s

	Copy-Item "$PSScriptRoot\advisor-site\script\DeployUsersService.ps1" "$OutputDirectory\"
}

$servicePath = Join-Path $ServicePath "bin\net5.0\Users.Service.exe"
$credential = New-Object System.Management.Automation.PSCredential("Leendert", "1922")
New-Service -BinaryPathName $servicePath -Name "Users.Service" -Credential
