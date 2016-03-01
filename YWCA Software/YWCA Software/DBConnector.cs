using System;
using System.Collections;
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
        private readonly Sql _sql = new Sql();
        static readonly OleDbCommand DbCommand = new OleDbCommand(); //commander for database
        public event PropertyChangedEventHandler PropertyChanged; //event handler for data binding to WPF

        /********************************************************************* Start Const Strings For DB Access *********************************************************************/
        public const string Provider = @"Provider=Microsoft.ACE.OLEDB.12.0;";

        public const string Path = @"Data Source=" +
                                    //@"P:\ywcaDbSoftware\" +
                                    @"C:\YWCADB\All\" + //for local debuging
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

        private string _dateNow = DateTime.Now.ToShortDateString();
        public string DateNow
        {
            get
            {
                return _dateNow;
            }
            set
            {
                _dateNow = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DateNow"));
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

        private int _zip = 55555;
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

        private string _dischargeDate = "NoInfo";
        public string DischargeDate
        {
            get
            {
                return _dischargeDate;
            }
            set
            {
                _dischargeDate = value ?? "NoInfo";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DischargeDate"));
            }
        }

        private string _dischargeLocation = "NoInfo";
        public string DischargeLocation
        {
            get
            {
                return _dischargeLocation;
            }
            set
            {
                _dischargeLocation = value ?? "NoInfo";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DischargeLocation"));
            }
        }

        private string _personsInHomeRelationship = "NoInfo";
        public string PersonsInHomeRelationship
        {
            get
            {
                return _personsInHomeRelationship;
            }
            set
            {
                _personsInHomeRelationship = value ?? "NoInfo";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeRelationship"));
            }
        }

        private string _personsInHomeGender = "NoInfo";
        public string PersonsInHomeGender
        {
            get
            {
                return _personsInHomeGender;
            }
            set
            {
                _personsInHomeGender = value ?? "NoInfo";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeGender"));
            }
        }

        private string _personsInHomeDob = "NoInfo";
        public string PersonsInHomeDob
        {
            get
            {
                return _personsInHomeDob;
            }
            set
            {
                _personsInHomeDob = value ?? "NoInfo";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeDob"));
            }
        }

        private string _personsInHomeRelationship2 = "NoInfo";
        public string PersonsInHomeRelationship2
        {
            get
            {
                return _personsInHomeRelationship2;
            }
            set
            {
                _personsInHomeRelationship2 = value ?? "NoInfo";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeRelationship2"));
            }
        }

        private string _personsInHomeGender2 = "NoInfo";
        public string PersonsInHomeGender2
        {
            get
            {
                return _personsInHomeGender2;
            }
            set
            {
                _personsInHomeGender2 = value ?? "NoInfo";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeGender2"));
            }
        }

        private string _personsInHomeDob2 = "NoInfo";
        public string PersonsInHomeDob2
        {
            get
            {
                return _personsInHomeDob2;
            }
            set
            {
                _personsInHomeDob2 = value ?? "NoInfo";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeDob2"));
            }
        }

        private string _personsInHomeRelationship3 = "NoInfo";
        public string PersonsInHomeRelationship3
        {
            get
            {
                return _personsInHomeRelationship3;
            }
            set
            {
                _personsInHomeRelationship3 = value ?? "NoInfo";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeRelationship3"));
            }
        }

        private string _personsInHomeGender3 = "NoInfo";
        public string PersonsInHomeGender3
        {
            get
            {
                return _personsInHomeGender3;
            }
            set
            {
                _personsInHomeGender3 = value ?? "NoInfo";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeGender3"));
            }
        }

        private string _personsInHomeDob3 = "NoInfo";
        public string PersonsInHomeDob3
        {
            get
            {
                return _personsInHomeDob3;
            }
            set
            {
                _personsInHomeDob3 = value ?? "NoInfo";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeDob3"));
            }
        }

        private string _personsInHomeRelationship4 = "NoInfo";
        public string PersonsInHomeRelationship4
        {
            get
            {
                return _personsInHomeRelationship4;
            }
            set
            {
                _personsInHomeRelationship4 = value ?? "NoInfo";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeRelationship4"));
            }
        }

        private string _personsInHomeGender4 = "NoInfo";
        public string PersonsInHomeGender4
        {
            get
            {
                return _personsInHomeGender4;
            }
            set
            {
                _personsInHomeGender4 = value ?? "NoInfo";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeGender4"));
            }
        }

        private string _personsInHomeDob4 = "NoInfo";
        public string PersonsInHomeDob4
        {
            get
            {
                return _personsInHomeDob4;
            }
            set
            {
                _personsInHomeDob4 = value ?? "NoInfo";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeDob4"));
            }
        }

        private string _personsInHomeRelationship5 = "NoInfo";
        public string PersonsInHomeRelationship5
        {
            get
            {
                return _personsInHomeRelationship5;
            }
            set
            {
                _personsInHomeRelationship5 = value ?? "NoInfo";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeRelationship5"));
            }
        }

        private string _personsInHomeGender5 = "NoInfo";
        public string PersonsInHomeGender5
        {
            get
            {
                return _personsInHomeGender5;
            }
            set
            {
                _personsInHomeGender5 = value ?? "NoInfo";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeGender5"));
            }
        }

        private string _personsInHomeDob5 = "NoInfo";
        public string PersonsInHomeDob5
        {
            get
            {
                return _personsInHomeDob5;
            }
            set
            {
                _personsInHomeDob5 = value ?? "NoInfo";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeDob5"));
            }
        }

        private string _personsInHomeRelationship6 = "NoInfo";
        public string PersonsInHomeRelationship6
        {
            get
            {
                return _personsInHomeRelationship6;
            }
            set
            {
                _personsInHomeRelationship6 = value ?? "NoInfo";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeRelationship6"));
            }
        }

        private string _personsInHomeGender6 = "NoInfo";
        public string PersonsInHomeGender6
        {
            get
            {
                return _personsInHomeGender6;
            }
            set
            {
                _personsInHomeGender6 = value ?? "NoInfo";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeGender6"));
            }
        }

        private string _personsInHomeDob6 = "NoInfo";
        public string PersonsInHomeDob6
        {
            get
            {
                return _personsInHomeDob6;
            }
            set
            {
                _personsInHomeDob6 = value ?? "NoInfo";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeDob6"));
            }
        }

        private string _personsInHomeRelationship7 = "NoInfo";
        public string PersonsInHomeRelationship7
        {
            get
            {
                return _personsInHomeRelationship7;
            }
            set
            {
                _personsInHomeRelationship7 = value ?? "NoInfo";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeRelationship7"));
            }
        }

        private string _personsInHomeGender7 = "NoInfo";
        public string PersonsInHomeGender7
        {
            get
            {
                return _personsInHomeGender7;
            }
            set
            {
                _personsInHomeGender7 = value ?? "NoInfo";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeGender7"));
            }
        }

        private string _personsInHomeDob7 = "NoInfo";
        public string PersonsInHomeDob7
        {
            get
            {
                return _personsInHomeDob7;
            }
            set
            {
                _personsInHomeDob7 = value ?? "NoInfo";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeDob7"));
            }
        }

        private string _personsInHomeRelationship8 = "NoInfo";
        public string PersonsInHomeRelationship8
        {
            get
            {
                return _personsInHomeRelationship8;
            }
            set
            {
                _personsInHomeRelationship8 = value ?? "NoInfo";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeRelationship8"));
            }
        }

        private string _personsInHomeGender8 = "NoInfo";
        public string PersonsInHomeGender8
        {
            get
            {
                return _personsInHomeGender8;
            }
            set
            {
                _personsInHomeGender8 = value ?? "NoInfo";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonsInHomeGender8"));
            }
        }

        private string _personsInHomeDob8 = "NoInfo";
        public string PersonsInHomeDob8
        {
            get
            {
                return _personsInHomeDob8;
            }
            set
            {
                _personsInHomeDob8 = value ?? "NoInfo";
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
                _mi = value ?? "NoInfo";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Mi"));
            }
        }

        private string _gender = "NoInfo";
        public string Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value ?? "NoInfo";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Gender"));
            }
        }

        private string _maritalStatus = "NoInfo";
        public string MaritalStatus
        {
            get
            {
                return _maritalStatus;
            }
            set
            {
                _maritalStatus = value ?? "NoInfo";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MaritalStatus"));
            }
        }

        private string _ethnicity = "NoInfo";
        public string Ethnicity
        {
            get
            {
                return _ethnicity;
            }
            set
            {
                _ethnicity = value ?? "NoInfo";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Ethnicity"));
            }
        }

        private string _disability = "NoInfo";
        public string Disability
        {
            get
            {
                return _disability;
            }
            set
            {
                _disability = value ?? "NoInfo";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Disability"));
            }
        }

        private string _secondDisability = "NoInfo";
        public string SecondDisability
        {
            get
            {
                return _secondDisability;
            }
            set
            {
                _secondDisability = value ?? "NoInfo";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SecondDisability"));
            }
        }

        private string _incomeType = "NoInfo";
        public string IncomeType
        {
            get
            {
                return _incomeType;
            }
            set
            {
                _incomeType = value ?? "NoInfo";
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
                _homePhone = value ?? "NoInfo";
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
                _workPhone = value ?? "NoInfo";
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
                _callTime = value ?? "NoInfo";
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
                _streetAddress = value ?? "NoInfo";
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
                _city = value ?? "NoInfo";
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
                _state = value ?? "NoInfo";
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
                _housingStatus = value ?? "NoInfo";
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
                _neighborhood = value ?? "NoInfo";
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
                _countyDetail = value ?? "NoInfo";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CountyDetail"));
            }
        }

        private string _staff = "NoInfo";
        public string Staff
        {
            get
            {
                return _staff;
            }
            set
            {
                _staff = value ?? "NoInfo";
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
                _ssn = value ?? "NoInfo";
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
                _fName = value ?? "NoInfo";
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
                _lName = value ?? "NoInfo";
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
                _pid = value ?? "NoInfo";
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

        private string _hmisId = "NoInfo";
        public string HmisId
        {
            get
            {
                return _hmisId;
            }
            set
            {
                _hmisId = value ?? "NoInfo";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HmisId"));
            }
        }

        private string _infoNetId = "NoInfo";
        public string InfoNetId
        {
            get
            {
                return _infoNetId;
            }
            set
            {
                _infoNetId = value ?? "NoInfo";
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

        public static bool CheckForExistingPid(string pid)
        {
            bool exists = true;
            DbCommand.Connection = new OleDbConnection(Provider + Path + Password); //provider and data source path and password;
            DbCommand.Connection.Open();
            DbCommand.CommandText = "SELECT Consumer_ID FROM tbl_Consumer_List_Entry WHERE Consumer_ID = '" + pid + "';";
            var oleDbDataReader = DbCommand.ExecuteReader();
            if (oleDbDataReader != null && oleDbDataReader.HasRows == false)
            {
                exists = false;
            }
            Disconnect();
            return exists;
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
                    target = (rdr.IsDBNull(0) == false) ? rdr.GetInt32(0) : 0;
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

        /// <summary>
        /// Used to test if table contains specified query data
        /// </summary>
        /// <param name="query"></param>
        /// <returns>false if is empty</returns>
        private ArrayList BoolQuery(string query)
        {
            ArrayList target = new ArrayList();
            Connect();
            DbCommand.CommandText = query;
            OleDbDataReader rdr = DbCommand.ExecuteReader();
            ListPiDs.Clear();
            if (rdr != null)
            {
                int rowNum;
                for (rowNum = 0; rdr.Read(); rowNum++)
                {
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        target.Add((rdr.IsDBNull(i) == false) && rdr.GetBoolean(i));
                    }
                }
            }
            Disconnect();
            return target;
        }

        /// <summary>
        /// Used to test if table contains specified query data
        /// </summary>
        /// <param name="query"></param>
        /// <returns>false if is empty</returns>
        private ArrayList DateQuery(string query)
        {
            ArrayList target = new ArrayList();
            Connect();
            DbCommand.CommandText = query;
            OleDbDataReader rdr = DbCommand.ExecuteReader();
            ListPiDs.Clear();
            if (rdr != null)
            {
                int rowNum;
                for (rowNum = 0; rdr.Read(); rowNum++)
                {
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        target.Add((rdr.IsDBNull(i) == false) ? rdr.GetDateTime(i).ToShortDateString() : "NoInfo");
                    }
                }
            }
            Disconnect();
            return target;
        }

        /// <summary>
        /// Runs the qurry passed to it
        /// </summary>
        /// <param name="query"></param>
        private ArrayList IntQuery(string query)
        {
            ArrayList target = new ArrayList();
            Connect();
            DbCommand.CommandText = query;
            OleDbDataReader rdr = DbCommand.ExecuteReader();
            ListPiDs.Clear();
            if (rdr != null)
            {
                int rowNum;
                for (rowNum = 0; rdr.Read(); rowNum++)
                {
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        target.Add((rdr.IsDBNull(0) == false) ? rdr.GetInt32(0) : 0);
                    }
                }
            }
            Disconnect();
            return target;
        }

        /// <summary>
        /// Runs the qurry passed to it
        /// </summary>
        /// <param name="query"></param>
        private ArrayList DoubleQuery(string query)
        {
            ArrayList target = new ArrayList();
            Connect();
            DbCommand.CommandText = query;
            OleDbDataReader rdr = DbCommand.ExecuteReader();
            if (rdr != null)
            {
                int rowNum;
                for (rowNum = 0; rdr.Read(); rowNum++)
                {
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        target.Add((rdr.IsDBNull(0) == false) ? rdr.GetDouble(0) : 0);
                    }
                }
            }
            Disconnect();
            return target;
        }

        /// <summary>
        /// Runs the qurry passed to it
        /// </summary>
        /// <param name="query"></param>
        private ArrayList DecimalQuery(string query)
        {
            ArrayList target = new ArrayList();
            Connect();
            DbCommand.CommandText = query;
            OleDbDataReader rdr = DbCommand.ExecuteReader();
            ListPiDs.Clear();
            if (rdr != null)
            {
                int rowNum;
                for (rowNum = 0; rdr.Read(); rowNum++)
                {
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        target.Add((rdr.IsDBNull(i) == false) ? rdr.GetDecimal(i) : 0);
                    }
                }
            }
            Disconnect();
            return target;
        }

        /// <summary>
        /// Runs the qurry passed to it
        /// </summary>
        /// <param name="query"></param>
        private ArrayList StringQuery(string query)
        {
            ArrayList target = new ArrayList();
            Connect();
            DbCommand.CommandText = query;
            OleDbDataReader rdr = DbCommand.ExecuteReader();
            ListPiDs.Clear();
            if (rdr != null)
            {
                int rowNum;
                for (rowNum = 0; rdr.Read(); rowNum++)
                {
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        target.Add((rdr.IsDBNull(i) == false) ? rdr.GetString(i) : "NoInfo");
                    }
                }
            }
            Disconnect();
            return target;
        }

        /// <summary>
        /// Used to test if table contains specified query data
        /// </summary>
        /// <param name="query"></param>
        /// <returns>false if is empty</returns>
        private void UpdateQuery(string query)
        {
            Connect();
            DbCommand.CommandText = query;
            DbCommand.ExecuteReader();
            Disconnect();
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
            ArrayList queryArray;
            if (selectUpdateAdd == "update")
            {
                //Dates
                UpdateQuery
                    (
                    "UPDATE " +
                        "tbl_Consumer_List_Entry " +
                    "SET " +
                        "DOB = " + DumpNoInfo(Dob) + ", " +
                        "LastUpdated = " + DumpNoInfo(DateDataEntered) + "  " +
                    "Where " +
                        "Consumer_ID = \"" + Pid + "\";"
                    );
                //Ints
                UpdateQuery
                    (
                    "UPDATE " +
                        "tbl_Forms_Flow_Table " +
                    "SET " +
                        "Zip = \"" + Zip + "\"  " +
                    "Where " +
                        "Consumer_ID = \"" + Pid + "\";"
                    );
                //Decimal
                UpdateQuery
                    (
                    "UPDATE " +
                        "tbl_Consumer_List_Entry " +
                    "SET " +
                        "TotalMoIncome = \"" + TotalMonthlyIncome + "\"  " +
                    "Where " +
                        "Consumer_ID = \"" + Pid + "\";"
                    );
                //bools
                UpdateQuery
                    (
                    "UPDATE " +
                        "tbl_Consumer_List_Entry " +
                    "SET " +
                        "VET = " + VeteranStatus + "  " +
                    "Where " +
                        "Consumer_ID = \"" + Pid + "\";"
                    );

                UpdateQuery
                    (
                    "UPDATE " +
                        "tbl_Forms_Flow_Table " +
                    "SET " +
                        "MSG_OK = " + MsgOk + "  " +
                    "Where " +
                        "Consumer_ID = \"" + Pid + "\";"
                    );
                //String
                UpdateQuery
                    (
                     "UPDATE " +
                        "tbl_Consumer_List_Entry " +
                    "SET " +
                       "FIRST_NAME = \"" + FirstName + "\", " +
                       "MIDDLE_INITIAL = \"" + Mi + "\", " +
                       "LAST_NAME = \"" + LastName + "\", " +
                       "HMIS_ID = \"" + HmisId + "\", " +
                       "INFO_NET_ID = \"" + InfoNetId + "\", " +
                       "NO_SSN_Reason = \"" + Ssn + "\", " +
                       "Gender = \"" + Gender + "\", " +
                       "Staff = \"" + Staff + "\", " +
                       "CurrentlyLivingIn = \"" + HousingStatus + "\", " +
                       "SpokaneCity = \"" + Neighborhood + "\", " +
                       "SpokaneCounty = \"" + CountyDetail + "\", " +
                       "AdultDisability = \"" + Disability + "\", " +
                       "AdultDisabilitySecond = \"" + SecondDisability + "\", " +
                       "income1 = \"" + IncomeType + "\"  " +
                    "Where " +
                        "Consumer_ID = \"" + Pid + "\";"
                    );

                UpdateQuery
                    (
                     "UPDATE " +
                        "tbl_Forms_Flow_Table " +
                    "SET " +
                       "city = \"" + City + "\", " +
                       "state = \"" + State + "\", " +
                       "Home_Phone = \"" + HomePhone + "\", " +
                       "Work_MSG = \"" + WorkPhone + "\", " +
                       "Call_Time = \"" + CallTime + "\", " +
                       "Street_Address = \"" + StreetAddress + "\", " +
                       "Marital_Status = \"" + MaritalStatus + "\", " +
                       "Ethnicity = \"" + Ethnicity + "\"  " +
                    "Where " +
                        "Consumer_ID = \"" + Pid + "\";"
                    );
            }
            else
            {
                //Dates
                queryArray = DateQuery
                    (
                    "SELECT " +
                        " DOB, " +
                        "LastUpdated " +
                    "FROM " +
                        "tbl_Consumer_List_Entry " +
                    "Where " +
                        "Consumer_ID = \"" + Pid + "\";"
                    );
                if (queryArray.Count > 0)
                {
                    Dob = queryArray[0].ToString();
                    DateDataEntered = queryArray[1].ToString();
                }
                //Ints
                queryArray = IntQuery
                    (
                    "SELECT " +
                        " Zip " +
                     "FROM " +
                        "tbl_Forms_Flow_Table " +
                    "Where " +
                        "Consumer_ID = \"" + Pid + "\";"
                    );
                if (queryArray.Count > 0)
                {
                    Zip = (int)queryArray[0];
                }
                //Decimals
                queryArray = DecimalQuery
                    (
                    "SELECT " +
                        " TotalMoIncome " +
                     "FROM " +
                        "tbl_Consumer_List_Entry " +
                    "Where " +
                        "Consumer_ID = \"" + Pid + "\";"
                    );
                if (queryArray.Count > 0)
                {
                    TotalMonthlyIncome = (decimal)queryArray[0];
                }
                //bools
                queryArray = BoolQuery
                    (
                    "SELECT " +
                        " tbl_Forms_Flow_Table.MSG_OK, " +
                        "tbl_Consumer_List_Entry.Vet " +
                    "FROM " +
                        "tbl_Consumer_List_Entry, " +
                        "tbl_Forms_Flow_Table " +
                    "WHERE " +
                        "tbl_Forms_Flow_Table.Consumer_ID = \"" + Pid + "\" AND tbl_Consumer_List_Entry.Consumer_ID = \"" + Pid + "\";"
                    );
                if (queryArray.Count > 0)
                {
                    MsgOk = (bool)queryArray[0];
                    VeteranStatus = (bool)queryArray[1];
                }
                //String
                queryArray = StringQuery
                    (
                    "SELECT " +
                        "tbl_Consumer_List_Entry.FIRST_NAME, " +
                        "tbl_Consumer_List_Entry.MIDDLE_INITIAL, " +
                        "tbl_Consumer_List_Entry.LAST_NAME, " +
                        "tbl_Consumer_List_Entry.HMIS_ID, " +
                        "tbl_Consumer_List_Entry.INFO_NET_ID, " +
                        "tbl_Consumer_List_Entry.NO_SSN_Reason, " +
                        "tbl_Consumer_List_Entry.Gender, " +
                        "tbl_Consumer_List_Entry.Staff, " +
                        "tbl_Consumer_List_Entry.CurrentlyLivingIn, " +
                        "tbl_Consumer_List_Entry.SpokaneCity, " +
                        "tbl_Consumer_List_Entry.SpokaneCounty, " +
                        "tbl_Consumer_List_Entry.AdultDisability, " +
                        "tbl_Consumer_List_Entry.AdultDisabilitySecond, " +
                        "tbl_Consumer_List_Entry.income1, " +
                        " tbl_Forms_Flow_Table.city, " +
                        " tbl_Forms_Flow_Table.state, " +
                        " tbl_Forms_Flow_Table.Home_Phone, " +
                        " tbl_Forms_Flow_Table.Work_MSG, " +
                        " tbl_Forms_Flow_Table.Call_Time, " +
                        " tbl_Forms_Flow_Table.Street_Address, " +
                        " tbl_Forms_Flow_Table.Marital_Status, " +
                        " tbl_Forms_Flow_Table.Ethnicity " +
                    "FROM " +
                        "tbl_Consumer_List_Entry, " +
                        "tbl_Forms_Flow_Table " +
                    "WHERE " +
                        "tbl_Forms_Flow_Table.Consumer_ID = \"" + Pid + "\" AND tbl_Consumer_List_Entry.Consumer_ID = \"" + Pid + "\";"
                    );
                if (queryArray.Count > 0)
                {
                    FirstName = (string)queryArray[0];
                    Mi = (string)queryArray[1];
                    LastName = (string)queryArray[2];
                    HmisId = (string)queryArray[3];
                    InfoNetId = (string)queryArray[4];
                    Ssn = (string)queryArray[5];
                    Gender = (string)queryArray[6];
                    Staff = (string)queryArray[7];
                    HousingStatus = (string)queryArray[8];
                    Neighborhood = (string)queryArray[9];
                    CountyDetail = (string)queryArray[10];
                    Disability = (string)queryArray[11];
                    SecondDisability = (string)queryArray[12];
                    IncomeType = (string)queryArray[13];
                    City = (string)queryArray[14];
                    State = (string)queryArray[15];
                    HomePhone = (string)queryArray[16];
                    WorkPhone = (string)queryArray[17];
                    CallTime = (string)queryArray[18];
                    StreetAddress = (string)queryArray[19];
                    MaritalStatus = (string)queryArray[20];
                    Ethnicity = (string)queryArray[21];
                }
            }
        }

        private string DumpNoInfo(string noInfo)
        {
            return (noInfo == "NoInfo") ? "NULL" : noInfo;
        }
        ////////////////////////////////////////////////////////////////////// END Demographics //////////////////////////////////////////////////////////////////////

        /********************************************************************* Start Advp *********************************************************************/

        public void Advp(string selectUpdateAdd, string pid, string date)
        {
            ArrayList queryArray;
            if (selectUpdateAdd == "update")
            {
                //Double
                UpdateQuery
                    (
                    "UPDATE " +
                        "tbl_Intake " +
                    "SET " +
                        "[Tot Adults Household] = \"" + TotalAdultsInHouseholdString + "\", " +
                        "[Tot Child Household] = \"" + TotalChildrenInHouseholdString + "\"  " +
                    "Where " +
                       "Date = #" + date + "# " +
                       "AND " +
                       "Consumer_ID = \"" + Pid + "\";"
                    );
                //Dates
                UpdateQuery
                    (
                    "UPDATE " +
                        "tbl_Intake " +
                    "SET " +
                        "[Adult 2 DOB] = " + DumpNoInfo(PersonsInHomeDob) + ", " +
                        "[Child 1 DOB] = " + DumpNoInfo(PersonsInHomeDob2) + ", " +
                        "[Child 2 DOB] = " + DumpNoInfo(PersonsInHomeDob3) + ", " +
                        "[Child 3 DOB] = " + DumpNoInfo(PersonsInHomeDob4) + ", " +
                        "[Child 4 DOB] = " + DumpNoInfo(PersonsInHomeDob5) + ", " +
                        "[Child 5 DOB] = " + DumpNoInfo(PersonsInHomeDob6) + ", " +
                        "[Child 6 DOB] = " + DumpNoInfo(PersonsInHomeDob7) + ", " +
                        "[Discharge Date] = " + DumpNoInfo(DischargeDate) + "  " +
                    "Where " +
                       "Date = #" + date + "# " +
                       "AND " +
                       "Consumer_ID = \"" + Pid + "\";"
                    );
                //bools
                UpdateQuery
                    (
                    "UPDATE " +
                        "tbl_Intake " +
                    "SET " +
                        "Outreach = " + Counseling + ", " +
                        "[YWCA Shelter] = " + Shelter + ", " +
                        "[Legal Office] = " + Legal + ", " +
                        "Advocacy = " + Advocacy + ", " +
                        "ChildAdvocacy = " + ChildAdvocacy + ", " +
                        "GroupClass = " + GroupClass + "  " +
                    "Where " +
                       "Date = #" + date + "# " +
                       "AND " +
                       "Consumer_ID = \"" + Pid + "\";"
                    );
                //String
                UpdateQuery
                    (
                     "UPDATE " +
                        "tbl_Intake " +
                    "SET " +
                       "[Adult 2 Relationship] = \"" + PersonsInHomeRelationship + "\", " +
                       "[Adult 2 Sex] = \"" + PersonsInHomeGender + "\", " +
                       "[Child 1 Relationship] = \"" + PersonsInHomeRelationship2 + "\", " +
                       "[Child 1 Sex] = \"" + PersonsInHomeGender2 + "\", " +
                       "[Child 2 Relationship] = \"" + PersonsInHomeRelationship3 + "\", " +
                       "[Child 2 Sex] = \"" + PersonsInHomeGender3 + "\", " +
                       "[Child 3 Relationship] = \"" + PersonsInHomeRelationship4 + "\", " +
                       "[Child 3 Sex] = \"" + PersonsInHomeGender4 + "\", " +
                       "[Child 4 Relationship] = \"" + PersonsInHomeRelationship5 + "\", " +
                       "[Child 4 Sex] = \"" + PersonsInHomeGender5 + "\", " +
                       "[Child 5 Relationship] = \"" + PersonsInHomeRelationship6 + "\", " +
                       "[Child 5 Sex] = \"" + PersonsInHomeGender6 + "\", " +
                       "[Child 6 Relationship] = \"" + PersonsInHomeRelationship7 + "\", " +
                       "[Child 6 Sex] = \"" + PersonsInHomeGender7 + "\", " +
                       "[Child 7 Relationship]  = \"" + PersonsInHomeRelationship8 + "\", " +
                       "[Child 7 Sex] = \"" + PersonsInHomeGender8 + "\", " +
                       "[DischargeLocation] = \"" + DischargeLocation + "\", " +
                       "[Comments] = \"" + Comments + "\"  " +
                    "Where " +
                       "Date = #" + date + "# " +
                       "AND " +
                       "Consumer_ID = \"" + Pid + "\";"
                    );
            }
            else
            {
                //doubles
                queryArray = DoubleQuery
                    (
                    "SELECT " +
                        " [Tot Adults Household], " +
                        "[Tot Child Household] " +
                     "FROM " +
                        "tbl_Intake " +
                    "Where " +
                       "Date = #" + date + "# " +
                       "AND " +
                       "Consumer_ID = \"" + Pid + "\";"
                    );
                if (queryArray.Count > 0)
                {
                    TotalAdultsInHouseholdString = queryArray[0].ToString();
                    TotalChildrenInHouseholdString = queryArray[1].ToString();
                }
                else
                {
                    TotalAdultsInHouseholdString = "NoInfo";
                    TotalChildrenInHouseholdString = "NoInfo";


                }
                //Dates
                queryArray = DateQuery
                    (
                    "SELECT " +
                        "[Adult 2 DOB], " +
                        "[Child 1 DOB], " +
                        "[Child 2 DOB], " +
                        "[Child 3 DOB], " +
                        "[Child 4 DOB], " +
                        "[Child 5 DOB], " +
                        "[Child 6 DOB], " +
                        "[Discharge Date] " +
                     "FROM " +
                        "tbl_Intake " +
                    "Where " +
                       "Date = #" + date + "# " +
                       "AND " +
                       "Consumer_ID = \"" + Pid + "\";"
                    );
                if (queryArray.Count > 0)
                {
                    PersonsInHomeDob = queryArray[0].ToString();
                    PersonsInHomeDob2 = queryArray[1].ToString();
                    PersonsInHomeDob3 = queryArray[2].ToString();
                    PersonsInHomeDob4 = queryArray[3].ToString();
                    PersonsInHomeDob5 = queryArray[4].ToString();
                    PersonsInHomeDob6 = queryArray[5].ToString();
                    PersonsInHomeDob7 = queryArray[6].ToString();
                    DischargeDate = queryArray[7].ToString();
                }
                else
                {
                    PersonsInHomeDob = "NoInfo";
                    PersonsInHomeDob2 = "NoInfo";
                    PersonsInHomeDob3 = "NoInfo";
                    PersonsInHomeDob4 = "NoInfo";
                    PersonsInHomeDob5 = "NoInfo";
                    PersonsInHomeDob6 = "NoInfo";
                    PersonsInHomeDob7 = "NoInfo";
                    DischargeDate = "NoInfo";
                }
                //bools
                queryArray = BoolQuery
                    (
                    "SELECT " +
                        " Outreach, " +
                        " [YWCA Shelter], " +
                        " [Legal Office], " +
                        " Advocacy, " +
                        " ChildAdvocacy, " +
                        "GroupClass " +
                     "FROM " +
                        "tbl_Intake " +
                    "Where " +
                       "Date = #" + date + "# " +
                       "AND " +
                       "Consumer_ID = \"" + Pid + "\";"
                    );
                if (queryArray.Count > 0)
                {
                    Counseling = (bool)queryArray[0];
                    Shelter = (bool)queryArray[1];
                    Legal = (bool)queryArray[2];
                    Advocacy = (bool)queryArray[3];
                    ChildAdvocacy = (bool)queryArray[4];
                    GroupClass = (bool)queryArray[5];
                }
                else
                {
                    Counseling = false;
                    Shelter = false;
                    Legal = false;
                    Advocacy = false;
                    ChildAdvocacy = false;
                    GroupClass = false;
                }
                //String
                queryArray = StringQuery
                    (
                    "SELECT " +
                        "[Adult 2 Relationship] , " +
                        "[Adult 2 Sex] , " +
                        "[Child 1 Relationship], " +
                        "[Child 1 Sex]  , " +
                        "[Child 2 Relationship], " +
                        "[Child 2 Sex], " +
                        "[Child 3 Relationship], " +
                        "[Child 3 Sex], " +
                        "[Child 4 Relationship], " +
                        "[Child 4 Sex] , " +
                        "[Child 5 Relationship]  , " +
                        "[Child 5 Sex]    , " +
                        "[Child 6 Relationship], " +
                        "[Child 6 Sex] , " +
                        "[Child 7 Relationship] , " +
                        "[Child 7 Sex] , " +
                        "[DischargeLocation] , " +
                        "[Comments] " +
                     "FROM " +
                        "tbl_Intake " +
                    "Where " +
                       "Date = #" + date + "# " +
                       "AND " +
                       "Consumer_ID = \"" + Pid + "\";"
                    );
                if (queryArray.Count > 0)
                {
                    PersonsInHomeRelationship = (string)queryArray[0];
                    PersonsInHomeGender = (string)queryArray[1];
                    PersonsInHomeRelationship2 = (string)queryArray[2];
                    PersonsInHomeGender2 = (string)queryArray[3];
                    PersonsInHomeRelationship3 = (string)queryArray[4];
                    PersonsInHomeGender3 = (string)queryArray[5];
                    PersonsInHomeRelationship4 = (string)queryArray[6];
                    PersonsInHomeGender4 = (string)queryArray[7];
                    PersonsInHomeRelationship5 = (string)queryArray[8];
                    PersonsInHomeGender5 = (string)queryArray[9];
                    PersonsInHomeRelationship6 = (string)queryArray[10];
                    PersonsInHomeGender6 = (string)queryArray[11];
                    PersonsInHomeRelationship7 = (string)queryArray[12];
                    PersonsInHomeGender7 = (string)queryArray[13];
                    PersonsInHomeRelationship8 = (string)queryArray[14];
                    PersonsInHomeGender8 = (string)queryArray[15];
                    DischargeLocation = (string)queryArray[16];
                    Comments = (string)queryArray[17];
                }
                else
                {
                    PersonsInHomeRelationship = "NoInfo";
                    PersonsInHomeGender = "NoInfo";
                    PersonsInHomeRelationship2 = "NoInfo";
                    PersonsInHomeGender2 = "NoInfo";
                    PersonsInHomeRelationship3 = "NoInfo";
                    PersonsInHomeGender3 = "NoInfo";
                    PersonsInHomeRelationship4 = "NoInfo";
                    PersonsInHomeGender4 = "NoInfo";
                    PersonsInHomeRelationship5 = "NoInfo";
                    PersonsInHomeGender5 = "NoInfo";
                    PersonsInHomeRelationship6 = "NoInfo";
                    PersonsInHomeGender6 = "NoInfo";
                    PersonsInHomeRelationship7 = "NoInfo";
                    PersonsInHomeGender7 = "NoInfo";
                    PersonsInHomeRelationship8 = "NoInfo";
                    PersonsInHomeGender8 = "NoInfo";
                    DischargeLocation = "NoInfo";
                    Comments = "NoInfo";
                }
            }
        }
        ////////////////////////////////////////////////////////////////////// END Advp //////////////////////////////////////////////////////////////////////
    }
}
