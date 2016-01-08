using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.OleDb;

namespace YWCA_Software
{
    /// <summary>
    /// ViewModel and SQL manager, this really should be two sepreate classes
    /// </summary>
    public class DbConnector : INotifyPropertyChanged
    {
        private static OleDbCommand _dbCommand = new OleDbCommand(); //commander for database
        public event PropertyChangedEventHandler PropertyChanged; //event handler for data binding to WPF

        /********************************************************************* Start Const Strings For DB Access *********************************************************************/
        private const string Provider = @"Provider=Microsoft.ACE.OLEDB.12.0;";

        private const string Path = @"Data Source=" +
                                    @"C:\YWCADB\All\" +
                                    @"13 ADVP - Database_20140718_1307.accdb" +
                                    @";";
        private const string Password = @"Jet OLEDB:Database Password=ywc@;";
        ////////////////////////////////////////////////////////////////////// END Const Strings For DB Access //////////////////////////////////////////////////////////////////////



        /********************************************************************* Start SQL Strings *********************************************************************/
        private string _fName = "First Name"; //Participant first name
        /// <summary>
        /// Pass pid to obtain a SQL string for obtaining first name of person with given pid
        /// </summary>
        /// <param name="participantId"></param>
        /// <returns>SQL string to obtain first name</returns>
        private string FNameFromParticipantId(string participantId)
        {
            return Select("FIRST_NAME") +
                   From("tbl_Consumer_List_Entry") +
                   Where("Consumer_ID" + Equals(participantId)) +
                   EndQuery();
        }
        /// <summary>
        /// Participant first name for WPF databinding
        /// </summary>
        public string FirstName
        {
            get
            {
                return _fName;
            }
            set
            {
                _fName = value ?? "";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FirstName"));
            }
        }

        private string _lName = "Last Name"; //Participant last name
        /// <summary>
        /// Pass pid to obtain a SQL string for obtaining last name of person with given pid
        /// </summary>
        /// <param name="participantId"></param>
        /// <returns>SQL string to obtain last name</returns>
        private string LNameFromParticipantId(string participantId)
        {
            return Select("LAST_NAME") +
                   From("tbl_Consumer_List_Entry") +
                   Where("Consumer_ID" + Equals(participantId)) +
                   EndQuery();
        }
        /// <summary>
        /// Participant last name for WPF databinding
        /// </summary>
        public string LastName
        {
            get
            {
                return _lName;
            }
            set
            {
                _lName = value ?? "";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LastName"));
            }
        }

        private string _pid = "PID"; //Participant last name
        /// <summary>
        /// Participant ID for WPF databinding
        /// </summary>
        public string Pid
        {
            get
            {
                return _pid;
            }
            set
            {
                _pid = value ?? "";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LastName"));
            }
        }

        /// <summary>
        /// Set pid to input
        /// </summary>
        /// <param name="pid"></param>
        public void SetPid(string pid)
        {
            Pid = pid;
        }

        private ObservableCollection<string> _listPids = new ObservableCollection<string> { @"results go here" }; //pid search results list
        /// <summary>
        /// Participant id for WPF databinding
        /// </summary>
        public ObservableCollection<string> ListPiDs
        {
            get
            {
                return _listPids;
            }
            set
            {
                _listPids = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ListPiDs"));
            }
        }
        ////////////////////////////////////////////////////////////////////// End SQL Strings //////////////////////////////////////////////////////////////////////

        /********************************************************************* Start SQL Operators *********************************************************************/

        private string Select(string conditions)
        {
            return @"SELECT " + conditions + @" ";
        }

        private string From(string conditions)
        {
            return @" FROM " + conditions + @" ";
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
            return " = '" + expr + "'";
        }
        ////////////////////////////////////////////////////////////////////// End SQL Operators //////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Constructor, when initialized test DB connection
        /// </summary>
        public DbConnector()
        {
            try
            {
                Connect();
            }
            catch (Exception e)
            {
                Console.WriteLine(@"Database connection error: " + e);
            }
        }

        /// <summary>
        /// Open connection to DB
        /// </summary>
        private void Connect()
        {
            _dbCommand.Connection = new OleDbConnection(Provider + Path + Password); //provider and data source path and password;
            _dbCommand.Connection.Open();
        }

        /// <summary>
        /// Close connection to DB
        /// </summary>
        private static void Disconnect()
        {
            _dbCommand.Connection.Close();
        }

        /// <summary>
        /// Get first name from DB with given DB and set FirstName for WPF update
        /// </summary>
        /// <param name="pid"></param>
        public void RunQueryFNameFromPid(string pid)
        {
            Connect();
            _dbCommand.CommandText = FNameFromParticipantId(pid);

            OleDbDataReader rdr = _dbCommand.ExecuteReader();

            rdr?.GetSchemaTable();

            if (rdr != null)
            {
                int rowNum;
                for (rowNum = 0; rdr.Read(); rowNum++)
                {
                    FirstName = (rdr.IsDBNull(0) == false) ? rdr.GetString(0) : null;
                    Console.WriteLine(@"{0}", _fName);
                }
                Console.WriteLine(@"Found " + rowNum + @" results");
            }

            Disconnect();
        }

        /// <summary>
        /// Get last name from DB with given DB and set LastName for WPF update
        /// </summary>
        /// <param name="pid"></param>
        public void RunQueryLNameFromPid(string pid)
        {
            Connect();

            _dbCommand.CommandText = LNameFromParticipantId(pid);

            OleDbDataReader rdr = _dbCommand.ExecuteReader();

            rdr?.GetSchemaTable();

            if (rdr != null)
            {
                int rowNum;
                for (rowNum = 0; rdr.Read(); rowNum++)
                {
                    LastName = (rdr.IsDBNull(0) == false) ? rdr.GetString(0) : null;
                    Console.WriteLine(@"{0}", _lName);
                }
                Console.WriteLine(@"Found " + rowNum + @" results");
            }

            Disconnect();
        }

        /// <summary>
        /// Search for pid based off FirstName AND LastName OR Pid
        /// </summary>
        public void RunQueryFindClient()
        {
            Connect();

            _dbCommand.CommandText =
                Select("*") +
                From("tbl_Consumer_List_Entry") +
                Where("FIRST_NAME" + Like(FirstName) + And("LAST_NAME") + Like(LastName) + Or("Consumer_ID" + Equals(Pid))) +
                EndQuery();

            OleDbDataReader rdr = _dbCommand.ExecuteReader();

            ListPiDs.Clear();

            if (rdr != null)
            {
                int rowNum;
                for (rowNum = 0; rdr.Read(); rowNum++)
                {
                    ListPiDs.Add((rdr.IsDBNull(0) == false) ? rdr.GetString(0) : null);
                }
            }
            Disconnect();
        }
    }
}
