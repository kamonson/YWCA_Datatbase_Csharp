namespace YWCA_Software
{
    class Sql
    {
        /********************************************************************* Start SQL Operators And Keywords*********************************************************************/
        public const string TblIntakeCollumns =
            @" ([CONSUMER_ID]) ";

        public string Values(string conditions)
        {
            return "Values ( \"" + conditions + "\" ) ";
        }
        public string ValuesNoQuote(string conditions)
        {
            return "Values (" + conditions + ") ";
        }

        public string Select(string conditions)
        {
            return (conditions != "*") ? @"SELECT [" + conditions + @"] " : @"SELECT " + conditions + @" ";
        }

        public string InsertInto(string conditions)
        {
            return @"INSERT INTO " + conditions + @" ";
        }

        public string Set(string conditions)
        {
            return @"SET " + conditions + @" ";
        }

        public string From(string conditions)
        {
            return @" FROM [" + conditions + @"] ";
        }

        public string Where(string conditions)
        {
            return @" WHERE " + conditions + @" ";
        }

        public string EndQuery()
        {
            return @";";
        }

        public string Like(string word)
        {
            return @" LIKE '" + word + @"%' ";
        }

        public string And(string expr)
        {
            return @"AND " + expr + @" ";
        }

        public string Or(string expr)
        {
            return @"OR " + expr + @" ";
        }

        public string Equals(string expr)
        {
            return @" = '" + expr + @"'";
        }

        public string EqualsNoQuote(string expr)
        {
            return @" = " + expr;
        }


        public string Update(string tbl)
        {
            return @"UPDATE " + tbl + @" ";
        }

        public string Prefix(string selectOrUpdate, string expr)
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

        public string Root(string selectOrUpdate, string tbl)
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
        /// Pass in partial first AND last OR PID to have PID returned
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public string FindClientPid(string department, string firstName, string lastName, string pid)
        {
            string fLocation = null;
            string id = null;

            if (department == @"ADVP")
            {
                fLocation = @"tbl_Consumer_List_Entry";
                id = @"Consumer_ID";
            }
            else if (department == @"ECAP")
            {
                fLocation = @"ECAP";
                id = @"ECAP_ID";
            }
            else if (department == @"ECAP_VHOURS")
            {

            }
            else if (department == @"WOCAPPT")
            {
                fLocation = @"WOCAPPT";
                id = @"WOCID";
            }
            else if (department == @"WOC_CLASS")
            {

            }
            else if (department == @"WOC_COMPLOG")
            {

            }


            return Select(id) +
                   From(fLocation) +
                   Where(@"FIRST_NAME" + Like(firstName) + And(@"LAST_NAME") + Like(lastName) + Or(id + Equals(pid)) + @"GROUP BY " + (id) + " ") +
                   EndQuery();
        }

        /// <summary>
        /// Get intake dates from pid
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public string FindClientDate(string pid)
        {
            return Select(@"Date") +
                   From(@"tbl_Intake") +
                   Where(@"Consumer_ID" + Equals(pid)) +
                   EndQuery();
        }

        /// <summary>
        /// Get intake dates from pid
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public string FindWocAptDate(string pid)
        {
            return Select(@"DateScheduled") +
                   From(@"WOCAPPT") +
                   Where(@"WOCID" + Equals(pid)) +
                   EndQuery();
        }

        /// <summary>
        /// Get intake dates from pid
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public string FindWocClassDate(string pid)
        {
            return Select(@"DateClass") +
                   From(@"WOCClass") +
                   Where(@"WOCID" + Equals(pid)) +
                   EndQuery();
        }

        /// <summary>
        /// Add date to intakes
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public string AddIntakeDate(string pid, string date)
        {
           return   InsertInto("tbl_Intake" + " (" +  @"[Consumer_ID]" + ", " + "[Date]" + ")") + 
                    ValuesNoQuote("'" + pid + "', " + "#" + date + "# ") +
                    EndQuery();

        }

        /// <summary>
        /// Add date to intakes
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public string AddWOCAptDate(string pid, string date)
        {
            return InsertInto("WOCAPPT" + " (" + @"[WOCID]" + ", " + "[DateScheduled]" + ")") +
                     ValuesNoQuote("'" + pid + "', " + "#" + date + "# ") +
                     EndQuery();

        }

        /// <summary>
        /// Add date to intakes
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public string AddWOClassDate(string pid, string date)
        {
            return InsertInto("WOCClass" + " (" + @"[WOCID]" + ", " + "[DateClass]" + ")") +
                     ValuesNoQuote("'" + pid + "', " + "#" + date + "# ") +
                     EndQuery();

        }
        ////////////////////////////////////////////////////////////////////// End SQL Queries //////////////////////////////////////////////////////////////////////


        /********************************************************************* Start *********************************************************************/

        ////////////////////////////////////////////////////////////////////// End //////////////////////////////////////////////////////////////////////
    }
}
