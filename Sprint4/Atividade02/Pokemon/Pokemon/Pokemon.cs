using System.ComponentModel.DataAnnotations;
namespace Pokemon
{
    public class Pokemon
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string PokemonTipo { get; set; }
    }
}
