echo off
echo Running %1
sqlcmd -i %1 -b -o out.log  -d BugTracker
if not errorlevel 1 goto next
echo == An error occurred in %1 >>errors.log
:next 
echo succesfully run
echo succesfully run %1 >>status.log