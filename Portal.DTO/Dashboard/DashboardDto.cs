using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO
{
    public class DashboardDto
    {
        public int TotalDay { get; set; }
        public int TotalMonth { get; set; }
        public int TotalYear { get; set; }
        public int TotalPositive { get; set; }
        public int TotalNegative { get; set; }
        public int TotalNotAnswered { get; set; }

        public List<DashboardChartDto> Chart { get; set; }
    }

    public class DashboardChartDto
    {
        public string Title { get; set; }
        public int TotalMen { get; set; }
        public int TotalWomen { get; set; }
        public int TotalMenPostive { get; set; }
        public int TotalMenNegative { get; set; }
        public int TotalWomenPostive { get; set; }
        public int TotalWomenNegative { get; set; }
    }

}
