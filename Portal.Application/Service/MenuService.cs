

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Context;
using Portal.DTO;
using Portal.Application.Validation;
using Portal.Entities.Models;
using Portal.Infrastructure;
using Portal.Application.Interface;
using Portal.Validation;

namespace Portal.Service
{
    //public class MenuService : IMenuService
    //{
    //    private readonly IUnitOfWork _unitOfWork;
    //    private readonly DbSet<Menu> _menuRepository;
    //    private readonly DbSet<UserPermission> _userPermissionRepository;
    //    private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
    //    private const string ErrorMessageHeader = "MenuService";

    //    public MenuService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharedLocalizer,
    //        IPrescriptionService prescriptionService)
    //    {
    //        _unitOfWork = unitOfWork;
    //        _menuRepository = _unitOfWork.Set<Menu>();
    //        _userPermissionRepository = _unitOfWork.Set<UserPermission>();
    //        _sharedLocalizer = sharedLocalizer;
    //        _prescriptionService = prescriptionService;
    //    }
    //    public async Task<BaseResponseDto> GetAllByUserId(MenuGetDto menuGetDto)
    //    {
    //        var result = CheckValidate.Valid<MenuGetDto>(new MenuGetVlidation(_sharedLocalizer), (MenuGetDto)menuGetDto);

    //        if (result.Status == ResponseStatus.NotValid)
    //        {
    //            return result;
    //        }

    //        var query = _menuRepository.AsQueryable();

    //        //if (sectionId == 0)
    //        //{
    //        //    List<string> errors = new List<string>();
    //        //    errors.Add(_sharedLocalizer["GlobalForm.FieldValidation.GreaterThan.SectionId"]);//+
    //        //    return new BaseResponseDto

    //        //    {
    //        //        Status = ResponseStatus.NotValid,
    //        //        Errors = Utilities.CreateErrorNoTValid("sectionId", errors)
    //        //    };
    //        //}

    //        var userPermission = await _userPermissionRepository.Where(d => d.UserId == menuGetDto.UserId && d.SectionId == menuGetDto.SectionId)
    //            .AsNoTracking()
    //            .ToListAsync();

    //        var lstAllMenu = await query
    //                    .Select(g => new GetMenuDb
    //                    {
    //                        Id = g.Id,
    //                        Title = g.Title,
    //                        TitleLang2 = g.TitleLang2,
    //                        Link = g.Link,
    //                        MenuName = g.MenuName,
    //                        MenuType = g.MenuType,
    //                        ParentId = g.ParentId,
    //                        IsActive = g.IsActive,
    //                        IconName = g.IconName,
    //                        ArrangeId = g.ArrangeId,
    //                        UserIds = userPermission.Where(d => d.PermissionId == g.PermissionId).Select(d => (int)d.UserId).ToList(),
    //                        SectionIds = userPermission.Where(d => d.PermissionId == g.PermissionId).Select(d => d.SectionId).ToList()
    //                    })
    //                    .AsNoTracking()
    //                    .ToListAsync();

    //        var lstMenu = lstAllMenu
    //                 .Where(m => m.UserIds.Any(d => d == menuGetDto.UserId) && m.SectionIds.Any(d => d == menuGetDto.SectionId) && m.IsActive == true && m.MenuType != "Right").ToList();

    //        List<GetMenuDb> lstMenuTemp = new List<GetMenuDb>();
    //        //find parrent menu
    //        foreach (var item in lstMenu)
    //        {
    //            var menuItme = lstAllMenu.Where(m => m.Id == item.ParentId && m.IsActive == true).FirstOrDefault();

    //            if (menuItme != null && !lstMenuTemp.Any(r => r.Id == menuItme.Id) && !lstMenu.Any(r => r.Id == menuItme.Id))
    //                lstMenuTemp.Add(menuItme);
    //        }

    //        lstMenu.AddRange(lstMenuTemp);

    //        var lstMenuTree = GetTreeMenu(lstMenu, 0);

    //        return new BaseResponseDto
    //        {
    //            Data = lstMenuTree,
    //            Status = ResponseStatus.Success
    //        };
    //    }

    //    public async Task<BaseResponseDto> GetMenuRight(MenuGetDto menuGetDto, long? receptionId)
    //    {
    //        // اعتبار سنجی
    //        var result = CheckValidate.Valid<MenuGetDto>(new MenuGetVlidation(_sharedLocalizer), (MenuGetDto)menuGetDto);
    //        if (result.Status == ResponseStatus.NotValid)
    //            return result;

    //        // کلیه خطاها
    //        var errorsMessage = new Dictionary<string, string>();

    //        // var query = _menuRepository.AsQueryable();
    //        // CheckValidate.Valid<MenuGetDto>(new MenuGetValidation(_sharedLocalizer), (MenuGetDto)menuGetDto);
    //        var lstAllMenu = await _menuRepository
    //                                    .Where(m => (m.Permission.UserPermission.Any(d => d.UserId == menuGetDto.UserId) && m.Permission.UserPermission.Any(d => d.SectionId == menuGetDto.SectionId) || m.ParentId == 0) && m.IsActive == true && m.MenuType == "Right")
    //                                    .OrderBy(d => d.ArrangeId)
    //                                    .Select(Mapper.MenuMapper.MapListMenu)
    //                                    .AsNoTracking()
    //                                    .ToListAsync();

    //        // منوی پدر برای ویزیت تایپ ها
    //        var parentId = lstAllMenu.FirstOrDefault(x => x.MenuName == "AdmissionRightParent")?.Id;

    //        // خطا در صورتی که پدر ویزیت تایپ ها وجود نداشته باشد
           

    //        lstAllMenu.AddRange(visitTypeMenuItems);
    //        var lstAllMenuSort = lstAllMenu.OrderBy(x => x.ArrangeId).ToList();

    //        var lstMenuTree = GetTreeRightMenu(lstAllMenuSort, 0);

    //        return new BaseResponseDto
    //        {
    //            Data = lstMenuTree,
    //            Status = ResponseStatus.Success,
              
    //        };
    //    }

    //    private List<MenuDto> GetTreeMenu(List<GetMenuDb> menus, int parentId)
    //    {
    //        var childMenu = menus.Where(m => m.ParentId == parentId).ToList();
    //        List<MenuDto> menuDtos = new List<MenuDto>();

    //        foreach (var item in childMenu)
    //        {
    //            MenuDto menuDto = new MenuDto();
    //            menuDto.Id = item.Id;
    //            menuDto.MenuName = item.MenuName;
    //            menuDto.MenuType = item.MenuType;
    //            menuDto.Title = Utilities.Language.GetTilteByLang(item, Utilities.Language.CurrentLanguage);
    //            menuDto.TitleLang2 = item.TitleLang2;
    //            menuDto.IconName = item.IconName;
    //            menuDto.Link = item.Link;

    //            menuDtos.Add(menuDto);

    //            var child = GetTreeMenu(menus, item.Id);

    //            if (child != null && child.Count > 0)
    //            {
    //                menuDto.Childs = new List<MenuDto>();
    //                menuDto.Childs.AddRange(child);
    //            }
    //        }

    //        return menuDtos;
    //    }

    //    private List<MenuRightDto> GetTreeRightMenu(List<MenuRightDto> menus, int parentId)
    //    {
    //        var childMenu = menus.Where(m => m.ParentId == parentId).ToList();
    //        List<MenuRightDto> menuDtos = new List<MenuRightDto>();

    //        foreach (var item in childMenu)
    //        {
    //            MenuRightDto menuDto = new MenuRightDto();
    //            menuDto.Id = item.Id;
    //            menuDto.MenuName = item.MenuName;
    //            menuDto.MenuType = item.MenuType;
    //            menuDto.Title = Utilities.Language.GetTilteByLang(item, Utilities.Language.CurrentLanguage);
    //            menuDto.TitleLang2 = item.TitleLang2;
    //            menuDto.IconName = item.IconName;
    //            menuDto.Link = item.Link;
    //            menuDto.IsActive = item.IsActive;

    //            menuDtos.Add(menuDto);

    //            var child = GetTreeRightMenu(menus, item.Id);

    //            if (child != null && child.Count > 0)
    //            {
    //                menuDto.Childs = new List<MenuRightDto>();
    //                menuDto.Childs.AddRange(child);
    //            }
    //        }

    //        return menuDtos;
    //    }
    //}
}
