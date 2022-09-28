**

## About the project

Scp Logger is a tool (written in C#) which can log to different environments. Our main focus was to create a simple Logger to track program issues and program behaviours using the Scp protocol which can copy files to another machines. Using that protocol we can export logs to another machine. 

## Levels of logging

 - Info   
 - Warn   
 - Error

Logs displayed as follows:

    "{DateTime.UtcNow}||{Environment.MachineName}||{GetExternalIp()}||{AssemblyName}||{nameof(Warning)}|| {message}"

## Important
Before using Scp Logger you need to declare the logging path.

Either you set `LogOnlyToLocalPath` value to `true`, in that case the logger uses the default Debug path for logs. Or with value `false` you can log to another machine. In that case you need to set

 

     - Hostname
     - LocalPath
     - RemotePath
     - UserName
     - Password
     - PortNumber
values for the code to work properly.

