using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.OleDb;

namespace YWCA_Software
{
    class DbConnector : INotifyPropertyChanged
    {
        private static OleDbCommand _dbCommand = new OleDbCommand(); //commander for database
        public event PropertyChangedEventHandler PropertyChanged; //event handler for data binding to WPF
        public List<string> listFirstNames = new List<string>();
        public List<string> listLastNames = new List<string>();

        private ObservableCollection<string> _listPids = new ObservableCollection<string> {@"results go here"};
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
        /// Pass PID to obtain a SQL string for obtaining first name of person with given PID
        /// </summary>
        /// <param name="participantId"></param>
        /// <returns>SQL string to obtain first name</returns>
        private string fNameFromParticipantID(string participantId)
        {
            return @"SELECT " + @"FIRST_NAME" + @" " +
                   @"FROM " + @"tbl_Consumer_List_Entry" + @" " +
                   @"WHERE " + @"Consumer_ID = '" + participantId + "'" + @" " +
                   @";";
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
                _fName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FirstName"));
            }
        }

        private string _lName = "Last Name"; //Participant last name
        /// <summary>
        /// Pass PID to obtain a SQL string for obtaining last name of person with given PID
        /// </summary>
        /// <param name="participantId"></param>
        /// <returns>SQL string to obtain last name</returns>
        private string lNameFromParticipantID(string participantId)
        {
            return @"SELECT " + @"LAST_NAME" + @" " +
                   @"FROM " + @"tbl_Consumer_List_Entry" + @" " +
                   @"WHERE " + @"Consumer_ID = '" + participantId + "'" + @" " +
                   @";";
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
                _lName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LastName"));
            }
        }
        ////////////////////////////////////////////////////////////////////// End SQL Strings //////////////////////////////////////////////////////////////////////

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
       /// <param name="PID"></param>
        public void RunQueryFNameFromPID(string PID)
        {
            Connect();
            _dbCommand.CommandText = fNameFromParticipantID(PID);

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
        /// <param name="PID"></param>
        public void RunQueryLNameFromPID(string PID)
        {
            Connect();

            _dbCommand.CommandText = lNameFromParticipantID(PID);

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

        public void RunQueryFindClient(string fname, string lname, string pid)
        {
            Connect();
            string SELECT = @"SELECT * ";
            string FROM = @"FROM tbl_Consumer_List_Entry ";
            string WHERE = @"WHERE FIRST_NAME LIKE '*" + FirstName + @"*' AND LAST_NAME LIKE '*" + LastName + @"*';";

            _dbCommand.CommandText = SELECT + FROM + WHERE;

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
