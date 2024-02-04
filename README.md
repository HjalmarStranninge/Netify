<img src="https://github.com/HjalmarStranninge/Netify/assets/146171251/976c8abc-6d67-4d8d-8fd8-cf6298e4ea0b" alt="C# Logo" width="600" height="400">

# Netify
Welcome to the Netify Api. This projects involves creating a minimal API around the Spotify open access api.

## Table of Contents
- [Features](#key-features)
- [Functionality](#functionality-highlights)
- [Using Endpoints](#using-endpoints)
- [ER Model](#er-model)
- [Getting Started](#getting-started)
- [Prerequisites](#prerequisites)
- [Api Reference](#api-reference)
- [Made by](#made-by)
- [Contributions](#contributions)

## Key Features
- **Store Users with Usernames**
- **Store Genres**
- **Store Artists**
- **User Interest in Genres and Artists**
- **Store Songs Linked to Genres and Artists**

## Functionality Highlights
- **Retrieve All Users**
- **Retrieve Genres Linked to a Specific Person**
- **Retrieve Artists Linked to a Specific Person**
- **Retrieve Songs Linked to a Specific Person**
- **Connect a Person to a New Genre, Artist, and Song**

## Using Endpoints
**GET** endpoints
- /users - **Get All Users**
- /user/{userId} - **Get a Specific User**
- /user/{userId}/genres - **Get a Specific User and Interests**
- /user/{userId}/artists - **Get a Specific User and Artists**
- /user/{userId}/tracks - **Get a Specific User and Tracks**

**POST** endpoints
- /user/{userId}/genre/{genreId} - **Link a Specific User to a Genre**
- /user/{userId}/artist/{artistId} - **Link a Specific User and Artist**
- /user/{userId}/tracks/{trackId} - **Link a Specific User and Track**

## ER model
![ER model Netify](https://github.com/HjalmarStranninge/Netify/assets/123236297/f284a0fd-26e8-426b-b178-a062b7e8e74c)
 
## Getting Started
1. Clone the project.
2. Open it in your development environment.
3. Run the program and explore the various features.

## Api Reference
- https://developer.spotify.com/documentation/web-api
## Prerequisites
Before you begin, ensure you have met the following requirements:

- [C#](https://docs.microsoft.com/en-us/dotnet/csharp/) - Ensure you have the .NET SDK installed.
- [Visual Studio Community](https://visualstudio.microsoft.com/) - An integrated development environment for C# and .NET development or any other you like.
- [Microsoft.EntityFrameworkCore](https://docs.microsoft.com/en-us/ef/core/) - Entity Framework Core for .NET.
- [Microsoft.EntityFrameworkCore.Tools](https://docs.microsoft.com/en-us/ef/core/cli/dotnet) - Command-line tools for Entity Framework Core.
- [Microsoft.EntityFrameworkCore.SqlServer](https://docs.microsoft.com/en-us/ef/core/providers/sql-server/?tabs=dotnet-core-cli) - SQL Server provider for Entity Framework Core.
- [Git](https://git-scm.com/) - Or any version control system of your choice.

## Built with
<div >
  <code><img width="40" src="https://user-images.githubusercontent.com/25181517/121405384-444d7300-c95d-11eb-959f-913020d3bf90.png" alt="C#" title="C#"/></code>
  <code><img width="40" src="https://user-images.githubusercontent.com/25181517/121405754-b4f48f80-c95d-11eb-8893-fc325bde617f.png" alt=".NET Core" title=".NET Core"/></code>
  <code><img width="40" src="https://github.com/marwin1991/profile-technology-icons/assets/19180175/3b371807-db7c-45b4-8720-c0cfc901680a" alt="MSSQL" title="MSSQL"/></code>
	<code><img width="40" src="https://user-images.githubusercontent.com/25181517/192108372-f71d70ac-7ae6-4c0d-8395-51d8870c2ef0.png" alt="Git" title="Git"/></code>
</div>

## Made by
&#9733; Niklas Sjödin´s [GitHub-profil](https://github.com/NiklasSjodin) <br>
&#9733; Huan Yang Ooi´s [GitHub-profil](https://github.com/bentonaw) <br>
&#9733; Charlotte Swenning Leyser´s [GitHub-profil](https://github.com/chasweley).
## Contributions
Contributions are welcome! Please follow these guidelines when contributing to the project:

- To report bugs, use the [GitHub Issues](https://github.com/HjalmarStranninge/BankNET/issues).
- For suggestions and improvements, feel free to open an issue and start a discussion.
- If you want to contribute code enhancements, fork the repository, create a new branch, and submit a pull request.

Thank you for contributing to this project!
