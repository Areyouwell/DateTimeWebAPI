namespace DTWebAPI.Services.AddClass
{
    public class IntermediateResult
    {
        public int MinTime { get; set; } = int.MaxValue;
        public int MaxTime { get; set; } = 0;
        public DateTime MinDateTime { get; set; } = DateTime.Now;
        public long AllTime { get; set; } = 0;
        public double AllIndex { get; set; } = 0;
        public List<double> ListForMedian { get; set; } = new List<double>();
        public double MedianIndex { get; set; } = 0;
        public double MaxIndex { get; set; } = 0;
        public double MinIndex { get; set; } = double.MaxValue;
        public int CountString { get; set; } = 0;
    }
}
