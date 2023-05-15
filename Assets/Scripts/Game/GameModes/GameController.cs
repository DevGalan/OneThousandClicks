using UnityEngine;
using TMPro;
using UnityEngine.Events;
using Util;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _gameModeText;
        [SerializeField]
        private TextMeshProUGUI _totalTouchesText;
        [SerializeField]
        private TextMeshProUGUI _timeText;

        public UnityEvent gameFinished;
        public UnityEvent increaseRateEvents;

        private Game _game;
        private int _playingGameMode;

        private void Start()
        {
            PressButton.instance.buttonPressed += CountTouch;
        }

        private void Update() 
        {
            if (_game == null) return;
            if (!_game.InGame) return;
            _game.GameExecution();
            _timeText.text = TimeFormatter.FormatTime(_game.Time);
        }

        public void CountTouch()
        {
            if (_game == null) return;
            if (_game.Finished) return;
            if (!_game.InGame) _game.StartGame();
            _game.CountTouch();
            _totalTouchesText.text = _game.TotalTouches.ToString();
        }

        public void NewGame(int gameMode)
        {
            _playingGameMode = gameMode;
            _game = GameFactory.CreateGame(gameMode);
            _gameModeText.text = _game.GetGameModeName(true);
            _totalTouchesText.text = _game.TotalTouches.ToString();
            _timeText.text = TimeFormatter.FormatTime(_game.Time);
            _game.FinishGame += FinishGame;
        }

        public void LeaveGame()
        {
            if (_game == null) return;
            if (_game.Finished) return;
            _game.StopGame();
        }

        public void FinishGame(bool left)
        {
            if (left) 
            {
                ModalWindowPanel.instance.NewWindow()
                    .SetVertical()
                    .SetTitle("Game abandoned")
                    .SetMessage("You have left the game")
                    .SetConfirmAction("Confirm", () => Debug.Log("Partida terminada"))
                    .OpenMenu();
                return;
            }

            if (_playingGameMode != 2) gameFinished?.Invoke();
            TopScores.PlayedGames++;
            double[] topScores = TopScores.GetTopScore(_playingGameMode);
            Game recordGame = GameFactory.CreateGame(_playingGameMode, (long) topScores[0], topScores[1]);

            if (_game.IsBetterThan(recordGame) && (topScores[0] != -1 && topScores[1] != -1))
            {
                TopScores.RecordsBeated++;
                increaseRateEvents?.Invoke();
                TopScores.SetTopScore(_playingGameMode, new double[] {_game.TotalTouches, _game.Time});
                Debug.Log(_game.TotalTouches);
                Debug.Log(_game.Time);
                Debug.Log("/----/");
                Debug.Log(TopScores.GetTopScore(_playingGameMode)[0]);
                Debug.Log(TopScores.GetTopScore(_playingGameMode)[1]);
                ModalWindowPanel.instance.NewWindow()
                    .SetVertical()
                    .SetTitle("Game finished with a\nNEW RECORD!")
                    .SetMessage(_game.GetGameModeName(false) + "\n" + "Last record:\n" + recordGame.GetMessage() + "\nNew record:\n" + _game.GetMessage())
                    .SetConfirmAction("Confirm", () => Debug.Log("Partida terminada"))
                    .OpenMenu();
                return;
            }

            ModalWindowPanel.instance.NewWindow()
                    .SetVertical()
                    .SetTitle("Game finished")
                    .SetMessage(_game.GetGameModeName(false) + "\n" + _game.GetMessage())
                    .SetConfirmAction("Confirm", () => Debug.Log("Partida terminada"))
                    .OpenMenu();
                if (topScores[0] == -1 || topScores[1] == -1) TopScores.SetTopScore(_playingGameMode, new double[] {_game.TotalTouches, _game.Time});
        }
    }
}