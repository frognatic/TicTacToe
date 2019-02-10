using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UI;
using UnityEngine;

namespace GameLogic
{
    public class AIMove : MonoSingleton<AIMove>
    {
        private TicTacToe _ticTacToe;
        private UIManager _uiManager;
        private bool _beingHandled;
        private float _computerTimeToMove = 2f;

        private void Start()
        {
            _ticTacToe = TicTacToe.Instance;
            _uiManager = UIManager.Instance;
        }

        private void Update()
        {
            if (_ticTacToe.IsPlayerMove && _ticTacToe.IsGameFinished)
            {
                return;
            }

            if (!_beingHandled)
            {
                StartCoroutine(WaitToComputerMove());
            }
        }

        private IEnumerator WaitToComputerMove()
        {
            _beingHandled = true;
            yield return new WaitForSeconds(_computerTimeToMove);
            ComputerMove(_ticTacToe.PlayerSign, _ticTacToe.ComputerSign);
            _beingHandled = false;
        }

        private void ComputerMove(string playerSign, string computerSign)
        {
            if (!_ticTacToe.IsPlayerMove)
            {
                CheckHorizontalMove(playerSign, computerSign);
                CheckVerticalMove(playerSign, computerSign);
                CheckCrossMove(playerSign, computerSign);
                RandomMove(computerSign);
                _ticTacToe.CheckWinCondition(_ticTacToe.ComputerSign, _ticTacToe.ComputerName);
            }
        }

        private void CheckHorizontalMove(string playerSign, string computerSign)
        {
            for (var i = 0; i <= 8; i += 3)
            {
                // check possibilities to win
                CheckPossibleWin(i, i + 1, i + 2, computerSign);
                CheckPossibleWin(i, i + 2, i + 1, computerSign);
                CheckPossibleWin(i + 1, i + 2, i, computerSign);
            
                // check possibilities to block player
                CheckBlockCase(i, i + 1, i + 2, playerSign, computerSign);
                CheckBlockCase(i, i + 2, i + 1, playerSign, computerSign);
                CheckBlockCase(i + 1, i + 2, i, playerSign, computerSign);
            }
        }

        private void CheckVerticalMove(string playerSign, string computerSign)
        {
            for (var i = 0; i <= 2; i++)
            {
                CheckPossibleWin(i, i + 3, i + 6, computerSign);
                CheckPossibleWin(i, i + 6, i + 3, computerSign);
                CheckPossibleWin(i + 3, i + 6, i, computerSign);

                CheckBlockCase(i, i + 3, i + 6, playerSign, computerSign);
                CheckBlockCase(i, i + 6, i + 3, playerSign, computerSign);
                CheckBlockCase(i + 3, i + 6, i, playerSign, computerSign);
            }
        }

        private void CheckCrossMove(string playerSign, string computerSign)
        {
            CheckPossibleWin(0, 4, 8, computerSign);
            CheckPossibleWin(2, 4, 6, computerSign);

            CheckBlockCase(0, 4, 8, playerSign, computerSign);
            CheckBlockCase(8, 4, 0, playerSign, computerSign);
            CheckBlockCase(8, 0, 4, playerSign, computerSign);

            CheckBlockCase(2, 4, 6, playerSign, computerSign);
            CheckBlockCase(2, 6, 4, playerSign, computerSign);
            CheckBlockCase(4, 6, 2, playerSign, computerSign);
        }

        private void CheckBlockCase(int fieldOne, int fieldTwo, int fieldThree, string playerSign, string computerSign)
        {
            if (_ticTacToe.Cells[fieldOne].ButtonText.text.Contains(playerSign) &&
                _ticTacToe.Cells[fieldTwo].ButtonText.text.Contains(playerSign) &&
                !_ticTacToe.Cells[fieldThree].ButtonText.text.Contains(computerSign) && !_ticTacToe.IsPlayerMove &&
                !_ticTacToe.IsGameFinished)
            {
                FillCell(fieldThree, computerSign);
            }
        }

        private void CheckPossibleWin(int fieldOne, int fieldTwo, int fieldThree, string computerSign)
        {
            if (_ticTacToe.Cells[fieldOne].ButtonText.text.Contains(computerSign) &&
                _ticTacToe.Cells[fieldTwo].ButtonText.text.Contains(computerSign) &&
                string.IsNullOrEmpty(_ticTacToe.Cells[fieldThree].ButtonText.text) && !_ticTacToe.IsPlayerMove &&
                !_ticTacToe.IsGameFinished)
            {
                FillCell(fieldThree, computerSign);
            }
        }

        private void FillCell(int fieldNumber, string computerSign)
        {
            if (!_ticTacToe.Cells[fieldNumber].CurrentButton.interactable)
            {
                return;
            }
            _ticTacToe.Cells[fieldNumber].ButtonText.text = computerSign;
            _ticTacToe.Cells[fieldNumber].CurrentButton.interactable = false;
            _ticTacToe.FreeCells--;
            _ticTacToe.IsPlayerMove = true;
            _uiManager.OnPlayerMoveChange(_ticTacToe.IsPlayerMove);
        }

        private void RandomMove(string sign)
        {
            if (!_ticTacToe.IsGameFinished && !_ticTacToe.IsPlayerMove && !_ticTacToe.IsDraw)
            {
                var tempList = new List<CellButton>();

                foreach (var cell in _ticTacToe.Cells)
                {
                    if (string.IsNullOrEmpty(cell.ButtonText.text))
                    {
                        tempList.Add(cell);
                    }
                }

                if (tempList.Count <= 0)
                {
                    return;
                }
                var randomCell = Random.Range(0, tempList.Count - 1);
                var id = int.Parse(Regex.Replace(tempList[randomCell].name, "[^0-9]", ""));
                FillCell(id, sign);
            }  
        }
    }
}