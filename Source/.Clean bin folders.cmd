@ECHO OFF

:: Debug folder
ECHO:[.] Cleaning Debug folder...
(
	RD /Q /S ".\TTRM\bin\Debug\net8.0-windows\cs"
	RD /Q /S ".\TTRM\bin\Debug\net8.0-windows\de"
	RD /Q /S ".\TTRM\bin\Debug\net8.0-windows\fr"
	RD /Q /S ".\TTRM\bin\Debug\net8.0-windows\it"
	RD /Q /S ".\TTRM\bin\Debug\net8.0-windows\ja"
	RD /Q /S ".\TTRM\bin\Debug\net8.0-windows\ko"
	RD /Q /S ".\TTRM\bin\Debug\net8.0-windows\pl"
	RD /Q /S ".\TTRM\bin\Debug\net8.0-windows\pt-BR"
	RD /Q /S ".\TTRM\bin\Debug\net8.0-windows\ru"
	RD /Q /S ".\TTRM\bin\Debug\net8.0-windows\tr"
	RD /Q /S ".\TTRM\bin\Debug\net8.0-windows\zh-Hans"
	RD /Q /S ".\TTRM\bin\Debug\net8.0-windows\zh-Hant"
	RD /Q /S ".\TTRM\bin\Debug\net8.0-windows\runtimes\linux"
	RD /Q /S ".\TTRM\bin\Debug\net8.0-windows\runtimes\osx"
	RD /Q /S ".\TTRM\bin\Debug\net8.0-windows\cache"
)>NUL 2>&1
ECHO:[+] Debug folder has been cleaned.

:: Release folder
ECHO:[.] Cleaning Release folder...
(
	RD /Q /S ".\TTRM\bin\Release\net8.0-windows\cs"
	RD /Q /S ".\TTRM\bin\Release\net8.0-windows\de"
	RD /Q /S ".\TTRM\bin\Release\net8.0-windows\fr"
	RD /Q /S ".\TTRM\bin\Release\net8.0-windows\it"
	RD /Q /S ".\TTRM\bin\Release\net8.0-windows\ja"
	RD /Q /S ".\TTRM\bin\Release\net8.0-windows\ko"
	RD /Q /S ".\TTRM\bin\Release\net8.0-windows\pl"
	RD /Q /S ".\TTRM\bin\Release\net8.0-windows\pt-BR"
	RD /Q /S ".\TTRM\bin\Release\net8.0-windows\ru"
	RD /Q /S ".\TTRM\bin\Release\net8.0-windows\tr"
	RD /Q /S ".\TTRM\bin\Release\net8.0-windows\zh-Hans"
	RD /Q /S ".\TTRM\bin\Release\net8.0-windows\zh-Hant"
	RD /Q /S ".\TTRM\bin\Release\net8.0-windows\runtimes\linux"
	RD /Q /S ".\TTRM\bin\Release\net8.0-windows\runtimes\osx"
	RD /Q /S ".\TTRM\bin\Release\net8.0-windows\cache"

	DEL /Q /S ".\TTRM\bin\Release\net8.0-windows\TTRM.pdb"
)>NUL 2>&1
ECHO:[+] Release folder has been cleaned.

ECHO+
PAUSE
EXIT