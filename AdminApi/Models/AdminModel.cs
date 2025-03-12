using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AdminApi.Models
{
    [Table("admin")]
    public class AdminModel
    {
        [Key]
        public int NIC { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public byte[]? Password { get; set; }
    }
}
