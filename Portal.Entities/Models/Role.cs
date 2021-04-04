using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Role
    {
        public Role()
        {
            BasketDrug = new HashSet<BasketDrug>();
            BloodStatusHistory = new HashSet<BloodStatusHistory>();
            Payment = new HashSet<Payment>();
            Prescription = new HashSet<Prescription>();
            PrescriptionDetailDrugHistory = new HashSet<PrescriptionDetailDrugHistory>();
            PrescriptionDetailServiceHistory = new HashSet<PrescriptionDetailServiceHistory>();
            PrescriptionDrugChart = new HashSet<PrescriptionDrugChart>();
            PrescriptionServiceChart = new HashSet<PrescriptionServiceChart>();
            PurchaseInvoice = new HashSet<PurchaseInvoice>();
            ReceptionDiagnosisHistory = new HashSet<ReceptionDiagnosisHistory>();
            ReceptionHistory = new HashSet<ReceptionHistory>();
            Request = new HashSet<Request>();
            RequestHistory = new HashSet<RequestHistory>();
            RolePermission = new HashSet<RolePermission>();
            SamplingHistory = new HashSet<SamplingHistory>();
            StoreEntry = new HashSet<StoreEntry>();
            StoreTransfer = new HashSet<StoreTransfer>();
            Template = new HashSet<Template>();
            UserPermission = new HashSet<UserPermission>();
            UserRole = new HashSet<UserRole>();
            UserRolePermission = new HashSet<UserRolePermission>();
            VitalSigns = new HashSet<VitalSigns>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public int? RoleGroupId { get; set; }
        public string Code1 { get; set; }
        public string Code2 { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsAdmin { get; set; }
        public string Note { get; set; }
        public string NoteLang2 { get; set; }

        public virtual RoleGroup RoleGroup { get; set; }
        public virtual ICollection<BasketDrug> BasketDrug { get; set; }
        public virtual ICollection<BloodStatusHistory> BloodStatusHistory { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }
        public virtual ICollection<Prescription> Prescription { get; set; }
        public virtual ICollection<PrescriptionDetailDrugHistory> PrescriptionDetailDrugHistory { get; set; }
        public virtual ICollection<PrescriptionDetailServiceHistory> PrescriptionDetailServiceHistory { get; set; }
        public virtual ICollection<PrescriptionDrugChart> PrescriptionDrugChart { get; set; }
        public virtual ICollection<PrescriptionServiceChart> PrescriptionServiceChart { get; set; }
        public virtual ICollection<PurchaseInvoice> PurchaseInvoice { get; set; }
        public virtual ICollection<ReceptionDiagnosisHistory> ReceptionDiagnosisHistory { get; set; }
        public virtual ICollection<ReceptionHistory> ReceptionHistory { get; set; }
        public virtual ICollection<Request> Request { get; set; }
        public virtual ICollection<RequestHistory> RequestHistory { get; set; }
        public virtual ICollection<RolePermission> RolePermission { get; set; }
        public virtual ICollection<SamplingHistory> SamplingHistory { get; set; }
        public virtual ICollection<StoreEntry> StoreEntry { get; set; }
        public virtual ICollection<StoreTransfer> StoreTransfer { get; set; }
        public virtual ICollection<Template> Template { get; set; }
        public virtual ICollection<UserPermission> UserPermission { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
        public virtual ICollection<UserRolePermission> UserRolePermission { get; set; }
        public virtual ICollection<VitalSigns> VitalSigns { get; set; }
    }
}
