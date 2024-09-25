using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class Pokemon
    {
        [Key]
        public int Id { get; set; } // ID del Pokémon

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } // Nombre del Pokémon

        [Required]
        [Url]
        public string Imagenfrontal { get; set; } // Imagen frontal del Pokémon

        [Required]
        public List<string> Tipos { get; set; } // Lista de tipos del Pokémon (pueden ser más de uno)

        [Required]
        public int Altura { get; set; } // Altura del Pokémon en decímetros

        [Required]
        public int Peso { get; set; } // Peso del Pokémon en hectogramos

        public string Descripcion { get; set; } // Descripción o flavour text del Pokémon

        public Cadenadeevolucion Cadenaevolucion { get; set; } // Información de la cadena evolutiva
    }

    public class Cadenadeevolucion
    {
        public string PokemonBebe { get; set; } // Primera evolución
        public string PrimeraEvolucion { get; set; } // Segunda evolución
        public string SiguienteEvolucion { get; set; } // Evolución final
    }
}
