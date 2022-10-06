# Documentation

## Configuration Properties

The following properties can be found in SystemGlobalProperties table in ARM_CORE.

Property Name and current value:

UploadTimeInterval	1000

This property is used to dictate how often the service will check the folder (It is in milliseconds so this is one second currently)

UploadQueue	C:\ARM2022\Data\Queue\ (Watched folder)

UploadCompletePath	C:\ARM2022\Data\Uploaded (where the file is moved to after upload)

UploadLogFile	C:\ARM2022\Data\Logs\upload_DDMMYY.log

(This file will contain any exceptions if the transfer process fails.)

ReUpload	C:\ARM2022\Data\Queue\ReUpload\

## Data Transfer

CsvHelper (version 27) is used to convert the file Contents to a DataTable (https://learn.microsoft.com/en-us/dotnet/api/system.data.datatable?view=netframework-4.7.2) in ArmRepository.AddBulkData

This is an open source package located here https://joshclose.github.io/CsvHelper/examples/csvdatareader/

System.Data.SqlClient.SqlBulkCopy (https://learn.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlbulkcopy?view=netframework-4.7.2) is used to copy from one data table to another.

On line 51 of ArmRepository.cs it is set up to use BatchSize of 500 million, this means it will try to transfer all records at once up to this amount.  Unfortunately this setting is hardcoded.

For example:

```cs
using (SqlBulkCopy bulk = new SqlBulkCopy(_connectionDB.con) { DestinationTableName = "[" + temTableNamePrefix1 + tableName + "]", BatchSize = 500000000, BulkCopyTimeout = 0 })
```

Recommend making this 4000 instead, to see if this improves performance for large files.

As per the suggestions here https://sqlbulkcopy-tutorial.net/batchsize


This Service uses .NET Framework, the software used for packaging the installation files, is no longer included in the latest versions of Visual Studio.

## Mapper Configuration

The MVC part of this uses MapperConfiguration table in ARM_CORE to set up mapping configurations, Including the INSERT INTO Statement to move from the temporary table into the actual table.


## Installing Locally

As per the following article:

https://learn.microsoft.com/en-us/dotnet/framework/windows-services/how-to-install-and-uninstall-services

I have been able to get, the revised Service, installed on my machine by using PowerShell scripts as follows:

New-Service -Name "CVUploadService" -BinaryPathName "C:\Users\MPhil\source\repos\CVUploadService\CVUpload\CVUploadService\bin\Release\CVUploadService.exe"

Remove-Service -Name "CVUploadService" 

Also I had to give the following login permissions to open the database ARM_CORE

NT AUTHORITY\SYSTEM

06/10/2022 18:29:37








