using System;
using System.Globalization;
using static YWCA_Software.DbConnector;

namespace YWCA_Software
{
    class Sql
    {
        /********************************************************************* Start SQL Operators And Keywords*********************************************************************/
        private const string TblIntakeCollumns =
            @" ([CONSUMER_ID]) ";

        private string ColumnEquals(string cName, string target)
        {
            return @"[" + cName + @"]" + " = \"" + target + "\" ";
        }

        private string ColumnEquals(string cName, bool target)
        {
            return @"[" + cName + @"]" + " = " + target.ToString() + " ";
        }

        private string Values(string conditions)
        {
            return "Values ( \"" + conditions + "\" ) ";
        }

        private string Select(string conditions)
        {
            return (conditions != "*") ? @"SELECT [" + conditions + @"] " : @"SELECT " + conditions + @" ";
        }

        private string InsertInto(string conditions)
        {
            return @"INSERT INTO " + conditions + @" ";
        }

        private string Set(string conditions)
        {
            return @"SET " + conditions + @" ";
        }

        private string From(string conditions)
        {
            return @" FROM [" + conditions + @"] ";
        }

        private string Where(string conditions)
        {
            return @" WHERE " + conditions + @" ";
        }

        private string EndQuery()
        {
            return @";";
        }

        private string Like(string word)
        {
            return @" LIKE '%" + word + @"%' ";
        }

        private string And(string expr)
        {
            return @"AND " + expr + @" ";
        }

        private string Or(string expr)
        {
            return @"OR " + expr + @" ";
        }

        private string Equals(string expr)
        {
            return @" = '" + expr + @"'";
        }

        private string Update(string tbl)
        {
            return @"UPDATE " + tbl + @" ";
        }

        private string Prefix(string selectOrUpdate, string expr)
        {
            if (selectOrUpdate.ToLower() == @"update")
            {
                return Set(expr);
            }
            else
            {
                return Select(expr);
            }
        }

        private string Root(string selectOrUpdate, string tbl)
        {
            if (selectOrUpdate.ToLower() == @"update")
            {
                return Update(tbl);
            }
            else
            {
                return From(tbl);
            }
        }
        ////////////////////////////////////////////////////////////////////// End SQL Operators And Keywords //////////////////////////////////////////////////////////////////////


        /********************************************************************* Start SQL Queries *********************************************************************/

        /// <summary>
        /// Pass pid to obtain a SQL string for obtaining first name of person with given pid
        /// </summary>
        /// <param name="selectOrUpdate"></param>
        /// <param name="participantId"></param>
        /// <param name="fname"></param>
        /// <returns>SQL string to obtain first name</returns>
        public string FirstNameFromPid(string selectOrUpdate, string participantId, string fname)
        {
            string query = (selectOrUpdate == @"select")
                ? Prefix(selectOrUpdate, @"FIRST_NAME") + Root(selectOrUpdate, @"tbl_Consumer_List_Entry")
                : Root(selectOrUpdate, @"tbl_Consumer_List_Entry") + Prefix(selectOrUpdate, @"FIRST_NAME = '" + fname + @"' ");
            return query + Where(@"Consumer_ID" + Equals(participantId)) + EndQuery();
        }

        /// <summary>
        /// Pass pid to obtain a SQL string for obtaining last name of person with given pid
        /// </summary>
        /// <param name="selectOrUpdate"></param>
        /// <param name="participantId"></param>
        /// <param name="lname"></param>
        /// <returns>SQL string to obtain last name</returns>
        public string LastNameFromPid(string selectOrUpdate, string participantId, string lname)
        {
            string query = (selectOrUpdate == @"select")
                ? Prefix(selectOrUpdate, @"LAST_NAME") + Root(selectOrUpdate, @"tbl_Consumer_List_Entry")
                : Root(selectOrUpdate, @"tbl_Consumer_List_Entry") + Prefix(selectOrUpdate, @"LAST_NAME = '" + lname + @"' ");
            return query + Where(@"Consumer_ID" + Equals(participantId)) + EndQuery();
        }

        /// <summary>
        /// Create query based on given information to add or update, if filed needs updating but does not exist create row
        /// </summary>
        /// <param name="selectOrUpdate"></param>
        /// <param name="table"></param>
        /// <param name="columnName"></param>
        /// <param name="participantId"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string SelectUpdateOrAdd(string selectOrUpdate, string table, string columnName, string participantId, string value)
        {
            string select = Prefix(selectOrUpdate, columnName) + Root(selectOrUpdate, table);
            string update = Root(selectOrUpdate, table) + Prefix(selectOrUpdate, ColumnEquals(columnName, value));
            string end = Where(@"Consumer_ID" + Equals(participantId)) + EndQuery();

            if (selectOrUpdate == "update" && !QueryTest(Select(columnName) + From(table) + end))
            {
                RunQuery(InsertInto(table) + TblIntakeCollumns + Values(participantId) + EndQuery());
                Console.WriteLine(@"One item added to the database");
            }

            string query = (selectOrUpdate == @"select") ? select : update;
            return query + end;
        }

        /// <summary>
        /// Create query based on given information to add or update, if filed needs updating but does not exist create row
        /// </summary>
        /// <param name="selectOrUpdate"></param>
        /// <param name="table"></param>
        /// <param name="columnName"></param>
        /// <param name="participantId"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string SelectUpdateOrAdd(string selectOrUpdate, string table, string columnName, string participantId, int value)
        {
            string select = Prefix(selectOrUpdate, columnName) + Root(selectOrUpdate, table);
            string update = Root(selectOrUpdate, table) + Prefix(selectOrUpdate, ColumnEquals(columnName, value.ToString()));
            string end = Where(@"Consumer_ID" + Equals(participantId)) + EndQuery();

            if (selectOrUpdate == "update" && !QueryTest(Select(columnName) + From(table) + end))
            {
                RunQuery(InsertInto(table) + TblIntakeCollumns + Values(participantId) + EndQuery());
                Console.WriteLine(@"One item added to the database");
            }

            string query = (selectOrUpdate == @"select") ? select : update;
            return query + end;
        }

        /// <summary>
        /// Create query based on given information to add or update, if filed needs updating but does not exist create row
        /// </summary>
        /// <param name="selectOrUpdate"></param>
        /// <param name="table"></param>
        /// <param name="columnName"></param>
        /// <param name="participantId"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string SelectUpdateOrAdd(string selectOrUpdate, string table, string columnName, string participantId, decimal value)
        {
            string select = Prefix(selectOrUpdate, columnName) + Root(selectOrUpdate, table);
            string update = Root(selectOrUpdate, table) + Prefix(selectOrUpdate, ColumnEquals(columnName, value.ToString(CultureInfo.CurrentCulture)));
            string end = Where(@"Consumer_ID" + Equals(participantId)) + EndQuery();

            if (selectOrUpdate == "update" && !QueryTest(Select(columnName) + From(table) + end))
            {
                RunQuery(InsertInto(table) + TblIntakeCollumns + Values(participantId) + EndQuery());
                Console.WriteLine(@"One item added to the database");
            }

            string query = (selectOrUpdate == @"select") ? select : update;
            return query + end;
        }

        /// <summary>
        /// Create query based on given information to add or update, if filed needs updating but does not exist create row
        /// </summary>
        /// <param name="selectOrUpdate"></param>
        /// <param name="table"></param>
        /// <param name="columnName"></param>
        /// <param name="participantId"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string SelectUpdateOrAdd(string selectOrUpdate, string table, string columnName, string participantId, bool value)
        {
            string select = Prefix(selectOrUpdate, columnName) + Root(selectOrUpdate, table);
            string update = Root(selectOrUpdate, table) + Prefix(selectOrUpdate, ColumnEquals(columnName, value));
            string end = Where(@"Consumer_ID" + Equals(participantId)) + EndQuery();

            if (selectOrUpdate == "update" && !QueryTest(Select(columnName) + From(table) + end))
            {
                RunQuery(InsertInto(table) + TblIntakeCollumns + Values(participantId) + EndQuery());
                Console.WriteLine(@"One item added to the database");
            }

            string query = (selectOrUpdate == @"select") ? select : update;
            return query + end;
        }

        /// <summary>
        /// Pass in partial first AND last OR PID to have PID returned
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public string FindClientPid(string firstName, string lastName, string pid)
        {
            return Select(@"*") +
                   From(@"tbl_Consumer_List_Entry") +
                   Where(@"FIRST_NAME" + Like(firstName) + And(@"LAST_NAME") + Like(lastName) + Or(@"Consumer_ID" + Equals(pid))) +
                   EndQuery();
        }
        ////////////////////////////////////////////////////////////////////// End SQL Queries //////////////////////////////////////////////////////////////////////


        /********************************************************************* Start *********************************************************************/

        ////////////////////////////////////////////////////////////////////// End //////////////////////////////////////////////////////////////////////
    }
}
