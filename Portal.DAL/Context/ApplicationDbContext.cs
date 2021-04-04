using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Portal.Context;
using Portal.DTO;
using Portal.Entities.Models;
using System;
using System.Collections.Generic;

using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;


namespace Portal.DAL.Context
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        #region dbsets
        public virtual DbSet<ActionType> ActionType { get; set; }
        public virtual DbSet<Allergy> Allergy { get; set; }
        public virtual DbSet<Answer> Answer { get; set; }
        public virtual DbSet<Answer1> Answer1 { get; set; }
        public virtual DbSet<AnswerService> AnswerService { get; set; }
        public virtual DbSet<AppointmentType> AppointmentType { get; set; }
        public virtual DbSet<AspNetUsersOk> AspNetUsersOk { get; set; }
        public virtual DbSet<BagBlood> BagBlood { get; set; }
        public virtual DbSet<BasketDrug> BasketDrug { get; set; }
        public virtual DbSet<Bed> Bed { get; set; }
        public virtual DbSet<BedReception> BedReception { get; set; }
        public virtual DbSet<BedReserveStatus> BedReserveStatus { get; set; }
        public virtual DbSet<BedStatus> BedStatus { get; set; }
        public virtual DbSet<BedType> BedType { get; set; }
        public virtual DbSet<BloodBankLabelStyle> BloodBankLabelStyle { get; set; }
        public virtual DbSet<BloodBankLabeling> BloodBankLabeling { get; set; }
        public virtual DbSet<BloodGroup> BloodGroup { get; set; }
        public virtual DbSet<BloodProductType> BloodProductType { get; set; }
        public virtual DbSet<BloodStatus> BloodStatus { get; set; }
        public virtual DbSet<BloodStatusHistory> BloodStatusHistory { get; set; }
        public virtual DbSet<Branch> Branch { get; set; }
        public virtual DbSet<CardCode> CardCode { get; set; }
        public virtual DbSet<CashStatus> CashStatus { get; set; }
        public virtual DbSet<CauseBloodNeed> CauseBloodNeed { get; set; }
        public virtual DbSet<Center> Center { get; set; }
        public virtual DbSet<CenterExamPlace> CenterExamPlace { get; set; }
        public virtual DbSet<CenterSellingType> CenterSellingType { get; set; }
        public virtual DbSet<CenterServices> CenterServices { get; set; }
        public virtual DbSet<CenterType> CenterType { get; set; }
        public virtual DbSet<CenterWorkItem> CenterWorkItem { get; set; }
        public virtual DbSet<Certificate> Certificate { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<ClearanceType> ClearanceType { get; set; }
        public virtual DbSet<Color> Color { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<CompanyAndType> CompanyAndType { get; set; }
        public virtual DbSet<CompanyPersonality> CompanyPersonality { get; set; }
        public virtual DbSet<CompanyType> CompanyType { get; set; }
        public virtual DbSet<ConsumptionInstruction> ConsumptionInstruction { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<DataTransferConvert> DataTransferConvert { get; set; }
        public virtual DbSet<DataType> DataType { get; set; }
        public virtual DbSet<DefineRoutine> DefineRoutine { get; set; }
        public virtual DbSet<DefineRoutineTest> DefineRoutineTest { get; set; }
        public virtual DbSet<Degree> Degree { get; set; }
        public virtual DbSet<Diagnosis> Diagnosis { get; set; }
        public virtual DbSet<DiagnosisStatus> DiagnosisStatus { get; set; }
        public virtual DbSet<Diet> Diet { get; set; }
        public virtual DbSet<DoctorAppointment> DoctorAppointment { get; set; }
        public virtual DbSet<DoctorAppointmentDetail> DoctorAppointmentDetail { get; set; }
        public virtual DbSet<DoctorAppointmentDetailStatus> DoctorAppointmentDetailStatus { get; set; }
        public virtual DbSet<DoctorCertificate> DoctorCertificate { get; set; }
        public virtual DbSet<DoctorDayOff> DoctorDayOff { get; set; }
        public virtual DbSet<DoctorDegree> DoctorDegree { get; set; }
        public virtual DbSet<DoctorService> DoctorService { get; set; }
        public virtual DbSet<DoctorVisitType> DoctorVisitType { get; set; }
        public virtual DbSet<Doctors> Doctors { get; set; }
        public virtual DbSet<DoctorsOk> DoctorsOk { get; set; }
        public virtual DbSet<DrugForm> DrugForm { get; set; }
        public virtual DbSet<DrugInteractions> DrugInteractions { get; set; }
        public virtual DbSet<DrugStoregCondition> DrugStoregCondition { get; set; }
        public virtual DbSet<Drugs> Drugs { get; set; }
        public virtual DbSet<DurationType> DurationType { get; set; }
        public virtual DbSet<Education> Education { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Examplace> Examplace { get; set; }
        public virtual DbSet<Expertise> Expertise { get; set; }
        public virtual DbSet<FileGroup> FileGroup { get; set; }
        public virtual DbSet<FileTag> FileTag { get; set; }
        public virtual DbSet<Files> Files { get; set; }
        public virtual DbSet<Frequency> Frequency { get; set; }
        public virtual DbSet<GeneralStatus> GeneralStatus { get; set; }
        public virtual DbSet<GenericDrug> GenericDrug { get; set; }
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<GroupAnswer> GroupAnswer { get; set; }
        public virtual DbSet<GroupAnswer1> GroupAnswer1 { get; set; }
        public virtual DbSet<GroupReadyAnswer> GroupReadyAnswer { get; set; }
        public virtual DbSet<GroupReadyAnswer1> GroupReadyAnswer1 { get; set; }
        public virtual DbSet<Illness> Illness { get; set; }
        public virtual DbSet<ImportedDicom> ImportedDicom { get; set; }
        public virtual DbSet<Instruction> Instruction { get; set; }
        public virtual DbSet<Interaction> Interaction { get; set; }
        public virtual DbSet<Job> Job { get; set; }
        public virtual DbSet<JobType> JobType { get; set; }
        public virtual DbSet<Kit> Kit { get; set; }
        public virtual DbSet<LabType> LabType { get; set; }
        public virtual DbSet<LabelBloodBag> LabelBloodBag { get; set; }
        public virtual DbSet<LabelSampleTest> LabelSampleTest { get; set; }
        public virtual DbSet<LabelSampleTestStyle> LabelSampleTestStyle { get; set; }
        public virtual DbSet<Login> Login { get; set; }
        public virtual DbSet<MaritalStatus> MaritalStatus { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<MethodType> MethodType { get; set; }
        public virtual DbSet<MobileActivation> MobileActivation { get; set; }
        public virtual DbSet<Nationality> Nationality { get; set; }
        public virtual DbSet<OwnershipType> OwnershipType { get; set; }
        public virtual DbSet<PackagingType> PackagingType { get; set; }
        public virtual DbSet<PanelType> PanelType { get; set; }
        public virtual DbSet<Passport> Passport { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<PatientExtraInfo> PatientExtraInfo { get; set; }
        public virtual DbSet<PatientHistoryBloodTransfusion> PatientHistoryBloodTransfusion { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<PaymentType> PaymentType { get; set; }
        public virtual DbSet<Pcr> Pcr { get; set; }
        public virtual DbSet<PermissionSectionField> PermissionSectionField { get; set; }
        public virtual DbSet<Permissions> Permissions { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Pharmacies> Pharmacies { get; set; }
        public virtual DbSet<Place> Place { get; set; }
        public virtual DbSet<PregnancySeason> PregnancySeason { get; set; }
        public virtual DbSet<PrescribtionType> PrescribtionType { get; set; }
        public virtual DbSet<Prescription> Prescription { get; set; }
        public virtual DbSet<PrescriptionAllergy> PrescriptionAllergy { get; set; }
        public virtual DbSet<PrescriptionChartActionType> PrescriptionChartActionType { get; set; }
        public virtual DbSet<PrescriptionChartCancelationReason> PrescriptionChartCancelationReason { get; set; }
        public virtual DbSet<PrescriptionDetailDrug> PrescriptionDetailDrug { get; set; }
        public virtual DbSet<PrescriptionDetailDrugHistory> PrescriptionDetailDrugHistory { get; set; }
        public virtual DbSet<PrescriptionDetailPharmacistNote> PrescriptionDetailPharmacistNote { get; set; }
        public virtual DbSet<PrescriptionDetailService> PrescriptionDetailService { get; set; }
        public virtual DbSet<PrescriptionDetailServiceHistory> PrescriptionDetailServiceHistory { get; set; }
        public virtual DbSet<PrescriptionDetails> PrescriptionDetails { get; set; }
        public virtual DbSet<PrescriptionDiet> PrescriptionDiet { get; set; }
        public virtual DbSet<PrescriptionDrugChart> PrescriptionDrugChart { get; set; }
        public virtual DbSet<PrescriptionDrugRoutine> PrescriptionDrugRoutine { get; set; }
        public virtual DbSet<PrescriptionGroupItem> PrescriptionGroupItem { get; set; }
        public virtual DbSet<PrescriptionGroupSetting> PrescriptionGroupSetting { get; set; }
        public virtual DbSet<PrescriptionHistoryStatus> PrescriptionHistoryStatus { get; set; }
        public virtual DbSet<PrescriptionInstruction> PrescriptionInstruction { get; set; }
        public virtual DbSet<PrescriptionPanel> PrescriptionPanel { get; set; }
        public virtual DbSet<PrescriptionRoutine> PrescriptionRoutine { get; set; }
        public virtual DbSet<PrescriptionServiceChart> PrescriptionServiceChart { get; set; }
        public virtual DbSet<PrescriptionServiceResult> PrescriptionServiceResult { get; set; }
        public virtual DbSet<PrescriptionServiceRoutine> PrescriptionServiceRoutine { get; set; }
        public virtual DbSet<PrescriptionSetting> PrescriptionSetting { get; set; }
        public virtual DbSet<PrescriptionShare> PrescriptionShare { get; set; }
        public virtual DbSet<PrescriptionSign> PrescriptionSign { get; set; }
        public virtual DbSet<PrescriptionType> PrescriptionType { get; set; }
        public virtual DbSet<Presenter> Presenter { get; set; }
        public virtual DbSet<PriceTypeMethod> PriceTypeMethod { get; set; }
        public virtual DbSet<Priority> Priority { get; set; }
        public virtual DbSet<ProceedFromBagNumber> ProceedFromBagNumber { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<ProhibitedUsage> ProhibitedUsage { get; set; }
        public virtual DbSet<Property> Property { get; set; }
        public virtual DbSet<Province> Province { get; set; }
        public virtual DbSet<PurchaseInvoice> PurchaseInvoice { get; set; }
        public virtual DbSet<PurchaseInvoiceDetails> PurchaseInvoiceDetails { get; set; }
        public virtual DbSet<PurposeRequestBlood> PurposeRequestBlood { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<ReadyAnswer> ReadyAnswer { get; set; }
        public virtual DbSet<ReadyAnswer1> ReadyAnswer1 { get; set; }
        public virtual DbSet<Reception> Reception { get; set; }
        public virtual DbSet<ReceptionAnswer> ReceptionAnswer { get; set; }
        public virtual DbSet<ReceptionDetail> ReceptionDetail { get; set; }
        public virtual DbSet<ReceptionDiagnosis> ReceptionDiagnosis { get; set; }
        public virtual DbSet<ReceptionDiagnosisHistory> ReceptionDiagnosisHistory { get; set; }
        public virtual DbSet<ReceptionDrug> ReceptionDrug { get; set; }
        public virtual DbSet<ReceptionHistory> ReceptionHistory { get; set; }
        public virtual DbSet<ReceptionSectionDoctor> ReceptionSectionDoctor { get; set; }
        public virtual DbSet<ReceptionService> ReceptionService { get; set; }
        public virtual DbSet<ReceptionServiceStatus> ReceptionServiceStatus { get; set; }
        public virtual DbSet<ReceptionSign> ReceptionSign { get; set; }
        public virtual DbSet<ReceptionType> ReceptionType { get; set; }
        public virtual DbSet<Receptions> Receptions { get; set; }
        public virtual DbSet<RefferFrom> RefferFrom { get; set; }
        public virtual DbSet<RefferReason> RefferReason { get; set; }
        public virtual DbSet<RelatedTest> RelatedTest { get; set; }
        public virtual DbSet<RelationShip> RelationShip { get; set; }
        public virtual DbSet<Religion> Religion { get; set; }
        public virtual DbSet<Request> Request { get; set; }
        public virtual DbSet<RequestDetail> RequestDetail { get; set; }
        public virtual DbSet<RequestDetails> RequestDetails { get; set; }
        public virtual DbSet<RequestHistory> RequestHistory { get; set; }
        public virtual DbSet<RequestStatus> RequestStatus { get; set; }
        public virtual DbSet<RequestType> RequestType { get; set; }
        public virtual DbSet<RespiratoryServices> RespiratoryServices { get; set; }
        public virtual DbSet<Rh> Rh { get; set; }
        public virtual DbSet<RichLongAnswer> RichLongAnswer { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RoleGroup> RoleGroup { get; set; }
        public virtual DbSet<RolePermission> RolePermission { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<RoomType> RoomType { get; set; }
        public virtual DbSet<SampleType> SampleType { get; set; }
        public virtual DbSet<SamplerType> SamplerType { get; set; }
        public virtual DbSet<Sampling> Sampling { get; set; }
        public virtual DbSet<SamplingHistory> SamplingHistory { get; set; }
        public virtual DbSet<SamplingStatus> SamplingStatus { get; set; }
        public virtual DbSet<ScaleType> ScaleType { get; set; }
        public virtual DbSet<Scientificlevel> Scientificlevel { get; set; }
        public virtual DbSet<Section> Section { get; set; }
        public virtual DbSet<SectionEssentialsDrug> SectionEssentialsDrug { get; set; }
        public virtual DbSet<SectionEssentialsGenericDrug> SectionEssentialsGenericDrug { get; set; }
        public virtual DbSet<SectionField> SectionField { get; set; }
        public virtual DbSet<SectionKind> SectionKind { get; set; }
        public virtual DbSet<SectionProperty> SectionProperty { get; set; }
        public virtual DbSet<SectionPropertyList> SectionPropertyList { get; set; }
        public virtual DbSet<SectionService> SectionService { get; set; }
        public virtual DbSet<SectionStatus> SectionStatus { get; set; }
        public virtual DbSet<SectionStore> SectionStore { get; set; }
        public virtual DbSet<SectionType> SectionType { get; set; }
        public virtual DbSet<SellingType> SellingType { get; set; }
        public virtual DbSet<SendSmsStatus> SendSmsStatus { get; set; }
        public virtual DbSet<ServiceCoding> ServiceCoding { get; set; }
        public virtual DbSet<ServiceLevel> ServiceLevel { get; set; }
        public virtual DbSet<ServiceType> ServiceType { get; set; }
        public virtual DbSet<Services> Services { get; set; }
        public virtual DbSet<Setting> Setting { get; set; }
        public virtual DbSet<Setting1> Setting1 { get; set; }
        public virtual DbSet<Sex> Sex { get; set; }
        public virtual DbSet<SideEffects> SideEffects { get; set; }
        public virtual DbSet<Sign> Sign { get; set; }
        public virtual DbSet<SmsReception> SmsReception { get; set; }
        public virtual DbSet<SpecialIllness> SpecialIllness { get; set; }
        public virtual DbSet<SpecialIllnessReception> SpecialIllnessReception { get; set; }
        public virtual DbSet<StoreDrug> StoreDrug { get; set; }
        public virtual DbSet<StoreEntery> StoreEntery { get; set; }
        public virtual DbSet<StoreEntry> StoreEntry { get; set; }
        public virtual DbSet<StoreEntryDetails> StoreEntryDetails { get; set; }
        public virtual DbSet<StoreTransfer> StoreTransfer { get; set; }
        public virtual DbSet<StoreTransferDetails> StoreTransferDetails { get; set; }
        public virtual DbSet<StoregCondition> StoregCondition { get; set; }
        public virtual DbSet<Table1> Table1 { get; set; }
        public virtual DbSet<Temp> Temp { get; set; }
        public virtual DbSet<TemperatureType> TemperatureType { get; set; }
        public virtual DbSet<Template> Template { get; set; }
        public virtual DbSet<TestAnswerTime> TestAnswerTime { get; set; }
        public virtual DbSet<TestAnswerType> TestAnswerType { get; set; }
        public virtual DbSet<TestLabeling> TestLabeling { get; set; }
        public virtual DbSet<TestRange> TestRange { get; set; }
        public virtual DbSet<TimeAspct> TimeAspct { get; set; }
        public virtual DbSet<Unit> Unit { get; set; }
        public virtual DbSet<Unit1> Unit1 { get; set; }
        public virtual DbSet<Unit2> Unit2 { get; set; }
        public virtual DbSet<UserCardCode> UserCardCode { get; set; }
        public virtual DbSet<UserPermission> UserPermission { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<UserRolePermission> UserRolePermission { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<VisitType> VisitType { get; set; }
        public virtual DbSet<VitalSigns> VitalSigns { get; set; }
        public virtual DbSet<WayOfPrescription> WayOfPrescription { get; set; }
        public virtual DbSet<WorkTimeType> WorkTimeType { get; set; }
        public virtual DbSet<Zone> Zone { get; set; }



        #endregion

        #region dbquary 

        //public DbQuery<Docdto> docdtos { get; set; }
        public virtual DbSet<PcrReportSp> PcrReportSp { get; set; }
        public virtual DbSet<PcrReportRefferFromSp> ReportRefferFromSp { get; set; }
        public virtual DbSet<PcrReportTestResultSp> PcrReportTestResultSp { get; set; }
        public virtual DbSet<SearchReceptionSp> SearchReceptionSp { get; set; }

        public DbContext Context => this.Context;

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActionType>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<Allergy>(entity =>
            {
                entity.Property(e => e.LocalCode).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(20);

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answer)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK_Answer_Question");
            });

            modelBuilder.Entity<Answer1>(entity =>
            {
                entity.ToTable("Answer", "Lab");

                entity.Property(e => e.Comment).HasMaxLength(2500);

                entity.Property(e => e.Result).HasMaxLength(4000);

                entity.HasOne(d => d.ReadyAnswer)
                    .WithMany(p => p.Answer1)
                    .HasForeignKey(d => d.ReadyAnswerId)
                    .HasConstraintName("FK_Answer_ReadyAnswer");

                entity.HasOne(d => d.ReceptionService)
                    .WithMany(p => p.Answer1)
                    .HasForeignKey(d => d.ReceptionServiceId)
                    .HasConstraintName("FK_Answer_ReceptionService");
            });

            modelBuilder.Entity<AnswerService>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.FileId).HasMaxLength(50);

                entity.Property(e => e.FileName).HasMaxLength(50);

                entity.Property(e => e.IsOutSystem).HasComment("ارسال جواب از سیستم خارجی");

                entity.Property(e => e.Mobile).HasMaxLength(20);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Nhsnumber)
                    .HasColumnName("NHSNumber")
                    .HasMaxLength(50);

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.Property(e => e.PatientStatus).HasMaxLength(50);

                entity.Property(e => e.RefferFrom).HasMaxLength(250);

                entity.Property(e => e.Result).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.HasOne(d => d.InsertUser)
                    .WithMany(p => p.AnswerServiceInsertUser)
                    .HasForeignKey(d => d.InsertUserId)
                    .HasConstraintName("FK_AnswerService_Users1");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.AnswerService)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK_AnswerService_Patient");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.AnswerService)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("FK_AnswerService_Section");

                entity.HasOne(d => d.SendsmsStatus)
                    .WithMany(p => p.AnswerService)
                    .HasForeignKey(d => d.SendsmsStatusId)
                    .HasConstraintName("FK_AnswerService_SendSmsStatus");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AnswerServiceUser)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_AnswerService_Users");
            });

            modelBuilder.Entity<AppointmentType>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<AspNetUsersOk>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<BagBlood>(entity =>
            {
                entity.ToTable("BagBlood", "Lab");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.DonationCode).HasMaxLength(50);

                entity.Property(e => e.DonationDate).HasColumnType("datetime");

                entity.Property(e => e.Doner).HasMaxLength(50);

                entity.Property(e => e.EnterDate).HasColumnType("datetime");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Volume).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.BloodProductType)
                    .WithMany(p => p.BagBlood)
                    .HasForeignKey(d => d.BloodProductTypeId)
                    .HasConstraintName("FK_BagBlood_BloodProductType");

                entity.HasOne(d => d.ProceedFromBagNumber)
                    .WithMany(p => p.BagBlood)
                    .HasForeignKey(d => d.ProceedFromBagNumberId)
                    .HasConstraintName("FK_BagBlood_ProceedFromBagNumber");
            });

            modelBuilder.Entity<BasketDrug>(entity =>
            {
                entity.ToTable("BasketDrug", "Drug");

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.HasOne(d => d.PrescriptionDetailDrug)
                    .WithMany(p => p.BasketDrug)
                    .HasForeignKey(d => d.PrescriptionDetailDrugId)
                    .HasConstraintName("FK_BasketDrug_PrescriptionDetailDrug");

                entity.HasOne(d => d.Reception)
                    .WithMany(p => p.BasketDrug)
                    .HasForeignKey(d => d.ReceptionId)
                    .HasConstraintName("FK_BasketDrug_Receptions");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.BasketDrug)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Role_BasketDrug");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BasketDrug)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_BasketDrug_Drugs");
            });

            modelBuilder.Entity<Bed>(entity =>
            {
                entity.Property(e => e.BedNo).HasMaxLength(20);

                entity.Property(e => e.LocalCode).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);

                entity.HasOne(d => d.BedType)
                    .WithMany(p => p.Bed)
                    .HasForeignKey(d => d.BedTypeId)
                    .HasConstraintName("FK_Bed_BedType");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Bed)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK_Bed_Room");
            });

            modelBuilder.Entity<BedReception>(entity =>
            {
                entity.Property(e => e.Createdate).HasColumnType("datetime");

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.FromId).HasComment("بعد از در ج جدید از ایدی جدول قبلی پر می شود");

                entity.Property(e => e.ToDate)
                    .HasColumnType("datetime")
                    .HasComment("بعد از درج ستر جدید ستر قبلی نسبت به تاریخ ان اپدیت می شود");

                entity.HasOne(d => d.Bed)
                    .WithMany(p => p.BedReception)
                    .HasForeignKey(d => d.BedId)
                    .HasConstraintName("FK_BedReception_Bed");

                entity.HasOne(d => d.BedReserveStatus)
                    .WithMany(p => p.BedReception)
                    .HasForeignKey(d => d.BedReserveStatusId)
                    .HasConstraintName("FK_BedReception_BedReserveStatus");

                entity.HasOne(d => d.Reception)
                    .WithMany(p => p.BedReception)
                    .HasForeignKey(d => d.ReceptionId)
                    .HasConstraintName("FK_BedReception_Receptions");
            });

            modelBuilder.Entity<BedReserveStatus>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<BedStatus>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<BedType>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<BloodBankLabelStyle>(entity =>
            {
                entity.ToTable("BloodBankLabelStyle", "Lab");

                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<BloodBankLabeling>(entity =>
            {
                entity.ToTable("BloodBankLabeling", "Lab");

                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<BloodGroup>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<BloodProductType>(entity =>
            {
                entity.ToTable("BloodProductType", "Lab");

                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<BloodStatus>(entity =>
            {
                entity.ToTable("BloodStatus", "Lab");

                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<BloodStatusHistory>(entity =>
            {
                entity.ToTable("BloodStatusHistory", "Lab");

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.Property(e => e.StatusDate).HasColumnType("datetime");

                entity.HasOne(d => d.BagBlood)
                    .WithMany(p => p.BloodStatusHistory)
                    .HasForeignKey(d => d.BagBloodId)
                    .HasConstraintName("FK_BloodStatusHistory_BagBlood");

                entity.HasOne(d => d.BloodStatus)
                    .WithMany(p => p.BloodStatusHistory)
                    .HasForeignKey(d => d.BloodStatusId)
                    .HasConstraintName("FK_BloodStatusHistory_BloodStatus");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.BloodStatusHistory)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Role_BloodStatusHistory");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BloodStatusHistory)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_BloodStatusHistory_Users");
            });

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<CardCode>(entity =>
            {
                entity.Property(e => e.HealthNumber).HasMaxLength(255);

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.Serial).HasMaxLength(255);

                entity.Property(e => e.Token).HasMaxLength(255);

                entity.Property(e => e.UserName).HasMaxLength(255);
            });

            modelBuilder.Entity<CashStatus>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<CauseBloodNeed>(entity =>
            {
                entity.ToTable("CauseBloodNeed", "Lab");

                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<Center>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.AnswerTime).HasMaxLength(50);

                entity.Property(e => e.AreaCode).HasMaxLength(20);

                entity.Property(e => e.Boss).HasMaxLength(100);

                entity.Property(e => e.BossPhone).HasMaxLength(20);

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.ExamTime).HasMaxLength(50);

                entity.Property(e => e.Fax).HasMaxLength(20);

                entity.Property(e => e.Latitude).HasMaxLength(20);

                entity.Property(e => e.Longitude).HasMaxLength(20);

                entity.Property(e => e.Mobile).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.PhoneResponseHourse).HasMaxLength(20);

                entity.Property(e => e.PostalCode).HasMaxLength(20);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.VisitorOrManaging).HasMaxLength(50);

                entity.Property(e => e.VisitorPhone).HasMaxLength(20);

                entity.Property(e => e.WorkingHours).HasMaxLength(200);

                entity.HasOne(d => d.CenterType)
                    .WithMany(p => p.Center)
                    .HasForeignKey(d => d.CenterTypeId)
                    .HasConstraintName("FK_CenterType_Center");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Center)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Center_City");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Center)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Center_Country");

                entity.HasOne(d => d.OwnershipType)
                    .WithMany(p => p.Center)
                    .HasForeignKey(d => d.OwnershipTypeId)
                    .HasConstraintName("FK_Center_OwnershipType");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Center)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK_Center_Province");

                entity.HasOne(d => d.WorkTimeType)
                    .WithMany(p => p.Center)
                    .HasForeignKey(d => d.WorkTimeTypeId)
                    .HasConstraintName("FK_Center_WorkTimeType");

                entity.HasOne(d => d.Zone)
                    .WithMany(p => p.Center)
                    .HasForeignKey(d => d.ZoneId)
                    .HasConstraintName("FK_Center_Zone");
            });

            modelBuilder.Entity<CenterExamPlace>(entity =>
            {
                entity.HasOne(d => d.Center)
                    .WithMany(p => p.CenterExamPlace)
                    .HasForeignKey(d => d.CenterId)
                    .HasConstraintName("FK_CenterExamPlace_Center");

                entity.HasOne(d => d.Examplace)
                    .WithMany(p => p.CenterExamPlace)
                    .HasForeignKey(d => d.ExamplaceId)
                    .HasConstraintName("FK_CenterExamPlace_Examplace");
            });

            modelBuilder.Entity<CenterSellingType>(entity =>
            {
                entity.HasOne(d => d.Center)
                    .WithMany(p => p.CenterSellingType)
                    .HasForeignKey(d => d.CenterId)
                    .HasConstraintName("FK_CenterSellingType_Center");

                entity.HasOne(d => d.SellingType)
                    .WithMany(p => p.CenterSellingType)
                    .HasForeignKey(d => d.SellingTypeId)
                    .HasConstraintName("FK_CenterSellingType_SellingType");
            });

            modelBuilder.Entity<CenterServices>(entity =>
            {
                entity.HasOne(d => d.Center)
                    .WithMany(p => p.CenterServices)
                    .HasForeignKey(d => d.CenterId)
                    .HasConstraintName("FK_CenterService_Center");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.CenterServices)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK_CenterService_Services");
            });

            modelBuilder.Entity<CenterType>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<CenterWorkItem>(entity =>
            {
                entity.HasOne(d => d.Center)
                    .WithMany(p => p.CenterWorkItem)
                    .HasForeignKey(d => d.CenterId)
                    .HasConstraintName("FK_CenterWorkItem_Center");

                entity.HasOne(d => d.WorkItem)
                    .WithMany(p => p.CenterWorkItem)
                    .HasForeignKey(d => d.WorkItemId)
                    .HasConstraintName("FK_CenterWorkItem_WorkTimeType");
            });

            modelBuilder.Entity<Certificate>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(250);

                entity.Property(e => e.TitleLang2).HasMaxLength(250);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.City)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK_City_Province");
            });

            modelBuilder.Entity<ClearanceType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.ToTable("Color", "Lab");

                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company", "Drug");

                entity.Property(e => e.Address).HasMaxLength(2500);

                entity.Property(e => e.Boss).HasMaxLength(100);

                entity.Property(e => e.BossPhone).HasMaxLength(20);

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Fax).HasMaxLength(20);

                entity.Property(e => e.NationalId).HasMaxLength(50);

                entity.Property(e => e.Note).HasMaxLength(2500);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.PostalCode).HasMaxLength(20);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.Visitor).HasMaxLength(100);

                entity.Property(e => e.VisitorPhone).HasMaxLength(20);

                entity.HasOne(d => d.ComponyPersonality)
                    .WithMany(p => p.Company)
                    .HasForeignKey(d => d.ComponyPersonalityId)
                    .HasConstraintName("FK_Company_ComponyPersonality");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Company)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Company_Country");
            });

            modelBuilder.Entity<CompanyAndType>(entity =>
            {
                entity.ToTable("CompanyAndType", "Drug");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyAndType)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_Company_CompanyAndType");

                entity.HasOne(d => d.CompanyType)
                    .WithMany(p => p.CompanyAndType)
                    .HasForeignKey(d => d.CompanyTypeId)
                    .HasConstraintName("FK_CompanyType_CompanyAndType");
            });

            modelBuilder.Entity<CompanyPersonality>(entity =>
            {
                entity.ToTable("CompanyPersonality", "Drug");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<CompanyType>(entity =>
            {
                entity.ToTable("CompanyType", "Drug");

                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<ConsumptionInstruction>(entity =>
            {
                entity.ToTable("ConsumptionInstruction", "Drug");

                entity.Property(e => e.Code)
                    .HasMaxLength(250)
                    .HasComment("ممکن است بخواهیم قسمت انگلیسی دستور مصرف را از قسمت فارسی جدا کنیم");

                entity.Property(e => e.Title).HasMaxLength(250);

                entity.Property(e => e.TitleLang2).HasMaxLength(250);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<DataTransferConvert>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.TableId).HasMaxLength(500);

                entity.Property(e => e.TableName).HasMaxLength(500);

                entity.Property(e => e.TargetTableId).HasMaxLength(500);
            });

            modelBuilder.Entity<DataType>(entity =>
            {
                entity.Property(e => e.CsharpType).HasMaxLength(20);

                entity.Property(e => e.SqlType).HasMaxLength(20);

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<DefineRoutine>(entity =>
            {
                entity.ToTable("DefineRoutine", "Lab");

                entity.Property(e => e.Title).HasMaxLength(150);
            });

            modelBuilder.Entity<DefineRoutineTest>(entity =>
            {
                entity.ToTable("DefineRoutineTest", "Lab");

                entity.HasOne(d => d.DefineRoutine)
                    .WithMany(p => p.DefineRoutineTest)
                    .HasForeignKey(d => d.DefineRoutineId)
                    .HasConstraintName("FK_DefineRoutineTest_DefineRoutine");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.DefineRoutineTest)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK_DefineRoutineTest_Services");
            });

            modelBuilder.Entity<Degree>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<Diagnosis>(entity =>
            {
                entity.Property(e => e.LocalCode).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(4000);

                entity.Property(e => e.TitleLang2).HasMaxLength(4000);
            });

            modelBuilder.Entity<DiagnosisStatus>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<Diet>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<DoctorAppointment>(entity =>
            {
                entity.Property(e => e.EndDateTime).HasColumnType("datetime");

                entity.Property(e => e.StartDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.AppointmentType)
                    .WithMany(p => p.DoctorAppointment)
                    .HasForeignKey(d => d.AppointmentTypeId)
                    .HasConstraintName("FK_DoctorAppointment_AppointmentType");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.DoctorAppointment)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_DoctorAppointment_Doctors");

                entity.HasOne(d => d.DurationType)
                    .WithMany(p => p.DoctorAppointment)
                    .HasForeignKey(d => d.DurationTypeId)
                    .HasConstraintName("FK_DoctorAppointment_DurationType");
            });

            modelBuilder.Entity<DoctorAppointmentDetail>(entity =>
            {
                entity.Property(e => e.AppointmentDate).HasColumnType("datetime");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.HasOne(d => d.DoctorAppointmentDetailStatus)
                    .WithMany(p => p.DoctorAppointmentDetail)
                    .HasForeignKey(d => d.DoctorAppointmentDetailStatusId)
                    .HasConstraintName("FK_DoctorAppointmentDetail_DoctorAppointmentDetail");

                entity.HasOne(d => d.DoctorAppointment)
                    .WithMany(p => p.DoctorAppointmentDetail)
                    .HasForeignKey(d => d.DoctorAppointmentId)
                    .HasConstraintName("FK_DoctorAppointmentDetail_DoctorAppointment");

                entity.HasOne(d => d.Reception)
                    .WithMany(p => p.DoctorAppointmentDetail)
                    .HasForeignKey(d => d.ReceptionId)
                    .HasConstraintName("FK_DoctorAppointmentDetail_Receptions");
            });

            modelBuilder.Entity<DoctorAppointmentDetailStatus>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<DoctorCertificate>(entity =>
            {
                entity.HasOne(d => d.Certificate)
                    .WithMany(p => p.DoctorCertificate)
                    .HasForeignKey(d => d.CertificateId)
                    .HasConstraintName("FK_Certificate_DoctorCertificate");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.DoctorCertificate)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_Doctors_DoctorCertificate");
            });

            modelBuilder.Entity<DoctorDayOff>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.DoctorAppointment)
                    .WithMany(p => p.DoctorDayOff)
                    .HasForeignKey(d => d.DoctorAppointmentId)
                    .HasConstraintName("FK_DoctorDayOff_DoctorAppointment");
            });

            modelBuilder.Entity<DoctorDegree>(entity =>
            {
                entity.Property(e => e.DateOfIssue).HasColumnType("datetime");

                entity.HasOne(d => d.Degree)
                    .WithMany(p => p.DoctorDegree)
                    .HasForeignKey(d => d.DegreeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Degree_DoctorDegree");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.DoctorDegree)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Doctors_DoctorDegree");
            });

            modelBuilder.Entity<DoctorService>(entity =>
            {
                entity.Property(e => e.Discount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<DoctorVisitType>(entity =>
            {
                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.DoctorVisitType)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_DoctorVisitType_Doctors");

                entity.HasOne(d => d.VisitType)
                    .WithMany(p => p.DoctorVisitType)
                    .HasForeignKey(d => d.VisitTypeId)
                    .HasConstraintName("FK_DoctorVisitType_VisitType");
            });

            modelBuilder.Entity<Doctors>(entity =>
            {
                entity.Property(e => e.AddressClinic).HasMaxLength(100);

                entity.Property(e => e.CooperationDate).HasColumnType("datetime");

                entity.Property(e => e.CostVisit).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CreditDate).HasColumnType("datetime");

                entity.Property(e => e.MedicalSystemNo).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.Property(e => e.PhoneClinic).HasMaxLength(20);

                entity.HasOne(d => d.Center)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.CenterId)
                    .HasConstraintName("FK_Doctors_Center");

                entity.HasOne(d => d.DoctorDegreeNavigation)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.DoctorDegreeId)
                    .HasConstraintName("FK_DoctorDegree_Doctors");

                entity.HasOne(d => d.Expertise)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.ExpertiseId)
                    .HasConstraintName("FK_Doctors_Expertise");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_Doctors_Person");

                entity.HasOne(d => d.Scientificlevel)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.ScientificlevelId)
                    .HasConstraintName("FK_Doctors_Scientificlevel");
            });

            modelBuilder.Entity<DoctorsOk>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.Arcertificates1).HasColumnName("arcertificates_1");

                entity.Property(e => e.Arcertificates2).HasColumnName("arcertificates_2");

                entity.Property(e => e.Arcertificates3).HasColumnName("arcertificates_3");

                entity.Property(e => e.Arname).HasColumnName("arname");

                entity.Property(e => e.Arspecialty).HasColumnName("arspecialty");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.Encertificates1).HasColumnName("encertificates_1");

                entity.Property(e => e.Encertificates2).HasColumnName("encertificates_2");

                entity.Property(e => e.Encertificates3).HasColumnName("encertificates_3");

                entity.Property(e => e.Enspecialty).HasColumnName("enspecialty");

                entity.Property(e => e.Phone).HasColumnName("phone");

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.Property(e => e.Visitfee).HasColumnName("visitfee");
            });

            modelBuilder.Entity<DrugForm>(entity =>
            {
                entity.ToTable("DrugForm", "Drug");

                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<DrugInteractions>(entity =>
            {
                entity.ToTable("DrugInteractions", "Drug");

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.HasOne(d => d.FirstGenericDrug)
                    .WithMany(p => p.DrugInteractionsFirstGenericDrug)
                    .HasForeignKey(d => d.FirstGenericDrugId)
                    .HasConstraintName("FK_DrugInteractions_FirstGenericDrug");

                entity.HasOne(d => d.FirstGroup)
                    .WithMany(p => p.DrugInteractionsFirstGroup)
                    .HasForeignKey(d => d.FirstGroupId)
                    .HasConstraintName("FK_DrugInteractions_FirstGroup");

                entity.HasOne(d => d.SecondGenericDrug)
                    .WithMany(p => p.DrugInteractionsSecondGenericDrug)
                    .HasForeignKey(d => d.SecondGenericDrugId)
                    .HasConstraintName("FK_DrugInteractions_SecondGenericDrug");

                entity.HasOne(d => d.SecondGroup)
                    .WithMany(p => p.DrugInteractionsSecondGroup)
                    .HasForeignKey(d => d.SecondGroupId)
                    .HasConstraintName("FK_DrugInteractions_SecondGroup");
            });

            modelBuilder.Entity<DrugStoregCondition>(entity =>
            {
                entity.ToTable("DrugStoregCondition", "Drug");

                entity.HasOne(d => d.Drug)
                    .WithMany(p => p.DrugStoregCondition)
                    .HasForeignKey(d => d.DrugId)
                    .HasConstraintName("FK_DrugStoregCondition_Drugs");

                entity.HasOne(d => d.StoregConditon)
                    .WithMany(p => p.DrugStoregCondition)
                    .HasForeignKey(d => d.StoregConditonId)
                    .HasConstraintName("FK_DrugStoregCondition_StoregCondition");
            });

            modelBuilder.Entity<Drugs>(entity =>
            {
                entity.ToTable("Drugs", "Drug");

                entity.Property(e => e.BrokenConsumptionId).HasComment("بیس این ایدی هم همان یونیت است");

                entity.Property(e => e.ContentsOf).HasMaxLength(150);

                entity.Property(e => e.LocalCode).HasMaxLength(50);

                entity.Property(e => e.Note).HasMaxLength(2500);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.BrokenConsumption)
                    .WithMany(p => p.DrugsBrokenConsumption)
                    .HasForeignKey(d => d.BrokenConsumptionId)
                    .HasConstraintName("FK_Drug_Unit1");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Drugs)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_Drug_Company");

                entity.HasOne(d => d.DrugForm)
                    .WithMany(p => p.Drugs)
                    .HasForeignKey(d => d.DrugFormId)
                    .HasConstraintName("FK_Drugs_DrugForm");

                entity.HasOne(d => d.Generic)
                    .WithMany(p => p.Drugs)
                    .HasForeignKey(d => d.GenericId)
                    .HasConstraintName("FK_Drug_GenericDrug");

                entity.HasOne(d => d.StoregConditon)
                    .WithMany(p => p.Drugs)
                    .HasForeignKey(d => d.StoregConditonId)
                    .HasConstraintName("FK_Drug_StoregConditon");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.DrugsUnit)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("FK_Drug_Unit");
            });

            modelBuilder.Entity<DurationType>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<Education>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.EmploymentStartDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_Employee_Person");
            });

            modelBuilder.Entity<Examplace>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<Expertise>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<FileGroup>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.Title)
                    .HasMaxLength(150)
                    .HasComment("Patient,Person,Drugs...");

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<FileTag>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.TagName).HasMaxLength(100);

                entity.HasOne(d => d.File)
                    .WithMany(p => p.FileTag)
                    .HasForeignKey(d => d.FileId)
                    .HasConstraintName("FK_FileTag_Files");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.FileTag)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK_FileTag_Patient");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FileTag)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_FileTag_Users");
            });

            modelBuilder.Entity<Files>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.FileGroupId).HasColumnName("FileGroupID");

                entity.Property(e => e.FileName).HasMaxLength(150);

                entity.Property(e => e.PrimeryKey).HasMaxLength(150);

                entity.Property(e => e.RefferKey)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.TableName).HasMaxLength(150);

                entity.HasOne(d => d.FileGroup)
                    .WithMany(p => p.Files)
                    .HasForeignKey(d => d.FileGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Files_FileGroup");
            });

            modelBuilder.Entity<Frequency>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<GeneralStatus>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<GenericDrug>(entity =>
            {
                entity.ToTable("GenericDrug", "Drug");

                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(250);

                entity.Property(e => e.TitleLang2).HasMaxLength(250);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GenericDrug)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_Group_GenericDrug");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group", "Drug");

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<GroupAnswer>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasComment("هر آزمایش در نوعی دیگر دسته¬بندی شده و بر اساس دسته بندی، در شرایط خاصی برای دریافت نمونه و جواب¬دهی با آن برخورد می-شود. این آزمایشات در ثبت و روش انجام با هم تفاوت دارند. به همین جهت، در ثبت آزمایش مهم است که نوع آزمایش مشخص شود.");

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<GroupAnswer1>(entity =>
            {
                entity.ToTable("GroupAnswer", "Lab");

                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasComment("هر آزمایش در نوعی دیگر دسته¬بندی شده و بر اساس دسته بندی، در شرایط خاصی برای دریافت نمونه و جواب¬دهی با آن برخورد می-شود. این آزمایشات در ثبت و روش انجام با هم تفاوت دارند. به همین جهت، در ثبت آزمایش مهم است که نوع آزمایش مشخص شود.");

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<GroupReadyAnswer>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.HasOne(d => d.GroupAnswer)
                    .WithMany(p => p.GroupReadyAnswer)
                    .HasForeignKey(d => d.GroupAnswerId)
                    .HasConstraintName("FK_GroupReadyAnswer_GroupAnswer");

                entity.HasOne(d => d.ReadyAnswer)
                    .WithMany(p => p.GroupReadyAnswer)
                    .HasForeignKey(d => d.ReadyAnswerId)
                    .HasConstraintName("FK_GroupReadyAnswer_ReadyAnswer");
            });

            modelBuilder.Entity<GroupReadyAnswer1>(entity =>
            {
                entity.ToTable("GroupReadyAnswer", "Lab");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.HasOne(d => d.GroupAnswer)
                    .WithMany(p => p.GroupReadyAnswer1)
                    .HasForeignKey(d => d.GroupAnswerId)
                    .HasConstraintName("FK_GroupReadyAnswer_GroupAnswer");

                entity.HasOne(d => d.ReadyAnswer)
                    .WithMany(p => p.GroupReadyAnswer1)
                    .HasForeignKey(d => d.ReadyAnswerId)
                    .HasConstraintName("FK_GroupReadyAnswer_ReadyAnswer");
            });

            modelBuilder.Entity<Illness>(entity =>
            {
                entity.Property(e => e.LocalCode).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLange2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(4000);

                entity.Property(e => e.TitleLang2).HasMaxLength(4000);
            });

            modelBuilder.Entity<ImportedDicom>(entity =>
            {
                entity.ToTable("ImportedDicom", "FileM");
            });

            modelBuilder.Entity<Instruction>(entity =>
            {
                entity.ToTable("Instruction", "Lab");

                entity.Property(e => e.FromAge).HasComment("بر حسب روز");

                entity.Property(e => e.SexId).HasComment("نامشخص یعنی هر دو");

                entity.Property(e => e.Title)
                    .HasMaxLength(2500)
                    .HasComment("متن همان دستور العمل است");

                entity.Property(e => e.TitleLang2).HasMaxLength(2500);

                entity.Property(e => e.ToAge).HasComment("بر حسب روز");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.Instruction)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK_Instruction_Services");

                entity.HasOne(d => d.Sex)
                    .WithMany(p => p.Instruction)
                    .HasForeignKey(d => d.SexId)
                    .HasConstraintName("FK_Instruction_Sex");
            });

            modelBuilder.Entity<Interaction>(entity =>
            {
                entity.ToTable("Interaction", "Drug");

                entity.Property(e => e.FirstDrugId).HasComment("احتمالا کاربردی نداشته باشد");

                entity.Property(e => e.Note).HasMaxLength(2500);

                entity.Property(e => e.SecondDrugId).HasComment("احتمالا کاربردی نداشته باشد");

                entity.HasOne(d => d.FirstDrug)
                    .WithMany(p => p.InteractionFirstDrug)
                    .HasForeignKey(d => d.FirstDrugId)
                    .HasConstraintName("FK_Interaction_FirstDrug");

                entity.HasOne(d => d.FirstGenericDrug)
                    .WithMany(p => p.InteractionFirstGenericDrug)
                    .HasForeignKey(d => d.FirstGenericDrugId)
                    .HasConstraintName("FK_Interaction_FirstGenericDrug");

                entity.HasOne(d => d.FirstGroup)
                    .WithMany(p => p.InteractionFirstGroup)
                    .HasForeignKey(d => d.FirstGroupId)
                    .HasConstraintName("FK_Interaction_FirstGroup");

                entity.HasOne(d => d.SecondDrug)
                    .WithMany(p => p.InteractionSecondDrug)
                    .HasForeignKey(d => d.SecondDrugId)
                    .HasConstraintName("FK_Interaction_SecondDrug");

                entity.HasOne(d => d.SecondGenericDrug)
                    .WithMany(p => p.InteractionSecondGenericDrug)
                    .HasForeignKey(d => d.SecondGenericDrugId)
                    .HasConstraintName("FK_Interaction_SecondGenericDrug");

                entity.HasOne(d => d.SecondGroup)
                    .WithMany(p => p.InteractionSecondGroup)
                    .HasForeignKey(d => d.SecondGroupId)
                    .HasConstraintName("FK_Interaction_SecondGroup");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<JobType>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<Kit>(entity =>
            {
                entity.ToTable("Kit", "Lab");

                entity.Property(e => e.Brand).HasMaxLength(50);

                entity.Property(e => e.Company).HasMaxLength(50);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.LotNumber).HasMaxLength(50);

                entity.Property(e => e.SerialNumber).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Kit)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("FK_Kit_Unit");
            });

            modelBuilder.Entity<LabType>(entity =>
            {
                entity.ToTable("LabType", "Lab");

                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasComment("هر آزمایش در نوعی دیگر دسته¬بندی شده و بر اساس دسته بندی، در شرایط خاصی برای دریافت نمونه و جواب¬دهی با آن برخورد می-شود. این آزمایشات در ثبت و روش انجام با هم تفاوت دارند. به همین جهت، در ثبت آزمایش مهم است که نوع آزمایش مشخص شود.");

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<LabelBloodBag>(entity =>
            {
                entity.ToTable("LabelBloodBag", "Lab");

                entity.HasOne(d => d.BloodBankLabelStyle)
                    .WithMany(p => p.LabelBloodBag)
                    .HasForeignKey(d => d.BloodBankLabelStyleId)
                    .HasConstraintName("FK_LabelBloodBag_LabelSampleTestStyle");

                entity.HasOne(d => d.BloodBankLabeling)
                    .WithMany(p => p.LabelBloodBag)
                    .HasForeignKey(d => d.BloodBankLabelingId)
                    .HasConstraintName("FK_LabelBloodBag_BloodBankLabeling");
            });

            modelBuilder.Entity<LabelSampleTest>(entity =>
            {
                entity.ToTable("LabelSampleTest", "Lab");

                entity.HasOne(d => d.LabelSampleTestStyle)
                    .WithMany(p => p.LabelSampleTest)
                    .HasForeignKey(d => d.LabelSampleTestStyleId)
                    .HasConstraintName("FK_LabelSampleTest_LabelSampleTestStyle");

                entity.HasOne(d => d.TestLabeling)
                    .WithMany(p => p.LabelSampleTest)
                    .HasForeignKey(d => d.TestLabelingId)
                    .HasConstraintName("FK_LabelSampleTest_TestLabling");
            });

            modelBuilder.Entity<LabelSampleTestStyle>(entity =>
            {
                entity.ToTable("LabelSampleTestStyle", "Lab");

                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.Property(e => e.Browser).HasMaxLength(50);

                entity.Property(e => e.ExpireDate).HasColumnType("datetime");

                entity.Property(e => e.Ip).HasMaxLength(50);

                entity.Property(e => e.Language).HasMaxLength(10);

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MaritalStatus>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.Property(e => e.IconName).HasMaxLength(100);

                entity.Property(e => e.Link).HasMaxLength(100);

                entity.Property(e => e.MenuName).HasMaxLength(50);

                entity.Property(e => e.MenuType).HasMaxLength(20);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.Menu)
                    .HasForeignKey(d => d.PermissionId)
                    .HasConstraintName("FK_Menu_Permissions");
            });

            modelBuilder.Entity<MethodType>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<MobileActivation>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ExpireDate).HasColumnType("datetime");

                entity.Property(e => e.Mobile).HasMaxLength(20);

                entity.Property(e => e.VerifyCode).HasMaxLength(15);
            });

            modelBuilder.Entity<Nationality>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<OwnershipType>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<PackagingType>(entity =>
            {
                entity.ToTable("PackagingType", "Drug");

                entity.Property(e => e.Title).HasMaxLength(250);

                entity.HasOne(d => d.DrugForm)
                    .WithMany(p => p.PackagingType)
                    .HasForeignKey(d => d.DrugFormId)
                    .HasConstraintName("FK_PackagingType_DrugForm");
            });

            modelBuilder.Entity<PanelType>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<Passport>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.FlightDate).HasColumnType("datetime");

                entity.Property(e => e.FlightNumber).HasMaxLength(50);

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Passport)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_Passport_Passport");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.FileNo).HasMaxLength(20);

                entity.Property(e => e.Hisno).HasColumnName("HISNo");

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.HasOne(d => d.BloodGroup)
                    .WithMany(p => p.Patient)
                    .HasForeignKey(d => d.BloodGroupId)
                    .HasConstraintName("FK_Paitient_BloodGroup");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Patient)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_Paitient_Person");
            });

            modelBuilder.Entity<PatientExtraInfo>(entity =>
            {
                entity.Property(e => e.ChronicIllnessId).HasComment("معرف");

                entity.Property(e => e.Height).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.IssueDate).HasColumnType("date");

                entity.Property(e => e.JobTypeId).HasComment("محل کار");

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.PassportIssueDate).HasColumnType("datetime");

                entity.Property(e => e.PassportNumber).HasMaxLength(50);

                entity.Property(e => e.RhId).HasComment("فاکتوری در خون");

                entity.Property(e => e.Weight).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.Word).HasMaxLength(250);

                entity.Property(e => e.WorkAddress).HasMaxLength(250);

                entity.Property(e => e.WorkPhone).HasMaxLength(12);

                entity.HasOne(d => d.Allergy)
                    .WithMany(p => p.PatientExtraInfo)
                    .HasForeignKey(d => d.AllergyId)
                    .HasConstraintName("FK_PatientExtraInfo_Allergy");

                entity.HasOne(d => d.ChronicIllness)
                    .WithMany(p => p.PatientExtraInfo)
                    .HasForeignKey(d => d.ChronicIllnessId)
                    .HasConstraintName("FK_PaitientExtraInfo_ChronicIIlness");

                entity.HasOne(d => d.Education)
                    .WithMany(p => p.PatientExtraInfo)
                    .HasForeignKey(d => d.EducationId)
                    .HasConstraintName("FK_PaitientExtraInfo_Education");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.PatientExtraInfo)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_PaitientExtraInfo_Job");

                entity.HasOne(d => d.JobType)
                    .WithMany(p => p.PatientExtraInfo)
                    .HasForeignKey(d => d.JobTypeId)
                    .HasConstraintName("FK_PatientExtraInfo_JobType");

                entity.HasOne(d => d.Nationality)
                    .WithMany(p => p.PatientExtraInfo)
                    .HasForeignKey(d => d.NationalityId)
                    .HasConstraintName("FK_PatientExtraInfo_Nationality");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.PatientExtraInfo)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK_PaitientExtraInfo_Patient");

                entity.HasOne(d => d.PregnancySeason)
                    .WithMany(p => p.PatientExtraInfo)
                    .HasForeignKey(d => d.PregnancySeasonId)
                    .HasConstraintName("FK_PatientExtraInfo_PregnancySeason");

                entity.HasOne(d => d.Religion)
                    .WithMany(p => p.PatientExtraInfo)
                    .HasForeignKey(d => d.ReligionId)
                    .HasConstraintName("FK_PatientExtraInfo_Religion");

                entity.HasOne(d => d.Rh)
                    .WithMany(p => p.PatientExtraInfo)
                    .HasForeignKey(d => d.RhId)
                    .HasConstraintName("FK_PatientExtraInfo_Rh");
            });

            modelBuilder.Entity<PatientHistoryBloodTransfusion>(entity =>
            {
                entity.ToTable("PatientHistoryBloodTransfusion", "Lab");

                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentNo)
                    .HasMaxLength(10)
                    .HasComment("شناسه پرداخت تولید خودکار");

                entity.HasOne(d => d.PaymentType)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.PaymentTypeId)
                    .HasConstraintName("FK_PrePayment_PaymentType");

                entity.HasOne(d => d.Reception)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.ReceptionId)
                    .HasConstraintName("FK_PrePayment_Receptions");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Role_Payment");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_PrePayment_Users");
            });

            modelBuilder.Entity<PaymentType>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<Pcr>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Pcr");

                entity.Property(e => e.AnswerDate).HasColumnType("datetime");

                entity.Property(e => e.Result).HasMaxLength(4000);
            });

            modelBuilder.Entity<PermissionSectionField>(entity =>
            {
                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.PermissionSectionField)
                    .HasForeignKey(d => d.PermissionId)
                    .HasConstraintName("FK_PermissionSectionField_Permissions");

                entity.HasOne(d => d.SectionField)
                    .WithMany(p => p.PermissionSectionField)
                    .HasForeignKey(d => d.SectionFieldId)
                    .HasConstraintName("FK_PermissionSectionField_SectionField");
            });

            modelBuilder.Entity<Permissions>(entity =>
            {
                entity.Property(e => e.ModuleName).HasMaxLength(50);

                entity.Property(e => e.Note).HasMaxLength(100);

                entity.Property(e => e.NoteLang2).HasMaxLength(100);

                entity.Property(e => e.PageAddress).HasMaxLength(100);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FatherName).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.GrandFatherName).HasMaxLength(100);

                entity.Property(e => e.GreatGrandFatherName)
                    .HasMaxLength(100)
                    .HasComment("پدر پدر بزرگ");

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.LatinName)
                    .HasMaxLength(100)
                    .HasComment("نام  انگلیسی");

                entity.Property(e => e.Mobile).HasMaxLength(20);

                entity.Property(e => e.MotherName).HasMaxLength(100);

                entity.Property(e => e.NationalCode).HasMaxLength(50);

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.ShNo).HasMaxLength(50);

                entity.HasOne(d => d.BirthPlace)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.BirthPlaceId)
                    .HasConstraintName("FK_birthplace_person");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Person_City");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Person_Country");

                entity.HasOne(d => d.MaritalStatus)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.MaritalStatusId)
                    .HasConstraintName("FK_Person_MaritalStatus");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK_Person_Province");

                entity.HasOne(d => d.Sex)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.SexId)
                    .HasConstraintName("FK_Person_Sex");

                entity.HasOne(d => d.Zone)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.ZoneId)
                    .HasConstraintName("FK_Person_Zone");
            });

            modelBuilder.Entity<Pharmacies>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<Place>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<PregnancySeason>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<PrescribtionType>(entity =>
            {
                entity.ToTable("PrescribtionType", "Drug");

                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.Property(e => e.Biography).HasMaxLength(4000);

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .HasComment("کد تجویز ");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasComment("تاریخ ثبت سیستمی");

                entity.Property(e => e.Diet).HasMaxLength(4000);

                entity.Property(e => e.InteractionsNote).HasMaxLength(4000);

                entity.Property(e => e.MainDoctorNote).HasMaxLength(4000);

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.Property(e => e.PharmacistNoteForNursing).HasMaxLength(4000);

                entity.Property(e => e.PostMedicalHistory).HasMaxLength(4000);

                entity.Property(e => e.PrescriptionDate)
                    .HasColumnType("datetime")
                    .HasComment("از کابر گرفته میشه");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Prescription)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_Prescription_Doctors");

                entity.HasOne(d => d.Reception)
                    .WithMany(p => p.Prescription)
                    .HasForeignKey(d => d.ReceptionId)
                    .HasConstraintName("FK_Prescription_Receptions");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Prescription)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Prescription_Role");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Prescription)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("FK_Prescription_Section");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Prescription)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Prescription_Users");

                entity.HasOne(d => d.VisitType)
                    .WithMany(p => p.Prescription)
                    .HasForeignKey(d => d.VisitTypeId)
                    .HasConstraintName("FK_Prescription_VisitType");
            });

            modelBuilder.Entity<PrescriptionAllergy>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Allergy)
                    .WithMany(p => p.PrescriptionAllergy)
                    .HasForeignKey(d => d.AllergyId)
                    .HasConstraintName("FK_PrescriptionAllergy_Allergy");

                entity.HasOne(d => d.Prescription)
                    .WithMany(p => p.PrescriptionAllergy)
                    .HasForeignKey(d => d.PrescriptionId)
                    .HasConstraintName("FK_PrescriptionAllergy_Prescription");
            });

            modelBuilder.Entity<PrescriptionChartActionType>(entity =>
            {
                entity.Property(e => e.ActionTitle).HasMaxLength(100);
            });

            modelBuilder.Entity<PrescriptionChartCancelationReason>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<PrescriptionDetailDrug>(entity =>
            {
                entity.Property(e => e.ActionDate)
                    .HasColumnType("datetime")
                    .HasComment("تاریخ شروع اقدام همچنین تاریخی که بخش مقصد این درخواست را میبیند در تناوب ها هم کاربرد دارد");

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.GenericDrugId).HasComment("به جنریک دراگ میخورد نه دراگ در زمان تجویز جنریک دراگ مهم است نه دراگ");

                entity.Property(e => e.Note).HasMaxLength(2500);

                entity.Property(e => e.PrescriptionInstructionId).HasComment("دستور العمل انجام مثلا هر هشت ساعت یه بار");

                entity.Property(e => e.PrescriptionTypeId).HasComment("تشخیصی درمانی مراقبتی");

                entity.Property(e => e.RequestFromSectionId).HasComment("از چه بخشی درخواست شده اگر در خود بخش انجام شود ایدی بخش جاری می خورد");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.WayOfPrescriptionId).HasComment("خوراکی تزریق مالیدنی");

                entity.HasOne(d => d.DrugForm)
                    .WithMany(p => p.PrescriptionDetailDrug)
                    .HasForeignKey(d => d.DrugFormId)
                    .HasConstraintName("FK_PrescriptionDetailDrug_DrugForm");

                entity.HasOne(d => d.Drug)
                    .WithMany(p => p.PrescriptionDetailDrug)
                    .HasForeignKey(d => d.DrugId)
                    .HasConstraintName("FK_PrescriptionDetailDrug_Drugs");

                entity.HasOne(d => d.DurationType)
                    .WithMany(p => p.PrescriptionDetailDrug)
                    .HasForeignKey(d => d.DurationTypeId)
                    .HasConstraintName("FK_PrescriptionDetailDrug_DurationType");

                entity.HasOne(d => d.Frequency)
                    .WithMany(p => p.PrescriptionDetailDrug)
                    .HasForeignKey(d => d.FrequencyId)
                    .HasConstraintName("FK_PrescriptionDetailDrug_Frequency");

                entity.HasOne(d => d.GenericDrug)
                    .WithMany(p => p.PrescriptionDetailDrug)
                    .HasForeignKey(d => d.GenericDrugId)
                    .HasConstraintName("FK_PrescriptionDetailDrug_GenericDrug");

                entity.HasOne(d => d.Panel)
                    .WithMany(p => p.PrescriptionDetailDrug)
                    .HasForeignKey(d => d.PanelId)
                    .HasConstraintName("FK_PrescriptionDetailDrug_PrescriptionPanel");

                entity.HasOne(d => d.PrescriberNurse)
                    .WithMany(p => p.PrescriptionDetailDrug)
                    .HasForeignKey(d => d.PrescriberNurseId)
                    .HasConstraintName("FK_PrescriptionDetailDrug_Nurse");

                entity.HasOne(d => d.Prescription)
                    .WithMany(p => p.PrescriptionDetailDrug)
                    .HasForeignKey(d => d.PrescriptionId)
                    .HasConstraintName("FK_PrescriptionDetailDrug_Prescription");

                entity.HasOne(d => d.PrescriptionInstruction)
                    .WithMany(p => p.PrescriptionDetailDrug)
                    .HasForeignKey(d => d.PrescriptionInstructionId)
                    .HasConstraintName("FK_PrescriptionDetailDrug_PrescriptionInstruction");

                entity.HasOne(d => d.PrescriptionType)
                    .WithMany(p => p.PrescriptionDetailDrug)
                    .HasForeignKey(d => d.PrescriptionTypeId)
                    .HasConstraintName("FK_PrescriptionDetailDrug_PrescriptionTypeId");

                entity.HasOne(d => d.Priority)
                    .WithMany(p => p.PrescriptionDetailDrug)
                    .HasForeignKey(d => d.PriorityId)
                    .HasConstraintName("FK_PrescriptionDetailDrug_Priority");

                entity.HasOne(d => d.RequestFromSection)
                    .WithMany(p => p.PrescriptionDetailDrug)
                    .HasForeignKey(d => d.RequestFromSectionId)
                    .HasConstraintName("FK_PrescriptionDetailDrug_Section");

                entity.HasOne(d => d.WayOfPrescription)
                    .WithMany(p => p.PrescriptionDetailDrug)
                    .HasForeignKey(d => d.WayOfPrescriptionId)
                    .HasConstraintName("FK_PrescriptionDetailDrug_WayOfPrescription");
            });

            modelBuilder.Entity<PrescriptionDetailDrugHistory>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.Property(e => e.StatusDate).HasColumnType("datetime");

                entity.Property(e => e.StopDate).HasColumnType("datetime");

                entity.HasOne(d => d.PrescriptionDetailDrug)
                    .WithMany(p => p.PrescriptionDetailDrugHistory)
                    .HasForeignKey(d => d.PrescriptionDetailDrugId)
                    .HasConstraintName("FK_PrescriptionDetailDrugHistory_PrescriptionDetailDrug");

                entity.HasOne(d => d.PrescriptionHistoryStatus)
                    .WithMany(p => p.PrescriptionDetailDrugHistory)
                    .HasForeignKey(d => d.PrescriptionHistoryStatusId)
                    .HasConstraintName("FK_PrescriptionDetailDrugHistory_PrescriptionHistoryStatus");

                entity.HasOne(d => d.Prescription)
                    .WithMany(p => p.PrescriptionDetailDrugHistory)
                    .HasForeignKey(d => d.PrescriptionId)
                    .HasConstraintName("FK_PrescriptionDetailDrugHistory_Prescription");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.PrescriptionDetailDrugHistory)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_PrescriptionDetailDrugHistory_Role");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.PrescriptionDetailDrugHistory)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("FK_PrescriptionDetailDrugHistory_Section");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PrescriptionDetailDrugHistory)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_PrescriptionDetailDrugHistory_Users");
            });

            modelBuilder.Entity<PrescriptionDetailPharmacistNote>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.Property(e => e.NoteForNurse).HasMaxLength(4000);

                entity.Property(e => e.NoteForTrainingPatient).HasMaxLength(4000);

                entity.Property(e => e.PrescriptionDetailDrugId).HasComment("مشخص می‌کند نکات دکتر داروساز مربوط به کدام قلم تجویز دارویی بیمار است");

                entity.HasOne(d => d.PrescriptionDetailDrug)
                    .WithMany(p => p.PrescriptionDetailPharmacistNote)
                    .HasForeignKey(d => d.PrescriptionDetailDrugId)
                    .HasConstraintName("FK_PrescriptionDetailPharmacistNote_PrescriptionDetailDrug");

                entity.HasOne(d => d.Prescription)
                    .WithMany(p => p.PrescriptionDetailPharmacistNote)
                    .HasForeignKey(d => d.PrescriptionId)
                    .HasConstraintName("FK_PrescriptionDetailPharmacistNote_Prescription");
            });

            modelBuilder.Entity<PrescriptionDetailService>(entity =>
            {
                entity.Property(e => e.ActionDate)
                    .HasColumnType("datetime")
                    .HasComment("تاریخ شروع اقدام همچنین تاریخی که بخش مقصد این درخواست را میبیند در تناوب ها هم کاربرد دارد");

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(2500);

                entity.Property(e => e.PanelId).HasComment("در صورتی که سرویس ایدی خالی باشد");

                entity.Property(e => e.PrescriptionInstructionId).HasComment("دستور العمل انجام مثلا هر هشت ساعت یه بار");

                entity.Property(e => e.PrescriptionTypeId).HasComment("تشخیصی درمانی مراقبتی");

                entity.Property(e => e.RequestFromSectionId).HasComment("از چه بخشی درخواست شده اگر در خود بخش انجام شود ایدی بخش جاری می خورد");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Frequency)
                    .WithMany(p => p.PrescriptionDetailService)
                    .HasForeignKey(d => d.FrequencyId)
                    .HasConstraintName("FK_PrescriptionDetailService_Frequency");

                entity.HasOne(d => d.Panel)
                    .WithMany(p => p.PrescriptionDetailService)
                    .HasForeignKey(d => d.PanelId)
                    .HasConstraintName("FK_PrescriptionDetailService_PrescriptionPanel");

                entity.HasOne(d => d.PrescriberNurse)
                    .WithMany(p => p.PrescriptionDetailService)
                    .HasForeignKey(d => d.PrescriberNurseId)
                    .HasConstraintName("FK_PrescriptionDetailService_Nurse");

                entity.HasOne(d => d.Prescription)
                    .WithMany(p => p.PrescriptionDetailService)
                    .HasForeignKey(d => d.PrescriptionId)
                    .HasConstraintName("FK_PrescriptionDetailService_Prescription");

                entity.HasOne(d => d.PrescriptionType)
                    .WithMany(p => p.PrescriptionDetailService)
                    .HasForeignKey(d => d.PrescriptionTypeId)
                    .HasConstraintName("FK_PrescriptionDetailService_PrescriptionTypeId");

                entity.HasOne(d => d.Priority)
                    .WithMany(p => p.PrescriptionDetailService)
                    .HasForeignKey(d => d.PriorityId)
                    .HasConstraintName("FK_PrescriptionDetailService_Priority");

                entity.HasOne(d => d.RequestFromSection)
                    .WithMany(p => p.PrescriptionDetailService)
                    .HasForeignKey(d => d.RequestFromSectionId)
                    .HasConstraintName("FK_PrescriptionDetailService_Section");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.PrescriptionDetailService)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK_PrescriptionDetailService_Services");
            });

            modelBuilder.Entity<PrescriptionDetailServiceHistory>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.Property(e => e.StatusDate).HasColumnType("datetime");

                entity.Property(e => e.StopDate).HasColumnType("datetime");

                entity.HasOne(d => d.PrescriptionDetailService)
                    .WithMany(p => p.PrescriptionDetailServiceHistory)
                    .HasForeignKey(d => d.PrescriptionDetailServiceId)
                    .HasConstraintName("FK_PrescriptionDetailServiceHistory_PrescriptionDetailService");

                entity.HasOne(d => d.PrescriptionHistoryStatus)
                    .WithMany(p => p.PrescriptionDetailServiceHistory)
                    .HasForeignKey(d => d.PrescriptionHistoryStatusId)
                    .HasConstraintName("FK_PrescriptionDetailServiceHistory_PrescriptionHistoryStatus");

                entity.HasOne(d => d.Prescription)
                    .WithMany(p => p.PrescriptionDetailServiceHistory)
                    .HasForeignKey(d => d.PrescriptionId)
                    .HasConstraintName("FK_PrescriptionDetailServiceHistory_Prescription");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.PrescriptionDetailServiceHistory)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_PrescriptionDetailServiceHistory_Role");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.PrescriptionDetailServiceHistory)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("FK_PrescriptionDetailServiceHistory_Section");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PrescriptionDetailServiceHistory)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_PrescriptionDetailServiceHistory_Users");
            });

            modelBuilder.Entity<PrescriptionDetails>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("PrescriptionDetails");
            });

            modelBuilder.Entity<PrescriptionDiet>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.HasOne(d => d.Diet)
                    .WithMany(p => p.PrescriptionDiet)
                    .HasForeignKey(d => d.DietId)
                    .HasConstraintName("FK_PrescriptionDiet_Diet");

                entity.HasOne(d => d.Prescription)
                    .WithMany(p => p.PrescriptionDiet)
                    .HasForeignKey(d => d.PrescriptionId)
                    .HasConstraintName("FK_PrescriptionDiet_Prescription");
            });

            modelBuilder.Entity<PrescriptionDrugChart>(entity =>
            {
                entity.Property(e => e.DoneDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.Property(e => e.PrescriptionDoDate).HasColumnType("datetime");

                entity.HasOne(d => d.Drug)
                    .WithMany(p => p.PrescriptionDrugChart)
                    .HasForeignKey(d => d.DrugId)
                    .HasConstraintName("FK_PrescriptionDrugChart_Drugs");

                entity.HasOne(d => d.Functor)
                    .WithMany(p => p.PrescriptionDrugChartFunctor)
                    .HasForeignKey(d => d.FunctorId)
                    .HasConstraintName("FK_PrescriptionDrugChart_Users_Functor");

                entity.HasOne(d => d.PrescriptionChartActionType)
                    .WithMany(p => p.PrescriptionDrugChart)
                    .HasForeignKey(d => d.PrescriptionChartActionTypeId)
                    .HasConstraintName("FK_PrescriptionDrugChart_PrescriptionChartActionType");

                entity.HasOne(d => d.PrescriptionChartCancelationReason)
                    .WithMany(p => p.PrescriptionDrugChart)
                    .HasForeignKey(d => d.PrescriptionChartCancelationReasonId)
                    .HasConstraintName("FK_PrescriptionDrugChart_PrescriptionChartCancelationReason");

                entity.HasOne(d => d.PrescriptionDetailDrug)
                    .WithMany(p => p.PrescriptionDrugChart)
                    .HasForeignKey(d => d.PrescriptionDetailDrugId)
                    .HasConstraintName("FK_PrescriptionDrugChart_PrescriptionDetailDrug");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.PrescriptionDrugChart)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Role_PrescriptionDrugChart");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PrescriptionDrugChartUser)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_PrescriptionDrugChart_Users_User");

                entity.HasOne(d => d.Witness)
                    .WithMany(p => p.PrescriptionDrugChartWitness)
                    .HasForeignKey(d => d.WitnessId)
                    .HasConstraintName("FK_PrescriptionDrugChart_Users_Witness");
            });

            modelBuilder.Entity<PrescriptionDrugRoutine>(entity =>
            {
                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.Property(e => e.Period).HasComment("بر حسب دقیقه");

                entity.Property(e => e.WayOfPrescriptionId).HasComment("نحوه مصرف");

                entity.HasOne(d => d.GenericDrug)
                    .WithMany(p => p.PrescriptionDrugRoutine)
                    .HasForeignKey(d => d.GenericDrugId)
                    .HasConstraintName("FK_PrescriptionDrugRoutine_GenericDrug");

                entity.HasOne(d => d.PrescriptionRoutine)
                    .WithMany(p => p.PrescriptionDrugRoutine)
                    .HasForeignKey(d => d.PrescriptionRoutineId)
                    .HasConstraintName("FK_PrescriptionDrugRoutine_PrescriptionRoutine");

                entity.HasOne(d => d.WayOfPrescription)
                    .WithMany(p => p.PrescriptionDrugRoutine)
                    .HasForeignKey(d => d.WayOfPrescriptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PrescriptionDrugRoutine_PrescriptionDrugRoutine");
            });

            modelBuilder.Entity<PrescriptionGroupItem>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<PrescriptionGroupSetting>(entity =>
            {
                entity.HasOne(d => d.GroupService)
                    .WithMany(p => p.PrescriptionGroupSetting)
                    .HasForeignKey(d => d.GroupServiceId)
                    .HasConstraintName("FK_PrescriptionGroupSetting_Services");

                entity.HasOne(d => d.PrescriptionGroupItem)
                    .WithMany(p => p.PrescriptionGroupSetting)
                    .HasForeignKey(d => d.PrescriptionGroupItemId)
                    .HasConstraintName("FK_PrescriptionGroupSetting_PrescriptionGroupItem");

                entity.HasOne(d => d.VisitType)
                    .WithMany(p => p.PrescriptionGroupSetting)
                    .HasForeignKey(d => d.VisitTypeId)
                    .HasConstraintName("FK_PrescriptionGroupSetting_VisitType");
            });

            modelBuilder.Entity<PrescriptionHistoryStatus>(entity =>
            {
                entity.Property(e => e.StatusTitle).HasMaxLength(100);
            });

            modelBuilder.Entity<PrescriptionInstruction>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Duration).HasComment("بر اساس دقیقه");

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<PrescriptionPanel>(entity =>
            {
                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.HasOne(d => d.Prescription)
                    .WithMany(p => p.PrescriptionPanel)
                    .HasForeignKey(d => d.PrescriptionId)
                    .HasConstraintName("FK_PrescriptionPanel_Prescription");
            });

            modelBuilder.Entity<PrescriptionRoutine>(entity =>
            {
                entity.Property(e => e.IsDrugType).HasDefaultValueSql("((0))");

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.HasOne(d => d.PanelType)
                    .WithMany(p => p.PrescriptionRoutine)
                    .HasForeignKey(d => d.PanelTypeId)
                    .HasConstraintName("FK_PrescriptionRoutine_PanelType");
            });

            modelBuilder.Entity<PrescriptionServiceChart>(entity =>
            {
                entity.Property(e => e.DoneDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.Property(e => e.PrescriptionChartDate).HasColumnType("datetime");

                entity.Property(e => e.Result).HasMaxLength(4000);

                entity.Property(e => e.ResultDate).HasColumnType("datetime");

                entity.HasOne(d => d.Functor)
                    .WithMany(p => p.PrescriptionServiceChartFunctor)
                    .HasForeignKey(d => d.FunctorId)
                    .HasConstraintName("FK_PrescriptionServiceChart_Users_Functor");

                entity.HasOne(d => d.PrescriptionChartActionType)
                    .WithMany(p => p.PrescriptionServiceChart)
                    .HasForeignKey(d => d.PrescriptionChartActionTypeId)
                    .HasConstraintName("FK_PrescriptionServiceChart_PrescriptionChartActionType");

                entity.HasOne(d => d.PrescriptionChartCancelationReason)
                    .WithMany(p => p.PrescriptionServiceChart)
                    .HasForeignKey(d => d.PrescriptionChartCancelationReasonId)
                    .HasConstraintName("FK_PrescriptionServiceChart_PrescriptionChartCancelationReason");

                entity.HasOne(d => d.PrescriptionDetailService)
                    .WithMany(p => p.PrescriptionServiceChart)
                    .HasForeignKey(d => d.PrescriptionDetailServiceId)
                    .HasConstraintName("FK_PrescriptionServiceChart_PrescriptionDetailService");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.PrescriptionServiceChart)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Role_PrescriptionServiceChart");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.PrescriptionServiceChart)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("FK_PrescriptionServiceChart_Unit");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PrescriptionServiceChartUser)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_PrescriptionServiceChart_Users");

                entity.HasOne(d => d.Witness)
                    .WithMany(p => p.PrescriptionServiceChartWitness)
                    .HasForeignKey(d => d.WitnessId)
                    .HasConstraintName("FK_PrescriptionServiceChart_Users_Witness");
            });

            modelBuilder.Entity<PrescriptionServiceResult>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Result).HasMaxLength(4000);

                entity.Property(e => e.Result2).HasMaxLength(4000);

                entity.HasOne(d => d.Prescription)
                    .WithMany(p => p.PrescriptionServiceResult)
                    .HasForeignKey(d => d.PrescriptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PrescriptionServiceResult_Prescription");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.PrescriptionServiceResult)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK_PrescriptionServiceResult_Services");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.PrescriptionServiceResult)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("FK_PrescriptionServiceResult_Unit");
            });

            modelBuilder.Entity<PrescriptionServiceRoutine>(entity =>
            {
                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.HasOne(d => d.PrescriptionRoutine)
                    .WithMany(p => p.PrescriptionServiceRoutine)
                    .HasForeignKey(d => d.PrescriptionRoutineId)
                    .HasConstraintName("FK_PrescriptionServiceRoutine_PrescriptionRoutine");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.PrescriptionServiceRoutine)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK_PrescriptionServiceRoutine_Services");
            });

            modelBuilder.Entity<PrescriptionSetting>(entity =>
            {
                entity.Property(e => e.ServiceGroupId).HasComment("ای دی سطرهای پدر سطح اول بهش وصل میشه");

                entity.Property(e => e.ServiceId).HasComment("خدمات سطح اول جدول");

                entity.Property(e => e.VisitTypeId).HasComment("وزیت اولیه و عادی");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.PrescriptionSetting)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("FK_PrescriptionSetting_Section");

                entity.HasOne(d => d.ServiceGroup)
                    .WithMany(p => p.PrescriptionSettingServiceGroup)
                    .HasForeignKey(d => d.ServiceGroupId)
                    .HasConstraintName("FK_PrescriptionSetting_Services1");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.PrescriptionSettingService)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK_PrescriptionSetting_Services");

                entity.HasOne(d => d.VisitType)
                    .WithMany(p => p.PrescriptionSetting)
                    .HasForeignKey(d => d.VisitTypeId)
                    .HasConstraintName("FK_PrescriptionSetting_VisitType");
            });

            modelBuilder.Entity<PrescriptionShare>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(250);

                entity.HasOne(d => d.Prescriptoin)
                    .WithMany(p => p.PrescriptionShare)
                    .HasForeignKey(d => d.PrescriptoinId)
                    .HasConstraintName("FK_PrescriptoinShare_Prescription");
            });

            modelBuilder.Entity<PrescriptionSign>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Prescription)
                    .WithMany(p => p.PrescriptionSign)
                    .HasForeignKey(d => d.PrescriptionId)
                    .HasConstraintName("FK_Table_1_Prescription");

                entity.HasOne(d => d.Sign)
                    .WithMany(p => p.PrescriptionSign)
                    .HasForeignKey(d => d.SignId)
                    .HasConstraintName("FK_Table_1_Sign");
            });

            modelBuilder.Entity<PrescriptionType>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<Presenter>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<PriceTypeMethod>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.Property(e => e.TitleLang2).HasMaxLength(100);
            });

            modelBuilder.Entity<Priority>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<ProceedFromBagNumber>(entity =>
            {
                entity.ToTable("ProceedFromBagNumber", "Lab");

                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasComment("هر آزمایش در نوعی دیگر دسته¬بندی شده و بر اساس دسته بندی، در شرایط خاصی برای دریافت نمونه و جواب¬دهی با آن برخورد می-شود. این آزمایشات در ثبت و روش انجام با هم تفاوت دارند. به همین جهت، در ثبت آزمایش مهم است که نوع آزمایش مشخص شود.");

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Enname)
                    .HasColumnName("enname")
                    .HasMaxLength(256);

                entity.Property(e => e.Fullname)
                    .HasColumnName("fullname")
                    .HasMaxLength(256);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.OnlineId).HasColumnName("online_id");
            });

            modelBuilder.Entity<ProhibitedUsage>(entity =>
            {
                entity.ToTable("ProhibitedUsage", "Drug");

                entity.Property(e => e.Note).HasMaxLength(2500);

                entity.HasOne(d => d.Drug)
                    .WithMany(p => p.ProhibitedUsage)
                    .HasForeignKey(d => d.DrugId)
                    .HasConstraintName("FK_ProhibitedUsage_Drugs");

                entity.HasOne(d => d.GenericDrug)
                    .WithMany(p => p.ProhibitedUsage)
                    .HasForeignKey(d => d.GenericDrugId)
                    .HasConstraintName("FK_ProhibitedUsage_GenericDrug");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.ProhibitedUsage)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_ProhibitedUsage_Group");
            });

            modelBuilder.Entity<Property>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Province)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Province_Country");
            });

            modelBuilder.Entity<PurchaseInvoice>(entity =>
            {
                entity.ToTable("PurchaseInvoice", "Drug");

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .HasComment(@"Purchase Invoice Number
کدسیستمی داخلی");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasComment("تاریخ ثبت فاکتور");

                entity.Property(e => e.Delivery)
                    .HasMaxLength(50)
                    .HasComment("فرد تحویل دهنده");

                entity.Property(e => e.Note)
                    .HasMaxLength(4000)
                    .HasComment("سایر توضیحات");

                entity.Property(e => e.OtherDiscount)
                    .HasColumnType("decimal(18, 0)")
                    .HasComment("تخفیفی که به صورت دستی می‌تواند فرد وارد کند");

                entity.Property(e => e.OutInvoiceCode)
                    .HasMaxLength(20)
                    .HasComment("شناسه فاکتور بیرونی مربوط به شرکت ارائه دهنده کالا و فاکتور");

                entity.Property(e => e.PaymentablePrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasComment("مبلغ قابل پرداخت و پرداخت شده");

                entity.Property(e => e.PurchaseDate)
                    .HasColumnType("datetime")
                    .HasComment("تاریخ خرید");

                entity.Property(e => e.PurchaserId).HasComment("کد پرسنلی خریدار یا کارپرداز مربوطه متصل به جدول کارمندان");

                entity.Property(e => e.StoreId).HasComment("شناسه انبار متصل به جدول بخش ها");

                entity.Property(e => e.TotalDiscount)
                    .HasColumnType("decimal(18, 0)")
                    .HasComment("فیلد محاسباتی تخفیف کل");

                entity.Property(e => e.TotalPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasComment("فلید محاسبانی قیمت کل");

                entity.Property(e => e.TotalTax)
                    .HasColumnType("decimal(18, 0)")
                    .HasComment("فیلد محاسباتی مالیات کل");

                entity.Property(e => e.TransfereeId).HasComment("شناسه پرسنلی فرد تحویل گیرنده دارو متصل به جدول کارمندان");

                entity.Property(e => e.UserId).HasComment("شناسه فرد ثبت‌کننده فاکتور متصل به جدول کاربران");

                entity.HasOne(d => d.DistributionCompany)
                    .WithMany(p => p.PurchaseInvoice)
                    .HasForeignKey(d => d.DistributionCompanyId)
                    .HasConstraintName("FK_PurchaseInvoice_Company");

                entity.HasOne(d => d.Purchaser)
                    .WithMany(p => p.PurchaseInvoicePurchaser)
                    .HasForeignKey(d => d.PurchaserId)
                    .HasConstraintName("FK_Employee_PurchaseInvoicePurchaser");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.PurchaseInvoice)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Role_PurchaseInvoice");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.PurchaseInvoice)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_PurchaseInvoice_Section");

                entity.HasOne(d => d.Transferee)
                    .WithMany(p => p.PurchaseInvoiceTransferee)
                    .HasForeignKey(d => d.TransfereeId)
                    .HasConstraintName("FK_Employee_PurchaseInvoiceTransferee");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PurchaseInvoice)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Users_PurchaseInvoice");
            });

            modelBuilder.Entity<PurchaseInvoiceDetails>(entity =>
            {
                entity.ToTable("PurchaseInvoiceDetails", "Drug");

                entity.Property(e => e.Award).HasComment("تعداد داروهایی که به عنوان اشانتیون دریافت شده");

                entity.Property(e => e.AwardProfit)
                    .HasColumnType("decimal(18, 0)")
                    .HasComment("مقدار سود براساس تعداد داروهای دریافتی اشانتیون");

                entity.Property(e => e.Code)
                    .HasMaxLength(20)
                    .HasComment("کد سیستمی داخلی");

                entity.Property(e => e.Discount)
                    .HasColumnType("decimal(18, 0)")
                    .HasComment("تخفیف");

                entity.Property(e => e.DrugId).HasComment("شناسه دارو متصل  جدول داروها");

                entity.Property(e => e.ExpirationDate)
                    .HasColumnType("datetime")
                    .HasComment("تاریخ انقضا دارو");

                entity.Property(e => e.PackagingQuantity).HasComment("تعداد بسته‌های انتخابی دارو");

                entity.Property(e => e.PackagingTypeId).HasComment("شناسه بسته بندی دارو متصل به جدول بسته‌بندی");

                entity.Property(e => e.PurchaseInvoiceId).HasComment("شناسه اتصال به جدول پدر (هدر) بنام پورچیس اینویس");

                entity.Property(e => e.PurchasePrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasComment("قیمت خرید");

                entity.Property(e => e.Quantity).HasComment("تعداد دارو");

                entity.Property(e => e.Returned).HasComment("مرجوعی");

                entity.Property(e => e.SalesPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasComment("قیمت فروش");

                entity.Property(e => e.Tax)
                    .HasColumnType("decimal(18, 0)")
                    .HasComment("مالیات");

                entity.Property(e => e.TotalPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasComment("فیلد محاسباتی قیمت کل");

                entity.HasOne(d => d.Drug)
                    .WithMany(p => p.PurchaseInvoiceDetails)
                    .HasForeignKey(d => d.DrugId)
                    .HasConstraintName("FK_Drugs_PurchaseInvoiceDetails");

                entity.HasOne(d => d.PackagingType)
                    .WithMany(p => p.PurchaseInvoiceDetails)
                    .HasForeignKey(d => d.PackagingTypeId)
                    .HasConstraintName("FK_PackagingType_PurchaseInvoiceDetails");

                entity.HasOne(d => d.PurchaseInvoice)
                    .WithMany(p => p.PurchaseInvoiceDetails)
                    .HasForeignKey(d => d.PurchaseInvoiceId)
                    .HasConstraintName("FK_PurchaseInvoice_PurchaseInvoiceDetails");
            });

            modelBuilder.Entity<PurposeRequestBlood>(entity =>
            {
                entity.ToTable("PurposeRequestBlood", "Lab");

                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<ReadyAnswer>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(255);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasComment("هر آزمایش در نوعی دیگر دسته¬بندی شده و بر اساس دسته بندی، در شرایط خاصی برای دریافت نمونه و جواب¬دهی با آن برخورد می-شود. این آزمایشات در ثبت و روش انجام با هم تفاوت دارند. به همین جهت، در ثبت آزمایش مهم است که نوع آزمایش مشخص شود.");

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<ReadyAnswer1>(entity =>
            {
                entity.ToTable("ReadyAnswer", "Lab");

                entity.Property(e => e.Code1).HasMaxLength(255);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasComment("هر آزمایش در نوعی دیگر دسته¬بندی شده و بر اساس دسته بندی، در شرایط خاصی برای دریافت نمونه و جواب¬دهی با آن برخورد می-شود. این آزمایشات در ثبت و روش انجام با هم تفاوت دارند. به همین جهت، در ثبت آزمایش مهم است که نوع آزمایش مشخص شود.");

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<Reception>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Reception");
            });

            modelBuilder.Entity<ReceptionAnswer>(entity =>
            {
                entity.Property(e => e.InfoDate).HasColumnType("datetime");

                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.ReceptionAnswer)
                    .HasForeignKey(d => d.AnswerId)
                    .HasConstraintName("FK_ReceptionAnswer_Answer");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.ReceptionAnswer)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK_ReceptionAnswer_Question");

                entity.HasOne(d => d.Reception)
                    .WithMany(p => p.ReceptionAnswer)
                    .HasForeignKey(d => d.ReceptionId)
                    .HasConstraintName("FK_ReceptionAnswer_Receptions");
            });

            modelBuilder.Entity<ReceptionDetail>(entity =>
            {
                entity.ToTable("ReceptionDetail", "Lab");

                entity.Property(e => e.AnswerDate).HasColumnType("datetime");

                entity.Property(e => e.LmpDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(2000);

                entity.Property(e => e.PrescriptionDate).HasColumnType("datetime");

                entity.Property(e => e.ReceptionDate).HasColumnType("datetime");

                entity.HasOne(d => d.PatoDoctor)
                    .WithMany(p => p.ReceptionDetailPatoDoctor)
                    .HasForeignKey(d => d.PatoDoctorId)
                    .HasConstraintName("FK_ReceptionDetail_Doctors");

                entity.HasOne(d => d.PrescriptionDoctor)
                    .WithMany(p => p.ReceptionDetailPrescriptionDoctor)
                    .HasForeignKey(d => d.PrescriptionDoctorId)
                    .HasConstraintName("FK_ReceptionDetail_Doctors1");

                entity.HasOne(d => d.Reception)
                    .WithMany(p => p.ReceptionDetail)
                    .HasForeignKey(d => d.ReceptionId)
                    .HasConstraintName("FK_LabReceptionDetail_Receptions");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.ReceptionDetail)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("FK_ReceptionDetail_Section");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.ReceptionDetail)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_ReceptionDetail_ReceptionServiceStatus");
            });

            modelBuilder.Entity<ReceptionDiagnosis>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(200);

                entity.HasOne(d => d.Diagnosis)
                    .WithMany(p => p.ReceptionDiagnosis)
                    .HasForeignKey(d => d.DiagnosisId)
                    .HasConstraintName("FK_ReceptionDiagnosis_Diagnosis");

                entity.HasOne(d => d.Prescription)
                    .WithMany(p => p.ReceptionDiagnosis)
                    .HasForeignKey(d => d.PrescriptionId)
                    .HasConstraintName("FK_ReceptionDiagnosis_Prescription");

                entity.HasOne(d => d.Reception)
                    .WithMany(p => p.ReceptionDiagnosis)
                    .HasForeignKey(d => d.ReceptionId)
                    .HasConstraintName("FK_ReceptionDiagnosis_Receptions");
            });

            modelBuilder.Entity<ReceptionDiagnosisHistory>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.HasOne(d => d.DiagnosisStatus)
                    .WithMany(p => p.ReceptionDiagnosisHistory)
                    .HasForeignKey(d => d.DiagnosisStatusId)
                    .HasConstraintName("FK_ReceptionDiagnosisHistory_DiagnosisStatus");

                entity.HasOne(d => d.ReceptionDiagnosis)
                    .WithMany(p => p.ReceptionDiagnosisHistory)
                    .HasForeignKey(d => d.ReceptionDiagnosisId)
                    .HasConstraintName("FK_ReceptionDiagnosisHistory_ReceptionDiagnosis");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.ReceptionDiagnosisHistory)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Role_ReceptionDiagnosisHistory");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ReceptionDiagnosisHistory)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_ReceptionDiagnosisHistory_Users");
            });

            modelBuilder.Entity<ReceptionDrug>(entity =>
            {
                entity.ToTable("ReceptionDrug", "Drug");

                entity.Property(e => e.ConsumerSectionId).HasComment("این دارو در چه بخشی مصرف شده");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ExtraPayment)
                    .HasColumnType("decimal(18, 0)")
                    .HasComment("پرداخت اضافی حق فنی داروخانه");

                entity.Property(e => e.FinalPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Note).HasMaxLength(200);

                entity.Property(e => e.NurseId).HasComment("شخص دومی که از تجویز دارو سود می برد");

                entity.Property(e => e.PatientShare).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PrescriptionId).HasComment("این دارو از کدام تجویز امده یا نسخه");

                entity.Property(e => e.PrescriptionShare)
                    .HasColumnType("decimal(18, 0)")
                    .HasComment("حق نسخه ");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PriceMethodTypeId).HasComment("تعرفه ازاد تعرفه دولتی");

                entity.Property(e => e.Providershare).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.RequestId).HasComment("این دارو از کدام درخواست امده");

                entity.Property(e => e.SectionDiscount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.SectionId).HasComment("داروخانه ای که دارو را اعطا کرده است");

                entity.Property(e => e.TechnicalShare)
                    .HasColumnType("decimal(18, 0)")
                    .HasComment("حق فنی");

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.ConsumerSection)
                    .WithMany(p => p.ReceptionDrugConsumerSection)
                    .HasForeignKey(d => d.ConsumerSectionId)
                    .HasConstraintName("FK_ReceptionDrug_Consumer_Section");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.ReceptionDrug)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_ReceptionDrug_Doctors");

                entity.HasOne(d => d.Drug)
                    .WithMany(p => p.ReceptionDrug)
                    .HasForeignKey(d => d.DrugId)
                    .HasConstraintName("FK_ReceptionDrug_Drugs");

                entity.HasOne(d => d.PriceMethodType)
                    .WithMany(p => p.ReceptionDrug)
                    .HasForeignKey(d => d.PriceMethodTypeId)
                    .HasConstraintName("FK_ReceptionDrug_PriceTypeMethod");

                entity.HasOne(d => d.Reception)
                    .WithMany(p => p.ReceptionDrug)
                    .HasForeignKey(d => d.ReceptionId)
                    .HasConstraintName("FK_ReceptionDrug_Receptions");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.ReceptionDrug)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("FK_ReceptionDrug_Request");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.ReceptionDrugSection)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("FK_ReceptionDrug_Section_Section");
            });

            modelBuilder.Entity<ReceptionHistory>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.Property(e => e.PatientStatusId).HasComment("وضعیت خود بیمار");

                entity.Property(e => e.SectionId).HasComment("احتیاطا برای جدرج وضعیت در بخش های مجازی");

                entity.Property(e => e.SectionStatusId).HasComment("وضعیت بیمار در بخش مثل (ترخیص- خروج -پذیرش) ");

                entity.HasOne(d => d.ClearanceType)
                    .WithMany(p => p.ReceptionHistory)
                    .HasForeignKey(d => d.ClearanceTypeId)
                    .HasConstraintName("FK_RecepiotnHistory_ClearanceType");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.ReceptionHistory)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_RecepiotnHistory_Doctors");

                entity.HasOne(d => d.PatientStatus)
                    .WithMany(p => p.ReceptionHistory)
                    .HasForeignKey(d => d.PatientStatusId)
                    .HasConstraintName("FK_RecepiotnHistory_GeneralStatus");

                entity.HasOne(d => d.Reception)
                    .WithMany(p => p.ReceptionHistory)
                    .HasForeignKey(d => d.ReceptionId)
                    .HasConstraintName("FK_RecepiotnHistory_Receptions");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.ReceptionHistory)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Role_ReceptionHistory");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.ReceptionHistory)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("FK_RecepiotnHistory_Section");

                entity.HasOne(d => d.SectionStatus)
                    .WithMany(p => p.ReceptionHistory)
                    .HasForeignKey(d => d.SectionStatusId)
                    .HasConstraintName("FK_RecepiotnHistory_SectionStatus");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ReceptionHistory)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_RecepiotnHistory_Users");
            });

            modelBuilder.Entity<ReceptionSectionDoctor>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.ReceptionSectionDoctor)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_ReceptionSectionDoctor_Doctors");

                entity.HasOne(d => d.Reception)
                    .WithMany(p => p.ReceptionSectionDoctor)
                    .HasForeignKey(d => d.ReceptionId)
                    .HasConstraintName("FK_ReceptionSectionDoctor_Receptions");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.ReceptionSectionDoctor)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("FK_ReceptionSectionDoctor_Section");
            });

            modelBuilder.Entity<ReceptionService>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DoctorDiscount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ExtraPayment).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.FinalPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Note).HasMaxLength(200);

                entity.Property(e => e.PatientShare).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PrescriptionDetailServiceId).HasComment("برای رسیدن به سرویس از طریق تجویز");

                entity.Property(e => e.PrescriptionShare)
                    .HasColumnType("decimal(18, 0)")
                    .HasComment("حق نسخه ");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Providershare).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.SectionDiscount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TechnicalShare)
                    .HasColumnType("decimal(18, 0)")
                    .HasComment("حق فنی");

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.AnswerUser)
                    .WithMany(p => p.ReceptionService)
                    .HasForeignKey(d => d.AnswerUserId)
                    .HasConstraintName("FK_ReceptionService_Users");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.ReceptionService)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_ReceptionService_Doctors");

                entity.HasOne(d => d.PrescriptionDetailService)
                    .WithMany(p => p.ReceptionService)
                    .HasForeignKey(d => d.PrescriptionDetailServiceId)
                    .HasConstraintName("FK_ReceptionService_PrescriptionDetailService");

                entity.HasOne(d => d.Prescription)
                    .WithMany(p => p.ReceptionService)
                    .HasForeignKey(d => d.PrescriptionId)
                    .HasConstraintName("FK_ReceptionService_Prescription");

                entity.HasOne(d => d.PriceMethodType)
                    .WithMany(p => p.ReceptionService)
                    .HasForeignKey(d => d.PriceMethodTypeId)
                    .HasConstraintName("FK_ReceptionService_PriceMethodType");

                entity.HasOne(d => d.Reception)
                    .WithMany(p => p.ReceptionService)
                    .HasForeignKey(d => d.ReceptionId)
                    .HasConstraintName("FK_ReceptionService_Reception");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.ReceptionService)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("FK_ReceptionService_Request");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.ReceptionService)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("FK_ReceptionService_Section");

                entity.HasOne(d => d.ServiceChild)
                    .WithMany(p => p.ReceptionServiceServiceChild)
                    .HasForeignKey(d => d.ServiceChildId)
                    .HasConstraintName("FK_ReceptionService_Services");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ReceptionServiceService)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK_ReceptionService_Service");

                entity.HasOne(d => d.ServiceType)
                    .WithMany(p => p.ReceptionService)
                    .HasForeignKey(d => d.ServiceTypeId)
                    .HasConstraintName("FK_ReceptionService_ServiceType");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.ReceptionService)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_ReceptionService_ReceptionServiceStatusId");
            });

            modelBuilder.Entity<ReceptionServiceStatus>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.Property(e => e.TitleLang2).HasMaxLength(100);

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.ReceptionServiceStatus)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("FK_ReceptionServiceStatus_Section");
            });

            modelBuilder.Entity<ReceptionSign>(entity =>
            {
                entity.HasOne(d => d.Reception)
                    .WithMany(p => p.ReceptionSign)
                    .HasForeignKey(d => d.ReceptionId)
                    .HasConstraintName("FK_ReceptionSign_Receptions");

                entity.HasOne(d => d.Sign)
                    .WithMany(p => p.ReceptionSign)
                    .HasForeignKey(d => d.SignId)
                    .HasConstraintName("FK_ReceptionSign_Sign");
            });

            modelBuilder.Entity<ReceptionType>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<Receptions>(entity =>
            {
                entity.Property(e => e.Advice).HasMaxLength(2000);

                entity.Property(e => e.AnswerDate).HasColumnType("datetime");

                entity.Property(e => e.ChiefComplaints).HasMaxLength(2000);

                entity.Property(e => e.ConsumeDrug)
                    .HasMaxLength(400)
                    .HasComment("داروهای خاص");

                entity.Property(e => e.DateOfSign)
                    .HasColumnType("datetime")
                    .HasComment("تاریخ علائم");

                entity.Property(e => e.HospitalEnteryDate)
                    .HasColumnType("datetime")
                    .HasComment("تاریخ ورود به بیمارستان");

                entity.Property(e => e.IsHaveSign).HasComment("ایا علائمی دارد یا نه");

                entity.Property(e => e.IsResult).HasComment("فعلا برای کوید 19 جواب");

                entity.Property(e => e.Note).HasMaxLength(2000);

                entity.Property(e => e.ParentId).HasComment("برای اینکه بدانیم شخصی که این کوید را ثبت کرد چه کسی است . فعلا به جدول پرسن مرتب میکنیم");

                entity.Property(e => e.ReceptionDate).HasColumnType("datetime");

                entity.Property(e => e.RecoveryDate)
                    .HasColumnType("datetime")
                    .HasComment("تاریخ بهبودی");

                entity.Property(e => e.RefferDate).HasColumnType("datetime");

                entity.Property(e => e.ResultNote).HasMaxLength(2500);

                entity.HasOne(d => d.CurrentIllness)
                    .WithMany(p => p.Receptions)
                    .HasForeignKey(d => d.CurrentIllnessId)
                    .HasConstraintName("FK_Receptions_Illness");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.ReceptionsDoctor)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_Reception_Doctors");

                entity.HasOne(d => d.GeneralStatus)
                    .WithMany(p => p.Receptions)
                    .HasForeignKey(d => d.GeneralStatusId)
                    .HasConstraintName("FK_Reception_GeneralStatus");

                entity.HasOne(d => d.HospitalizationDoctor)
                    .WithMany(p => p.ReceptionsHospitalizationDoctor)
                    .HasForeignKey(d => d.HospitalizationDoctorId)
                    .HasConstraintName("FK_Reception_Doctors1");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.Receptions)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Receptions_Person");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Receptions)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK_Reciption_Paitient");

                entity.HasOne(d => d.Presenter)
                    .WithMany(p => p.Receptions)
                    .HasForeignKey(d => d.PresenterId)
                    .HasConstraintName("FK_Receptions_Presenter");

                entity.HasOne(d => d.ReceptionType)
                    .WithMany(p => p.Receptions)
                    .HasForeignKey(d => d.ReceptionTypeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Reciption_ReceptionType");

                entity.HasOne(d => d.RefferFrom)
                    .WithMany(p => p.Receptions)
                    .HasForeignKey(d => d.RefferFromId)
                    .HasConstraintName("FK_Receptions_RefferFrom");

                entity.HasOne(d => d.Relation)
                    .WithMany(p => p.Receptions)
                    .HasForeignKey(d => d.RelationId)
                    .HasConstraintName("FK_Receptions_RelationShip");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Receptions)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("FK_Reciption_Section");
            });

            modelBuilder.Entity<RefferFrom>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<RefferReason>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_RefferReason_RefferReason");
            });

            modelBuilder.Entity<RelatedTest>(entity =>
            {
                entity.ToTable("RelatedTest", "Lab");

                entity.HasOne(d => d.MainService)
                    .WithMany(p => p.RelatedTestMainService)
                    .HasForeignKey(d => d.MainServiceId)
                    .HasConstraintName("FK_RelatedTest_Services");

                entity.HasOne(d => d.SubService)
                    .WithMany(p => p.RelatedTestSubService)
                    .HasForeignKey(d => d.SubServiceId)
                    .HasConstraintName("FK_RelatedTest_Services1");
            });

            modelBuilder.Entity<RelationShip>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<Religion>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DoneDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(2000);

                entity.Property(e => e.PrescriptionDate).HasColumnType("datetime");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Request)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_Request_Doctors");

                entity.HasOne(d => d.Prescription)
                    .WithMany(p => p.Request)
                    .HasForeignKey(d => d.PrescriptionId)
                    .HasConstraintName("FK_Request_Prescription");

                entity.HasOne(d => d.Reception)
                    .WithMany(p => p.Request)
                    .HasForeignKey(d => d.ReceptionId)
                    .HasConstraintName("FK_Request_Receptions");

                entity.HasOne(d => d.RequestType)
                    .WithMany(p => p.Request)
                    .HasForeignKey(d => d.RequestTypeId)
                    .HasConstraintName("FK_Request_RequestType");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Request)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Role_Request");

                entity.HasOne(d => d.SourceSection)
                    .WithMany(p => p.RequestSourceSection)
                    .HasForeignKey(d => d.SourceSectionId)
                    .HasConstraintName("FK_Request_Source_Section");

                entity.HasOne(d => d.TargetSection)
                    .WithMany(p => p.RequestTargetSection)
                    .HasForeignKey(d => d.TargetSectionId)
                    .HasConstraintName("FK_Request_Tareget_Section");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Request)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Request_Users");
            });

            modelBuilder.Entity<RequestDetail>(entity =>
            {
                entity.ToTable("RequestDetail", "Drug");

                entity.Property(e => e.ActionDate)
                    .HasColumnType("datetime")
                    .HasComment("احتیاطا اگر بخواهد دارو را از ساخت خاصی شروع کند");

                entity.Property(e => e.StatusId).HasComment("درحال بررسی - تایید شده - ابطال");

                entity.HasOne(d => d.DrugForm)
                    .WithMany(p => p.RequestDetail)
                    .HasForeignKey(d => d.DrugFormId)
                    .HasConstraintName("FK_RequestDetail_DrugForm");

                entity.HasOne(d => d.Drug)
                    .WithMany(p => p.RequestDetail)
                    .HasForeignKey(d => d.DrugId)
                    .HasConstraintName("FK_RequestDetail_Drugs1");

                entity.HasOne(d => d.GenericDrug)
                    .WithMany(p => p.RequestDetail)
                    .HasForeignKey(d => d.GenericDrugId)
                    .HasConstraintName("FK_RequestDetail_GenericDrug");

                entity.HasOne(d => d.PrescriptionInstruction)
                    .WithMany(p => p.RequestDetail)
                    .HasForeignKey(d => d.PrescriptionInstructionId)
                    .HasConstraintName("FK_RequestDetail_PrescriptionInstruction");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.RequestDetail)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_RequestDetail_RequestStatus");

                entity.HasOne(d => d.WayOfPrescription)
                    .WithMany(p => p.RequestDetail)
                    .HasForeignKey(d => d.WayOfPrescriptionId)
                    .HasConstraintName("FK_RequestDetail_WayOfPrescription");
            });

            modelBuilder.Entity<RequestDetails>(entity =>
            {
                entity.ToTable("RequestDetails", "Lab");

                entity.Property(e => e.Duration).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Note).HasMaxLength(2000);
            });

            modelBuilder.Entity<RequestHistory>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Delivery)
                    .WithMany(p => p.RequestHistoryDelivery)
                    .HasForeignKey(d => d.DeliveryId)
                    .HasConstraintName("FK_RequestHistory_DeliveryUsers");

                entity.HasOne(d => d.Receive)
                    .WithMany(p => p.RequestHistoryReceive)
                    .HasForeignKey(d => d.ReceiveId)
                    .HasConstraintName("FK_RequestHistory_ReceiveUsers");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.RequestHistory)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("FK_RequestHistory_Request");

                entity.HasOne(d => d.RequestStatus)
                    .WithMany(p => p.RequestHistory)
                    .HasForeignKey(d => d.RequestStatusId)
                    .HasConstraintName("FK_RequestHistory_RequestStatus");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RequestHistory)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Role_RequestHistory");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RequestHistory)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_RequestHistory_Users");
            });

            modelBuilder.Entity<RequestStatus>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<RequestType>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<RespiratoryServices>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.AbbreviationTitle).HasMaxLength(255);

                entity.Property(e => e.InterNationalCode).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(255);
            });

            modelBuilder.Entity<Rh>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<RichLongAnswer>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(30);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.RichLongAnswer)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK_RichLongAnswer_Services");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);

                entity.HasOne(d => d.RoleGroup)
                    .WithMany(p => p.Role)
                    .HasForeignKey(d => d.RoleGroupId)
                    .HasConstraintName("FK_Role_RoleGroup");
            });

            modelBuilder.Entity<RoleGroup>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.RolePermission)
                    .HasForeignKey(d => d.PermissionId)
                    .HasConstraintName("FK_RolePermission_Permissions");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RolePermission)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_RolePermission_Role");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.RolePermission)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("FK_RolePermission_Section");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.LocalCode).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.RoomNo).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);

                entity.HasOne(d => d.RoomType)
                    .WithMany(p => p.Room)
                    .HasForeignKey(d => d.RoomTypeId)
                    .HasConstraintName("FK_Room_RoomType");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Room)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("FK_Room_Section");
            });

            modelBuilder.Entity<RoomType>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<SampleType>(entity =>
            {
                entity.ToTable("SampleType", "Lab");

                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<SamplerType>(entity =>
            {
                entity.ToTable("SamplerType", "Lab");

                entity.Property(e => e.LocalCode).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<Sampling>(entity =>
            {
                entity.ToTable("Sampling", "Lab");

                entity.Property(e => e.Code).HasMaxLength(30);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.IsPatientIdentificationByQuestion).HasComment("شناسایی بیمار از طریق سوال");

                entity.Property(e => e.IsPatientIdentificationByWrist).HasComment("شناسایی بیمار از طریق مچ بند");

                entity.Property(e => e.LabReceptionId).HasComment("به ریسپشن ازمایشگاه متصل است");

                entity.Property(e => e.OtherSampler).HasMaxLength(100);

                entity.Property(e => e.SampleSizeTaken).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.SamplerTypeId).HasComment("نمونه گیری توسط چه کسی یا کجا");

                entity.HasOne(d => d.LabReception)
                    .WithMany(p => p.Sampling)
                    .HasForeignKey(d => d.LabReceptionId)
                    .HasConstraintName("FK_Sampling_ReceptionDetail");

                entity.HasOne(d => d.SampleType)
                    .WithMany(p => p.Sampling)
                    .HasForeignKey(d => d.SampleTypeId)
                    .HasConstraintName("FK_SamplingPatient_SampleType");

                entity.HasOne(d => d.SamplerType)
                    .WithMany(p => p.Sampling)
                    .HasForeignKey(d => d.SamplerTypeId)
                    .HasConstraintName("FK_SamplingPatient_SamplerType");

                entity.HasOne(d => d.SamplingPerson)
                    .WithMany(p => p.Sampling)
                    .HasForeignKey(d => d.SamplingPersonId)
                    .HasConstraintName("FK_SamplingPatient_Users");
            });

            modelBuilder.Entity<SamplingHistory>(entity =>
            {
                entity.ToTable("SamplingHistory", "Lab");

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.Property(e => e.StatusDate).HasColumnType("datetime");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.SamplingHistory)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Role_SamplingHistory");

                entity.HasOne(d => d.Sampling)
                    .WithMany(p => p.SamplingHistory)
                    .HasForeignKey(d => d.SamplingId)
                    .HasConstraintName("FK_SamplingHistory_Sampling");

                entity.HasOne(d => d.SamplingStatus)
                    .WithMany(p => p.SamplingHistory)
                    .HasForeignKey(d => d.SamplingStatusId)
                    .HasConstraintName("FK_SamplingHistory_SamplingStatus");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SamplingHistory)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_SamplingHistory_Users");
            });

            modelBuilder.Entity<SamplingStatus>(entity =>
            {
                entity.ToTable("SamplingStatus", "Lab");

                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<ScaleType>(entity =>
            {
                entity.HasComment("--مقیاس(Scale): انواع کمیتها. کمی (یک اندازه گیری واقعی)؛ ترتیب (مجموعه ای از گزینه ها) اسمی (پاسخ هایی که ترتیب طبیعی ندارند ، مانند نام باکتری ها)؛ و روایت ، (توضیحات ، مانند نتایج دیکته ناشی از اشعه X).");

                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<Scientificlevel>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(150);

                entity.Property(e => e.LocalCode).HasMaxLength(50);

                entity.Property(e => e.No).HasMaxLength(10);

                entity.Property(e => e.Note).HasMaxLength(200);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.SectionFieldId).HasComment(" جنس کلی بخش از نظر اینکه ازمایشگاه است یا اتاق عمل یا کلینیک");

                entity.Property(e => e.SectionKindId).HasComment("جنس بخش از نظر انبار بودن و نوع انبار");

                entity.Property(e => e.SectionTypeId).HasComment("جنس بخش از نظر بخش یا واحد بودن");

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);

                entity.HasOne(d => d.Boss)
                    .WithMany(p => p.SectionBoss)
                    .HasForeignKey(d => d.BossId)
                    .HasConstraintName("FK_Section_Employee1");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Section)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK_Section_BranchId");

                entity.HasOne(d => d.Center)
                    .WithMany(p => p.Section)
                    .HasForeignKey(d => d.CenterId)
                    .HasConstraintName("FK_Section_Center");

                entity.HasOne(d => d.SectionField)
                    .WithMany(p => p.Section)
                    .HasForeignKey(d => d.SectionFieldId)
                    .HasConstraintName("FK_Section_SectionField");

                entity.HasOne(d => d.SectionKind)
                    .WithMany(p => p.Section)
                    .HasForeignKey(d => d.SectionKindId)
                    .HasConstraintName("FK_Section_SectionKind");

                entity.HasOne(d => d.SectionType)
                    .WithMany(p => p.Section)
                    .HasForeignKey(d => d.SectionTypeId)
                    .HasConstraintName("FK_Section_SectionType");

                entity.HasOne(d => d.SuperVisorPersonel)
                    .WithMany(p => p.SectionSuperVisorPersonel)
                    .HasForeignKey(d => d.SuperVisorPersonelId)
                    .HasConstraintName("FK_Section_Employee");

                entity.HasOne(d => d.TemperatureType)
                    .WithMany(p => p.Section)
                    .HasForeignKey(d => d.TemperatureTypeId)
                    .HasConstraintName("FK_Section_TemperatureType");
            });

            modelBuilder.Entity<SectionEssentialsDrug>(entity =>
            {
                entity.ToTable("SectionEssentialsDrug", "Drug");

                entity.Property(e => e.DrugId).HasComment("در مورد انبار مرکزی ملزومات دارویی به صورت ملزومات تجاری هم ممکن است لازم شود.");

                entity.Property(e => e.OrderPoint).HasComment("نقطه سفارش اگر به تعداد خاصی رسید باید دارو خریداری شود");

                entity.HasOne(d => d.Drug)
                    .WithMany(p => p.SectionEssentialsDrug)
                    .HasForeignKey(d => d.DrugId)
                    .HasConstraintName("FK_SectionEssentialsDrug_Drugs");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.SectionEssentialsDrug)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("FK_SectionEssentialsDrug_Section");
            });

            modelBuilder.Entity<SectionEssentialsGenericDrug>(entity =>
            {
                entity.ToTable("SectionEssentialsGenericDrug", "Drug");

                entity.Property(e => e.GenericDrugId).HasComment("در بیشتر مواقع ملزومات دارویی به صورت جنریک ذخیره میشود.");

                entity.Property(e => e.OrderPoint).HasComment("نقطه سفارش اگر به تعداد خاصی رسید باید دارو خریداری شود");

                entity.HasOne(d => d.DrugForm)
                    .WithMany(p => p.SectionEssentialsGenericDrug)
                    .HasForeignKey(d => d.DrugFormId)
                    .HasConstraintName("FK_SectionEssentialsGenericDrug_DrugForm");

                entity.HasOne(d => d.GenericDrug)
                    .WithMany(p => p.SectionEssentialsGenericDrug)
                    .HasForeignKey(d => d.GenericDrugId)
                    .HasConstraintName("FK_SectionEssentialsGenericDrug_GenericDrug");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.SectionEssentialsGenericDrug)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("FK_SectionEssentialsGenericDrug_Section");
            });

            modelBuilder.Entity<SectionField>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<SectionKind>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<SectionProperty>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<SectionPropertyList>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.SectionPropertyList)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("FK_SectionPropertyList_Section");

                entity.HasOne(d => d.SectionProperty)
                    .WithMany(p => p.SectionPropertyList)
                    .HasForeignKey(d => d.SectionPropertyId)
                    .HasConstraintName("FK_SectionPropertyList_SectionProperty");
            });

            modelBuilder.Entity<SectionService>(entity =>
            {
                entity.HasOne(d => d.Section)
                    .WithMany(p => p.SectionService)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("FK_SectinService_Section");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.SectionService)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK_SectinService_Services");
            });

            modelBuilder.Entity<SectionStatus>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<SectionStore>(entity =>
            {
                entity.HasOne(d => d.Section)
                    .WithMany(p => p.SectionStoreSection)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("FK_SectionStore_SectionStore");

                entity.HasOne(d => d.SectionKind)
                    .WithMany(p => p.SectionStore)
                    .HasForeignKey(d => d.SectionKindId)
                    .HasConstraintName("FK_SectionStore_SectionKind");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.SectionStoreStore)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_SectionStore_Section");
            });

            modelBuilder.Entity<SectionType>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<SellingType>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<SendSmsStatus>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<ServiceCoding>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(50);

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ServiceCoding)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK_ServiceCoding_Services");
            });

            modelBuilder.Entity<ServiceLevel>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<ServiceType>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<Services>(entity =>
            {
                entity.Property(e => e.AbbreviationTitle).HasMaxLength(255);

                entity.Property(e => e.BasePrice).HasColumnType("money");

                entity.Property(e => e.Component).HasMaxLength(255);

                entity.Property(e => e.FormulaExpr).HasMaxLength(200);

                entity.Property(e => e.HourToResponseEmergency).HasComment("روزانه را هم به صورت ساعت در این فیلد ذخیره می شود");

                entity.Property(e => e.HourToResponseNormal).HasComment("روزانه را هم به صورت ساعت در این فیلد ذخیره می شود");

                entity.Property(e => e.InterNationalCode).HasMaxLength(255);

                entity.Property(e => e.LabTypeId).HasComment("هر آزمایش در نوعی دیگر دسته¬بندی شده و بر اساس دسته بندی، در شرایط خاصی برای دریافت نمونه و جواب¬دهی با آن برخورد می-شود. این آزمایشات در ثبت و روش انجام با هم تفاوت دارند. به همین جهت، در ثبت آزمایش مهم است که نوع آزمایش مشخص شود.");

                entity.Property(e => e.LatinDescription)
                    .HasMaxLength(100)
                    .HasComment("abbreviation name in lab test");

                entity.Property(e => e.LocalCode).HasMaxLength(255);

                entity.Property(e => e.LongCommonName).HasMaxLength(255);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.OrderObservationId).HasColumnName("OrderOBServationId");

                entity.Property(e => e.RelatedNames).HasMaxLength(255);

                entity.Property(e => e.SampleSize).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.TitleLang2).HasMaxLength(100);

                entity.HasOne(d => d.DataType)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.DataTypeId)
                    .HasConstraintName("FK_Services_DataType");

                entity.HasOne(d => d.GroupAnswer)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.GroupAnswerId)
                    .HasConstraintName("FK_Services_GroupAnswer");

                entity.HasOne(d => d.MethodType)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.MethodTypeId)
                    .HasConstraintName("FK_Services_MethodType");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Services_ParentServices");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.PropertyId)
                    .HasConstraintName("FK_Services_Property");

                entity.HasOne(d => d.RichLongAnswerNavigation)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.RichLongAnswerId)
                    .HasConstraintName("FK_Services_RichLongAnswer");

                entity.HasOne(d => d.ScaleType)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.ScaleTypeId)
                    .HasConstraintName("FK_Services_ScaleType");

                entity.HasOne(d => d.ServiceLevel)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.ServiceLevelId)
                    .HasConstraintName("FK_Service_ServiceLevel");

                entity.HasOne(d => d.ServiceType)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.ServiceTypeId)
                    .HasConstraintName("FK_Service_ServiceType");

                entity.HasOne(d => d.TestAnswerType)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.TestAnswerTypeId)
                    .HasConstraintName("FK_Services_TestAnswerType");

                entity.HasOne(d => d.TimeAspct)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.TimeAspctId)
                    .HasConstraintName("FK_Services_TimeAspct");
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.KeyName).HasMaxLength(150);

                entity.Property(e => e.KeyValue)
                    .HasColumnName("keyValue")
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Setting1>(entity =>
            {
                entity.ToTable("Setting", "Lab");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.KeyName).HasMaxLength(150);

                entity.Property(e => e.KeyValue)
                    .HasColumnName("keyValue")
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Sex>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<SideEffects>(entity =>
            {
                entity.ToTable("SideEffects", "Drug");

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.HasOne(d => d.Drug)
                    .WithMany(p => p.SideEffects)
                    .HasForeignKey(d => d.DrugId)
                    .HasConstraintName("FK_SideEffects_Drug");

                entity.HasOne(d => d.GenericDrug)
                    .WithMany(p => p.SideEffects)
                    .HasForeignKey(d => d.GenericDrugId)
                    .HasConstraintName("FK_SideEffects_GenericDrug");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.SideEffects)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_SideEffects_Group");
            });

            modelBuilder.Entity<Sign>(entity =>
            {
                entity.Property(e => e.LocalCode).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLange2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(4000);

                entity.Property(e => e.TitleLang2).HasMaxLength(4000);
            });

            modelBuilder.Entity<SmsReception>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(100);

                entity.Property(e => e.Mobile).HasMaxLength(50);

                entity.Property(e => e.SendDate).HasColumnType("datetime");

                entity.Property(e => e.SmsContent).HasMaxLength(4000);

                entity.HasOne(d => d.Reception)
                    .WithMany(p => p.SmsReception)
                    .HasForeignKey(d => d.ReceptionId)
                    .HasConstraintName("FK_SmsReception_Receptions");

                entity.HasOne(d => d.SendsmsStatus)
                    .WithMany(p => p.SmsReception)
                    .HasForeignKey(d => d.SendsmsStatusId)
                    .HasConstraintName("FK_SmsReception_SendSmsStatus");
            });

            modelBuilder.Entity<SpecialIllness>(entity =>
            {
                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<SpecialIllnessReception>(entity =>
            {
                entity.HasOne(d => d.Reception)
                    .WithMany(p => p.SpecialIllnessReception)
                    .HasForeignKey(d => d.ReceptionId)
                    .HasConstraintName("FK_SpecialIllnessReception_Receptions");

                entity.HasOne(d => d.SpecialIllness)
                    .WithMany(p => p.SpecialIllnessReception)
                    .HasForeignKey(d => d.SpecialIllnessId)
                    .HasConstraintName("FK_Illness_SpecialIllnessReception");
            });

            modelBuilder.Entity<StoreDrug>(entity =>
            {
                entity.ToTable("StoreDrug", "Drug");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.SectionId).HasComment("انبار یا ترالی یا استوک");

                entity.HasOne(d => d.Drug)
                    .WithMany(p => p.StoreDrug)
                    .HasForeignKey(d => d.DrugId)
                    .HasConstraintName("FK_StoreDrug_Drugs");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.StoreDrug)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("FK_StoreDrug_Section");
            });

            modelBuilder.Entity<StoreEntry>(entity =>
            {
                entity.ToTable("StoreEntry", "Drug");

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.HasOne(d => d.PurchaseInvoice)
                    .WithMany(p => p.StoreEntry)
                    .HasForeignKey(d => d.PurchaseInvoiceId)
                    .HasConstraintName("FK_StoreEntry_PurchaseInvoice");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.StoreEntry)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Role_StoreEntry");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StoreEntry)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_StoreEntery_Section");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.StoreEntry)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_StoreEntery_Users");
            });

            modelBuilder.Entity<StoreEntryDetails>(entity =>
            {
                entity.ToTable("StoreEntryDetails", "Drug");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.HasOne(d => d.Drug)
                    .WithMany(p => p.StoreEntryDetails)
                    .HasForeignKey(d => d.DrugId)
                    .HasConstraintName("FK_StoreEnteryDetails_Drugs");

                entity.HasOne(d => d.PackagingType)
                    .WithMany(p => p.StoreEntryDetails)
                    .HasForeignKey(d => d.PackagingTypeId)
                    .HasConstraintName("FK_StoreEnteryDetails_PackagingType");

                entity.HasOne(d => d.PurchaseInvoiceDetails)
                    .WithMany(p => p.StoreEntryDetails)
                    .HasForeignKey(d => d.PurchaseInvoiceDetailsId)
                    .HasConstraintName("FK_StoreEntryDetails_PurchaseInvoiceDetails");

                entity.HasOne(d => d.StoreEntry)
                    .WithMany(p => p.StoreEntryDetails)
                    .HasForeignKey(d => d.StoreEntryId)
                    .HasConstraintName("FK_StoreEnteryDetails_StoreEntery");
            });

            modelBuilder.Entity<StoreTransfer>(entity =>
            {
                entity.ToTable("StoreTransfer", "Drug");

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.TransferDate).HasColumnType("datetime");

                entity.HasOne(d => d.DeliveryUser)
                    .WithMany(p => p.StoreTransferDeliveryUser)
                    .HasForeignKey(d => d.DeliveryUserId);

                entity.HasOne(d => d.DestinationStore)
                    .WithMany(p => p.StoreTransferDestinationStore)
                    .HasForeignKey(d => d.DestinationStoreId);

                entity.HasOne(d => d.ReceiveUser)
                    .WithMany(p => p.StoreTransferReceiveUser)
                    .HasForeignKey(d => d.ReceiveUserId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.StoreTransfer)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Role_StoreTransfer");

                entity.HasOne(d => d.SourceStore)
                    .WithMany(p => p.StoreTransferSourceStore)
                    .HasForeignKey(d => d.SourceStoreId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.StoreTransfer)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<StoreTransferDetails>(entity =>
            {
                entity.ToTable("StoreTransferDetails", "Drug");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.HasOne(d => d.Drug)
                    .WithMany(p => p.StoreTransferDetails)
                    .HasForeignKey(d => d.DrugId)
                    .HasConstraintName("FK_StoreTransferDetails_Drugs");

                entity.HasOne(d => d.PackagingType)
                    .WithMany(p => p.StoreTransferDetails)
                    .HasForeignKey(d => d.PackagingTypeId)
                    .HasConstraintName("FK_StoreTransferDetails_PackagingType1");

                entity.HasOne(d => d.StoreTransfer)
                    .WithMany(p => p.StoreTransferDetails)
                    .HasForeignKey(d => d.StoreTransferId)
                    .HasConstraintName("FK_StoreTransferDetails_StoreTransfer");
            });

            modelBuilder.Entity<StoregCondition>(entity =>
            {
                entity.ToTable("StoregCondition", "Drug");

                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<Table1>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Table_1");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(100);

                entity.Property(e => e.Satet)
                    .HasColumnName("satet")
                    .HasMaxLength(100);

                entity.Property(e => e.Zone)
                    .HasColumnName("zone")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Temp>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("temp");

                entity.Property(e => e.Advice).HasMaxLength(2000);

                entity.Property(e => e.AnswerDate).HasColumnType("datetime");

                entity.Property(e => e.ChiefComplaints).HasMaxLength(2000);

                entity.Property(e => e.ConsumeDrug).HasMaxLength(400);

                entity.Property(e => e.DateOfSign).HasColumnType("datetime");

                entity.Property(e => e.HospitalEnteryDate).HasColumnType("datetime");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Note).HasMaxLength(2000);

                entity.Property(e => e.ReceptionDate).HasColumnType("datetime");

                entity.Property(e => e.RecoveryDate).HasColumnType("datetime");

                entity.Property(e => e.RefferDate).HasColumnType("datetime");

                entity.Property(e => e.ResultNote).HasMaxLength(2500);
            });

            modelBuilder.Entity<TemperatureType>(entity =>
            {
                entity.ToTable("TemperatureType", "Drug");

                entity.Property(e => e.Note).HasMaxLength(2500);

                entity.Property(e => e.Range).HasMaxLength(250);

                entity.Property(e => e.Title).HasMaxLength(250);
            });

            modelBuilder.Entity<Template>(entity =>
            {
                entity.ToTable("Template", "Lab");

                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Template)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Template_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Template)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Template_Users");
            });

            modelBuilder.Entity<TestAnswerTime>(entity =>
            {
                entity.ToTable("TestAnswerTime", "Lab");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.TestAnswerTime)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK_Table_1_Services");
            });

            modelBuilder.Entity<TestAnswerType>(entity =>
            {
                entity.ToTable("TestAnswerType", "Lab");

                entity.HasComment("این جدول نوع جواب تست تعیین مکنید مثلا از نوعی است");

                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<TestLabeling>(entity =>
            {
                entity.ToTable("TestLabeling", "Lab");

                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<TestRange>(entity =>
            {
                entity.ToTable("TestRange", "Lab");

                entity.Property(e => e.AgeFrom).HasComment("برحسب روز می باشد");

                entity.Property(e => e.AgeTo).HasComment("برحسب روز می باشد");

                entity.Property(e => e.CriticalHighComment).HasMaxLength(500);

                entity.Property(e => e.CriticalLowComment).HasMaxLength(500);

                entity.Property(e => e.LowComment).HasMaxLength(500);

                entity.Property(e => e.NormalComment).HasMaxLength(500);

                entity.Property(e => e.OutOfRangeComment).HasMaxLength(500);

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.TestRange)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK_TestRange_Services");

                entity.HasOne(d => d.Sex)
                    .WithMany(p => p.TestRange)
                    .HasForeignKey(d => d.SexId)
                    .HasConstraintName("FK_TestRange_Sex");
            });

            modelBuilder.Entity<TimeAspct>(entity =>
            {
                entity.HasComment("فاصله زمانی که آزمون انجام شده است ، مانند یک نتیجه در یک زمان یا نتیجه ای که در طی 24 ساعت تدوین شده باشد");

                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<Unit1>(entity =>
            {
                entity.ToTable("Unit", "Drug");

                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<Unit2>(entity =>
            {
                entity.ToTable("Unit", "Lab");

                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<UserCardCode>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ExpireDate).HasColumnType("datetime");

                entity.HasOne(d => d.CardCode)
                    .WithMany(p => p.UserCardCode)
                    .HasForeignKey(d => d.CardCodeId)
                    .HasConstraintName("FK_UserCardCode_CardCode");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserCardCode)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserCardCode_Users");
            });

            modelBuilder.Entity<UserPermission>(entity =>
            {
                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.UserPermission)
                    .HasForeignKey(d => d.PermissionId)
                    .HasConstraintName("FK_UserPermission_Permissions");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserPermission)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Role_UserPermission");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.UserPermission)
                    .HasForeignKey(d => d.SectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserPermission_Section");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPermission)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserPermission_Users");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_UserRole_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserRole_Users");
            });

            modelBuilder.Entity<UserRolePermission>(entity =>
            {
                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.UserRolePermission)
                    .HasForeignKey(d => d.PermissionId)
                    .HasConstraintName("FK_UserRolePermission_Permissions");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRolePermission)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_UserRolePermission_Role");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.UserRolePermission)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("FK_UserRolePermission_Section");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRolePermission)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserRolePermission_Users");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.MacAddress).HasMaxLength(100);

                entity.Property(e => e.Password).HasMaxLength(250);

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.HasOne(d => d.CardCode)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.CardCodeId)
                    .HasConstraintName("FK_Users_CardCode");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_Users_Person");
            });

            modelBuilder.Entity<VisitType>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<VitalSigns>(entity =>
            {
                entity.Property(e => e.Bmi)
                    .HasColumnName("BMI")
                    .HasComment("رابط بین قد وزن");

                entity.Property(e => e.BodyMassIndexes).HasComment("چگالی");

                entity.Property(e => e.Bpmean).HasColumnName("BPMean");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Creatinine).HasComment("دفع کراتین");

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.HasOne(d => d.Reception)
                    .WithMany(p => p.VitalSigns)
                    .HasForeignKey(d => d.ReceptionId)
                    .HasConstraintName("FK_VitalSigns_Receptions");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.VitalSigns)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Role_VitalSigns");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.VitalSigns)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_VitalSigns_Users");
            });

            modelBuilder.Entity<WayOfPrescription>(entity =>
            {
                entity.ToTable("WayOfPrescription", "Drug");

                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);
            });

            modelBuilder.Entity<WorkTimeType>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.TitleLang2).HasMaxLength(50);
            });

            modelBuilder.Entity<Zone>(entity =>
            {
                entity.Property(e => e.Code1).HasMaxLength(20);

                entity.Property(e => e.Code2).HasMaxLength(20);

                entity.Property(e => e.Note).HasMaxLength(300);

                entity.Property(e => e.NoteLang2).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(150);

                entity.Property(e => e.TitleLang2).HasMaxLength(150);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Zone)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Zone_City");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Zone_Zone");
            });
        }

        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            Set<TEntity>().AddRange(entities);
        }

        public void ExecuteSqlCommand(string query)
        {
            Database.ExecuteSqlCommand(query);
        }

        public void ExecuteSqlCommand(string query, params object[] parameters)
        {
            Database.ExecuteSqlCommand(query, parameters);
        }

        public T GetShadowPropertyValue<T>(object entity, string propertyName) where T : IConvertible
        {
            var value = this.Entry(entity).Property(propertyName).CurrentValue;
            return value != null
                ? (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture)
                : default(T);
        }

        public object GetShadowPropertyValue(object entity, string propertyName)
        {
            return this.Entry(entity).Property(propertyName).CurrentValue;
        }

        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            Update(entity);
        }

        public DatabaseFacade Databases => this.Database;

        public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            Set<TEntity>().RemoveRange(entities);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ChangeTracker.DetectChanges();

            beforeSaveTriggers();

            ChangeTracker.AutoDetectChangesEnabled = false; // for performance reasons, to avoid calling DetectChanges() again.
            var result = base.SaveChanges(acceptAllChangesOnSuccess);
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            beforeSaveTriggers();

            ChangeTracker.AutoDetectChangesEnabled = false; // for performance reasons, to avoid calling DetectChanges() again.
            var result = base.SaveChanges();
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.DetectChanges();

            beforeSaveTriggers();

            ChangeTracker.AutoDetectChangesEnabled = false; // for performance reasons, to avoid calling DetectChanges() again.
            int result = 0;
            try
            {

                //var entities = from e in ChangeTracker.Entries()
                //               where e.State == EntityState.Added
                //                   || e.State == EntityState.Modified || e.State==EntityState.Deleted
                //               select e.Entity;
                //foreach (var entity in entities)
                //{
                //    var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(entity);
                //    System.ComponentModel.DataAnnotations.Validator.ValidateObject(entity, validationContext);
                //}
                result = await base.SaveChangesAsync(cancellationToken);

            }
            catch (DbUpdateException ex)
            {
                var msg = ex.InnerException.Message;
                var sqlException = ex.GetBaseException() as SqlException;


                #region Deleted

                var listDelete = ChangeTracker.Entries().Where(d => d.State == EntityState.Deleted).ToList();

                if (listDelete != null && listDelete.Count > 0)
                {

                    if (sqlException != null)
                    {
                        var number = sqlException.Number;

                        if (number == 547)
                        {
                            // throw new Exception("Use Other Table For Delete");
                            throw new CanNotDeleteException(HttpStatusCode.BadRequest, "Use Other Table Can't Delete");
                        }
                    }
                }

                #endregion


                if (sqlException != null)
                {
                    var number = sqlException.Number;

                    if (number == 547)
                    {
                        throw new Exception("Use Other Table ");
                    }
                }

                BaseResponseDto baseResponseDto = new BaseResponseDto();
                baseResponseDto.Message = msg;
                //500 error
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, ex.InnerException.Message);
            }

            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.DetectChanges();

            beforeSaveTriggers();

            ChangeTracker.AutoDetectChangesEnabled = false; // for performance reasons, to avoid calling DetectChanges() again.
            var result = base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        private void beforeSaveTriggers()
        {
            validateEntities();
            setShadowProperties();
            //this.ApplyCorrectYeKe();
        }

        private void setShadowProperties()
        {
            // we can't use constructor injection anymore, because we are using the `AddDbContextPool<>`
            var httpContextAccessor = this.GetService<IHttpContextAccessor>();

            //httpContextAccessor.CheckArgumentIsNull(nameof(httpContextAccessor));
            //ChangeTracker.SetAuditableEntityPropertyValues(httpContextAccessor);
        }

        private void validateEntities()
        {
            //var errors = this.GetValidationErrors();
            //if (!string.IsNullOrWhiteSpace(errors))
            //{
            //    // we can't use constructor injection anymore, because we are using the `AddDbContextPool<>`
            //    var loggerFactory = this.GetService<ILoggerFactory>();
            //    loggerFactory.CheckArgumentIsNull(nameof(loggerFactory));
            //    var logger = loggerFactory.CreateLogger<ApplicationDbContext>();
            //    logger.LogError(errors);
            //    throw new InvalidOperationException(errors);
            //}
        }

        public async Task DeleteByIdAsync<TEntity>(object id) where TEntity : class
        {
            var entity = await GetByIdAsync<TEntity>(id);
            Set<TEntity>().Remove(entity);
        }

        public async Task<TEntity> GetByIdAsync<TEntity>(object id) where TEntity : class
        {
            return await Set<TEntity>().FindAsync(id);
        }

        public List<TEntity> CallProc<TEntity>(string proc, SqlParameter[] parameters)
        {
            object obj = new object();    //dfsdfsdf 


            // var lst= Set<TEntity>().FromSql("[dbo].[GetDoctors] @DoctorId", parameters).ToList();
            throw new NotImplementedException();
        }
    }

}
