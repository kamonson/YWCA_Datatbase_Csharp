using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.OleDb;
using System.Globalization;

namespace YWCA_Software
{
    /// <summary>
    /// ViewModel and SQL manager, this really should be two sepreate classes
    /// </summary>
    public class DbConnector : INotifyPropertyChanged
    {
        private readonly Sql _sql = new Sql();
        static readonly OleDbCommand DbCommand = new OleDbCommand(); //commander for database
        public event PropertyChangedEventHandler PropertyChanged; //event handler for data binding to WPF

        /********************************************************************* Start Const Strings For DB Access *********************************************************************/
        public const string Provider = @"Provider=Microsoft.ACE.OLEDB.12.0;";

        public const string Path = @"Data Source=" +
                                    @"P:\ywcaDbSoftware\" +
                                    @"YWCACounselingANDLegal.accdb" +
                                    @";";
        public const string Password = @"Jet OLEDB:Database Password=ywc@;";
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

        private double _totalAdultsInHousehold;
        public double TotalAdultsInHousehold
        {
            get
            {
                return _totalAdultsInHousehold;
            }
            set
            {
                _totalAdultsInHousehold = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalAdultsInHousehold"));
            }
        }

        private string _totalAdultsInHouseholdString;
        public string TotalAdultsInHouseholdString
        {
            get
            {
                return _totalAdultsInHouseholdString;
            }
            set
            {
                _totalAdultsInHouseholdString = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalAdultsInHouseholdString"));
            }
        }

        private string _intakeDate;
        public string IntakeDate
        {
            get
            {
                return _intakeDate;
            }
            set
            {
                _intakeDate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IntakeDate"));
            }
        }

        public void SetIntakeDate(string date)
        {
            IntakeDate = date;
        }

        private string _totalChildrenInHouseholdString;
        public string TotalChildrenInHouseholdString
        {
            get
            {
                return _totalChildrenInHouseholdString;
            }
            set
            {
                _totalChildrenInHouseholdString = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalChildrenInHouseholdString"));
            }
        }

        private double _totalChildrenInHousehold;
        public double TotalChildrenInHousehold
        {
            get
            {
                return _totalChildrenInHousehold;
            }
            set
            {
                _totalChildrenInHousehold = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TotalChildrenInHousehold"));
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

        private string _dischargeDate = "None";
        public string DischargeDate
        {
            get
            {
                return _dischargeDate;
            }
            set
            {
                _dischargeDate = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DischargeDate"));
            }
        }

        private string _dischargeLocation = "None";
        public string DischargeLocation
        {
            get
            {
                return _dischargeLocation;
            }
            set
            {
                _dischargeLocation = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DischargeLocation"));
            }
        }

        private string _personsInHomeRelationship = "None";
        public string PersonsInHomeRelationship
        {
            get
            {
                return _personsInHomeRelationship;
            }
            set
            {
                _personsInHomeRelationship = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeRelationship"));
            }
        }

        private string _personsInHomeGender = "NA";
        public string PersonsInHomeGender
        {
            get
            {
                return _personsInHomeGender;
            }
            set
            {
                _personsInHomeGender = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeGender"));
            }
        }

        private string _personsInHomeDob = "NA";
        public string PersonsInHomeDob
        {
            get
            {
                return _personsInHomeDob;
            }
            set
            {
                _personsInHomeDob = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeDob"));
            }
        }

        private string _personsInHomeRelationship2 = "None";
        public string PersonsInHomeRelationship2
        {
            get
            {
                return _personsInHomeRelationship2;
            }
            set
            {
                _personsInHomeRelationship2 = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeRelationship2"));
            }
        }

        private string _personsInHomeGender2 = "NA";
        public string PersonsInHomeGender2
        {
            get
            {
                return _personsInHomeGender2;
            }
            set
            {
                _personsInHomeGender2 = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeGender2"));
            }
        }

        private string _personsInHomeDob2 = "NA";
        public string PersonsInHomeDob2
        {
            get
            {
                return _personsInHomeDob2;
            }
            set
            {
                _personsInHomeDob2 = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeDob2"));
            }
        }

        private string _personsInHomeRelationship3 = "None";
        public string PersonsInHomeRelationship3
        {
            get
            {
                return _personsInHomeRelationship3;
            }
            set
            {
                _personsInHomeRelationship3 = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeRelationship3"));
            }
        }

        private string _personsInHomeGender3 = "NA";
        public string PersonsInHomeGender3
        {
            get
            {
                return _personsInHomeGender3;
            }
            set
            {
                _personsInHomeGender3 = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeGender3"));
            }
        }

        private string _personsInHomeDob3 = "NA";
        public string PersonsInHomeDob3
        {
            get
            {
                return _personsInHomeDob3;
            }
            set
            {
                _personsInHomeDob3 = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeDob3"));
            }
        }

        private string _personsInHomeRelationship4 = "None";
        public string PersonsInHomeRelationship4
        {
            get
            {
                return _personsInHomeRelationship4;
            }
            set
            {
                _personsInHomeRelationship4 = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeRelationship4"));
            }
        }

        private string _personsInHomeGender4 = "NA";
        public string PersonsInHomeGender4
        {
            get
            {
                return _personsInHomeGender4;
            }
            set
            {
                _personsInHomeGender4 = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeGender4"));
            }
        }

        private string _personsInHomeDob4 = "NA";
        public string PersonsInHomeDob4
        {
            get
            {
                return _personsInHomeDob4;
            }
            set
            {
                _personsInHomeDob4 = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeDob4"));
            }
        }

        private string _personsInHomeRelationship5 = "None";
        public string PersonsInHomeRelationship5
        {
            get
            {
                return _personsInHomeRelationship5;
            }
            set
            {
                _personsInHomeRelationship5 = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeRelationship5"));
            }
        }

        private string _personsInHomeGender5 = "NA";
        public string PersonsInHomeGender5
        {
            get
            {
                return _personsInHomeGender5;
            }
            set
            {
                _personsInHomeGender5 = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeGender5"));
            }
        }

        private string _personsInHomeDob5 = "NA";
        public string PersonsInHomeDob5
        {
            get
            {
                return _personsInHomeDob5;
            }
            set
            {
                _personsInHomeDob5 = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeDob5"));
            }
        }

        private string _personsInHomeRelationship6 = "None";
        public string PersonsInHomeRelationship6
        {
            get
            {
                return _personsInHomeRelationship6;
            }
            set
            {
                _personsInHomeRelationship6 = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeRelationship6"));
            }
        }

        private string _personsInHomeGender6 = "NA";
        public string PersonsInHomeGender6
        {
            get
            {
                return _personsInHomeGender6;
            }
            set
            {
                _personsInHomeGender6 = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeGender6"));
            }
        }

        private string _personsInHomeDob6 = "NA";
        public string PersonsInHomeDob6
        {
            get
            {
                return _personsInHomeDob6;
            }
            set
            {
                _personsInHomeDob6 = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeDob6"));
            }
        }

        private string _personsInHomeRelationship7 = "None";
        public string PersonsInHomeRelationship7
        {
            get
            {
                return _personsInHomeRelationship7;
            }
            set
            {
                _personsInHomeRelationship7 = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeRelationship7"));
            }
        }

        private string _personsInHomeGender7 = "NA";
        public string PersonsInHomeGender7
        {
            get
            {
                return _personsInHomeGender7;
            }
            set
            {
                _personsInHomeGender7 = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeGender7"));
            }
        }

        private string _personsInHomeDob7 = "NA";
        public string PersonsInHomeDob7
        {
            get
            {
                return _personsInHomeDob7;
            }
            set
            {
                _personsInHomeDob7 = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeDob7"));
            }
        }

        private string _personsInHomeRelationship8 = "None";
        public string PersonsInHomeRelationship8
        {
            get
            {
                return _personsInHomeRelationship8;
            }
            set
            {
                _personsInHomeRelationship8 = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeRelationship8"));
            }
        }

        private string _personsInHomeGender8 = "NA";
        public string PersonsInHomeGender8
        {
            get
            {
                return _personsInHomeGender8;
            }
            set
            {
                _personsInHomeGender8 = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeGender8"));
            }
        }

        private string _personsInHomeDob8 = "NA";
        public string PersonsInHomeDob8
        {
            get
            {
                return _personsInHomeDob8;
            }
            set
            {
                _personsInHomeDob8 = value ?? " ";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeDob8"));
            }
        }

        private bool _legal;
        public bool Legal
        {
            get
            {
                return _legal;
            }
            set
            {
                _legal = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Legal"));
            }
        }

        private bool _counseling;
        public bool Counseling
        {
            get
            {
                return _counseling;
            }
            set
            {
                _counseling = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Counseling"));
            }
        }

        private bool _shelter;
        public bool Shelter
        {
            get
            {
                return _shelter;
            }
            set
            {
                _shelter = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Shelter"));
            }
        }

        private bool _advocacy;
        public bool Advocacy
        {
            get
            {
                return _advocacy;
            }
            set
            {
                _advocacy = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Advocacy"));
            }
        }

        private bool _groupClass;
        public bool GroupClass
        {
            get
            {
                return _groupClass;
            }
            set
            {
                _groupClass = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GroupClass"));
            }
        }

        private bool _childAdvocacy;
        public bool ChildAdvocacy
        {
            get
            {
                return _childAdvocacy;
            }
            set
            {
                _childAdvocacy = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ChildAdvocacy"));
            }
        }

        private string _comments;
        public string Comments
        {
            get
            {
                return _comments;
            }
            set
            {
                _comments = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Comments"));
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

        private ObservableCollection<string> _listDates = new ObservableCollection<string> { @"results go here" }; //pid search results list
        /// <summary>
        /// Participant Advp form date for WPF databinding
        /// </summary>
        public ObservableCollection<string> ListDates//List for displaying search results of dates for advp
        {
            get
            {
                return _listDates;
            }
            set
            {
                _listDates = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ListDates"));
            }
        }
        ////////////////////////////////////////////////////////////////////// End SQL Data Sections //////////////////////////////////////////////////////////////////////


        /********************************************************************* Start Advp View Mode Methods *********************************************************************/
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
        public void Connect()
        {
            DbCommand.Connection = new OleDbConnection(Provider + Path + Password); //provider and data source path and password;
            DbCommand.Connection.Open();
        }

        /// <summary>
        /// Close connection to DB
        /// </summary>
        public static void Disconnect()
        {
            DbCommand.Connection.Close();
        }

        /// <summary>
        /// Get first name from DB with given DB and set FirstName for WPF update
        /// </summary>
        public void RunQueryFNameFromPid(string selectUpdate)
        {
            Connect();
            DbCommand.CommandText = _sql.FirstNameFromPid(selectUpdate, Pid, FirstName);
            OleDbDataReader rdr = DbCommand.ExecuteReader();
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
            DbCommand.CommandText = _sql.LastNameFromPid(selectUpdate, Pid, LastName);
            OleDbDataReader rdr = DbCommand.ExecuteReader();
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
            DbCommand.CommandText = _sql.SelectUpdateOrAdd(selectUpdateAdd, table, column, Pid, date);
            OleDbDataReader rdr = DbCommand.ExecuteReader();

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
        /// Get DOB from DB with given DB and Update LastName for WPF update
        /// </summary>
        public void QueryDateFromPid(string selectUpdateAdd, string table, string column, string pid, string dateString, ref string date)
        {
            DateTime dtDob = new DateTime();
            Connect();
            DbCommand.CommandText = _sql.SelectUpdateOrAdd(selectUpdateAdd, table, column, Pid, dateString, date);
            OleDbDataReader rdr = DbCommand.ExecuteReader();

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
            DbCommand.CommandText = _sql.FindClientPid(FirstName, LastName, Pid);
            OleDbDataReader rdr = DbCommand.ExecuteReader();
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
        public void RunQueryFindDate()
        {
            Connect();
            DbCommand.CommandText = _sql.FindClientDate(Pid);
            OleDbDataReader rdr = DbCommand.ExecuteReader();
            ListDates.Clear();
            if (rdr != null)
            {
                int rowNum;
                for (rowNum = 0; rdr.Read(); rowNum++)
                {
                    ListDates.Add((rdr.IsDBNull(0) == false) ? rdr.GetDateTime(0).ToShortDateString() : DateTime.Now.ToShortDateString());
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
            DbCommand.CommandText = query;
            OleDbDataReader rdr = DbCommand.ExecuteReader();
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
            DbCommand.CommandText = query;
            OleDbDataReader rdr = DbCommand.ExecuteReader();
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
        /// Runs the qurry passed to it for doubles
        /// </summary>
        /// <param name="target"></param>
        /// <param name="query"></param>
        public void RunQuery(ref double target, string query)
        {
            Connect();
            DbCommand.CommandText = query;
            OleDbDataReader rdr = DbCommand.ExecuteReader();
            if (rdr != null)
            {
                rdr.Read();
                if (rdr.HasRows)
                {
                    target = (rdr.IsDBNull(0) == false) ? rdr.GetDouble(0) : 0;
                }
            }
            Disconnect();
        }

        /// <summary>
        /// Add an intake date to the data base
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="date"></param>
        public void RunQueryAddDate(string pid, string date)
        {
            Connect();
            DbCommand.CommandText = _sql.AddIntakeDate(pid, date);
            DbCommand.ExecuteReader();
            ListPiDs.Clear();
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
            DbCommand.CommandText = query;
            OleDbDataReader rdr = DbCommand.ExecuteReader();
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
            DbCommand.CommandText = query;
            OleDbDataReader rdr = DbCommand.ExecuteReader();
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
            DbCommand.Connection = new OleDbConnection(Provider + Path + Password); //provider and data source path and password;
            DbCommand.Connection.Open();
            DbCommand.CommandText = query;
            DbCommand.ExecuteReader();
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
                bool value = rdr != null && rdr.HasRows;
                cmd.Connection.Close();
                return value;
            }
            catch
            {
                return false;
            }

        }

        ////////////////////////////////////////////////////////////////////// END Advp View Model Methods //////////////////////////////////////////////////////////////////////


        /********************************************************************* Start Demographics *********************************************************************/
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
            RunQuery(ref _zip, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Forms_Flow_Table", "Zip", pid, Zip));

            //decimals
            RunQuery(ref _totalMonthlyIncome, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Intake", "Total Mo Income", pid, TotalMonthlyIncome));

            //bools                                                                         
            RunQuery(ref _msgOk, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Forms_Flow_Table", "MSG_OK", pid, MsgOk));

            RunQuery(ref _veteranStatus, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Intake", "Vet", pid, VeteranStatus));

            //text                                        
            RunQuery(ref _fName, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Consumer_List_Entry", "FIRST_NAME", pid, FirstName));
            RunQuery(ref _mi, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Consumer_List_Entry", "MIDDLE_INITIAL", pid, Mi));
            RunQuery(ref _lName, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Consumer_List_Entry", "LAST_NAME", pid, LastName));
            RunQuery(ref _hmisId, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Consumer_List_Entry", "HMIS_ID", pid, HmisId));
            RunQuery(ref _infoNetId, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Consumer_List_Entry", "INFO_NET_ID", pid, InfoNetId));
            RunQuery(ref _ssn, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Consumer_List_Entry", "NO_SSN_Reason", pid, Ssn));
            RunQuery(ref _gender, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Consumer_List_Entry", "Gender", pid, Gender));


            RunQuery(ref _city, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Forms_Flow_Table", "city", pid, City));
            RunQuery(ref _state, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Forms_Flow_Table", "state", pid, State));
            RunQuery(ref _homePhone, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Forms_Flow_Table", "Home_Phone", pid, HomePhone));
            RunQuery(ref _workPhone, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Forms_Flow_Table", "Work_MSG", pid, WorkPhone));
            RunQuery(ref _callTime, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Forms_Flow_Table", "Call_Time", pid, CallTime));
            RunQuery(ref _streetAddress, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Forms_Flow_Table", "Street_Address", pid, StreetAddress));
            RunQuery(ref _maritalStatus, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Forms_Flow_Table", "Marital_Status", pid, MaritalStatus));
            RunQuery(ref _ethnicity, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Forms_Flow_Table", "Ethnicity", pid, Ethnicity));

            RunQuery(ref _staff, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Consumer_List_Entry", "Staff", pid, Staff));
            RunQuery(ref _housingStatus, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Consumer_List_Entry", "CurrentlyLivingIn", pid, HousingStatus));
            RunQuery(ref _neighborhood, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Consumer_List_Entry", "SpokaneCity", pid, Neighborhood));
            RunQuery(ref _countyDetail, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Consumer_List_Entry", "SpokaneCounty", pid, CountyDetail));
            RunQuery(ref _disability, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Consumer_List_Entry", "AdultDisability", pid, Disability));
            RunQuery(ref _secondDisability, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Consumer_List_Entry", "AdultDisabilitySecond", pid, SecondDisability));
            RunQuery(ref _incomeType, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Consumer_List_Entry", "income1", pid, IncomeType));
        }
        ////////////////////////////////////////////////////////////////////// END Demographics //////////////////////////////////////////////////////////////////////

        /********************************************************************* Start Advp *********************************************************************/

        public void Advp(string selectUpdateAdd, string pid, string date)
        {
            //Doubles
            TotalAdultsInHousehold = Convert.ToDouble(TotalAdultsInHouseholdString);
            TotalChildrenInHousehold = Convert.ToDouble(TotalChildrenInHouseholdString);
            RunQuery(ref _totalAdultsInHousehold, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Intake", "Tot Adults Household", pid, date, TotalAdultsInHousehold));
            TotalAdultsInHouseholdString = TotalAdultsInHousehold.ToString(CultureInfo.InvariantCulture);
            RunQuery(ref _totalChildrenInHousehold, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Intake", "Tot Child Household", pid, date, TotalChildrenInHousehold));
            TotalChildrenInHouseholdString = TotalChildrenInHousehold.ToString(CultureInfo.InvariantCulture);

            //Participant relationships
            RunQuery(ref _personsInHomeRelationship, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Intake", "Adult 2 Relationship", pid, date, PersonsInHomeRelationship));
            PersonsInHomeRelationship = _personsInHomeRelationship;
            RunQuery(ref _personsInHomeGender, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Intake", "Adult 2 Sex", pid, date, PersonsInHomeGender));
            PersonsInHomeGender = _personsInHomeGender;
            QueryDateFromPid(selectUpdateAdd, "tbl_Intake", "Adult 2 DOB", _personsInHomeDob, IntakeDate, ref _personsInHomeDob);
            PersonsInHomeDob = _personsInHomeDob;

            RunQuery(ref _personsInHomeRelationship2, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Intake", "Child 1 Relationship", pid, date, PersonsInHomeRelationship2));
            PersonsInHomeRelationship2 = _personsInHomeRelationship2;
            RunQuery(ref _personsInHomeGender2, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Intake", "Child 1 Sex", pid, date, PersonsInHomeGender2));
            PersonsInHomeGender2 = _personsInHomeGender2;
            QueryDateFromPid(selectUpdateAdd, "tbl_Intake", "Child 1 DOB", _personsInHomeDob2, IntakeDate, ref _personsInHomeDob2);
            PersonsInHomeDob2 = _personsInHomeDob2;

            RunQuery(ref _personsInHomeRelationship3, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Intake", "Child 2 Relationship", pid, date, PersonsInHomeRelationship3));
            PersonsInHomeRelationship3 = _personsInHomeRelationship3;
            RunQuery(ref _personsInHomeGender3, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Intake", "Child 2 Sex", pid, date, PersonsInHomeGender3));
            PersonsInHomeGender3 = _personsInHomeGender3;
            QueryDateFromPid(selectUpdateAdd, "tbl_Intake", "Child 2 DOB", _personsInHomeDob3, IntakeDate, ref _personsInHomeDob3);
            PersonsInHomeDob3 = _personsInHomeDob3;

            RunQuery(ref _personsInHomeRelationship4, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Intake", "Child 3 Relationship", pid, date, PersonsInHomeRelationship4));
            PersonsInHomeRelationship4 = _personsInHomeRelationship4;
            RunQuery(ref _personsInHomeGender4, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Intake", "Child 3 Sex", pid, date, PersonsInHomeGender4));
            PersonsInHomeGender4 = _personsInHomeGender4;
            QueryDateFromPid(selectUpdateAdd, "tbl_Intake", "Child 3 DOB", _personsInHomeDob4, IntakeDate, ref _personsInHomeDob4);
            PersonsInHomeDob4 = _personsInHomeDob4;

            RunQuery(ref _personsInHomeRelationship5, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Intake", "Child 4 Relationship", pid, date, PersonsInHomeRelationship5));
            PersonsInHomeRelationship5 = _personsInHomeRelationship5;
            RunQuery(ref _personsInHomeGender5, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Intake", "Child 4 Sex", pid, date, PersonsInHomeGender5));
            PersonsInHomeGender5 = _personsInHomeGender5;
            QueryDateFromPid(selectUpdateAdd, "tbl_Intake", "Child 4 DOB", _personsInHomeDob5, IntakeDate, ref _personsInHomeDob5);
            PersonsInHomeDob5 = _personsInHomeDob5;

            RunQuery(ref _personsInHomeRelationship6, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Intake", "Child 5 Relationship", pid, date, PersonsInHomeRelationship6));
            PersonsInHomeRelationship6 = _personsInHomeRelationship6;
            RunQuery(ref _personsInHomeGender6, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Intake", "Child 5 Sex", pid, date, PersonsInHomeGender6));
            PersonsInHomeGender6 = _personsInHomeGender6;
            QueryDateFromPid(selectUpdateAdd, "tbl_Intake", "Child 5 DOB", _personsInHomeDob6, IntakeDate, ref _personsInHomeDob6);
            PersonsInHomeDob6 = _personsInHomeDob6;

            RunQuery(ref _personsInHomeRelationship7, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Intake", "Child 6 Relationship", pid, date, PersonsInHomeRelationship7));
            PersonsInHomeRelationship7 = _personsInHomeRelationship7;
            RunQuery(ref _personsInHomeGender7, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Intake", "Child 6 Sex", pid, date, PersonsInHomeGender7));
            PersonsInHomeGender7 = _personsInHomeGender7;
            QueryDateFromPid(selectUpdateAdd, "tbl_Intake", "Child 6 DOB", _personsInHomeDob7, IntakeDate, ref _personsInHomeDob7);
            PersonsInHomeDob7 = _personsInHomeDob7;

            RunQuery(ref _personsInHomeRelationship8, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Intake", "Child 7 Relationship", pid, date, PersonsInHomeRelationship8));
            PersonsInHomeRelationship8 = _personsInHomeRelationship8;
            RunQuery(ref _personsInHomeGender8, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Intake", "Child 7 Sex", pid, date, PersonsInHomeGender8));
            PersonsInHomeGender8 = _personsInHomeGender8;
            QueryDateFromPid(selectUpdateAdd, "tbl_Intake", "Child 7 DOB", _personsInHomeDob8, IntakeDate, ref _personsInHomeDob8);
            PersonsInHomeDob8 = _personsInHomeDob8;

            //Discharge
            QueryDateFromPid(selectUpdateAdd, "tbl_Intake", "Discharge Date", _dischargeDate, IntakeDate, ref _dischargeDate);
            DischargeDate = _dischargeDate;

            RunQuery(ref _dischargeLocation, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Intake", "DischargeLocation", pid, date, DischargeLocation));
            DischargeLocation = _dischargeLocation;

            RunQuery(ref _comments, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_Intake", "Comments", pid, date, Comments));
            Comments = _comments;

            RunQuery(ref _counseling, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_intake", "Outreach", pid, _counseling));
            Counseling = _counseling;

            RunQuery(ref _shelter, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_intake", "YWCA Shelter", pid, _shelter));
            Shelter = _shelter;

            RunQuery(ref _legal, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_intake", "Legal Office", pid, _legal));
            Legal = _legal;

            RunQuery(ref _advocacy, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_intake", "Advocacy", pid, _advocacy));
            Advocacy = _advocacy;

            RunQuery(ref _childAdvocacy, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_intake", "ChildAdvocacy", pid, _childAdvocacy));
            ChildAdvocacy = _childAdvocacy;

            RunQuery(ref _groupClass, _sql.SelectUpdateOrAdd(selectUpdateAdd, "tbl_intake", "GroupClass", pid, _groupClass));
            GroupClass = _groupClass;
        }
        ////////////////////////////////////////////////////////////////////// END Advp //////////////////////////////////////////////////////////////////////
    }
}
