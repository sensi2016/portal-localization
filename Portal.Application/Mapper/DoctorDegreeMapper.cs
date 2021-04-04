using System;
using Portal.DTO;
using Portal.Entities.Models;

namespace Portal.Application.Mapper
{
    public class DoctorDegreeMapper
    {
        public static DoctorDegreeDto MapToDto(DoctorDegree model)
        {
            return new DoctorDegreeDto
            {
                Id = model.Id,
                DoctorId = model.DoctorId,
                DoctorFulName = model.Doctor.Person?.FullName,
                DegreeId = model.DegreeId,
                DegreeTitle = model.Degree?.Title,
                DateOfIssue = model.DateOfIssue
            };
        }

        public static DoctorDegree MapToModel(DoctorDegreeDto dto, DoctorDegree model = null)
        {
            if (model is null) model = new DoctorDegree();

            model.Id = dto.Id;
            model.DoctorId = dto.DoctorId;
            model.DegreeId = dto.DegreeId;
            model.DateOfIssue = dto.DateOfIssue;

            return model;
        }

    }
}
