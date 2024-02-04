<img src="https://github.com/HjalmarStranninge/Netify/assets/146171251/9ae92942-1930-4812-8f86-5a8a5b1f60af" alt="C# Logo" width="800" height="700">

# Netify
Welcome to the Netify Api. This projects involves creating a minimal API around the Spotify open access api.

## Table of Contents
- [Features](#key-features)
- [Functionality](#functionality-highlights)
- [Using Endpoints](#using-endpoints)
- [ER Model](#er-model)
- [Insomnia](#insomnia)
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
 
## Insomnia
### GET requests
- https://localhost:0000/users - All users

![Skärmbild 2024-02-04 160723](https://github.com/HjalmarStranninge/Netify/assets/146171251/61bdf3b3-e0de-4a97-8ebb-aa21ab5de1df)

- https://localhost:0000/user/{userId}" - Get a specific user

![Skärmbild 2024-02-04 161059](https://github.com/HjalmarStranninge/Netify/assets/146171251/a08583f1-31ab-4eb1-a787-ebf1be963e2f)

- https://localhost:0000/user/{userId}/genres" - Get a specific user and genres

![Skärmbild 2024-02-04 161220](https://github.com/HjalmarStranninge/Netify/assets/146171251/46feafb9-6a77-4dc5-b754-53d21207337c)

- https://localhost:0000/user/{userId}/artists" - Get a specific user and artists

![Skärmbild 2024-02-04 161240](https://github.com/HjalmarStranninge/Netify/assets/146171251/f04e5a3d-37c1-41bc-bada-ef9c9cf875fe)

- https://localhost:0000/user/{userId}/tracks" - Get a specific user and tracks

![Skärmbild 2024-02-04 161259](https://github.com/HjalmarStranninge/Netify/assets/146171251/4c1ef84b-a309-476e-ae81-c8bda00bf090)

### Post requests

https://localhost:7105/users - Add a new user

![Skärmbild 2024-02-04 163056](https://github.com/HjalmarStranninge/Netify/assets/146171251/40648a48-ae42-4f92-8787-4ea32c7b9f38)

https://localhost:0000/user/saveartist - Add a new artist

![Skärmbild 2024-02-04 171825](https://github.com/HjalmarStranninge/Netify/assets/146171251/43265fba-d013-4eb2-b4db-9c63c1e1137e)

https://localhost:0000/user/savetrack - Add a new track

![Skärmbild 2024-02-04 173742](https://github.com/HjalmarStranninge/Netify/assets/146171251/c0aac0ca-dd1b-40ee-b340-eda0f8f235ec)


### Spotify requests

https://localhost:0000/spotifytracksearch/kanye/5 - Get track/-s from spotify. In this case /kanye/5 - kanyes first 5 tracks

![Skärmbild 2024-02-04 164023](https://github.com/HjalmarStranninge/Netify/assets/146171251/1cbc0875-6c33-4243-b034-975aeb9eb946)

https://localhost:0000/spotifyartistsearch/demilovato/5 - Get artist from spotify. In this case /demilovato/5 - get demilovato and the 5 artist related to her

![Skärmbild 2024-02-04 164430](https://github.com/HjalmarStranninge/Netify/assets/146171251/99e97f49-f02a-47b8-b9c1-06def5e225b5)

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
