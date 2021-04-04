

// ReSharper disable once CheckNamespace
namespace Portal.DTO
{
    public static class ConstProperty
    {
        public class Covid19TestResultProperty
        {
            public static readonly string Negative = "Negative";
            public static readonly string Positive = "Positive";
        }

        public class TestResultProperty
        {
            public static readonly string Negative = "Negative";
            public static readonly string Positive = "Positive";
        }

        public enum PcrReportAgeRangeEnum
        {
           Until10 = 0,
           Between10Until20 = 10,
           Between20Until30 = 20,
           Between30Until40 = 30,
           Between40Until50 = 40,
           Between50Until60 = 50,
           Between60Until70 = 60,
           Between70Until80 = 70,
           Between80Until90 = 80,
           Between90Until100 =90,
           Unknown = -1,
        }

        public enum GeneralStatusEnum
        {
            One=1,Two=2,Three=3,Four=4,Five=5,Six=6
        }
    }
}
