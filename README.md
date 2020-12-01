![reptihurt](https://user-images.githubusercontent.com/44526067/97150889-14a37f00-176f-11eb-9569-4789763077dc.jpg)
# Hurtownia-Repti-Good

## General info
Web application for placing orders in pet wholesale www.reptihurt.pl. Project was created by using MVC pattern. Customers have accounts, after logging can search products and make order. 
The application sets the appropriate discounts, has a products warehouse and orders history in the MSSQL database , send confirmation mails with order in pdf attachments and connecting with DPD web services via SOAP. 
I made the project myself. Application have two way for updating current products stock from SubiektGT, first using API https://github.com/mojeq/SubiektGT-API and second way through upload JSON file who was generated with https://github.com/mojeq/GeneratorProductsStock-SubiektGT

## Technologies
Project is created with:
* ASP.NET CORE MVC
* Razor Page
* MSSQL 2017
* Entity Framework Core
* AutoMapper
* EFCoreSecondLevelCacheInterceptor
* SOAP
* Identity
* MailKit
* iTextSharp
* Hangfire
* Logging Serilog

## Status
Project in commercial using.

## Plans
Maintained and develop new features.

## Contact
Created by Piotr Moj - feel free to contact me if needed.
