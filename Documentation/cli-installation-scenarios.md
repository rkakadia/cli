Obtaining .NET CLI 
==================

# Contents
* [Overview](#overview)
* [General principles](#general-principles)
* [Channels](#channels)
* [Dependencies](#dependencies)
* [Upgrades](#upgrades)
* [Layout on disk](#layout-on-disk)
* [Acquisition vectors](#acquisition-vectors)
  * [Getting Started page](#getting-started-page)
  * [Repo landing page](#repo-landing-page)
* [Acquisition modes](#acquisition-modes)
  * [Native installers](#native-installers)
  * [Installation script](#installation-script)
    * [Windows one-liner](#windows-command)
    * [OSX/Linux one-liner](#osxlinux-shell-command)
  * [Complete manual installation](#complete-manual-installation)
  * [Docker](#docker)
  * [NuGet Packages](#nuget-packages)
* [Acquiring through other products](#acquiring-through-other-products)
  * [Visual Studio](#visual-studio)
  * [VS Code](#vs-code)
  

# Overview
This document/spec outlines the CLI install experience. This covers the technologies being used for install, the principles that are driving the installation experience, the ways users are coming to the installs and what each of the installs contains, in terms of stability and similar. 

# General principles

- All installers are signed properly 
- Upgrades using the native installers Just Work(tm)
- All user facing materials point to the getting started page
- The user needs extra effort to install the "bleeding edge" bits
- Only HTTPS links are allowed in any online property 

# Channels
Channels represent a way for users who are getting the bits to reason about the stability and quality of the bits they are getting. Each channel has, potentially, a different 

The table below outlines the channels:

| Property         	| Description                                                                                                                                                                                                                                                       	|
|------------------	|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------	|
| Dev              	| Unstable bits that are "bleeding edge". Users are nto expected to us this channel often, however it is there for those situations when someone needs a feature or maybe a new bug fix urgently that hasn't been stabilizied yet. Also, used for internal testing. 	|
| Beta/Pre-release 	| Pre-release stable builds with known issues and/or known feature gaps. We are OK with users installing these for preview purposes.                                                                                                                                	|
| Release          	| Actual releases. Most users are encouraged to install these.                                                                                                                                                                                                      	|
Channels also impact the NuGet packages. Based on the version of the package specified, the user may end up getting different channels. This is described in more details in [Nuget Packages section](#nuget-packages).  

# Dependencies
.NET Core CLI is built on top of CoreFX and CoreCLR and as such its' dependencies set is defined by the platform that those two combine. Some of the install options provide an automatic way to install these dependencies, while other do not. However, it is important to note that the CLI bundle will **not carry those dependencies with it** in any case. This means that for those install options that do not support automatic 

A list of dependencies can be found on [](). 

# Upgrades
**TODO**

# Layout on disk
**TODO**

# Acquisition vectors
By "vectors" we are refering here to the way any given user can obtain the installers. In the CLI, there are two main planned vectors:

1. [Getting Started Page](https://aka.ms/dotnetcoregs)
2. [Repo landing page](https://github.com/dotnet/cli/blob/rel/1.0.0/README.md)

There are two more planned vectors:

1. Visual Studio
2. VS Code 

## Getting Started page
The page can be found on https://aka.ms/dotnetcoregs. This is the main curated first-run experience for the dotnet CLI. The intent of the page is to help users "kick the tires" quickly and become familiar with what the platform offers. This should be the most stable and curated experience we can offer. Getting started page can never point to unstable builds.

The below table shows the pertinent information for installs on the "Getting started" page. 

The Getting Started page should only point users to curated install experiences that can contain only stable or LKG bits. 
 
| Property              	| Description                                                  	|
|-----------------------	|--------------------------------------------------------------	|
| Installer type          	| Native installers; scripts for curl installs              	|
| Source branch         	| rel/1.0.0                                                    	|
| Build to install         	| Last Known Good (LKG) **or** latest green build of rel/1.0.0 	|
| Debian feed           	| Development                                                  	|
| Local install scripts 	| Latest from rel/1.0.0                                        	|

## Repo landing page
The repo landing page can be found on: https://github.com/dotnet/cli/readme.md. Download links on the landing page should be decreased in importance. First thing for "you want to get started" section should link to the getting started page on the marketing site. The Repo Landing Page should be used primarily by contributors to the CLI. There should be a separate page that has instructions on how to install both the latest stable as well as latest development with proper warnings around it. The separate page is to really avoid the situation from people accidentally installing unstable bits (since search engines can drop them in the repo first). 

The below table shows the pertinent information for installs on the repo landing page.  

| Property             	| Description                                      	|
|----------------------	|--------------------------------------------------	|
| Installation targets 	| Native installers; scripts for curl installs   	|
| Source branch        	| rel/1.0.0                                        	|
| Linked builds        	| Latest green build of rel/1.0.0                  	|

# Acquisition modes
There are multiple acquisition modes that the CLI will have:

1. Native installers
2. Local installs manually
3. NuGet packages (for use in other people's commands/code)
4. Install scripts
5. Docker

All of these are explained below.

## Native installers
These installation experiences are the primary way new users are getting the bits. They are aimed towards users kicking the tires. They are found using (not not limited to) the following means:

* Web searches
* Marketing materials
* Presentations
* Documentation

The primary way to get information about this mode of installation is the marketing website. The native installers are considered to be stable by default; this does not imply lack of bugs, but it does imply predictable behavior. They are generated from the stable branches and are never used to get the "bleeding edge" bits. 

The native installers are:

| Platform            	| Installer        	| Description                                                                                                                                                                                                                                        	|
|---------------------	|------------------	|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------	|
| Windows             	| Bundle installer 	| Windows native installation technology is what we use to install stuff.                                                                                                                                                                            	|
| Ubuntu 14.04/Debian 	| apt-get feed     	| We provide the Debian package on a hosted feed. This means that the users need to add the feed, add keys and then use apt-get to install the CLI tools.                                                                                            	|
| OS X                	| PKG              	| PKGs are files that installer(1) on OS X uses to install software. One big drawback is that we cannot bundle depencencies for CoreCLR and CoreFX within this installation method, unless those dependencies are available in binary form already.  	|
| OS X                	| Homebrew         	| Though not really "native" as in "available OOB on the system", homebrew has risen as a *de facto* package manager for OS X.                                                                                                                       	|
| CentOS/RH           	| RPM              	| Though it currently doesn't exist, we should look into enabling RPMs for CentOS/RH-based distributions. This would bring it to par with Debian ones.                                                                                               	|


## Installation script via code snippet
This approach is a shell one-liner that downloads an installation script and runs it. The installation script will download the latest zip/tarball (depending on the script used) and will unpack it to a given location. After that, the script will print out what needs to be set for the entire CLI to work (env variables, $PATH modification etc.).

This install covers the following main scenario: 

* Local installation on a dev machine
* Acquiring tools on a CI build

  
The features the script needs to support/have are:
* Support for dev and stable channel
* Support for specifying the version
* Support for specfying the installation location


### Local installation
The local installation puts the bits in any folder on disk that the user specifies when invoking the script. The layout is pretty much the same as with native installers.

The local install can "grow up" into a global one simply by pointing the system PATH to the relevant bin folder within the layout on disk. 

Acquiring the tools on the CI server would be done in the same way as on a user machine with a local install: the install script would be invoked with a given set of options and then further build scripts would actually use the tools installed in this way to do their thing (build, restore, pack/publish etc.)

The guidance is, of course, to always use the beta channel for the script, and this is what the script will have as the default option. 

### Installation script features
The following arguments are needed for the installation script:

>--channel / -Channel
>Which channel (i.e. "dev", "beta", "release") to install from; defaults to beta if not present. 

>--version / -Version
>Which version of CLI to install; you need to specify the version as 3-part version (i.e. 1.0.0-13232323); defaults to latest if not present. 

>--prefix / -InstallDir
>Path to where to install the CLI bundle; defaults to /.dotnet if not present.

**TODO: how do you call the script**

### Windows command example 

```
@powershell -NoProfile -ExecutionPolicy unrestricted -Command "&{iex ((new-object net.webclient).DownloadString('https://raw.githubusercontent.com/dotnet/cli/rel/1.0.0/scripts/obtain/install.ps1'))}"
```

### OSX/Linux shell command

```
curl -sSL https://raw.githubusercontent.com/dotnet/cli/rel/1.0.0/scripts/obtain/install.sh | bash /dev/stdin [args] 
```

## Complete manual installation
Same bits as the previous install way, just that instead of the script, the user downloads the zip/tarball manually and then points the path and whatnot. 

This install covers the following scenarios: 

* Local installation 

The scenarios are completely the same as with the install script.  

### Installation example for manual installation

This example is using OS X. Windows and Linux are similar, the commands may be different but the flow is the same. 

1. Download the tarball from a stable location

```console
curl -so cli_tarball.tar.gz https://url.org/to/cli/tarball
```

2. Extract into the needed location

```console
tar -xf cli_tarball.tar.gz
```

3. Run the tools
```console
./bin/dotnet build
```

4. At this point, the local installation is complete. The tools can be ran from the directory where they are extracted. 

5. Point the system-wide path to extracted package

```console
export PATH=$PATH:/path/to/dotnet:/path/to/dotnet/bin
```

6. At this point, the global install is done. You can access that version of CLI tools from any place in the shell. 

## Docker 
[Docker](https://docs.docker.com/) has become a pretty good way to use developer tools, from trying them out in an interactive image use to using it for deployment. We have Docker images on DockerHub already. 

Docker images should always be updated as we make new releases. We should have Docker images of stable releases, built from the rel/* branches. 

## NuGet packages
**TODO: add stuff around how to version properly to not get into wrong channel by accident.**

# Acquiring through other products

## Visual Studio
Visual Studio will not be shipping CLI in-box. However, it will use CLI when installed. The install will be tied into other installs like WET and similar. Also, we are working towards VS toasting users when need to install the tools. 

Anything that goes into the higher-level tools should always use a stable build of CLI coming frol rel/* branches as required. 

Any toast should point users to the Getting Started page to acquire the installers, or, if that is deemed too heavy-handed, it should point people to the last stable release. The URL that is baked in VS should be an aka.ms URL because it needs to be stable on that end. The other, pointing end, should also be a stable URL/location. 

## VS Code
VS Code will work in similar way as above. The only thing that can toast/notify the user will be the .NET Extension (known as "built in OmniSharp"). The notification should also point people to the Getting Started page. The reason is simple: it is the one page where users can pick and choose their installation experience. This is different than VS, since VS Code is x-plat by default.



