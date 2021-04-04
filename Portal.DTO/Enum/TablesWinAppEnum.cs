namespace Portal.DTO
{
    public enum GenderWinAppEnum
    {
        Male = 0,
        Female = 1,
        Unknown = 2,
    }
    public enum MartialStateWinAppEnum
    {
        Married = 0,
        Seperated = 1,
        Divorced = 2,
        Widowed = 3,
        Single = 4,
        Other = 5,
    }

    public enum BloodWinAppEnum
    {
        A = 0,
        B = 1,
        AB = 2,
        O = 3,
        Unknown = 4
    }

    public enum RHWinAppEnum
    {
        Positive = 0,
        Negative = 1,
        Unknown = 2
    }
    public enum TablesWinAppEnum
    {
        Doctor,//0
        Hospital,//1
        IOL,
        MdtRegistry,//3
        Patient,//4
        PatientHistory,//5
        PatientNote,//6
        Pharmacy,//7
        Procedure,//8
        Radiology,//9
        Surgery,//10
        DoctorPharmacy,
        Visit,//12
        Prescription,//13
        Drug,//14
        PatientICD10,//15
        // New
        VitalSign,//16
        TestSendFromDoctor,
        Notes,
        EyeSurgery,//19
        VisualAcuity,//20
        GsBasics,//21
        GsFollowUp,//22
        GsSurgery,//23
        Whipple,//24
        Dermatology,//25
        DermatologyFollowUp,//26
        // Prostate
        ProstateClinicalState,//27
        ProstateCoMorbidites,//28
        ProstateDiagnosisTypes,
        ProstateLabTests,//30
        ProstatePcDiagnosis,//31
        ProstateRelativesDegree,
        //ProstateRiskGroup,//
        ProstateDemographics
        //Pictures
    }

}
