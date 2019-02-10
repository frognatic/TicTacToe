using UI;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic
{
    public class CellButton : MonoBehaviour
    {
        public Text ButtonText;
        public Button CurrentButton;
        private TicTacToe _ticTacToe;
        private UIManager _uiManager;

        private void Awake()
        {
            ButtonText = GetComponentInChildren<Text>();
            CurrentButton = GetComponent<Button>();
            CurrentButton.onClick.AddListener(SetPlayerMark);
        }

        private void Start()
        {
            _ticTacToe = TicTacToe.Instance;
            _uiManager = UIManager.Instance;
        }

        private void SetPlayerMark()
        {
            if (_ticTacToe.IsPlayerMove && !_ticTacToe.IsGameFinished)
            {
                ButtonText.text = _ticTacToe.PlayerSign;
                CurrentButton.interactable = false;
                _ticTacToe.CheckWinCondition(_ticTacToe.PlayerSign, _ticTacToe.PlayerName);
                _ticTacToe.FreeCells--;
                _ticTacToe.IsPlayerMove = false;
                _uiManager.OnPlayerMoveChange(_ticTacToe.IsPlayerMove);
            }
        }
    }
}