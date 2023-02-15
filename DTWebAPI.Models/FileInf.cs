using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTWebAPI.Models
{
    [Table("tblFileInf")]
    public class FileInf
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Value> Values { get; set; } = new();
        public Result? Result { get; set; }
    }
}
