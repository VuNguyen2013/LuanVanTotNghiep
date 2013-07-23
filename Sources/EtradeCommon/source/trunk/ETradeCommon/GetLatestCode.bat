@ECHO off
ECHO "----------------Get latest code from svn"
"C:\Program Files\TortoiseSVN\bin\TortoiseProc.exe" /command:update /path:"." /closeonend:1
ECHO "----------------End get latest code from svn"
PAUSE
ECHO "----------------Build project"
"C:\Windows\Microsoft.NET\Framework\v3.5\MSBuild.exe" ".\ETradeCommon\ETradeCommon.csproj" /t:Build
ECHO "----------------End build project"
PAUSE
ECHO "Copy dll file to project folder"
COPY ".\ETradeCommon\bin\Debug\ETradeCommon.dll" "D:\OTS\svnProjects\Etrade\AMServices\source\trunk\AccountManager\References\ETradeCommon.dll"
COPY ".\ETradeCommon\bin\Debug\ETradeCommon.dll" "D:\OTS\svnProjects\Etrade\EtradeServices\source\trunk\ETradeServices\References\ETradeCommon.dll"
COPY ".\ETradeCommon\bin\Debug\ETradeCommon.dll" "D:\OTS\svnProjects\Etrade\FinanceServices\source\trunk\ETradeFinance\References\ETradeCommon.dll"
PAUSE
