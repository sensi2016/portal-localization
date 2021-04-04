namespace Portal.DTO
{
    public class DrugWinAppDto : BaseWinAppDto
    {
        public string Name { get; set; }
        public string Details { get; set; }
        public string Notes { get; set; }
        public string Meal { get; set; }
        public int StartFrequency { get; set; }
        public int EndFrequency { get; set; }
        public int DurartionMonth { get; set; }
        public int DurartionDay { get; set; }
        public string PrescriptionId { get; set; }
    }

}
