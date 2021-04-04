using Portal.DTO;
using System.Threading.Tasks;

namespace Portal.Application.Interface
{
    public interface ISyncService
    {
        Task<string> SyncData(SyncDto syncDto);
        Task<BaseResponseDto> RegisterDoctorAsync(SyncDoctorDto doctorDto);
    }
}
