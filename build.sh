#!/bin/bash

echo "Building Asaas SDK..."

# Restore dependencies
dotnet restore

if [ $? -ne 0 ]; then
    echo "Failed to restore dependencies"
    exit 1
fi

# Build the solution
dotnet build --configuration Release

if [ $? -ne 0 ]; then
    echo "Build failed"
    exit 1
fi

echo "Build successful!"
echo ""

# Ask if user wants to run the example
read -p "Do you want to run the example application? (Y/N): " response
if [ "$response" = "Y" ] || [ "$response" = "y" ]; then
    echo ""
    echo "Running example application..."
    echo "Note: Make sure to set your ASAAS_API_KEY environment variable or edit the Program.cs file"
    echo ""
    
    dotnet run --project examples/Asaas.Sdk.Examples/Asaas.Sdk.Examples.csproj
fi
