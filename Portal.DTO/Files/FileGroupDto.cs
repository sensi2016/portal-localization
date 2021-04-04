namespace Portal.DTO
{
    public class FileGroupDto
    {
        public long Id { get; set; }
        public int ParentId { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public int? FileCount { get; set; }
        public int LevelNo { get; set; }
        public string Code { get; set; }
        

    }
}
