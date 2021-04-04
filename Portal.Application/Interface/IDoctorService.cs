using Portal.DTO;
using Portal.DTO.Doctor;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Interface
{
    public interface IDoctorService
    {
        Task<BaseResponseDto> GetById(int id);
        Task<ListResponseDto> Search(FilterDoctorDto filterCenterDto);
        Task<BaseResponseDto> Info(int doctorId);
        Task<ListResponseDto> SearchHome(FilterDoctorHomeDto filterCenterDto);
        Task<ListResponseDto> SearchAppHome(FilterDoctorAppHomeDto filterDoctorAppHomeDto);
        Task<BaseResponseDto> SetIsActive(List<DoctorDto> dtos);


        Task<BaseResponseDto> Add(DoctorDto doctorDto );
        Task<BaseResponseDto> Edit(DoctorDto doctorDto);
        Task<BaseResponseDto> Delete(int id);
        Task<BaseResponseDto> UploadLogo(UploadLogoDoctorDto uploadLogoDto);
    }
}
