![.NET Core](https://github.com/jamesloyd/gitcommander/workflows/.NET/badge.svg?branch=mainline)
[![License](https://img.shields.io/github/license/jamesloyd/gitcommander.svg)](LICENSE)
# GitCommander - A terminal user interface for git

GitCommander is right now a tool I use to open up PR's from repos and auto-check them out, so I can review the pull requests. I decided to open source it, cause why not.

## Features
* List PR's in a Repo and check them out
* More features are planned, will be updating with a roadmap in the near future

## Current Dependencies
* Git Commander depends on [github cli](https://github.com/cli/cli) to handle its PR checkout functionality and also interacting with Github. There are some plans to add more support to work with other git hosting sites like bitbucket and gitlab. But, as of right now, its just github. Gotta start somewhere :)

## How to build
You will need to clone this repo and build it.

### Current Steps
* `git clone git@github.com:JamesLoyd/gitcommander.git`
* `cd gitcommander`
* `dotnet run` 
