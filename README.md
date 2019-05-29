# ProductAPI
A simple Web API with Unit tests

## Installation
This is a .net core app. To run the code it should be a simple matter of cloning the project locally and running the main solution in Visual Studio, which will automatically download Nuget Packages.

## Running the app without Visual Studio
From the ProductAPI folder, run the following commands:

- dotnet restore
- dotnet publish -c release -r win10-x64
- dotnet run

The site should run at http://localhost:5000.
