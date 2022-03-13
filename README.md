# Evren.dev Asp.Net Core Web Api

This repo is source of code for Asp.Net Core Web Api

## Installation
After cloning or downloading the repo you should configure database connection strings point to a local SQL Server instance from `Appsettings.json` on `PublicApi` folder.

You will need to run its Entity Framework Core migrations after configure database string. (see below).

* Ensure the tool EF was already installed.

```bash
dotnet ef database update -s ../PublicApi --verbose
```

## Running

Open a command prompt in the PublicApi folder and execute the following commands:

```bash
dotnet watch run
```
## Running the sample using Docker

You can run the PublicApi sample by running these commands from the root folder (where the .sln file is located):

```
docker-compose build
docker-compose up
```

## License
[MIT](https://choosealicense.com/licenses/mit/)