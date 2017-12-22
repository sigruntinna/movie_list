# movie_list

## About

RESTful .NET Core Web API and a Angular app for movie lists.

## Requirements

* Download and install the [.NET Core SDK](https://www.microsoft.com/net/download)
* Download and install [NodeJS and npm](https://nodejs.org/en/download/)
* Download and install [Angular](https://angular.io/), you can install the latest version with npm like so:
```
npm install -g @angular/cli
```

## Build and Run

1. Execute: `dotnet run --project Api` from within the root of this repository
1. Open the API documentation in a browser at {URL}/swagger

## Build and Run (from a clean machine)

cd into the /Api folder

```
cd /Api
```

To build and run the server execute the following commands:

```
dotnet build; dotnet run
```

To start the Angular UI cd into the UI/online-library folder and run npm install

```
cd /UI/online-library/
npm install
```

Then run:

```
ng serve
```

You can now go to localhost:4200 in your browser to view the page
## Authors

- [Hlynur Stefánsson](https://github.com/hlynurstef/)
- [Sigrún Tinna Gissurardóttir](https://github.com/sigrung15/)
