namespace DTWebAPI.Models
{
    public class ValueSimply
    {
        public ValueSimply()
        {

        }
        public ValueSimply(Value row)
        {
            FullDate = row.FullDate;
            TimeInSec = row.TimeInSec;
            Index = row.Index;
        }
        public DateTime FullDate { get; set; }
        public int TimeInSec { get; set; }
        public double Index { get; set; }
    }
}
