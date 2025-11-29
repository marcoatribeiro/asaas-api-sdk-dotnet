# Build and run the Asaas SDK examples

Write-Host "Building Asaas SDK..." -ForegroundColor Cyan

# Restore dependencies
dotnet restore

if ($LASTEXITCODE -ne 0) {
    Write-Host "Failed to restore dependencies" -ForegroundColor Red
    exit 1
}

# Build the solution
dotnet build --configuration Release

if ($LASTEXITCODE -ne 0) {
    Write-Host "Build failed" -ForegroundColor Red
    exit 1
}

Write-Host "Build successful!" -ForegroundColor Green
Write-Host ""

# Ask if user wants to run the example
$response = Read-Host "Do you want to run the example application? (Y/N)"
if ($response -eq 'Y' -or $response -eq 'y') {
    Write-Host ""
    Write-Host "Running example application..." -ForegroundColor Cyan
    Write-Host "Note: Make sure to set your ASAAS_API_KEY environment variable or edit the Program.cs file" -ForegroundColor Yellow
    Write-Host ""
    
    dotnet run --project examples\Asaas.Sdk.Examples\Asaas.Sdk.Examples.csproj
}
