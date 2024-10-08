# TableOfUdtOracle
How to add a table of UDT (element object having a Oracle Blob column/field) defined in Oracle Types passed as parameter to a Stored procedure defined in an Oracle package.  Custom types generated with Oracle Developer tools for visual studio 2022. 
<hr/>

# Why this project? To answer these questions :
- How to send a table of Oracle UDT as parameter to Oracle Stored Procedure defined in a Package.
- How to make an integration test of an Azure function triggered by a service bus topic message.
- How to add a local.settings.json in a test project via Dependency injection, by referencing the API project.
- How to implement an Onion architecture.
- How to pass media file, Oracle Blob / bytes array to Oracle DB.
- To understand how Oracle Types / UDT and ODP.NET work.  

# How to go ? 
The necessary files and scripts used to test are found in the repo.  

I used Oracle XE 21c, ODP.NET Managed Driver 21.3.0.0.0,

Connection string(that will differ) :  DATA SOURCE=localhost:1521/xe;TNS_ADMIN=C:\Users\User\Oracle\network\admin;PERSIST SECURITY INFO=True;USER ID="C##STEPHANE"

**NB: I'm not the owner of the ressources found in this project like Gaming.jpeg.**

- Install Oracle XE <a href="https://www.oracle.com/database/technologies/xe-downloads.html" target="_blank">get it here</a>. 
- Download Oracle SQL Developer to run the scripts <a href="https://www.oracle.com/database/sqldeveloper/technologies/download/" target="_blank">get it here</a>.
- Create a db user and give him the required privileges.
- Make sure the Listener (OracleOraDB21Home1TNSListener) is running in services.msc (if you have that issue trying to connect).
- Connect with the created user before running the scripts (that you adjusted to match the user/schema, replace the schema name C##STEPHANE).
- Run the scripts found in the scripts folder to create the Types (UDT) the package with its body (in that order).
- Open the VS solution, restore the projects, adjust the used schema and package names in the entities and DbContext.
- Connect to the Oracle Db via the VS Server Explorer, by making a new DB connection using ODP.NET Managed config.
- Open the integration test project and create a local.settings.json file and add the connection property (ORACLE_DB_CONNECTION_STRING) string from the new connection you made (can be obtained from properties of the DB Connection created).
- Adjust the path to the gaming.jpeg file in the retrieval of the bytes array to match your file to be read and saved in DB.
- Reached here you can run the test in debug and follow the call and save of the data.
