using His.Reception.DTO;
using Portal.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace  Portal.Application.Interface
{
    public interface IPortalResourceService
    {
        Task<BaseResponseDto> GetLabelReport(string reportName, List<Type> models);
        Task<BaseResponseDto> New(NewResourceDto addResource);
    }
}
