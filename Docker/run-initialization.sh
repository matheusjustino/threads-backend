# Wait to be sure that SQL Server came up
sleep 90s

/opt/mssql-tools/bin/sqlcmd -S localhost -U postgres -P docker -d master -i create-database.sql