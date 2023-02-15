namespace DTWebAPI.Models
{
    public class ResultSimply
    {
        public ResultSimply()
        {

        }
        public ResultSimply(Result row)
        {
            FileName = row.FileInf.Name;
            AllTime = row.AllTime;
            MinDateTime = row.MinDateTime;
            AverTime = row.AverTime;
            AverIndex = row.AverIndex;
            MedianIndex = row.MedianIndex;
            MaxIndex = row.MaxIndex;
            MinIndex = row.MinIndex;
            CountString = row.CountString;
        }

        public string? FileName { get; set; }
        public int AllTime { get; set; }
        public DateTime MinDateTime { get; set; }
        public int AverTime { get; set; }
        public double AverIndex { get; set; }
        public double MedianIndex { get; set; }
        public double MaxIndex { get; set; }
        public double MinIndex { get; set; }
        public int CountString { get; set; }
    }
}
