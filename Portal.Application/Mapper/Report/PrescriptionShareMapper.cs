using Portal.DTO;
using Portal.DTO.PatientCard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Mapper
{
    public class PrescriptionShareMapper
    {
        public static VitalSignDto VitalMap(List<ResponsePrescriptionServiceResultReport> responsePrescriptionServiceResultReport)
        {
            if (responsePrescriptionServiceResultReport.Count == 0)
                return null;

            VitalSignDto vitalSignDto = new VitalSignDto();
            foreach (var item in responsePrescriptionServiceResultReport)
            {
                if (item.Code == "60621009")
                    vitalSignDto.BMI = item.Result;

                if (item.Code == "271649006")
                    vitalSignDto.BR = item.Result;

                if (item.Code == "251872006")
                    vitalSignDto.Creatinine = item.Result;

                if (item.Code == "737105002")
                    vitalSignDto.eGFR = item.Result;

                if (item.Code == "50373000")
                    vitalSignDto.Height = item.Result;

                if (item.Code == "78564009")
                    vitalSignDto.PR = item.Result;


                if (item.Code == "103579009")
                    vitalSignDto.SkinColor = item.Result;

                if (item.Code == "276885007")
                    vitalSignDto.Temperature = item.Result;

                if (item.Code == "431314004")
                    vitalSignDto.SPO2 = item.Result;

                if (item.Code == "27113001")
                    vitalSignDto.Weight = item.Result;

            }

            return vitalSignDto;

        }

        public static PatientInfoDto PaitenInfoMap(List<ResponsePrescriptionServiceResultReport> responsePrescriptionServiceResultReport)
        {
            if (responsePrescriptionServiceResultReport.Count == 0)
                return null;
            PatientInfoDto patientInfo = new PatientInfoDto();
            foreach (var item in responsePrescriptionServiceResultReport)
            {
                if (item.Code == "422625006")
                    patientInfo.DiseaseHistory = item.Result;

                if (item.Code == "422625006")
                    patientInfo.DiseasRecords = item.Result;

                if (item.Code == "33962009")
                    patientInfo.PatientComplaint = item.Result;

                if (item.Code == "161615003")
                    patientInfo.HistoryofSurgery = item.Result;

            }

            return patientInfo;

        }
    }
}
