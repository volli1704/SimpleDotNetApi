sleep 30

/opt/mssql-tools/bin/sqlcmd -S localhost -U $MSSQL_USER -P $MSSQL_SA_PASSWORD -d master -i /temp/migrations/0001_create_database.sql && sleep 1
/opt/mssql-tools/bin/sqlcmd -S localhost -U $MSSQL_USER -P $MSSQL_SA_PASSWORD -d master -i /temp/migrations/0002_create_catalog_table.sql && sleep 1
