FROM mcr.microsoft.com/mssql/server:2019-latest

WORKDIR /temp

COPY ./Database/startup /temp

CMD /bin/bash /temp/entrypoint.sh