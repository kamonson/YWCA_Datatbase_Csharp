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
        Sql _sql = new Sql();
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


        /********************************************************************* Start SQL Data Sections *********************************************************************/
        private string _mi = "MI"; //Participant date of birth
        /// <summary>
        /// Participant DOB for WPF databinding
        /// </summary>
        public string Mi//Participant date of birth
        {
            get
            {
                return _mi;
            }
            set
            {
                _mi = value ?? "";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Mi"));
            }
        }

        private string _dob; //Participant date of birth
        /// <summary>
        /// Participant DOB for WPF databinding
        /// </summary>
        public string Dob//Participant date of birth
        {
            get
            {
                return _dob;
            }
            set
            {
                _dob = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Dob"));
            }
        }

        private string _fName = "First Name"; //Participant first name
        /// <summary>
        /// Participant first name for WPF databinding
        /// </summary>
        public string FirstName//Participant first name
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
        /// Participant last name for WPF databinding
        /// </summary>
        public string LastName //Participant last name
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
        public string Pid //Participant last name
        {
            get
            {
                return _pid;
            }
            set
            {
                _pid = value ?? "";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PID"));
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

        private string _hmisId = " "; //Participant last name
        /// <summary>
        /// HMIS ID for WPF databinding
        /// </summary>
        public string HmisId //Participant last name
        {
            get
            {
                return _hmisId;
            }
            set
            {
                _pid = value ?? "";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HmisId"));
            }
        }

        private ObservableCollection<string> _listPids = new ObservableCollection<string> { @"results go here" }; //pid search results list
        /// <summary>
        /// Participant id for WPF databinding
        /// </summary>
        public ObservableCollection<string> ListPiDs//List for displaying search results of pids
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
        ////////////////////////////////////////////////////////////////////// End SQL Data Sections //////////////////////////////////////////////////////////////////////

		
		/********************************************************************* Start ADVP View Mode Methods *********************************************************************/
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
        public void RunQueryFNameFromPid(string selectUpdate)
        {
            Connect();
            _dbCommand.CommandText = _sql.FirstNameFromPid(selectUpdate, Pid, FirstName);
            OleDbDataReader rdr = _dbCommand.ExecuteReader();
            rdr?.GetSchemaTable();
            if (rdr != null)
            {
                int rowNum;
                for (rowNum = 0; rdr.Read(); rowNum++)
                {
                    FirstName = (rdr.IsDBNull(0) == false) ? rdr.GetString(0) : null;
                    Console.WriteLine(@"{0}", FirstName);
                }
                Console.WriteLine(@"Found " + rowNum + @" results");
            }
            Disconnect();
        }

        /// <summary>
        /// Get first name from DB with given DB and Update FirstName for WPF update
        /// </summary>
        public void RunQueryMInitialFromPid(string selectUpdate)
        {
            Connect();
            _dbCommand.CommandText = _sql.MiddleInitialFromPid(selectUpdate, Pid, Mi);
            OleDbDataReader rdr = _dbCommand.ExecuteReader();
            rdr?.GetSchemaTable();
            if (rdr != null)
            {
                int rowNum;
                for (rowNum = 0; rdr.Read(); rowNum++)
                {
                    Mi = (rdr.IsDBNull(0) == false) ? rdr.GetString(0) : null;
                    Console.WriteLine(@"{0}", Mi);
                }
                Console.WriteLine(@"Found " + rowNum + @" results");
            }
            Disconnect();
        }

        /// <summary>
        /// Get last name from DB with given DB and Update LastName for WPF update
        /// </summary>
        public void RunQueryLNameFromPid(string selectUpdate)
        {
            Connect();
            _dbCommand.CommandText = _sql.LastNameFromPid(selectUpdate, Pid, LastName);
            OleDbDataReader rdr = _dbCommand.ExecuteReader();
            rdr?.GetSchemaTable();
            if (rdr != null)
            {
                int rowNum;
                for (rowNum = 0; rdr.Read(); rowNum++)
                {
                    LastName = (rdr.IsDBNull(0) == false) ? rdr.GetString(0) : null;
                    Console.WriteLine(@"{0}", LastName);
                }
                Console.WriteLine(@"Found " + rowNum + @" results");
            }
            Disconnect();
        }

        /// <summary>
        /// Get DOB from DB with given DB and Update LastName for WPF update
        /// </summary>
        public void RunQueryDobFromPid(string selectUpdate)
        {
            DateTime dtDob = new DateTime();
            Connect();
            _dbCommand.CommandText = _sql.DobFromPid(selectUpdate, Pid, Dob);
            OleDbDataReader rdr = _dbCommand.ExecuteReader();
            rdr?.GetSchemaTable();
            if (rdr != null)
            {
                int rowNum;
                for (rowNum = 0; rdr.Read(); rowNum++)
                {
                    dtDob = (rdr.IsDBNull(0) == false) ? rdr.GetDateTime(0) : DateTime.Now;   
                    Console.WriteLine(@"{0}", Dob);
                }
                Console.WriteLine(@"Found " + rowNum + @" results");
                if (rowNum > 0)
                {
                    Dob = dtDob.ToShortDateString();
                }
            }
            Disconnect();
        }

        /// <summary>
        /// Search for pid based off FirstName AND LastName OR Pid
        /// </summary>
        public void RunQueryFindClient()
        {
            Connect();
            _dbCommand.CommandText = _sql.FindClientPid(FirstName, LastName, Pid);
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

        /// <summary>
        /// Search for pid based off FirstName AND LastName OR Pid
        /// </summary>
        public void RunQuery(ref string target, string query)
        {
            Connect();
            _dbCommand.CommandText = query;
            OleDbDataReader rdr = _dbCommand.ExecuteReader();
            ListPiDs.Clear();
            if (rdr != null)
            {
                int rowNum;
                for (rowNum = 0; rdr.Read(); rowNum++)
                {
                   target = ((rdr.IsDBNull(0) == false) ? rdr.GetString(0) : null);
                }
            }
            Disconnect();
        }

        public void GetIntakeForm(string pid)
        {
            RunQueryDobFromPid("select");
            RunQuery(ref _fName, _sql.FirstNameFromPid("select", pid, FirstName));
            RunQuery(ref _lName, _sql.LastNameFromPid("select", pid, LastName));
            RunQuery(ref _hmisId, _sql.HmisIdNameFromPid("select", pid, HmisId));
            RunQuery(ref _mi, _sql.MiddleInitialFromPid("select", pid, HmisId));
        }           
        ////////////////////////////////////////////////////////////////////// END ADVP View Model Methods //////////////////////////////////////////////////////////////////////
		
		
		 /********************************************************************* Start *********************************************************************/
		 
		  ////////////////////////////////////////////////////////////////////// END //////////////////////////////////////////////////////////////////////
    }
}
