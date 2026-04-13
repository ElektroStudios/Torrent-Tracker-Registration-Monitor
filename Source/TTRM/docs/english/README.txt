----------------------------------------------------------------------------------------------------------
🧩 Torrent Tracker Registration Monitor (TTRM)

Author: ElektroStudios
Website: https://github.com/ElektroStudios/Torrent-Tracker-Registration-Monitor
----------------------------------------------------------------------------------------------------------

TTRM is a tool designed to automatically check 
registration availability across multiple trackers and forums.

It allows you to quickly analyze registration status using a modular, 
plugin-based system that facilitates the integration of 
new sites without modifying the program core.

README FILE
_____________

📋 Requirements

	This program requires .NET Desktop Runtime 8.0 installed on your computer.

	Download the .NET Desktop Runtime 8.0 64-Bit package here:
		► https://dotnet.microsoft.com/en-us/download/dotnet/8.0

📦 Plugin-Based Architecture

	The program works through a plugin system.

	Each plugin is composed of three main elements:

		● JSON file
			Contains the plugin's basic metadata:
			name, description, login or registration URL, etc.

		● PNG, JPG, BMP, or ICO image file
			This is usually a banner or favicon
			that visually represents the plugin.
			A square image is recommended,
			preferably 48x48 pixels.

		● VB file
			Contains the VB.NET source code that implements the 
			plugin's logic and behavior within the program.

📁 Plugins Location

	All plugins are located in the "plugins" folder.
	If you don't find one of them useful and want to remove it from the program,
	you can delete the folder corresponding to the plugin name,
	or move it to another directory outside of its original location.

🔧 Built-in plugins

	Index  Name             Url
	------ ------           ---
	01    # Site Auto Login
	02    3ChangTrai        https://3changtrai.com/login.php
	03    3D Torrents       http://www.3dtorrents.org/index.php?page=login
	04    4th Dimension     https://4thd.xyz/login.php
	05    AlphaRatio        https://alpharatio.cc/login.php
	06    Animebytes        https://animebytes.tv/user/login
	07    AnimeTorrents     https://animetorrents.me/login.php
	08    AvistaZ           https://avistaz.to/auth/login
	09    BeyondHD          https://beyond-hd.me/login
	10    BitPorn           https://bitporn.eu/login
	11    Blutopia          https://blutopia.cc/login
	12    Cinemageddon      https://cinemageddon.net/login.php
	13    CinemaZ           https://cinemaz.to/auth/login
	14    DarkPeers         https://darkpeers.org/login
	15    Digital Core      https://digitalcore.club/login
	16    Elite-HD          https://www.elitehd.li/ucp.php?mode=login
	17    eMuwarez          https://emuwarez.com/login
	18    ExoticaZ          https://exoticaz.to/login
	19    Fappaizuri        https://fappaizuri.me/account-login.php
	20    FearNoPeer        https://fearnopeer.com/login
	21    Femdomcult        https://femdomcult.org/login
	22    GazelleGames      https://gazellegames.net/login.php
	23    GreatPosterWall   https://greatposterwall.com/login.php
	24    HD Dolby          https://www.hddolby.com/login.php
	25    HD-Forever        https://hdf.world/login.php
	26    HD-Olimpo         https://hd-olimpo.club/login
	27    HD-Space          https://hd-space.org/index.php?page=login
	28    HD-Zero           https://www.hdzero.org/login
	29    HDHome            https://hdhome.org/login.php
	30    HDTime            https://hdtime.org/login.php
	31    Hebits            https://hebits.net/login.php
	32    iPtorrents        https://www.iptorrents.com/login.php
	33    Kleverig          https://www.kleverig.eu/login.php?do=login
	34    KrazyZone         https://krazyzone.net/account-login.php
	35    Lat-Team          https://lat-team.com/login
	36    Locadora          https://locadora.cc/login
	37    LST               https://lst.gg/login
	38    More Than TV      https://www.morethantv.me/login
	39    NicePT            https://www.nicept.net/login.php
	40    Old Toons World   https://oldtoons.world/login
	41    OnlyEncodes       https://onlyencodes.cc/login
	42    ParabellumHD      https://parabellumhd.cx/login.php
	43    PassThePopcorn    https://passthepopcorn.me/login.php
	44    PrivateHD         https://privatehd.to/auth/login
	45    Punto Torrent     https://xbt.puntotorrent.com/index.php?page=login
	46    Rastastugan       https://rastastugan.org/login
	47    ReelFlix          https://reelflix.cc/login
	48    SceneRush         https://www.scene-rush.com/login.php
	49    SeedFile          https://seedfile.io/home
	50    SportsCult        https://sportscult.org/index.php?page=login
	51    Superbits         https://login.superbits.org/login
	52    Tekno3D           https://tracker.tekno3d.com/login.php
	53    TorrentCFF        https://et8.org/login.php
	54    Torrent Day       https://www.torrentday.me/login.php
	55    Torrenteros       https://torrenteros.org/login
	56    TorrentLand       https://torrentland.li/login
	57    TorrentLeech      https://www.torrentleech.org/
	58    Upscale Vault     https://upscalevault.com/login
	59    xBytesv2          https://xbytesv2.li/login

💡 Tips

	1.  Some websites may keep registration open for several days, or even weeks and months. 
		However —according to collected information and user feedback— other sites only allow registration 
		for very short periods, sometimes less than 24 hours, including Spanish sites such as Torrentland.

		For this reason, if you don’t want to miss the chance to register on time, it is highly recommended 
		to configure automatic execution every hour. This way, TTRM can reliably detect very short 
		registration periods on a website.

	2.  If you enable parallel execution, plugins will run four at a time, meaning that four instances 
		of the chromedriver.exe process (and their respective Chrome.exe child processes) will open 
		simultaneously. This will significantly reduce the total time needed to run all plugins; 
		however, it may also cause unexpected issues in the application. This feature is experimental.

	3.  If you enable automatic plugin execution, it is recommended to select only those plugins 
		that genuinely interest you. Sometimes a website may be down or cause an unexpected error, 
		prolonging the total execution time excessively.

	4.  Every time you run a plugin, TTRM will automatically generate certain files on disk. 
		This is the application cache. If you wish to clear the cache, simply press the "Clear Cache" 
		button in the program options.

	5.  The first time you run a plugin that interacts with a website protected by Cloudflare 
		("AvistaZ", "CinemaZ", "ExoticaZ", etc), the Cloudflare challenge may fail; however, 
		subsequent attempts should not fail. Keep in mind that if you clear the program cache, 
		the next time you run the plugin it will be as if you are running it for the first time.

	6.  If for any reason TTRM logs strange errors related to Chrome while running a plugin, 
		try closing all running processes with the following names: "Chrome.exe" and "ChromeDriver.exe". 
		You can do this easily using the following command in the Windows CMD:
			Taskkill /F /T /IM "Chrome.exe" /IM "ChromeDriver.exe"
		Once the running processes are closed, open TTRM and try running the plugin again.
