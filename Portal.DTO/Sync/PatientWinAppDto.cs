using System;

namespace Portal.DTO
{
    public class PatientWinAppDto : BaseWinAppDto
    {
        public long addmision_num { get; set; }
        public Guid? hospitalId { get; set; }
        public Guid? DoctorId { get; set; }
        public long id_card { get; set; }
        public DateTime? Birthday { get; set; }
        public GenderWinAppEnum Gender { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public MartialStateWinAppEnum? MartialState { get; set; }
        public int Children { get; set; }
        public string Occupation { get; set; }
        public string Address { get; set; }
        public string chronic { get; set; }
        public string note { get; set; }
        public string NHS_Number { get; set; }
        public string Allergy { get; set; }
        public string Work { get; set; }
        public BloodWinAppEnum? Blood { get; set; }
        public RHWinAppEnum? RH { get; set; }
        public bool Smoking { get; set; }
        public bool Drinking { get; set; }
    }

}
