# RestoManage

This is web API solution for Restaurants, it is implemented under Dot Net Core open-source technology, also used JSON flat file to store data 

## Requirment

1. This API Solution implemented in Dot Net Core 3.1
2. To run this, it requires Visual Studio 2019 or VS Code with Dot Net Core 3.1 SDK installed

## NuGet Dependency

1. JsonFlatFileDataStore (v2.2.2) (https://www.nuget.org/packages/JsonFlatFileDataStore/2.2.2?_src=template)

## Build & Run

1. Open RestoManage.sln file in Visual Studio 2019
2. Hit Run button to start project

## Test

1. Using Postman we can test this solution.
1. 'RestoManage.postman_collection.json' open this file in Postman to test the solution.

## Security

1. Open API, No user model integrated
2. Anyone can place restaurant in any geolocation (Region wise validation not applied)
3. It is public rating because there is no User login


## Known Detail

1. Response Model have not implemented (Success Model, Fault Model), but It is implementable
2. Exception Handling taken care by parent level, I suppose to write different wrapper to handle generic exception.
3. 