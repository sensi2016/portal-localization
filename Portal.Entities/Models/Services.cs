using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Services
    {
        public Services()
        {
            CenterServices = new HashSet<CenterServices>();
            DefineRoutineTest = new HashSet<DefineRoutineTest>();
            Instruction = new HashSet<Instruction>();
            InverseParent = new HashSet<Services>();
            PrescriptionDetailService = new HashSet<PrescriptionDetailService>();
            PrescriptionGroupSetting = new HashSet<PrescriptionGroupSetting>();
            PrescriptionServiceResult = new HashSet<PrescriptionServiceResult>();
            PrescriptionServiceRoutine = new HashSet<PrescriptionServiceRoutine>();
            PrescriptionSettingService = new HashSet<PrescriptionSetting>();
            PrescriptionSettingServiceGroup = new HashSet<PrescriptionSetting>();
            ReceptionServiceService = new HashSet<ReceptionService>();
            ReceptionServiceServiceChild = new HashSet<ReceptionService>();
            RelatedTestMainService = new HashSet<RelatedTest>();
            RelatedTestSubService = new HashSet<RelatedTest>();
            RichLongAnswer = new HashSet<RichLongAnswer>();
            SectionService = new HashSet<SectionService>();
            ServiceCoding = new HashSet<ServiceCoding>();
            TestAnswerTime = new HashSet<TestAnswerTime>();
            TestRange = new HashSet<TestRange>();
        }

        public int Id { get; set; }
        public int? Code { get; set; }
        public string LocalCode { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public decimal? BasePrice { get; set; }
        public string InterNationalCode { get; set; }
        public string LatinDescription { get; set; }
        public int? IcdCodeId { get; set; }
        public int? ParentId { get; set; }
        public int? ServiceTypeId { get; set; }
        public bool? IsActive { get; set; }
        public int? ServiceLevelId { get; set; }
        public string Note { get; set; }
        public string NoteLang2 { get; set; }
        public int? Info { get; set; }
        public int? UnitId { get; set; }
        public int? SampleTypeId { get; set; }
        public decimal? SampleSize { get; set; }
        public int? TestTubeColorId { get; set; }
        public bool? IsDiff { get; set; }
        public string AbbreviationTitle { get; set; }
        public int? LabTypeId { get; set; }
        public int? LabKitId { get; set; }
        public bool? IsNumeric { get; set; }
        public bool? IsHasAttachment { get; set; }
        public int? HourToResponseNormal { get; set; }
        public int? HourToResponseEmergency { get; set; }
        public string FormulaExpr { get; set; }
        public bool? IsLongAnswer { get; set; }
        public int? GroupAnswerId { get; set; }
        public int? TestAnswerTypeId { get; set; }
        public int? Arrange { get; set; }
        public int? DataTypeId { get; set; }
        public bool? HasBiopsy { get; set; }
        public bool? IsAnswerAtMoment { get; set; }
        public int? RichLongAnswerId { get; set; }
        public string Component { get; set; }
        public string RelatedNames { get; set; }
        public string LongCommonName { get; set; }
        public int? TimeAspctId { get; set; }
        public int? MethodTypeId { get; set; }
        public int? OrderObservationId { get; set; }
        public int? ScaleTypeId { get; set; }
        public int? PropertyId { get; set; }
        public bool? IsCommon { get; set; }
        public bool? IsLaboratoryOrderPanels { get; set; }

        public virtual DataType DataType { get; set; }
        public virtual GroupAnswer1 GroupAnswer { get; set; }
        public virtual MethodType MethodType { get; set; }
        public virtual Services Parent { get; set; }
        public virtual Property Property { get; set; }
        public virtual RichLongAnswer RichLongAnswerNavigation { get; set; }
        public virtual ScaleType ScaleType { get; set; }
        public virtual ServiceLevel ServiceLevel { get; set; }
        public virtual ServiceType ServiceType { get; set; }
        public virtual TestAnswerType TestAnswerType { get; set; }
        public virtual TimeAspct TimeAspct { get; set; }
        public virtual ICollection<CenterServices> CenterServices { get; set; }
        public virtual ICollection<DefineRoutineTest> DefineRoutineTest { get; set; }
        public virtual ICollection<Instruction> Instruction { get; set; }
        public virtual ICollection<Services> InverseParent { get; set; }
        public virtual ICollection<PrescriptionDetailService> PrescriptionDetailService { get; set; }
        public virtual ICollection<PrescriptionGroupSetting> PrescriptionGroupSetting { get; set; }
        public virtual ICollection<PrescriptionServiceResult> PrescriptionServiceResult { get; set; }
        public virtual ICollection<PrescriptionServiceRoutine> PrescriptionServiceRoutine { get; set; }
        public virtual ICollection<PrescriptionSetting> PrescriptionSettingService { get; set; }
        public virtual ICollection<PrescriptionSetting> PrescriptionSettingServiceGroup { get; set; }
        public virtual ICollection<ReceptionService> ReceptionServiceService { get; set; }
        public virtual ICollection<ReceptionService> ReceptionServiceServiceChild { get; set; }
        public virtual ICollection<RelatedTest> RelatedTestMainService { get; set; }
        public virtual ICollection<RelatedTest> RelatedTestSubService { get; set; }
        public virtual ICollection<RichLongAnswer> RichLongAnswer { get; set; }
        public virtual ICollection<SectionService> SectionService { get; set; }
        public virtual ICollection<ServiceCoding> ServiceCoding { get; set; }
        public virtual ICollection<TestAnswerTime> TestAnswerTime { get; set; }
        public virtual ICollection<TestRange> TestRange { get; set; }
    }
}
