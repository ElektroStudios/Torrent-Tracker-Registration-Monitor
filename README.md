<!-- Common Project Tags:
desktop-app 
desktop-application 
dotnet 
dotnet-core 
netcore 
tool 
tools 
vbnet 
visualstudio 
windows 
windows-app 
windows-application 
windows-applications 
windows-forms 
winforms 
plugin 
plugins
forum 
tracker 
trackers 
torrenting 
torrent 
torrents 
private 
internet
www
register
registration
automation
checker
 -->

# <p align="center">Torrent Tracker Registration Monitor
# <p align="center">(TTRM)

> [!IMPORTANT]
> ### The application and the GitHub repository has been renamed from “Multi-Tracker and Forums Registration Availability Tool” (MTAFRAT) to “Torrent Tracker Registration Monitor” (TTRM), as the previous name was somewhat too long and confusing. Plugin developers now must use "TTRM" namespace instead of "MTAFRAT".

------------------

## 👋 Introduction

Ever wanted to join your favorite private torrent tracker or forum but got tired of waiting for open registration? Well, this program will not do magic — but it will help you take the edge off that frustrating wait...

**TTRM** is a tool designed to quickly and automatically check account registration availability status across multiple torrent trackers and forums using a modular, plugin-based system.

## 👌 Features

📦 Plugin-Based Architecture

The program works through a plugin system.

Each plugin is composed of three main elements:

-  JSON file
	Contains the plugin's basic metadata:
	name, description, login or registration URL, etc.

- PNG, JPG, BMP, or ICO image file
	This is usually a banner or favicon
	that visually represents the plugin.
	A square image is recommended,
	preferably 48x48 pixels.

- VB file
	Contains the VB.NET source code that implements the 
	plugin's logic and behavior within the program.

📁 Plugins Location

All plugins are located in the `plugins` folder. If you don't find one of them useful and want to remove it from the program, you can delete the folder corresponding to the plugin name, or move it to another directory outside of its original location.

🔧 Built-in plugins

- \# Site Auto Login
- 3ChangTrai
- 3D Torrents
- 4th Dimension
- AlphaRatio
- Animebytes
- AnimeTorrents
- AvistaZ
- BeyondHD
- BitPorn
- Blutopia
- Cinemageddon
- CinemaZ
- DarkPeers
- Digital Core
- Elite-HD
- eMuwarez
- ExoticaZ
- Fappaizuri
- FearNoPeer
- Femdomcult
- GazelleGames
- GreatPosterWall
- HD Dolby
- HD-Forever
- HD-Olimpo
- HD-Space
- HD-Zero
- HDHome
- HDTime
- Hebits
- iPtorrents
- Kleverig
- KrazyZone
- Lat-Team
- Locadora
- LST
- More Than TV
- NicePT
- Old Toons World
- OnlyEncodes
- PassThePopcorn
- PrivateHD
- Punto Torrent
- Rastastugan
- ReelFlix
- SceneRush
- SeedFile
- SportsCult
- Superbits
- TorrentCFF
- Torrent Day
- Torrenteros
- TorrentLand
- TorrentLeech
- Upscale Vault
- xBytesv2

## 🖼️ Screenshots

![screenshot](/Images/Screenshot_01.png)

![screenshot](/Images/Screenshot_02.png)

![screenshot](/Images/Screenshot_03.png)

## 📝 Requirements

- Microsoft Windows OS. (It may work in Linux with Wine)
- [.NET Desktop Runtime 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) (64-Bit)

## 🤖 Getting Started (common users)

Download the latest build by clicking [here](https://github.com/ElektroStudios/Torrent-Tracker-Registration-Monitor/releases/latest).

And simply run the program to get started. It features an intuitive, user-friendly interface.

💡 Also remember to read the included `README.txt` file located in the `docs` folder for more details and also tips for using the application. 
   Or click [here](https://raw.githubusercontent.com/ElektroStudios/Torrent-Tracker-Registration-Monitor/main/Source/TTRM/docs/english/README.txt) to read the document now.

## 🤖 Getting Started (plugin developers)

Read the included `README (for programmers).md` file located in the `docs` folder for more details. 
Or click [here](https://github.com/ElektroStudios/Torrent-Tracker-Registration-Monitor/tree/main/Source/TTRM/docs/english/README%20%28for%20programmers%29.md) to read the document now.

## 🔄 Change Log

Explore the complete list of changes, bug fixes, and improvements across different releases by clicking [here](/Docs/CHANGELOG.md).

## 🏆 Credits

This work relies on the following resources: 

 - [Microsoft .net](https://dotnet.microsoft.com/en-us/download/dotnet)
 - [DarkModeUI](https://www.nuget.org/packages/DarkModeUI)
 - [Jot](https://github.com/anakic/Jot)
 - [Selenium.WebDriver](https://github.com/SeleniumHQ/selenium/)

## ⚠️ Disclaimer:

This Work (the repository and the content provided in) is provided "as is", without warranty of any kind, express or implied, including but not limited to the warranties of merchantability, fitness for a particular purpose and noninfringement. In no event shall the authors or copyright holders be liable for any claim, damages or other liability, whether in an action of contract, tort or otherwise, arising from, out of or in connection with the Work or the use or other dealings in the Work.

This Work has no affiliation, approval or endorsement by the author(s) of the third-party libraries used by this Work.

## 💪 Contributing

Your contribution is highly appreciated!. If you have any ideas, suggestions, or encounter issues, feel free to open an issue by clicking [here](https://github.com/ElektroStudios/Torrent-Tracker-Registration-Monitor/issues/new/choose). 

Your input helps make this Work better for everyone. Thank you for your support! 🚀

## 💰 Beyond Contribution 

This work is distributed for educational purposes and without any profit motive. However, if you find value in my efforts and wish to support and motivate my ongoing work, you may consider contributing financially through the following options:

<br></br>
<p align="center"><img src="/Images/github_circle.png" height=100></p>
<p align="center">__________________</p>
<h3 align="center">Becoming my sponsor on Github:</h3>
<p align="center">You can show me your support by clicking <a href="https://github.com/sponsors/ElektroStudios/">here</a>, <br align="center">contributing any amount you prefer, and unlocking rewards!</br></p>
<br></br>

<p align="center"><img src="/Images/paypal_circle.png" height=100></p>
<p align="center">__________________</p>
<h3 align="center">Making a Paypal Donation:</h3>
<p align="center">You can donate to me any amount you like via Paypal by clicking <a href="https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=E4RQEV6YF5NZY">here</a>.</p>
<br></br>

<p align="center"><img src="/Images/envato_circle.png" height=100></p>
<p align="center">__________________</p>
<h3 align="center">Purchasing software of mine at Envato's Codecanyon marketplace:</h3>
<p align="center">If you are a .NET developer, you may want to explore '<b>DevCase Class Library for .NET</b>', <br align="center">a huge set of APIs that I have on sale. Check out the product by clicking <a href="https://codecanyon.net/item/elektrokit-class-library-for-net/19260282">here</a></br><br align="center"><i>It also contains all piece of reusable code that you can find across the source code of my open source works.</i></p>
<br></br>

<h2 align="center"><u>Your support means the world to me! Thank you for considering it!</u> 👍</h2>
