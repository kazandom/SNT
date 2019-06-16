# SNT
Information system for the accounting of accruals and payments in the Gardening NonCommercial Partnership (in Russia)

Firebird 3.0.4 is required for use as DBMS.

The database should be created with charset UTF8. You should specify an alias in databases.conf from Firebird directory adding something like this: base = $(dir_secDb)/../db/BASE.fdb. Execute CreateDBScript.SQL script file to import the database structure (including procedures, functions, and triggers) into an empty base.

Targeted to work with .Net Framework 4.0 and higher (Windows XP or higher).

Using: C#, WPF, MVVM, ADO.NET.

All of the code is licenced under Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported and can not be used for commercial purposes. Please see https://creativecommons.org/licenses/by-nc-sa/3.0/deed.en for details.
