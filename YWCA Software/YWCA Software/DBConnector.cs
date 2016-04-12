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
                                    //@"C:\YWCADB\All\" + //for local debuging
                                    @"L:\YWCACounselingANDLegal.accdb" +
                                    @";";
        public const string Password = @"Jet OLEDB:Database Password=ywc@;";
        ////////////////////////////////////////////////////////////////////// END Const Strings For DB Access //////////////////////////////////////////////////////////////////////


        /********************************************************************* Start SQL Data Sections *********************************************************************/

        #region Data Binding
//////////////////////////////////////////////////////////////////////////////////////
///// WOCAppt
        private string _wocId;
        public string WocId
        {
            get
            {
                return _wocId;
            }
            set
            {
                _wocId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocId"));
            }
        }

        private string _wocDateScheduled;
        public string WocDateScheduled
        {
            get
            {
                return _wocDateScheduled;
            }
            set
            {
                _wocDateScheduled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocDateScheduled"));
            }
        }

        private string _wocProgram;
        public string WocProgram
        {
            get
            {
                return _wocProgram;
            }
            set
            {
                _wocProgram = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocProgram"));
            }
        }

        private bool _wocFirstVisit;
        public bool WocFirstVisit
        {
            get
            {
                return _wocFirstVisit;
            }
            set
            {
                _wocFirstVisit = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocFirstVisit"));
            }
        }

        private string _wocGoal;
        public string WocGoal
        {
            get
            {
                return _wocGoal;
            }
            set
            {
                _wocGoal = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocGoal"));
            }
        }

        private string _wocPreparationBefore;
        public string WocPreparationBefore
        {
            get
            {
                return _wocPreparationBefore;
            }
            set
            {
                _wocPreparationBefore = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocPreparationBefore"));
            }
        }

        private string _wocReferenced;
        public string WocReferenced
        {
            get
            {
                return _wocReferenced;
            }
            set
            {
                _wocReferenced = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocReferenced"));
            }
        }

        private string _wocRace;
        public string WocRace
        {
            get
            {
                return _wocRace;
            }
            set
            {
                _wocRace = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocRace"));
            }
        }

        private string _wocEthnicity;
        public string WocEthnicity
        {
            get
            {
                return _wocEthnicity;
            }
            set
            {
                _wocEthnicity = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocEthnicity"));
            }
        }

        private bool _wocCurrentlyEmployed;
        public bool WocCurrentlyEmployed
        {
            get
            {
                return _wocCurrentlyEmployed;
            }
            set
            {
                _wocCurrentlyEmployed = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocCurrentlyEmployed"));
            }
        }

        private bool _wocMainProvider;
        public bool WocMainProvider
        {
            get
            {
                return _wocMainProvider;
            }
            set
            {
                _wocMainProvider = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocMainProvider"));
            }
        }

        private string _wocFamilyMonthlyIncome;
        public string WocFamilyMonthlyIncome
        {
            get
            {
                return _wocFamilyMonthlyIncome;
            }
            set
            {
                _wocFamilyMonthlyIncome = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocFamilyMonthlyIncome"));
            }
        }

        private string _wocMaritalSTatus;
        public string WocMaritalStatus
        {
            get
            {
                return _wocMaritalSTatus;
            }
            set
            {
                _wocMaritalSTatus = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocMaritalStatus"));
            }
        }

        private string _wocChildrenUnder18;
        public string WocChildrenUnder18
        {
            get
            {
                return _wocChildrenUnder18;
            }
            set
            {
                _wocChildrenUnder18 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocChildrenUnder18"));
            }
        }

        private string _wocPreparationAfter;
        public string WocPreparationAfter
        {
            get
            {
                return _wocPreparationAfter;
            }
            set
            {
                _wocPreparationAfter = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocPreparationAfter"));
            }
        }

        private bool _wocSuccess;
        public bool WocSuccess
        {
            get
            {
                return _wocSuccess;
            }
            set
            {
                _wocSuccess = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocSuccess"));
            }
        }

        private string _wocImprovements;
        public string WocImprovements
        {
            get
            {
                return _wocImprovements;
            }
            set
            {
                _wocImprovements = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocImprovements"));
            }
        }

        private string _wocComments;
        public string WocComments
        {
            get
            {
                return _wocComments;
            }
            set
            {
                _wocComments = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocComments"));
            }
        }

        private bool _wocUseComment;
        public bool WocUseComment
        {
            get
            {
                return _wocUseComment;
            }
            set
            {
                _wocUseComment = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocUseComment"));
            }
        }

        private bool _wocAnonymous;
        public bool WocAnonymous
        {
            get
            {
                return _wocAnonymous;
            }
            set
            {
                _wocAnonymous = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocAnonymous"));
            }
        }

        private bool _wocContact;
        public bool WocContact
        {
            get
            {
                return _wocContact;
            }
            set
            {
                _wocContact = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocContact"));
            }
        }

        private string _wocEmail;
        public string WocEmail
        {
            get
            {
                return _wocEmail;
            }
            set
            {
                _wocEmail = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocEmail"));
            }
        }

//////////////////////////////////////////////////////////////////////////////////////
///// WOCClass

        private string _wocDateClass;
        public string WocDateClass
        {
            get
            {
                return _wocDateClass;
            }
            set
            {
                _wocDateClass = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocDateClass"));
            }
        }

        private string _wocClassId;
        public string WocClassId
        {
            get
            {
                return _wocClassId;
            }
            set
            {
                _wocClassId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocClassId"));
            }
        }

        private string _wocClassId;
        public string WocClassId
        {
            get
            {
                return _wocClassId;
            }
            set
            {
                _wocClassId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocClassId"));
            }
        }

        private string _wocClassName;
        public string WocClassName
        {
            get
            {
                return _wocClassName;
            }
            set
            {
                _wocClassName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocClassName"));
            }
        }

        private bool _wocAttended;
        public bool WocAttended
        {
            get
            {
                return _wocAttended;
            }
            set
            {
                _wocAttended = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocAttended"));
            }
        }

        private string _wocSupervisor;
        public string WocSupervisor
        {
            get
            {
                return _wocSupervisor;
            }
            set
            {
                _wocSupervisor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocSupervisor"));
            }
        }

        private string _wocCompleted;
        public string WocCompleted
        {
            get
            {
                return _wocCompleted;
            }
            set
            {
                _wocCompleted = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocCompleted"));
            }
        }

        private string _wocFoundValue;
        public string WocFoundValue
        {
            get
            {
                return _wocFoundValue;
            }
            set
            {
                _wocFoundValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocFoundValue"));
            }
        }

        private string _wocIncreasedConfidence;
        public string WocIncreasedConfidence
        {
            get
            {
                return _wocIncreasedConfidence;
            }
            set
            {
                _wocIncreasedConfidence = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocIncreasedConfidence"));
            }
        }

        private string _wocPreparedForGoals;
        public string WocPreparedForGoals
        {
            get
            {
                return _wocPreparedForGoals;
            }
            set
            {
                _wocPreparedForGoals = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocPreparedForGoals"));
            }
        }

        private string _wocStimulatedThinking;
        public string WocStimulatedThinking
        {
            get
            {
                return _wocStimulatedThinking;
            }
            set
            {
                _wocStimulatedThinking = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocStimulatedThinking"));
            }
        }

        private string _wocImprovedSkills;
        public string WocImprovedSkills
        {
            get
            {
                return _wocImprovedSkills;
            }
            set
            {
                _wocImprovedSkills = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocImprovedSkills"));
            }
        }

        private string _wocRecommendClass;
        public string WocRecommendClass
        {
            get
            {
                return _wocRecommendClass;
            }
            set
            {
                _wocRecommendClass = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocRecommendClass"));
            }
        }

        private string _wocApplyLearning;
        public string WocApplyLearning
        {
            get
            {
                return _wocApplyLearning;
            }
            set
            {
                _wocApplyLearning = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocApplyLearning"));
            }
        }

        private string _wocMetExpectations;
        public string WocMetExpectations
        {
            get
            {
                return _wocMetExpectations;
            }
            set
            {
                _wocMetExpectations = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocMetExpectations"));
            }
        }

        private string _wocFutureDesc;
        public string WocFutureDesc
        {
            get
            {
                return _wocFutureDesc;
            }
            set
            {
                _wocFutureDesc = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocFutureDesc"));
            }
        }

        private string _wocAppreciatedDesc;
        public string WocAppreciatedDesc
        {
            get
            {
                return _wocAppreciatedDesc;
            }
            set
            {
                _wocAppreciatedDesc = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocAppreciatedDesc"));
            }
        }

        private string _wocChangeOrAddDesc;
        public string WocChangeOrAddDesc
        {
            get
            {
                return _wocChangeOrAddDesc;
            }
            set
            {
                _wocChangeOrAddDesc = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocChangeOrAddDesc"));
            }
        }


//////////////////////////////////////////////////////////////////////////////////////
///// WOCClass

        private string _wocPosition;
        public string WocPosition
        {
            get
            {
                return _wocPosition;
            }
            set
            {
                _wocPosition = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocPosition"));
            }
        }

        private string _wocHoursWeekly;
        public string WocHoursWeekly
        {
            get
            {
                return _wocHoursWeekly;
            }
            set
            {
                _wocHoursWeekly = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocHoursWeekly"));
            }
        }

        private string _wocWage;
        public string WocWage
        {
            get
            {
                return _wocWage;
            }
            set
            {
                _wocWage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocWage"));
            }
        }

        private string _wocHearAboutPosition;
        public string WocHearAboutPosition
        {
            get
            {
                return _wocHearAboutPosition;
            }
            set
            {
                _wocHearAboutPosition = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocHearAboutPosition"));
            }
        }

        private string _wocTrainingSchool;
        public string WocTrainingSchool
        {
            get
            {
                return _wocTrainingSchool;
            }
            set
            {
                _wocTrainingSchool = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocTrainingSchool"));
            }
        }

        private string _wocTrainSchoolStartDate;
        public string WocTrainSchoolStartDate
        {
            get
            {
                return _wocTrainSchoolStartDate;
            }
            set
            {
                _wocTrainSchoolStartDate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocTrainSchoolStartDate"));
            }
        }

        private string _wocFavoriteClass;
        public string WocFavoriteClass
        {
            get
            {
                return _wocFavoriteClass;
            }
            set
            {
                _wocFavoriteClass = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocFavoriteClass"));
            }
        }

        private string _wocMostHelpfulClass;
        public string WocMostHelpfulClass
        {
            get
            {
                return _wocMostHelpfulClass;
            }
            set
            {
                _wocMostHelpfulClass = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocMostHelpfulClass"));
            }
        }

        private string _wocOtherJobSupportServices;
        public string WocOtherJobSupportServices
        {
            get
            {
                return _wocOtherJobSupportServices;
            }
            set
            {
                _wocOtherJobSupportServices = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocOtherJobSupportServices"));
            }
        }

        private string _wocCompareServices;
        public string WocCompareServices
        {
            get
            {
                return _wocCompareServices;
            }
            set
            {
                _wocCompareServices = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocCompareServices"));
            }
        }

        private string _wocCompareServicesReason;
        public string WocCompareServicesReason
        {
            get
            {
                return _wocCompareServicesReason;
            }
            set
            {
                _wocCompareServicesReason = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocCompareServicesReason"));
            }
        }

        private string _wocDoBetter;
        public string WocDoBetter
        {
            get
            {
                return _wocDoBetter;
            }
            set
            {
                _wocDoBetter = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocDoBetter"));
            }
        }

        private string _wocOfferOtherServices;
        public string WocOfferOtherServices
        {
            get
            {
                return _wocOfferOtherServices;
            }
            set
            {
                _wocOfferOtherServices = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WocOfferOtherServices"));
            }
        }

        private string _ecapId;
        public string EcapId
        {
            get
            {
                return _ecapId;
            }
            set
            {
                _ecapId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EcapId"));
            }
        }


        private string _ecapFirstName;
        public string EcapFirstName
        {
            get
            {
                return _ecapFirstName;
            }
            set
            {
                _ecapFirstName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EcapFirstName"));
            }
        }

        private string _ecapLastName;
        public string EcapLastName
        {
            get
            {
                return _ecapLastName;
            }
            set
            {
                _ecapLastName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EcapLastName"));
            }
        }

        private string _ecapNumFamilyMembers;
        public string EcapNumFamilyMembers
        {
            get
            {
                return _ecapNumFamilyMembers;
            }
            set
            {
                _ecapNumFamilyMembers = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EcapNumFamilyMembers"));
            }
        }

        private string _ecapNumAdults;
        public string EcapNumAdults
        {
            get
            {
                return _ecapNumAdults;
            }
            set
            {
                _ecapNumAdults = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EcapNumAdults"));
            }
        }

        private string _ecapNumChildren;
        public string EcapNumChildren
        {
            get
            {
                return _ecapNumChildren;
            }
            set
            {
                _ecapNumChildren = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EcapNumChildren"));
            }
        }

        private string _ecapRace;
        public string EcapRace
        {
            get
            {
                return _ecapRace;
            }
            set
            {
                _ecapRace = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EcapRace"));
            }
        }

        private string _ecapFamilyStructure;
        public string EcapFamilyStructure
        {
            get
            {
                return _ecapFamilyStructure;
            }
            set
            {
                _ecapFamilyStructure = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EcapFamilyStructure"));
            }
        }

        private string _ecapEducationLevel;
        public string EcapEducationLevel
        {
            get
            {
                return _ecapEducationLevel;
            }
            set
            {
                _ecapEducationLevel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EcapEducationLevel"));
            }
        }

        private bool _ecapEnglishSpeaking;
        public bool EcapEnglishSpeaking
        {
            get
            {
                return _ecapEnglishSpeaking;
            }
            set
            {
                _ecapEnglishSpeaking = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EcapEnglishSpeaking"));
            }
        }

        private string _ecapHours;
        public string EcapHours
        {
            get
            {
                return _ecapHours;
            }
            set
            {
                _ecapHours = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EcapSchoolYear"));
            }
        }

        private string _ecapSite;
        public string EcapSite
        {
            get
            {
                return _ecapSite;
            }
            set
            {
                _ecapSite = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EcapSite"));
            }
        }

        private string _ecapSchoolYear;
        public string EcapSchoolYear
        {
            get
            {
                return _ecapSchoolYear;
            }
            set
            {
                _ecapSchoolYear = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EcapSchoolYear"));
            }
        }

        private string _ecapSchoolMonth;
        public string EcapSchoolMonth
        {
            get
            {
                return _ecapSchoolMonth;
            }
            set
            {
                _ecapSchoolMonth = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EcapSchoolMonth"));
            }
        }

        private string _ecapVolunteerMember;
        public string EcapVolunteerMember
        {
            get
            {
                return _ecapVolunteerMember;
            }
            set
            {
                _ecapVolunteerMember = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EcapVolunteerMember"));
            }
        }


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
            get { return _personsInHomeGender8; }
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

        private string _dateNow = "NoInfo";
        public string DateNow
        {
            get
            {
                return _dateNow;
            }
            set
            {
                _dateNow = value ?? "NoInfo";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Date Now"));
            }
        }

        /// <summary>
        /// Set Pid to Input
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

        #endregion

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

        private ObservableCollection<string> _listDates = new ObservableCollection<string> { @"results go here" }; //pid search results list\

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
        /// 
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
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
                      target.Add((rdr.IsDBNull(0) == false) ? rdr.GetInt32(i) : 0);
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
                        target.Add((rdr.IsDBNull(0) == false) ? rdr.GetDouble(i) : 0);
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
            return (noInfo == "NoInfo") ? "NULL" : "#"+noInfo+"#";
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
                        "[Child 7 DOB] = " + DumpNoInfo(PersonsInHomeDob8) + ", " +
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

        /********************************************************************* Start ECAP *********************************************************************/

        public void Ecap(string selectUpdateAdd, string pid)
        {
            EcapId = pid;
            ArrayList queryArray;
            if (selectUpdateAdd == "update")
            {
                //Double
                UpdateQuery
                    (
                    "UPDATE " +
                        "ECAP " +
                    "SET " +
                        "NUM_FAMILY_MEMBERS = \"" + EcapNumFamilyMembers + "\", " +
                        "NUM_ADULTS = \"" + EcapNumAdults + "\", " +
                        "NUM_CHILDREN = \"" + EcapNumChildren + "\"  " +
                    "Where " +
                       "ECAP_ID = \"" + EcapId + "\";"
                    );
                //bools
                UpdateQuery
                    (
                    "UPDATE " +
                        "ECAP " +
                    "SET " +
                        "ENGLISH_SPEAKING = " + EcapEnglishSpeaking + "  " +
                    "Where " +
                       "ECAP_ID = \"" + EcapId + "\";"
                    );
                //String
                UpdateQuery
                    (
                     "UPDATE " +
                        "ECAP " +
                    "SET " +
                       "FIRST_NAME = \"" + EcapFirstName + "\", " +
                       "LAST_NAME = \"" + EcapLastName + "\", " +
                       "RACE = \"" + EcapRace + "\", " +
                       "FAMILY_STRUCTURE = \"" + EcapFamilyStructure + "\", " +
                       "EDUCATION_LEVEL = \"" + EcapEducationLevel + "\"  " +
                    "Where " +
                       "ECAP_ID = \"" + EcapId + "\";"
                    );
            }
            else
            {
                //doubles
                queryArray = IntQuery
                    (
                    "SELECT " +
                        " NUM_FAMILY_MEMBERS, " +
                        " NUM_ADULTS, " +
                        "NUM_CHILDREN " +
                     "FROM " +
                        "ECAP " +
                    "Where " +
                       "ECAP_ID = \"" + EcapId + "\";"
                    );
                if (queryArray.Count > 0)
                {
                    EcapNumFamilyMembers = queryArray[0].ToString();
                    EcapNumAdults = queryArray[1].ToString();
                    EcapNumChildren = queryArray[2].ToString();
                }
                else
                {
                    EcapNumFamilyMembers = "NoInfo";
                    EcapNumAdults = "NoInfo";
                    EcapNumChildren = "NoInfo";
                }
                //bools
                queryArray = BoolQuery
                    (
                    "SELECT " +
                        "ENGLISH_SPEAKING " +
                     "FROM " +
                        "ECAP " +
                    "Where " +
                       "ECAP_ID = \"" + EcapId + "\";"
                    );
                if (queryArray.Count > 0)
                {
                    EcapEnglishSpeaking = (bool)queryArray[0];
                }
                else
                {
                    EcapEnglishSpeaking = false;
                }
                //String
                queryArray = StringQuery
                    (
                    "SELECT " +
                        "FIRST_NAME , " +
                        "LAST_NAME , " +
                        "RACE , " +
                        "FAMILY_STRUCTURE  , " +
                        "EDUCATION_LEVEL " +
                     "FROM " +
                        "ECAP " +
                    "Where " +
                       "ECAP_ID = \"" + EcapId + "\";"
                    );
                if (queryArray.Count > 0)
                {
                    EcapFirstName = (string)queryArray[0];
                    EcapLastName = (string)queryArray[1];
                    EcapRace = (string)queryArray[2];
                    EcapFamilyStructure = (string)queryArray[3];
                    EcapEducationLevel = (string)queryArray[4];
                }
                else
                {
                    EcapFirstName = "NoInfo";
                    EcapLastName = "NoInfo";
                    EcapRace = "NoInfo";
                    EcapFamilyStructure = "NoInfo";
                    EcapEducationLevel = "NoInfo";
                }
            }
        }
        ////////////////////////////////////////////////////////////////////// END ECAP //////////////////////////////////////////////////////////////////////

        /********************************************************************* Start ECAP_V_Hours *********************************************************************/

        public void EcapVHours(string selectUpdateAdd, string pid)
        {
            EcapSite = pid;
            ArrayList queryArray;
            if (selectUpdateAdd == "update")
            {
                //Double
                UpdateQuery
                    (
                    "UPDATE " +
                        "ECAP_V_HOURS " +
                    "SET " +
                        "SCHOOL_YEAR = \"" + EcapSchoolYear + "\", " +
                        "HOURS = \"" + EcapHours + "\"  " +
                    "Where " +
                       "SITE = \"" + EcapSite + "\";"
                    );
                //String
                UpdateQuery
                    (
                     "UPDATE " +
                        "ECAP_V_HOURS " +
                    "SET " +
                       "SCHOOL_MONTH = \"" + EcapSchoolMonth + "\", " +
                       "VOLUNTEER_MEMBER = \"" + EcapVolunteerMember + "\"  " +
                    "Where " +
                       "SITE = \"" + EcapSite + "\";"
                    );
            }
            else
            {
                //doubles
                queryArray = IntQuery
                    (
                    "SELECT " +
                        " SCHOOL_YEAR, " +
                        " HOURS " +
                     "FROM " +
                        "ECAP_V_HOURS " +
                    "Where " +
                       "SITE = \"" + EcapSite + "\";"
                    );
                if (queryArray.Count > 0)
                {
                    EcapSchoolYear = queryArray[0].ToString();
                    EcapHours = queryArray[1].ToString();
                }
                else
                {
                    EcapSchoolYear = "NoInfo";
                    EcapHours = "NoInfo";
                }
                //String
                queryArray = StringQuery
                    (
                    "SELECT " +
                        "SCHOOL_MONTH  , " +
                        "VOLUNTEER_MEMBER " +
                     "FROM " +
                        "ECAP_V_HOURS " +
                    "Where " +
                       "SITE = \"" + EcapSite + "\";"
                    );
                if (queryArray.Count > 0)
                {
                    EcapSchoolMonth = (string)queryArray[0];
                    EcapVolunteerMember = (string)queryArray[1];
                }
                else
                {
                    EcapSchoolMonth = "NoInfo";
                    EcapVolunteerMember = "NoInfo";
                }
            }
        }
        ////////////////////////////////////////////////////////////////////// END ECAP_V_Hours //////////////////////////////////////////////////////////////////////
    }

}
