namespace BingilAPI.Models
{
    public class PokemonResult
    {
        public string name { get; set; } = "";
        public int height { get; set; }
        public int weight { get; set; }
        public PokemonSprites sprites { get; set; } = new();
        public List<PokemonTypeSlot> types { get; set; } = new();
        public List<PokemonStatSlot> stats { get; set; } = new();
    }

    public class PokemonSprites
    {
        public string front_default { get; set; } = "";
    }

    public class PokemonTypeSlot
    {
        public PokemonType type { get; set; } = new();
    }

    public class PokemonType
    {
        public string name { get; set; } = "";
    }

    public class PokemonStatSlot
    {
        public int base_stat { get; set; }
        public PokemonStat stat { get; set; } = new();
    }

    public class PokemonStat
    {
        public string name { get; set; } = "";
    }
}
