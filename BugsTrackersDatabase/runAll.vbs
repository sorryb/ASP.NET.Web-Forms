Dim shell,ArgObj,var1,cmd
Set ArgObj = WScript.Arguments 
Set shell = WScript.CreateObject("WScript.Shell")
var1 = ArgObj(0) 
cmd = var1 + "runAll.bat"
shell.run cmd
set shell=nothing

