<% 
    set wshell = CreateObject("WScript.Shell") 
    wshell.run "D:\Program Files\Nant\bin\nant.exe -buildfile: C:\BROM\my_buildfile.build", 0, TRUE 
    set wshell = nothing 
 
    'set fso = CreateObject("Scripting.FileSystemObject") 
    'set fs = fso.openTextFile("C:\BROM\my_buildfile.build", 1, TRUE) 
    'response.write replace(replace(fs.readall,"<","<"),vbCrLf,"<br>") 
    'fs.close: set fs = nothing: set fso = nothing 
%>