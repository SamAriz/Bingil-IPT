namespace BingilAPI.Models
{
    public class DrawCardResponse
    {
        public bool success { get; set; }
        public List<PlayingCard> cards { get; set; } = new();
        public int remaining { get; set; }
    }

    public class PlayingCard
    {
        internal object cards;

        public string image { get; set; } = "";
        public string value { get; set; } = "";
        public string suit { get; set; } = "";
    }
}