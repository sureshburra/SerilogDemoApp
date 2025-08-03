For running the seq server, we can use the below docker container Information
docker run -d --restart unless-stopped --name seq -e ACCEPT_EULA=Y -v D:\Demos\Logs:/data -p 8081:80 datalust/seq:latest
