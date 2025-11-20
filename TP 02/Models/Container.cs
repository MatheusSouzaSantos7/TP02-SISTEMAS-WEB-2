using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TP_02.Models;

namespace TP_02.Models
{
    public class Container
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "O número do container é obrigatório.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O número do container deve ter exatamente 11 caracteres.")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "O tipo do container é obrigatório.")]
        [RegularExpression("Dry|Reefer", ErrorMessage = "O tipo deve ser 'Dry' ou 'Reefer'.")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "O tamanho do container é obrigatório.")]
        [Range(20, 40, ErrorMessage = "O tamanho do container deve ser 20 ou 40.")]
        public int Tamanho { get; set; }

        // Chave estrangeira obrigatória
        [Required(ErrorMessage = "O BL associado é obrigatório.")]
        [ForeignKey("BL")]
        public int BL_ID { get; set; }

        // Navegação para BL
        public BL BL { get; set; }
    }
}
