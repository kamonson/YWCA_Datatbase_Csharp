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

        private bool _msgOk;
        public bool MsgOk
        {
            get
            {
                return _msgOk;
            }
            set
            {
                _msgOk = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MsgOk"));
            }
        }

        private bool _veteranStatus;
        public bool VeteranStatus
        {
            get
            {
                return _veteranStatus;
            }
            set
            {
                _veteranStatus = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("VeteranStatus"));
            }
        }

        private decimal _totalMonthlyIncome;
        public decimal TotalMonthlyIncome
        {
            get
            {
                return _totalMonthlyIncome;
            }
            set
            {
                _totalMonthlyIncome = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalMonthlyIncome"));
            }
        }

        private int _zip;
        public int Zip
        {
            get
            {
                return _zip;
            }
            set
            {
                _zip = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Zip"));
            }
        }

        private string _mi = "MI";
        public string Mi
        {
            get
            {
                return _mi;
            }
            set
            {
                _mi = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Mi"));
            }
        }

        private string _gender = " ";
        public string Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Gender"));
            }
        }

        private string _maritalStatus = " ";
        public string MaritalStatus
        {
            get
            {
                return _maritalStatus;
            }
            set
            {
                _maritalStatus = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MaritalStatus"));
            }
        }

        private string _ethnicity = " ";
        public string Ethnicity
        {
            get
            {
                return _ethnicity;
            }
            set
            {
                _ethnicity = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Ethnicity"));
            }
        }

        private string _disability = " ";
        public string Disability
        {
            get
            {
                return _disability;
            }
            set
            {
                _disability = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Disability"));
            }
        }

        private string _secondDisability = " ";
        public string SecondDisability
        {
            get
            {
                return _secondDisability;
            }
            set
            {
                _secondDisability = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SecondDisability"));
            }
        }

        private string _incomeType = "None";
        public string IncomeType
        {
            get
            {
                return _incomeType;
            }
            set
            {
                _incomeType = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IncomeType"));
            }
        }

        private string _homePhone = "None Listed";
        public string HomePhone
        {
            get
            {
                return _homePhone;
            }
            set
            {
                _homePhone = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HomePhone"));
            }
        }

        private string _workPhone = "None Listed";
        public string WorkPhone
        {
            get
            {
                return _workPhone;
            }
            set
            {
                _workPhone = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WorkPhone"));
            }
        }

        private string _callTime = "Do not call";
        public string CallTime
        {
            get
            {
                return _callTime;
            }
            set
            {
                _callTime = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CallTime"));
            }
        }

        private string _streetAddress = "Homeless";
        public string StreetAddress
        {
            get
            {
                return _streetAddress;
            }
            set
            {
                _streetAddress = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StreetAddress"));
            }
        }

        private string _city = "Spokane";
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("City"));
            }
        }

        private string _state = "Washington";
        public string State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("State"));
            }
        }


        private string _housingStatus = "Homeless";
        public string HousingStatus
        {
            get
            {
                return _housingStatus;
            }
            set
            {
                _housingStatus = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HousingStatus"));
            }
        }

        private string _neighborhood = "Unknown"; 
        public string Neighborhood
        {
            get
            {
                return _neighborhood;
            }
            set
            {
                _neighborhood = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Neighborhood"));
            }
        }

        private string _countyDetail = "Unknown";
        public string CountyDetail
        {
            get
            {
                return _countyDetail;
            }
            set
            {
                _countyDetail = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CountyDetail"));
            }
        }

        private string _staff = "None";
        public string Staff
        {
            get
            {
                return _staff;
            }
            set
            {
                _staff = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Staff"));
            }
        }

        private string _ssn = "Unknown"; 
        public string Ssn
        {
            get
            {
                return _ssn;
            }
            set
            {
                _ssn = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Ssn"));
            }
        }

        private string _dateDataEntered; 
        public string DateDataEntered
        {
            get
            {
                return _dateDataEntered;
            }
            set
            {
                _dateDataEntered = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DateDataEntered"));
            }
        }

        private string _dob; 
        public string Dob
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

        private string _fName = "First Name"; 
        public string FirstName
        {
            get
            {
                return _fName;
            }
            set
            {
                _fName = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FirstName"));
            }
        }

        private string _lName = "Last Name"; 
        public string LastName 
        {
            get
            {
                return _lName;
            }
            set
            {
                _lName = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LastName"));
            }
        }

        private string _pid = "PID"; 
        public string Pid 
        {
            get
            {
                return _pid;
            }
            set
            {
                _pid = value ?? " ";
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

        private string _hmisId = " "; 
        public string HmisId 
        {
            get
            {
                return _hmisId;
            }
            set
            {
                _hmisId = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HmisId"));
            }
        }

        private string _infoNetId = " "; 
        public string InfoNetId 
        {
            get
            {
                return _infoNetId;
            }
            set
            {
                _infoNetId = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("InfoNetId"));
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
        public void QueryDateFromPid(string selectUpdateAdd, string table, string column, ref string date)
        {
            DateTime dtDob = new DateTime();
            Connect();
            _dbCommand.CommandText = _sql.SelectUpdateOrAdd(selectUpdateAdd, table, column, Pid, date);
            OleDbDataReader rdr = _dbCommand.ExecuteReader();

            if (rdr != null)
            {
                int rowNum;
                for (rowNum = 0; rdr.Read(); rowNum++)
                {
                    dtDob = (rdr.IsDBNull(0) == false) ? rdr.GetDateTime(0) : DateTime.Now;
                    Console.WriteLine(@"{0}", date);
                }
                Console.WriteLine(@"Found " + rowNum + @" results");
                if (rowNum > 0)
                {
                    date = dtDob.ToShortDateString();
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
        /// Runs the qurry passed to it
        /// </summary>
        /// <param name="target"></param>
        /// <param name="query"></param>
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

        /// <summary>
        /// Runs the qurry passed to it
        /// </summary>
        /// <param name="target"></param>
        /// <param name="query"></param>
        public void RunQuery(ref int target, string query)
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
               target = (rdr.IsDBNull(0) == false) ? rdr.GetInt32(0) : 55555;                
                }
            }
            Disconnect();
        }

        /// <summary>
        /// Runs the qurry passed to it
        /// </summary>
        /// <param name="target"></param>
        /// <param name="query"></param>
        public void RunQuery(ref decimal target, string query)
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
                                          target = (rdr.IsDBNull(0) == false) ? rdr.GetDecimal(0) : 0; 
                }
            }
            Disconnect();
        }

        /// <summary>
        /// Runs the qurry passed to it
        /// </summary>
        /// <param name="target"></param>
        /// <param name="query"></param>
        public void RunQuery(ref bool target, string query)
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
                    target = (rdr.IsDBNull(0) == false) && rdr.GetBoolean(0);
                }
            }
            Disconnect();
        }

        /// <summary>
        /// Search for pid based off FirstName AND LastName OR Pid
        /// </summary>
        public static void RunQuery(string query)
        {
            _dbCommand.Connection = new OleDbConnection(Provider + Path + Password); //provider and data source path and password;
            _dbCommand.Connection.Open();
            _dbCommand.CommandText = query;
            _dbCommand.ExecuteReader();
            Disconnect();
        }

        /// <summary>
        /// Used to test if table contains specified query data
        /// </summary>
        /// <param name="query"></param>
        /// <returns>false if is empty</returns>
        public static bool QueryTest(string query)
        {
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = new OleDbConnection(Provider + Path + Password); //provider and data source path and password;
                cmd.Connection.Open();
                cmd.CommandText = query;
                OleDbDataReader rdr = cmd.ExecuteReader();
                bool  value = rdr != null && rdr.HasRows;
                cmd.Connection.Close();
                return value;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// Method to set, update, or add data to DB
        /// </summary>
        /// <param name="selectUpdateAdd"></param>
        /// <param name="pid"></param>
        public void Demographics(string selectUpdateAdd, string pid)
        {
            //date times
            QueryDateFromPid(selectUpdateAdd, "tbl_Consumer_List_Entry", "DOB", ref _dob);
            QueryDateFromPid(selectUpdateAdd, "tbl_Consumer_List_Entry", "LastUpdated", ref _dateDataEntered);
            
            //ints
            RunQuery(ref        _zip,                    _sql.SelectUpdateOrAdd(selectUpdateAdd,            "tbl_Forms_Flow_Table",         "Zip",                      pid,    Zip                 ));
                                                            
            //decimals
            RunQuery(ref        _totalMonthlyIncome,     _sql.SelectUpdateOrAdd(selectUpdateAdd,            "tbl_Intake",                   "Total Mo Income",          pid,    TotalMonthlyIncome  ));
                                                                                        
            //bools                                                                         
            RunQuery(ref        _msgOk,                  _sql.SelectUpdateOrAdd(selectUpdateAdd,            "tbl_Forms_Flow_Table",         "MSG_OK",                   pid,    MsgOk               ));
                                                          
            RunQuery(ref        _veteranStatus,          _sql.SelectUpdateOrAdd(selectUpdateAdd,            "tbl_Intake",                   "Vet",                      pid,    VeteranStatus       ));
                                                          
            //text                                        
            RunQuery(ref        _fName,                  _sql.SelectUpdateOrAdd(selectUpdateAdd,            "tbl_Consumer_List_Entry",      "FIRST_NAME",               pid,    FirstName           ));
            RunQuery(ref        _mi,                     _sql.SelectUpdateOrAdd(selectUpdateAdd,            "tbl_Consumer_List_Entry",      "MIDDLE_INITIAL",           pid,    Mi                  ));
            RunQuery(ref        _lName,                  _sql.SelectUpdateOrAdd(selectUpdateAdd,            "tbl_Consumer_List_Entry",      "LAST_NAME",                pid,    LastName            ));
            RunQuery(ref        _hmisId,                 _sql.SelectUpdateOrAdd(selectUpdateAdd,            "tbl_Consumer_List_Entry",      "HMIS_ID",                  pid,    HmisId              ));
            RunQuery(ref        _infoNetId,              _sql.SelectUpdateOrAdd(selectUpdateAdd,            "tbl_Consumer_List_Entry",      "INFO_NET_ID",              pid,    InfoNetId           ));
            RunQuery(ref        _ssn,                    _sql.SelectUpdateOrAdd(selectUpdateAdd,            "tbl_Consumer_List_Entry",      "NO_SSN_Reason",            pid,    Ssn                 ));
            RunQuery(ref        _gender,                 _sql.SelectUpdateOrAdd(selectUpdateAdd,            "tbl_Consumer_List_Entry",      "Gender",                   pid,    Gender              ));


            RunQuery(ref        _city,                   _sql.SelectUpdateOrAdd(selectUpdateAdd,            "tbl_Forms_Flow_Table",         "city",                     pid,    City                ));
            RunQuery(ref        _state,                  _sql.SelectUpdateOrAdd(selectUpdateAdd,            "tbl_Forms_Flow_Table",         "state",                    pid,    State               ));
            RunQuery(ref        _homePhone,              _sql.SelectUpdateOrAdd(selectUpdateAdd,            "tbl_Forms_Flow_Table",         "Home_Phone",               pid,    HomePhone           ));
            RunQuery(ref        _workPhone,              _sql.SelectUpdateOrAdd(selectUpdateAdd,            "tbl_Forms_Flow_Table",         "Work_MSG",                 pid,    WorkPhone           ));
            RunQuery(ref        _callTime,               _sql.SelectUpdateOrAdd(selectUpdateAdd,            "tbl_Forms_Flow_Table",         "Call_Time",                pid,    CallTime            ));
            RunQuery(ref        _streetAddress,          _sql.SelectUpdateOrAdd(selectUpdateAdd,            "tbl_Forms_Flow_Table",         "Street_Address",           pid,    StreetAddress       ));
            RunQuery(ref        _maritalStatus,          _sql.SelectUpdateOrAdd(selectUpdateAdd,            "tbl_Forms_Flow_Table",         "Marital_Status",           pid,    MaritalStatus       ));
            RunQuery(ref        _ethnicity,              _sql.SelectUpdateOrAdd(selectUpdateAdd,            "tbl_Forms_Flow_Table",         "Ethnicity",                pid,    Ethnicity           ));

            RunQuery(ref        _staff,                  _sql.SelectUpdateOrAdd(selectUpdateAdd,            "tbl_Intake",                   "Staff",                    pid,    Staff               ));
            RunQuery(ref        _housingStatus,          _sql.SelectUpdateOrAdd(selectUpdateAdd,            "tbl_Intake",                   "Currently Living In",      pid,    HousingStatus       ));
            RunQuery(ref        _neighborhood,           _sql.SelectUpdateOrAdd(selectUpdateAdd,            "tbl_Intake",                   "Spokane City",             pid,    Neighborhood        ));
            RunQuery(ref        _countyDetail,           _sql.SelectUpdateOrAdd(selectUpdateAdd,            "tbl_Intake",                   "Spokane County",           pid,    CountyDetail        ));
            RunQuery(ref        _disability,             _sql.SelectUpdateOrAdd(selectUpdateAdd,            "tbl_Intake",                   "Adult 1 Disability",       pid,    Disability          ));
            RunQuery(ref        _secondDisability,       _sql.SelectUpdateOrAdd(selectUpdateAdd,            "tbl_Intake",                   "Adult 1 Disability 1",     pid,    SecondDisability    ));
            RunQuery(ref        _incomeType,             _sql.SelectUpdateOrAdd(selectUpdateAdd,            "tbl_Intake",                   "income 1",                 pid,    IncomeType          ));
        }
        ////////////////////////////////////////////////////////////////////// END ADVP View Model Methods //////////////////////////////////////////////////////////////////////


        /********************************************************************* Start *********************************************************************/

        ////////////////////////////////////////////////////////////////////// END //////////////////////////////////////////////////////////////////////
    }
}
