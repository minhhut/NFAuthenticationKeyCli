# NFAuthenticationKeyCli

## Introduction

- This is CLI Tool to generate an NFAuthenticationKey which is used to login to Netflix Add-On in Kodi. (Inspiration ![Original](https://github.com/CastagnaIT/NFAuthenticationKey))
- It should work on all platforms (Windows/Linux/MacOS)

## Prerequisite 

- .NET Core runtime installed (https://dotnet.microsoft.com/download/dotnet-core/3.1)

## Usage

```
dotnet NFAuthenticationKeyCli.dll -f <file>
```

- ```<file>``` is Netscape HTTP Cookie File, generated use an browser add-on/extentions after logged in to Netflix)
- In Firefox, try this https://github.com/lennonhill/cookies-txt
- In Chrome/Edge, ...
