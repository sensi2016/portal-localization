
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Portal.DTO
{
    public class BaseDto
    {
        /// <summary>
        /// در هنگام ویرایش آیدی الزامی است
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// تایتل الزامی است
        /// </summary>
        public string Title { get; set; }

        public string TitleLang2 { get; set; }
        public string Note { get; set; }
        public string NoteLang2 { get; set; }


        public string Code1 { get; set; }
        public string Code2 { get; set; }
        public string Arrange { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
    }

    public class RequestBaseDto: BaseDto, IPaging
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

    public class RequestBaseFilterDto : BaseRequestPaging
    {
        /// <summary>
        /// آرایه ای از آیدی ها
        /// در آینده این پراپرتی حذف خواهد شد
        /// استفاده کنید Ids از این پراپرتی استفاد نکنید و به جای از پراپرتی
        /// </summary>
        public List<int> IdList { get; set; }

        /// <summary>
        /// آرایه ای از آیدی ها
        /// </summary>
        public List<int> Ids { get; set; }
    }
}

