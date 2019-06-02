# ProductAPI
A simple Web API with Unit tests

## Installation
This is a .net core app. To run the code it should be a simple matter of cloning the project locally and running the main solution in Visual Studio, which will automatically download Nuget Packages.

## Running the app without Visual Studio
From the ProductAPI folder, run the following commands:

- dotnet restore
- dotnet publish -c release -r win10-x64
- dotnet run

## Get a token
Hit https://localhost:44301/token with a POST action and use the following query parameters:

  username = test
  password = test

This will return a JWT which you can use to authenticate against the products controller. 

## Send GET to /products with Bearer token

Send GET request to https://localhost:44301/products with the following header:
  Authorization = "Bearer [YOUR TOKEN]"
  
## Use ODATA to filter and sort data

Use standard ODATA commands to get the data you need, eg https://localhost:44301/products?$select=brand
