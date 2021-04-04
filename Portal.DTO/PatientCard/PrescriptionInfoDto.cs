using Portal.DTO.Message;
using System;
using System.Collections.Generic;
using System.Text;
using Portal.DTO.Doctor;

namespace Portal.DTO.PatientCard
{
    public class PrescriptionInfoDto
    {
        public int Id { get; set; }
        public string PatientFullName { get; set; }
        public string DoctorNote { get; set; }
        public object Drugs { get; set; }
        public object Tests { get; set; }
        public object Radilogies { get; set; }
        public object VitalSign { get; set; }
        public object PatientHistory { get; set; }
        public DoctorInfoDto DoctorInfoDto { get; set; }
    }

    public class PrescriptionReportInfoInfoDto
    {
        public string FullName { get; set; }
        public string SexAndAge { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
        public string Phone { get; set; }
        public string NationalCode { get; set; }
        public string DoctorName { get; set; }
        public string ClinicName { get; set; }
        public string ClinicPhone { get; set; }
        public string DoctorNote { get; set; }
        public string Tests { get; set; }
    }
    public class PrescriptionReportInfoDto : ITranslateReport
    {
        public PrescriptionReportInfoInfoDto PatientInfo { get; set; }

        public object Translate { get; set; }
        
        public List<DrugDto> Drugs { get; set; }
        //public string Tests { get; set; }
        public List<RadiologyDto> Radiologies { get; set; }
        public VitalSignDto VitalSigns { get; set; }
        public PatientInfoDto PatientHistories { get; set; }
        public List<LaboratoryAnswerDto> LaboratoryAnswers { get; set; }
    }


    public class TestDto
    {
        public string Tests { get; set; }
    }

    public class DrugDto
    {
        public string DrugName { get; set; }
        public string DrugDetail { get; set; }
        public string DurationTime { get; set; }
        public string DrugNote { get; set; }
    }

    public class RadiologyDto
    {
        public string Name { get; set; }
        public string Note { get; set; }
    }

    public class RadiologReportDto
    {
        public string RadiologyName { get; set; }
        public string RadiologyNote { get; set; }
    }

    public class PatientInfoDto
    {
        public string PatientComplaint { get; set; }
        public string DiseaseHistory { get; set; }
        public string DiseasRecords { get; set; }
        public string HistoryofSurgery { get; set; }
    }

    public class VitalSignDto
    {
        public string BR { get; set; }
        public string PR { get; set; }
        public string Temperature { get; set; }
        public string Weight { get; set; }
        public string Creatinine { get; set; }
        public string SkinColor { get; set; }
        public string SPO2 { get; set; }
        public string eGFR { get; set; }
        public string BMI { get; set; }
        public string Height { get; set; }
    }

    public class LaboratoryAnswerDto
    {
        public string AnswerDate { get; set; }
        public string LinkAnswer { get; set; }
    }
}
