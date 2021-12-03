# Evren.dev Asp.Net Core Web Api

This repo is source of code for Asp.Net Core Web Api

## Installation
After cloning or downloading the repo you should configure database connection strings point to a local SQL Server instance from `Appsettings.json` on `Api` folder.

You will need to run its Entity Framework Core migrations after configure database string. (see below).

* Ensure the tool EF was already installed. You can find some help here.

```bash
dotnet ef database update -s ../Web --verbose
```

## Running

Open a command prompt in the Web folder and execute the following commands:

```bash
dotnet watch run
```

## License
[MIT](https://choosealicense.com/licenses/mit/)