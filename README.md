# Database
This is for a SQL server db. \
You can create a db by setting the MeterReadingConnectionString in appsettings.json then running the following command from the MeterReadingsApi.Storage directory:

```dotnet ef --startup-project ../MeterReadingsApi database update```

# Uploading
Uploads are via a post to /api/MeterReadings with the file for upload as a file named 'file' in the form-data
