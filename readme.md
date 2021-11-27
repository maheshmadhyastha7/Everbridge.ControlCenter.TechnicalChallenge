# Everbridge Control Center Technical Challenge Project

This project purpose is to provide a base launching point for candidates to start their challenge projects. This project helps by providing a rough standalone environment to begin their work spending more time on the project than the environment.

What this project is not is a recommended boilerplate. It represents a rough prototype grade environment intended only to provide a simple starting point for candidates intending to take the challenge.


## The Challenge

We would like you up to spend 2 hours (ignore dev environment setup time) on one of the below challenges and submit the source code of this functional application for review (as zip or github repo). I will highlight the objective of the challenge is not necessarily to complete all the given aspects of the challenge but to develop the starting project and iterate within the given timespan.

Please include with your submission the following:
* Which challenge you chose and why.
* Instructions providing simple steps to run the application with all dependent scripts, preferably using the docker-compose already included.
* Summary of the overall design of the application. Please include what features you added and any notes you think is noteworthy about any design decisions you made within the application.
* If you had access to the customer when you were developing this application, what questions would you have asked in relation to your design?
* If you were given another 2 hours, what would you have done next and why


### Challenge 1 - WPF Client

Build a small C# .Net WPF Client/Server application providing a simple door management system for a facility. 

Please consider the following use cases:
* When I connect my client to the server – 
  * Then I would like a list of all the doors at the facility.
* When reviewing a door within the client – 
  * Then I would expect each door to have a customisable label to provide user friendly reference to an individual door 
  * And I would like to know whether door is Open or Closed
  * And I would like to know whether door is Locked or Unlocked
* When configuring doors within the facility – 
  * Then I would like to be able to Add a new door
  * And be able to remove an already existing door
* When controlling doors within the facility – 
  * Then I would expect to be able to Open a Closed Door
  * And be able to Close an Open door
  * And be able to Lock a Closed Door
  * And be able to Unlock a Locked Door

For this application we would expect to support multiple concurrent clients monitoring the system and that changes to door statuses or configuration to be promptly reflected on all clients.


### Challenge 2 - React Web Client

Build a small Typescript React Web Client and C# .Net Server application providing a simple door management system for a facility. 

See [Challenge 1](#Challenge 1 - WPF Client) for use cases.


### Challenge 3 - Do something inspiring

We given a canvas to draw on, show us something interesting. The only condition is that it must have some form of async notificaiton to the clients based on actions from another client and that you do some meaningful demostration of using C#.

To give some inspiration here, our product is a platform for physical security management. We deal with real-time events, live and historic video content, dynamic geospatial mapping and user and event driven workflow management.

Topics you may have experience in or interested to show your experiments in:
* Video Streaming
* Mapping
* Analytics
* 3D display
* Workflows


## The Project

Visual Studio solution containing a library, server and a docker-compose. The configuration of the project is based on being self-contained docker environment for development and evaluation. There is no restrictions to the modifications or changes a candidate may do to the project however we expect we will only need to run the docker-compose to demo it.


### Everbridge.ControlCenter.TechnicalChallenge

This C# Core 5.0 ASP.Net server supports a single DoorController based on */api/door* which uses the *DoorDatabase* library to access the MS SQL database configured in the docker environment.

The project also hosts the React WEb client within the */ClientApp* directory. This is a typescript template application built using [Create-React-App](https://create-react-app.dev/docs/adding-typescript/). This is just a stub application to be built upon by candidates that is hosted by the server. 

The server is hosted on port **12443** and only supports HTTPS. All non-static or controller requests will be directed to the React SPA. If the application is run with Environment Variables *ASPNETCORE_ENVIRONMENT* = *Development* and *USE_DEVELOPMENT_SERVER* = *true* then the server will use the React development server to host the web client allowing hot-reload editing of the web client.

The configured dockerfile takes a build argument to switch the final image based on the build-configuration of the project. The launch settings has both setups configured giving 'Docker (Debug)' to be used with the Debug build configuration and supports hot-reloading of the web client. 'Docker (Release)' is intended to be used with the Release build configuration and will used the optimised web client build.


### DoorDatabase

This C# project provides a simple base Entity Framework database context for storing Door records.


### Docker Compose

Docker compose providing a MS SQL 2019 server on host *sqlserver* and the ASP.Net Everbridge.ControlCenter.TechnicalChallenge service. The run configuration in visual studio provides the development build of the Everbridge.ControlCenter.TechnicalChallenge service with hosting the react application via the development server.
