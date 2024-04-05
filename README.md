# Bygg
Bygg

API description for Bygg project Bygg

|   Bygg.sln
API for the application

|   |-- API\
|   |   |-- API.csproj
|   |   |-- API.csproj has a reference to Infrastructure.csproj

Entities and Interfaces for the application
|   |-- Core\
|   |   |-- Core.csproj
|   |   |-- Core.csproj has no references

Data Base with logic repository and Unit of Work
|   |-- Infrastructure\
|   |   |-- Infrastructure.csproj
|   |   |-- Infrastructure.csproj has a reference to Core.csproj

## API
API is the main project that will be used to interact with the application. It will be the entry point for the user to interact with the application.

## Core
Core is the project that will contain all the business logic for the application. It will be the main project that will be used to interact with the database.

## Infrastructure
Infrastructure is the project that will contain all the database logic for the application. It will be the main project that will be used to interact with the database.

![Bygg](./DotnetBygg.jpg)