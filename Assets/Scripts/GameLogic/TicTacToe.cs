using System.Collections.Generic;
using UI;
using UnityEngine.UI;

namespace GameLogic
{
    public class TicTacToe : MonoSingleton<TicTacToe>
    {
        private UIManager _uiManager;
        public List<CellButton> Cells = new List<CellButton>();

        public int FreeCells { get; set; }

        public string PlayerSign { get; } = "X";
        public string PlayerName { get; } = "Player";
        public string ComputerSign { get; } = "O";
        public string ComputerName { get; } = "Computer";

        public bool IsPlayerMove { get; set; }
        public bool IsGameFinished { get; set; } = true;
        public bool IsDraw { get; set; }

        private void Awake()
        {
            _uiManager = UIManager.Instance;
            FreeCells = Cells.Count;
        }

        private void Start()
        {
            _uiManager.ResetBoard();
        }

        public void CheckWinCondition(string sign, string winnerName)
        {
            // vertical case
            for (var i = 0; i <= 2; i++)
            {
                if (CompareCells(Cells[i].ButtonText, Cells[i + 3].ButtonText, Cells[i + 6].ButtonText, sign))
                {
                    _uiManager.ActivePopup(winnerName);
                    break;
                }
            }

            // horizontal case
            for (var i = 0; i <= 8; i += 3)
            {
                if (CompareCells(Cells[i].ButtonText, Cells[i + 1].ButtonText, Cells[i + 2].ButtonText, sign))
                {
                    _uiManager.ActivePopup(winnerName);
                    break;
                }
            }

            // cross case
            if (CompareCells(Cells[0].ButtonText, Cells[4].ButtonText, Cells[8].ButtonText, sign) ||
                CompareCells(Cells[2].ButtonText, Cells[4].ButtonText, Cells[6].ButtonText, sign))
            {
                _uiManager.ActivePopup(winnerName);
            }
            CheckDraw();
        }

        public bool CompareCells(Text fieldOne, Text fieldTwo, Text fieldThree, string sign)
        {
            return fieldOne.text.Contains(sign) && fieldTwo.text.Contains(sign) && fieldThree.text.Contains(sign);
        }

        private void CheckDraw()
        {
            if (FreeCells != 0)
            {
                return;
            }
            IsDraw = true;
            if (!IsGameFinished)
            {
                _uiManager.ActivePopup();
            }
        } 
    }
}