$sourcePath = "GroceryApp.DAOMock1\bin\Debug\net8.0"
$destinationPath = "GroceryApp.WPF\bin\Debug\net8.0-windows"

Write-Host "Copying GroceryApp.DAOMock1.dll..."
Copy-Item -Path "$sourcePath\GroceryApp.DAOMock1.dll" -Destination $destinationPath -Force

Write-Host "Copy complete."
