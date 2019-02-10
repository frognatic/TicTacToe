using Data;
using GameLogic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : MonoSingleton<UIManager>
    {
        public PlayerStats ComputerStats;
        public PlayerStats PlayerStats;

        public Button LoadDataButton;
        public Button RestartGameButton;
        public Button StartGameButton;
        public Button ExitGameButton;

        public Transform ResultPopup;
        public Text ResultPopupText;

        public Transform CurrentPlayerMove;
        public Text CurrentPlayerText;

        public Transform StartTextLabel;

        private TicTacToe _ticTacToe;

        private void Awake()
        {
            RestartGameButton.onClick.AddListener(ResetGame);
            LoadDataButton.onClick.AddListener(LoadScores);
            StartGameButton.onClick.AddListener(StartGame);
            ExitGameButton.onClick.AddListener(QuitGame);
            ExitGameButton.gameObject.SetActive(false);
            CurrentPlayerMove.gameObject.SetActive(false);
            StartTextLabel.gameObject.SetActive(true);
            _ticTacToe = TicTacToe.Instance;
        }

        public void OnPlayerMoveChange(bool isPlayerMove)
        {
            CurrentPlayerText.text = isPlayerMove ? string.Concat(_ticTacToe.PlayerName, "'s").ToUpper() : string.Concat(_ticTacToe.ComputerName, "'s").ToUpper();
        }

        private void StartGame()
        {
            _ticTacToe.IsGameFinished = false;
            StartGameButton.gameObject.SetActive(false);
            ExitGameButton.gameObject.SetActive(true);
            CurrentPlayerMove.gameObject.SetActive(true);
            StartTextLabel.gameObject.SetActive(false);
        }

        private void LoadScores()
        {
            LoadData(PlayerStats);
            LoadData(ComputerStats);
        }

        private void QuitGame()
        {
            Application.Quit();
        }

        public void ResetBoard()
        {
            foreach (var cell in _ticTacToe.Cells)
            {
                cell.CurrentButton.interactable = true;
                cell.ButtonText.text = "";
            }

            _ticTacToe.FreeCells = _ticTacToe.Cells.Count;
            _ticTacToe.IsPlayerMove = Random.value > 0.5f;
            OnPlayerMoveChange(_ticTacToe.IsPlayerMove);
            ResultPopup.gameObject.SetActive(false);
        }

        public void ResetGame()
        {
            ResetBoard();
            ResetParameters();
        }

        public void ResetParameters()
        {
            _ticTacToe.IsDraw = false;
            _ticTacToe.IsGameFinished = false;
            CurrentPlayerMove.gameObject.SetActive(true);
        }

        private void LoadData(PlayerStats player)
        {
            var data = Save.LoadPlayer(player);
            player.Wins = data.Wins;
            player.Draws = data.Draws;
            player.Loses = data.Loses;
            player.SetText();
        }

        public void ActivePopup(string winner = "")
        {
            ResultPopup.gameObject.SetActive(true);
            if (_ticTacToe.IsDraw)
            {
                ResultPopupText.text = "DRAW".ToUpper();
                ComputerStats.UpdateDraws();
                PlayerStats.UpdateDraws();
                _ticTacToe.IsDraw = false;
            }
            else
            {
                ResultPopupText.text = string.Concat(winner, " win").ToUpper();
                if (winner == _ticTacToe.PlayerName)
                {
                    PlayerStats.UpdateWins();
                    ComputerStats.UpdateLoses();
                }
                else
                {
                    ComputerStats.UpdateWins();
                    PlayerStats.UpdateLoses();
                }
            }

            _ticTacToe.IsGameFinished = true;
            CurrentPlayerMove.gameObject.SetActive(false);
        }
    }
}