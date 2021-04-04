using System;
using System.Collections.Generic;
using System.Text;
using Portal.DTO.Doctor;

namespace Portal.DTO.PatientCard
{
    public class PatientCardPrescriptionDto
    {
        public long Id { get; set; }
        public string PrescriptionDate { get; set; }
        public string CreateDate { get; set; }
        public string ClinicName { get; set; }
        public string DoctorName { get; set; }
        public DoctorInfoDto DoctorInfo { get; set; }
        public int? DoctorId { get; set; }
        public string Status { get; set; }
        public string StatusCode { get; set; }
        public string PatientFullName { get; set; }

    }

    public class PatientCardListVitalSignDto
    {
        public long Id { get; set; }
        public string PrescriptionDate { get; set; }
        public string BP { get; set; }
        public string BR { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public string BMI { get; set; }
        public string SPO2 { get; set; }
        public string SkinRace { get; set; }
        public string Creatinine { get; set; }
        public string eGFR { get; set; }

    }


    public class PatientCardListPaitentHistoryDto
    {
        public long Id { get; set; }
        public string PrescriptionDate { get; set; }
        public string Title { get; set; }
        public string ConsumptionInstruction { get; set; }
        public string DurationOfUse { get; set; }
        public string Frequency { get; set; }
    }

    public class PatientCardListDrugDto
    {
        public long Id { get; set; }
        public string PrescriptionDate { get; set; }
        public string Title { get; set; }
        public string WayOfPrescription { get; set; }
        public string DurationOfUse { get; set; }
        public string Frequency { get; set; }
        public string Note { get; set; }
    }

    public class PatientCardListTestDto
    {
        public long Id { get; set; }
        public string PrescriptionDate { get; set; }
        public string Title { get; set; }
    }

    public class PatientCardListTestOrderDto
    {
        public long Id { get; set; }
        public string PrescriptionDate { get; set; }
        public string Result { get; set; }
    }

    public class PatientCardListRisDto
    {
        public long Id { get; set; }
        public string PrescriptionDate { get; set; }
        public string CreateDate { get; set; }
        public string Type { get; set; }
    }

    public class PatientCardListRisOrderDto
    {
        public long Id { get; set; }
        public string PrescriptionDate { get; set; }
        public string CreateDate { get; set; }
        public string Type { get; set; }
        public string Report { get; set; }

    }
}
