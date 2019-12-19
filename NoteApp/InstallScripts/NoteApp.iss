#define AppName = "NoteApp"
#define Version = "1.0"
#define NoteAppExeName = "NoteAppUI.exe"
#define Publisher = "Анастасия Смакотина"
#define AppURL = "http://github.com/anastasia1810"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId = {{F87318C9-FDE0-4B2F-B78F-60CE6B19DDF0}
AppName =  {#AppName}
AppVersion = {#Version}
AppPublisher = {#Publisher}
AppPublisherURL = {#AppURL}
AppSupportURL = {#AppURL}
AppUpdatesURL = {#AppURL}
DefaultDirName = {pf}\{#AppName}
DisableProgramGroupPage = yes
SetupIconFile = Release\icon.ico
OutputBaseFilename = setup NoteApp
Compression = lzma
SolidCompression = yes
AllowNoIcons = yes


[Languages] 
Name: "english" ; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: desktopicon; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
[Files]
Source:"Release\*.exe"; DestDir:"{app}"; Flags:ignoreversion
Source:"Release\*.dll"; DestDir:"{app}"; Flags:ignoreversion
Source:"Release\*.ico"; DestDir:"{app}\icon"; Flags:ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files
[Icons]
Name:"{commonprograms}\{#AppName}";Filename:"{app}\{#NoteAppExeName}";
Name:"{commondesktop}\{#AppName}";Filename:"{app}\{#NoteAppExeName}"; Tasks:desktopicon; IconFilename:"{app}\icon\icon.ico"

[Run]
Filename: "{app}\{#NoteAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(AppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent
