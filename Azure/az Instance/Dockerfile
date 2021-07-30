FROM mcr.microsoft.com/dotnet/aspnet:5.0

RUN mkdir /app
WORKDIR /app

COPY ./webapp/bin/Release/net5.0/publish ./
#COPY ./config.sh ./

#RUN bash config.sh

EXPOSE 80

ENTRYPOINT [ "dotnet", "webapp.dll" ] 