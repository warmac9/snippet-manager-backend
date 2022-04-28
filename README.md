# Snippet Manager

Snippet Manager provides an easy method to copy short messages (snippets) to the clipboard. Snippet Manager is suitable to use if you have to write or copy the same text repeatedly.

![application-gif](https://user-images.githubusercontent.com/19654413/165815981-f77d5d62-ef69-4138-8809-c5c86d7870cf.gif)

1. Press <kbd>⊞/⌘ (Super key)</kbd> + <kbd>N</kbd> to open snippet manager
2. Search snippet by title
3. Press <kbd>Enter</kbd> to copy first snippet to clipboard / click on snippet
4. Enjoy 

## Tech Details
Snippet Manager consists of two applications:
- [Snippet Manager Desktop Client](https://github.com/warmac9/snippet-manager-desktop-client)
    - Desktop client synchronizing data with [Snippet Manager Backend](https://github.com/warmac9/snippet-manager-backend) 
    - Made with Electron, uses Vue (Composition), Tailwind CSS
- [Snippet Manager Backend](https://github.com/warmac9/snippet-manager-backend)
  - Stores snippets across multiple devices, authorize and authenticate users
  - Made with ASP.NET Core, uses Entity Framework, Identity Server

### Backend API Details

#### Server url
Development: https://localhost:7137/api

#### Account

> **POST /account/signup** - register user

Request body:
```
{
    email: ...,
    password: ...,
    confirmpassword: ...'
}
```

> **POST /account/login** - authenticate user

Request body:
```
{
    email: ...,
    password: ...
}
```

Returns Bearer token

#### Snippet

> **GET /snippet** - list all snippets

Response:
```
[
  {
    id: ...,
    title: ...,
    content: ...
  },
... ]
```

> **POST /snippet** - upload snippet

Request body:
```
{
  title: ...,
  content: ...
}
```

> **DELETE /snippet/{key}** - delete snippet identified by {key}

> **PUT /snippet/{key}** - replaces snippet identified by {key}

Request body:
```
{
  title: ...,
  content: ...
}
```

## Install Details

### Snippet Manager Desktop Backend

System requirements:
- Install .NET 6.0
- Install Microsoft SQL Server

Download and set up:

```
git clone https://github.com/warmac9/snippet-manager-backend.git
cd snippet-manager-backend
dotnet ef database update
dotnet run
```

### Snippet Manager Desktop Client

System requirements:
- Install node.js 16+

Download and set up:

```
git clone https://github.com/warmac9/snippet-manager-desktop-client.git
cd snippet-manager-desktop-client
npm install
yarn electron:dev
```
