using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTWebAPI.Models
{
    [Table("Results")]
    public class Result
    {
        [Key]
        public int Id { get; set; }
        public int AllTime { get; set; }
        public DateTime MinDateTime { get; set; }
        public int AverTime { get; set; }
        public double AverIndex { get; set; }
        public double MedianIndex { get; set; }
        public double MaxIndex { get; set; }
        public double MinIndex { get; set; }
        public int CountString { get; set; }
        public int FileId { get; set; }
        [ForeignKey("FileId")]
        public FileInf? FileInf { get; set; }
    }
}
