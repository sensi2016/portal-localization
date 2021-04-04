// ReSharper disable once CheckNamespace
namespace Portal.DTO
{
    public interface IPaging
    {
        int PageSize { get; set; }
        int PageNumber { get; set; }
    }
}
