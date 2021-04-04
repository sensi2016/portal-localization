using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Users
    {
        public Users()
        {
            AnswerServiceInsertUser = new HashSet<AnswerService>();
            AnswerServiceUser = new HashSet<AnswerService>();
            BasketDrug = new HashSet<BasketDrug>();
            BloodStatusHistory = new HashSet<BloodStatusHistory>();
            FileTag = new HashSet<FileTag>();
            Payment = new HashSet<Payment>();
            Prescription = new HashSet<Prescription>();
            PrescriptionDetailDrug = new HashSet<PrescriptionDetailDrug>();
            PrescriptionDetailDrugHistory = new HashSet<PrescriptionDetailDrugHistory>();
            PrescriptionDetailService = new HashSet<PrescriptionDetailService>();
            PrescriptionDetailServiceHistory = new HashSet<PrescriptionDetailServiceHistory>();
            PrescriptionDrugChartFunctor = new HashSet<PrescriptionDrugChart>();
            PrescriptionDrugChartUser = new HashSet<PrescriptionDrugChart>();
            PrescriptionDrugChartWitness = new HashSet<PrescriptionDrugChart>();
            PrescriptionServiceChartFunctor = new HashSet<PrescriptionServiceChart>();
            PrescriptionServiceChartUser = new HashSet<PrescriptionServiceChart>();
            PrescriptionServiceChartWitness = new HashSet<PrescriptionServiceChart>();
            PurchaseInvoice = new HashSet<PurchaseInvoice>();
            ReceptionDiagnosisHistory = new HashSet<ReceptionDiagnosisHistory>();
            ReceptionHistory = new HashSet<ReceptionHistory>();
            ReceptionService = new HashSet<ReceptionService>();
            Request = new HashSet<Request>();
            RequestHistory = new HashSet<RequestHistory>();
            Sampling = new HashSet<Sampling>();
            SamplingHistory = new HashSet<SamplingHistory>();
            StoreEntry = new HashSet<StoreEntry>();
            StoreTransfer = new HashSet<StoreTransfer>();
            Template = new HashSet<Template>();
            UserCardCode = new HashSet<UserCardCode>();
            UserPermission = new HashSet<UserPermission>();
            UserRole = new HashSet<UserRole>();
            UserRolePermission = new HashSet<UserRolePermission>();
            VitalSigns = new HashSet<VitalSigns>();
        }

        public int Id { get; set; }
        public int? PersonId { get; set; }
        public long? CardCodeId { get; set; }
        public bool? IsActive { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool? IsLimitByIp { get; set; }
        public bool? IsVerify { get; set; }
        public string MacAddress { get; set; }
        public bool? IsSync { get; set; }

        public virtual CardCode CardCode { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<AnswerService> AnswerServiceInsertUser { get; set; }
        public virtual ICollection<AnswerService> AnswerServiceUser { get; set; }
        public virtual ICollection<BasketDrug> BasketDrug { get; set; }
        public virtual ICollection<BloodStatusHistory> BloodStatusHistory { get; set; }
        public virtual ICollection<FileTag> FileTag { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }
        public virtual ICollection<Prescription> Prescription { get; set; }
        public virtual ICollection<PrescriptionDetailDrug> PrescriptionDetailDrug { get; set; }
        public virtual ICollection<PrescriptionDetailDrugHistory> PrescriptionDetailDrugHistory { get; set; }
        public virtual ICollection<PrescriptionDetailService> PrescriptionDetailService { get; set; }
        public virtual ICollection<PrescriptionDetailServiceHistory> PrescriptionDetailServiceHistory { get; set; }
        public virtual ICollection<PrescriptionDrugChart> PrescriptionDrugChartFunctor { get; set; }
        public virtual ICollection<PrescriptionDrugChart> PrescriptionDrugChartUser { get; set; }
        public virtual ICollection<PrescriptionDrugChart> PrescriptionDrugChartWitness { get; set; }
        public virtual ICollection<PrescriptionServiceChart> PrescriptionServiceChartFunctor { get; set; }
        public virtual ICollection<PrescriptionServiceChart> PrescriptionServiceChartUser { get; set; }
        public virtual ICollection<PrescriptionServiceChart> PrescriptionServiceChartWitness { get; set; }
        public virtual ICollection<PurchaseInvoice> PurchaseInvoice { get; set; }
        public virtual ICollection<ReceptionDiagnosisHistory> ReceptionDiagnosisHistory { get; set; }
        public virtual ICollection<ReceptionHistory> ReceptionHistory { get; set; }
        public virtual ICollection<ReceptionService> ReceptionService { get; set; }
        public virtual ICollection<Request> Request { get; set; }
        public virtual ICollection<RequestHistory> RequestHistory { get; set; }
        public virtual ICollection<Sampling> Sampling { get; set; }
        public virtual ICollection<SamplingHistory> SamplingHistory { get; set; }
        public virtual ICollection<StoreEntry> StoreEntry { get; set; }
        public virtual ICollection<StoreTransfer> StoreTransfer { get; set; }
        public virtual ICollection<Template> Template { get; set; }
        public virtual ICollection<UserCardCode> UserCardCode { get; set; }
        public virtual ICollection<UserPermission> UserPermission { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
        public virtual ICollection<UserRolePermission> UserRolePermission { get; set; }
        public virtual ICollection<VitalSigns> VitalSigns { get; set; }
    }
}
