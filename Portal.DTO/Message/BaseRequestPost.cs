using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

// ReSharper disable once CheckNamespace
namespace Portal.DTO
{
    public class BaseRequestPost<T> : BaseRequestPaging
    {
        /// <summary>
        /// آیدی رکورد انتخابی برای انجام عملیات الزامی است
        /// </summary>
        public T Id { get; set; }
    }

    public class BaseListRequestPost<T> : BaseRequestPaging
    {
        public List<T> List { get; set; }

    }

    public class BaseRequestPagingPost : BaseRequestPaging
    {
        /// <summary>
        /// تعداد رکورد نمایش در هر صفحه الزامی است
        /// </summary>
        public override int PageSize { get; set; }

        /// <summary>
        /// شماره صفحه الزامی است
        /// </summary>
        public override int PageNumber { get; set; }
    }

    public abstract class BaseRequest<T> : BaseRequestPaging
    {
        public virtual T Id { get; set; }

        private int _pageSize;
        public override int PageSize { get => _pageSize == 0 ? 10 : _pageSize; set => _pageSize = value; }

        public override int PageNumber { get; set; }
    }


    public abstract class BaseRequestPaging : IPaging
    {
        /// <summary>
        /// تعداد رکورد نمایش در هر صفحه
        /// </summary>
        public virtual int PageSize { get; set; }

        /// <summary>
        /// شماره صفحه
        /// </summary>
        public virtual int PageNumber { get; set; }
    }


}

