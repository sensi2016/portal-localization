using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Section
    {
        public Section()
        {
            AnswerService = new HashSet<AnswerService>();
            Prescription = new HashSet<Prescription>();
            PrescriptionDetailDrug = new HashSet<PrescriptionDetailDrug>();
            PrescriptionDetailDrugHistory = new HashSet<PrescriptionDetailDrugHistory>();
            PrescriptionDetailService = new HashSet<PrescriptionDetailService>();
            PrescriptionDetailServiceHistory = new HashSet<PrescriptionDetailServiceHistory>();
            PrescriptionSetting = new HashSet<PrescriptionSetting>();
            PurchaseInvoice = new HashSet<PurchaseInvoice>();
            ReceptionDetail = new HashSet<ReceptionDetail>();
            ReceptionDrugConsumerSection = new HashSet<ReceptionDrug>();
            ReceptionDrugSection = new HashSet<ReceptionDrug>();
            ReceptionHistory = new HashSet<ReceptionHistory>();
            ReceptionSectionDoctor = new HashSet<ReceptionSectionDoctor>();
            ReceptionService = new HashSet<ReceptionService>();
            ReceptionServiceStatus = new HashSet<ReceptionServiceStatus>();
            Receptions = new HashSet<Receptions>();
            RequestSourceSection = new HashSet<Request>();
            RequestTargetSection = new HashSet<Request>();
            RolePermission = new HashSet<RolePermission>();
            Room = new HashSet<Room>();
            SectionEssentialsDrug = new HashSet<SectionEssentialsDrug>();
            SectionEssentialsGenericDrug = new HashSet<SectionEssentialsGenericDrug>();
            SectionPropertyList = new HashSet<SectionPropertyList>();
            SectionService = new HashSet<SectionService>();
            SectionStoreSection = new HashSet<SectionStore>();
            SectionStoreStore = new HashSet<SectionStore>();
            StoreDrug = new HashSet<StoreDrug>();
            StoreEntry = new HashSet<StoreEntry>();
            StoreTransferDestinationStore = new HashSet<StoreTransfer>();
            StoreTransferSourceStore = new HashSet<StoreTransfer>();
            UserPermission = new HashSet<UserPermission>();
            UserRolePermission = new HashSet<UserRolePermission>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public string LocalCode { get; set; }
        public int? CenterId { get; set; }
        public int? BranchId { get; set; }
        public string No { get; set; }
        public string Phone { get; set; }
        public int? SuperVisorPersonelId { get; set; }
        public int? SectionTypeId { get; set; }
        public bool? IsActive { get; set; }
        public int? BossId { get; set; }
        public int? SectionKindId { get; set; }
        public int? SectionFieldId { get; set; }
        public int? TemperatureTypeId { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }

        public virtual Employee Boss { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual Center Center { get; set; }
        public virtual SectionField SectionField { get; set; }
        public virtual SectionKind SectionKind { get; set; }
        public virtual SectionType SectionType { get; set; }
        public virtual Employee SuperVisorPersonel { get; set; }
        public virtual TemperatureType TemperatureType { get; set; }
        public virtual ICollection<AnswerService> AnswerService { get; set; }
        public virtual ICollection<Prescription> Prescription { get; set; }
        public virtual ICollection<PrescriptionDetailDrug> PrescriptionDetailDrug { get; set; }
        public virtual ICollection<PrescriptionDetailDrugHistory> PrescriptionDetailDrugHistory { get; set; }
        public virtual ICollection<PrescriptionDetailService> PrescriptionDetailService { get; set; }
        public virtual ICollection<PrescriptionDetailServiceHistory> PrescriptionDetailServiceHistory { get; set; }
        public virtual ICollection<PrescriptionSetting> PrescriptionSetting { get; set; }
        public virtual ICollection<PurchaseInvoice> PurchaseInvoice { get; set; }
        public virtual ICollection<ReceptionDetail> ReceptionDetail { get; set; }
        public virtual ICollection<ReceptionDrug> ReceptionDrugConsumerSection { get; set; }
        public virtual ICollection<ReceptionDrug> ReceptionDrugSection { get; set; }
        public virtual ICollection<ReceptionHistory> ReceptionHistory { get; set; }
        public virtual ICollection<ReceptionSectionDoctor> ReceptionSectionDoctor { get; set; }
        public virtual ICollection<ReceptionService> ReceptionService { get; set; }
        public virtual ICollection<ReceptionServiceStatus> ReceptionServiceStatus { get; set; }
        public virtual ICollection<Receptions> Receptions { get; set; }
        public virtual ICollection<Request> RequestSourceSection { get; set; }
        public virtual ICollection<Request> RequestTargetSection { get; set; }
        public virtual ICollection<RolePermission> RolePermission { get; set; }
        public virtual ICollection<Room> Room { get; set; }
        public virtual ICollection<SectionEssentialsDrug> SectionEssentialsDrug { get; set; }
        public virtual ICollection<SectionEssentialsGenericDrug> SectionEssentialsGenericDrug { get; set; }
        public virtual ICollection<SectionPropertyList> SectionPropertyList { get; set; }
        public virtual ICollection<SectionService> SectionService { get; set; }
        public virtual ICollection<SectionStore> SectionStoreSection { get; set; }
        public virtual ICollection<SectionStore> SectionStoreStore { get; set; }
        public virtual ICollection<StoreDrug> StoreDrug { get; set; }
        public virtual ICollection<StoreEntry> StoreEntry { get; set; }
        public virtual ICollection<StoreTransfer> StoreTransferDestinationStore { get; set; }
        public virtual ICollection<StoreTransfer> StoreTransferSourceStore { get; set; }
        public virtual ICollection<UserPermission> UserPermission { get; set; }
        public virtual ICollection<UserRolePermission> UserRolePermission { get; set; }
    }
}
