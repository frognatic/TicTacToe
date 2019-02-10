using Data;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerStats : MonoBehaviour
    {
        public Text Name;
        public Text Stats;

        public int Wins { get; set; }
        public int Loses { get; set; }
        public int Draws { get; set; }

        public void UpdateWins()
        {
            Wins++;
            SetText();
        }

        public void UpdateLoses()
        {
            Loses++;
            SetText();
        }

        public void UpdateDraws()
        {
            Draws++;
            SetText();
        }

        public void SetText()
        {
            Stats.text = $"{Wins}-{Loses}-{Draws}";
            SaveData();
        }

        private void SaveData()
        {
            Save.SaveData(this);
        }
    }
}