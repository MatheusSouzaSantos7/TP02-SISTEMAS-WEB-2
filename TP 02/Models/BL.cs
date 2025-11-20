using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TP_02.Models
{
    public class BL
    {
        [Key] // Define a chave primária
        public int ID { get; set; }

        [Required(ErrorMessage = "O número do BL é obrigatório.")]
        [StringLength(50, ErrorMessage = "O número do BL não pode ter mais de 50 caracteres.")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "O consignatário é obrigatório.")]
        [StringLength(255, ErrorMessage = "O consignatário não pode ter mais de 255 caracteres.")]
        public string Consignee { get; set; }

        [Required(ErrorMessage = "O navio é obrigatório.")]
        [StringLength(255, ErrorMessage = "O nome do navio não pode ter mais de 255 caracteres.")]
        public string Navio { get; set; }

        // Relacionamento 1:N com Containers
        public ICollection<Container> Containers { get; set; }
    }
}
