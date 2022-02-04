[CmdletBinding()]
param (
	[string]
	$OutputDirectory = "$PSScriptRoot\..\_Artifacts\Users.Service"
)

function CleanStagingDirectory {
	Write-Host "`nCleaning output directory"

	& Remove-Item $OutputDirectory
}

function StageApplicationFiles {
	Write-Host "`nStaging Files"

	Write-Host "$PSScriptRoot\..\API\Users.Service\bin\Release\"

	xcopy "$PSScriptRoot\..\API\Users.Service\bin\Release\" "$OutputDirectory\bin\" /y /I /s

	Copy-Item "$PSScriptRoot\DeployUserService.ps1" "$OutputDirectory\"
}

CleanStagingDirectory
StageApplicationFiles
