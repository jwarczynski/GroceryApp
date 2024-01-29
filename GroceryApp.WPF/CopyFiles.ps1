$sourcePath = "GroceryApp.SQLDao\bin\Debug\net8.0"
$destinationPath = "GroceryApp.WPF\bin\Debug\net8.0-windows"
$destinationPath2 = "GroceryApp.MVC"

Write-Host "Copying GroceryApp.SQLDao.dll..."
Copy-Item -Path "$sourcePath\GroceryApp.SQLDao.dll" -Destination $destinationPath -Force
Copy-Item -Path "$sourcePath\GroceryApp.SQLDao.dll" -Destination $destinationPath2 -Force

Write-Host "Copy complete."
