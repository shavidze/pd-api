## pitchdeck-server

## This is Pitch Deck BackEnd applicaiton

## Installation
  
 Install .net5 sdk for your operation system(https://dotnet.microsoft.com/en-us/download/dotnet/5.0)

 Application uses postgresql, you shoud configure ConnectionString('PitchDeckConnectionString') with your own,'User Id', 'Host','Port' and 'Password'. If you don't have a postgresql, download and install it.(https://www.postgresql.org/download/)

 In the appsettings.json file , there is a variable with key 'ApiAddress'. The 'ApiAddress' is a hotname of the this application. You should provide it.

 After all configuration above, go to the 'PitchDeckBack' folder and run, 'dotnet run' from command line tool.

 To use swagger, navigate to /swagger/index.html.
