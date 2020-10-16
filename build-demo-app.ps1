Push-Location $PSScriptRoot

function Write-Message{
    param([string]$message)
    Write-Host
    Write-Host $message
    Write-Host
}
function Confirm-PreviousCommand {
    param([string]$errorMessage)
    if ( $LASTEXITCODE -ne 0) { 
        if( $errorMessage) {
            Write-Message $errorMessage
        }    
        exit $LASTEXITCODE 
    }
}

function Confirm-Process {
    param ([System.Diagnostics.Process]$process,[string]$errorMessage)
    $process.WaitForExit()
    if($process.ExitCode -ne 0){
        Write-Host $process.ExitCode
        if( $errorMessage) {
            Write-Message $errorMessage
        }    
        exit $process.ExitCode 
    }
}

# Check prerequisites
$proc = Start-Process "dotnet" -ArgumentList "--version" -PassThru
Confirm-Process $proc "Could not find dotnet sdk, please install and run again ..."

Write-Message "Building ..."
dotnet build DemoApp\BlazorDialog.DemoApp\Server -c Release -o
Confirm-PreviousCommand

Write-Message "Publishing ..."
dotnet publish DemoApp\BlazorDialog.DemoApp\Server -c Release -o artifacts\demoapp --no-build
Confirm-PreviousCommand

Write-Message "Build completed successfully"