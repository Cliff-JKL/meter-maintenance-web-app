# Meter Maintenance

## Description

This is a simple meter maintenance service web app.

This app is built with ASP.NET Core and MS SQL Express Database as Docker container.

## Database settings

- Dockerize a new sql express database with the following command:

```bash
$ docker run -d --name sql_server -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=someThingComplicated1234' -p 1433:1433 mcr.microsoft.com/mssql/server:2019-latest
```
