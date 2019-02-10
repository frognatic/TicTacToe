using UI;

namespace Data
{
    [System.Serializable]
    public class PlayerData
    {
        public int Wins;
        public int Loses;
        public int Draws;

        public PlayerData(PlayerStats player)
        {
            Wins = player.Wins;
            Loses = player.Loses;
            Draws = player.Draws;
        }
    }
}
