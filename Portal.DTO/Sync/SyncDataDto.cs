using System;

namespace Portal.DTO
{
    public class SyncDataDto
    {
        public TablesWinAppEnum Table { get; set; }
        public string SerializedData { get; set; }
        public OperationWinAppEnum Operation { get; set; }
        public string ServerId { get; set; }
        public long LocalId { get; set; }
    }
    
}
