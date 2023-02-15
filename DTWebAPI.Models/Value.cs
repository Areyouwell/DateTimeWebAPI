using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTWebAPI.Models
{
    [Table("Valuess")]
    public class Value
    {
        [Key]
        public int Id { get; set; }
        public DateTime FullDate { get; set; }
        public int TimeInSec { get; set; }
        public double Index { get; set; }
        public int FileId { get; set; }
        [ForeignKey("FileId")]
        public FileInf? FileInf { get; set; }
    }
}
