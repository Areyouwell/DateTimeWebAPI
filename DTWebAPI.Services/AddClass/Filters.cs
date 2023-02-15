namespace DTWebAPI.Services.AddClass
{
    public class Filters
    {
        public string? FileName { get; set; } = "";
        public DateTime FromMinDateTime { get; set; } = DateTime.MaxValue;
        public DateTime ToMinDateTime { get; set; } = DateTime.MaxValue;
        public double MinAverIndex { get; set; } = 0;
        public double MaxAverIndex { get; set; } = 0;
        public int MinAverTime { get; set; } = 0;
        public int MaxAverTime { get; set; } = 0;
    }
}
