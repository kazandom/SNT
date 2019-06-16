# SNT
Information system for the accounting of accruals and payments in the Gardening Non-Profit Partnership (in Russia)

Firebird 3.0.4 is required for use as DBMS.

The database should be created with encoding UTF8. You should specify an alias in databases.conf from Firebird directory adding something like this: base = $(dir_secDb)/../db/BASE.fdb. Execute CreateDBScript.SQL script file to import the database structure (including procedures, functions, and triggers) into an empty base.

Targeted to work with .Net Framework 4.0 and higher.
