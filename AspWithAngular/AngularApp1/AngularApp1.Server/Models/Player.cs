using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularApp1.Server.Models
{
    public class Player
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nama pemain harus diisi.")]
        [StringLength(100, ErrorMessage = "Nama maksimal 100 karakter.")]
        public string Name { get; set; }

        [Range(1, 99, ErrorMessage = "Nomor harus antara 1 hingga 99.")]
        public int Number { get; set; }

        [Required(ErrorMessage = "Posisi harus diisi.")]
        [StringLength(50, ErrorMessage = "Posisi maksimal 50 karakter.")]
        public string Position { get; set; }
    }
}
