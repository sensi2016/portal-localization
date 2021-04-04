using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Files
    {
        public Files()
        {
            FileTag = new HashSet<FileTag>();
        }

        public int Id { get; set; }
        public int FileGroupId { get; set; }
        public string RefferKey { get; set; }
        public string FileName { get; set; }
        public string TableName { get; set; }
        public string PrimeryKey { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual FileGroup FileGroup { get; set; }
        public virtual ICollection<FileTag> FileTag { get; set; }
    }
}
