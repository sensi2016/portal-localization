using Portal.DTO.PatientCard;
using Portal.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Portal.DAL.Extensions;
using Portal.DTO;
using Newtonsoft.Json;
using Portal.DTO.Doctor;

namespace Portal.Application.Mapper
{
    public class PatientCardMapper
    {
        public static Expression<Func<Prescription, PatientCardPrescriptionDto>> MapPrescriptions
        {
            get
            {
                return x => new PatientCardPrescriptionDto
                {
                    Id = x.Id,
                    DoctorName = x.Doctor.Person.FullName,
                    PrescriptionDate = x.PrescriptionDate.ToDateTimeStringTry(),
                    CreateDate = x.CreateDate.ToDateTimeStringTry(),
                    DoctorId = x.DoctorId,
                    PatientFullName=x.Reception !=null ? x.Reception.Patient.Person.FullName :"",
                    DoctorInfo = new DoctorInfoDto
                    {
                        Id=x.DoctorId.GetValueOrDefault(),
                        FullName = x.Doctor.Person != null ? x.Doctor.Person.LatinName : "",
                        ArabicFullName = x.Doctor.Person != null ? x.Doctor.Person.LastName : "",
                        Address = x.Doctor.Person != null ? x.Doctor.Person.Address : "",
                        Phone = x.Doctor.Person != null ? x.Doctor.Person.Phone : "",
                        Email = x.Doctor.Person != null ? x.Doctor.Person.Email : "",
                        Note = x.Doctor.Note,
                        ExpertiseTitle = x.Doctor.Expertise != null ? x.Doctor.Expertise.Title : "",
                        Degrees = x.Doctor.DoctorDegree != null && x.Doctor.DoctorDegree.Count > 0 ? x.Doctor.DoctorDegree.Select(y => new DoctorDegreeDto
                        {
                            Id = y.Id,
                            DoctorId = y.DoctorId,
                            DoctorFulName = y.Doctor.Person!=null?y.Doctor.Person.FullName:"",
                            DegreeId = y.DegreeId,
                            DegreeTitle = y.Degree !=null? y.Degree.Title:"",
                            DateOfIssue = y.DateOfIssue
                        }).ToList() : null,
                        WorkTimes= x.Doctor.Center != null ? (x.Doctor.Center.CenterWorkItem !=null ?   x.Doctor.Center.CenterWorkItem.Select(g=> new ListMultiResponse<int> { Id=g.Id,Title=g.WorkItem.Title}).ToList():null):null 
                    }
                };
            }
        }

        public static DrugWinAppBackendDto MapDrug(PrescriptionDetailDrug prescriptionDetailDrug)
        {
            return JsonConvert.DeserializeObject<DrugWinAppBackendDto>(prescriptionDetailDrug.Note);
        }
    }
}
