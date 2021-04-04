using Portal.DAL.Extensions;
using His.Reception.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Portal.Application.Interface.Base;
using Portal.Application.Validation;
using Portal.Context;
using Portal.DTO;
using Portal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Application.Base
{
    public class BasicService<T> : IBasicService<T> where T : class, new()
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<T> _baseRepository;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public BasicService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _unitOfWork = unitOfWork;
            _baseRepository = _unitOfWork.Set<T>();
            _sharedLocalizer = sharedLocalizer;
        }

        public async Task<BaseResponseDto> Get(int id)
        {
            var cur = await _baseRepository.FindAsync(id);

            var map = Mapper.BaseMapper.Map(cur);

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = map
            };
        }

        public async Task<ListResponseDto> Search(RequestBaseFilterDto requestFilterBaseDto)
        {
            IQueryable<T> query = _baseRepository;

            // ids filter
            if (requestFilterBaseDto.IdList.TryAny()) query = query
                .Where(x => requestFilterBaseDto.IdList.Contains(Convert.ToInt32(x.GetType().GetProperty("Id").GetValue(x))));
            if (requestFilterBaseDto.Ids.TryAny()) query = query
                .Where(x => requestFilterBaseDto.Ids.Contains(Convert.ToInt32(x.GetType().GetProperty("Id").GetValue(x))));

            query = query.ToPagedQuery(requestFilterBaseDto);

            return new ListResponseDto
            {
                Status = ResponseStatus.Success,
                Count = await query.CountAsync(),
                Data = await query.Select(x => Mapper.BaseMapper.Map(x)).ToListAsync()
            };
        }

        public async Task<BaseResponseDto> GetAll()
        {
            var lst = _baseRepository
                .OrderByDescending(x => EF.Property<int>(x,"Id"))
                .Select(g => Mapper.BaseMapper.Map(g));

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = await lst.ToListAsync()
            };
        }

        public async Task<ListResponseDto> GetListPaging(IPaging paging)
        {
            var list = _baseRepository
                .OrderByDescending(x => EF.Property<int>(x, "Id"))
                .Select(g => Mapper.BaseMapper.Map(g));

            return new ListResponseDto
            {
                Status = ResponseStatus.Success,
                Count = await _baseRepository.CountAsync(),
                Data = await list.ToPagedQuery(paging).ToListAsync()
            };
        }

        public async Task<BaseResponseDto> AddAsync(RequestBaseDto baseDto)
        {
            var resultValid = CheckValidate.Valid<BaseDto>(new BaseValidation(_sharedLocalizer), baseDto);

            if (resultValid.Status == ResponseStatus.NotValid)
            {
                return resultValid;
            }

            //check duplicate

            if (await _baseRepository.WhereEquals("Title", baseDto.Title).AnyAsync())
            {
                var dicError = new Dictionary<string, string>()
                    {
                        { "DuplicateTitle",_sharedLocalizer[$"General.Response.DuplicateTitle"]}//+
                    };

                var error = Utilities.CreateErrorMessage("DuplicateTitle", dicError);

                return new BaseResponseDto
                {
                    Status = ResponseStatus.Fail,
                    Errors = error
                };
            }

            object ob = new object();

            // var rr=  Convert.ChangeType(ob, typeof(T));

            T generic = (T)Activator.CreateInstance<T>();
            //var val = generic.GetType().GetProperty("Id").GetValue(typeof(T));
            var map = Mapper.BaseMapper.Map(generic, baseDto);

            _baseRepository.Add((T)map);
            await _unitOfWork.SaveChangesAsync();

            var result = new ListResponseDto
            {
                Status = ResponseStatus.Success,
                Message = _sharedLocalizer["GeneralForm.Response.AddSuccess"]//+
            };

            if (baseDto.PageSize > 0 && baseDto.PageNumber > 0)
            {
                var getList = await GetListPaging(baseDto);
                result.Count = getList.Count;
                result.Data = getList.Data;
            }

            return result;
        }

        public async Task<BaseResponseDto> EditAsync(RequestBaseDto baseDto)
        {
            var cur = await _baseRepository.FindAsync(baseDto.Id);

            var id = cur.GetType().GetProperty("Id")?.GetValue(cur);

            var duplicate = await _baseRepository.WhereEquals("Title", baseDto.Title).FirstOrDefaultAsync();
            //check duplicate
            //در زمان ویرایش هم اگر تایتل با رکورد دیگر تکراری بود خطا میدهد
            if (duplicate != null)
            {
                var duplicateId = duplicate.GetType().GetProperty("Id")?.GetValue(duplicate);

                if (Convert.ToInt32(duplicateId) != Convert.ToInt32(id))
                {
                    var dicError = new Dictionary<string, string>()
                    {
                        { "DuplicateTitle",_sharedLocalizer[$"General.Response.DuplicateTitle"]}//+
                    };

                    var error = Utilities.CreateErrorMessage("DuplicateTitle", dicError);


                    return new BaseResponseDto
                    {
                        Status = ResponseStatus.NotValid,
                        Errors = error
                    };
                }
            }

            object map = null;

            if (Utilities.IsIdSystem(cur) || Utilities.RowIsAdmin(cur))
            {
                map = Mapper.BaseMapper.MapColSystem(cur, baseDto);
            }
            else
            {
                map = Mapper.BaseMapper.Map(cur, baseDto);
            }

            _baseRepository.Update((T)map);
            await _unitOfWork.SaveChangesAsync();

            var result = new ListResponseDto
            {
                Status = ResponseStatus.Success,
                Message = _sharedLocalizer["GeneralForm.Response.EditSuccess"]//+
            };

            if (baseDto.PageSize > 0 && baseDto.PageNumber > 0)
            {
                var getList = await GetListPaging(baseDto);
                result.Count = getList.Count;
                result.Data = getList.Data;
            }

            return result;
        }

        public async Task<BaseResponseDto> DeleteAsync(BaseRequestPost<int> baseRequestPost)
        {
            var cur = await _baseRepository
                .FindAsync(baseRequestPost.Id);

            var resultCheck = Utilities.CheckIsAdminForUpdateOrDelete(cur, "General", _sharedLocalizer, true);
            if (resultCheck.Status != ResponseStatus.Success)
            {
                return resultCheck;
            }

            _baseRepository.Remove(cur);
            await _unitOfWork.SaveChangesAsync();

            var result = new ListResponseDto
            {
                Status = ResponseStatus.Success,
                Message = _sharedLocalizer["GeneralForm.Response.DeleteSuccess"]//+
            };

            if (baseRequestPost.PageSize > 0 && baseRequestPost.PageNumber > 0)
            {
                var getList = await GetListPaging(baseRequestPost);
                result.Count = getList.Count;
                result.Data = getList.Data;
            }

            return result;
        }

        public BaseResponseDto CheckIsAdmin(object obj, string form)
        {
            var type = obj.GetType().GetProperty("IsAdmin")?.GetValue(obj);
            if (type != null)
            {
                var isadmin = Boolean.Parse(type.ToString());
                if (isadmin)
                {
                    var dicError = new Dictionary<string, string>()
                    {
                        { "NotCahnge",_sharedLocalizer[$"GlobalForm.Response.NotCahngeIsAdmin"]}
                    };

                    var error = Utilities.CreateErrorMessage("NotCahnge", dicError);


                    return new BaseResponseDto
                    {
                        Status = ResponseStatus.NotValid,
                        Errors = error
                    };
                }
            }

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success
            };
        }
    }
}
