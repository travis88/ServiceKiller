

sc create service_killer_svc binPath= "D:\projects\ServiceKiller\ServiceKiller.Svc\bin\Release\ServiceKiller.Svc.exe" start=auto DisplayName=service_killer_svc
sc start service_killer_svc

pause