using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _gameModeText;
        [SerializeField]
        private TextMeshProUGUI _pressedTimesText;
        [SerializeField]
        private TextMeshProUGUI _timeText;
        public UnityEvent gameFinished;
        public UnityEvent increaseRateEvents;

        private GameMode _gameMode;
        private int _playingGameMode;

        private void Awake()
        {
            Application.targetFrameRate = 60;
        }

        private void Start()
        {
            PressButton.instance.buttonPressed += CountTouch;
        }

        private void Update() 
        {
            if (_gameMode == null) return;
            if (!_gameMode.InGame) return;
            _gameMode.GameExecution();
        }

        public void CountTouch()
        {
            if (_gameMode == null) return;
            if (_gameMode.Finished) return;
            if (!_gameMode.InGame) _gameMode.StartGame();
            _gameMode.CountTouch();
        }

        public void NewGame(int type)
        {
            _playingGameMode = type;
            _gameMode = CreateGameMode(type, true);
            _gameModeText.text = _gameMode.GetModeName(true);
            _gameMode.finishGame += FinishGame;
        }

        private GameMode CreateGameMode(int type, bool withTexts)
        {
            switch (type)
            {
                case 0:
                    if (withTexts) return new TimeTrial(_pressedTimesText, _timeText, 1000);
                    return new TimeTrial(null, null, 1000);
                case 1:
                    if (withTexts) return new TestCPS(_pressedTimesText, _timeText, 0, 15);
                    return new TestCPS(null, null, 0, 15);
                case 2:
                    if (withTexts) return new FreePlay(_pressedTimesText, _timeText);
                    return new FreePlay(null, null);
                default:
                    if (withTexts) return new FreePlay(_pressedTimesText, _timeText);
                    return new FreePlay(null, null);
            }
        }

        public void LeaveGame()
        {
            if (_gameMode == null) return;
            if (_gameMode.Finished) return;
            _gameMode.StopGame();
        }

        public void FinishGame(bool left)
        {
            if (left) 
            {
                if (!_gameMode.InGame) return;
                ModalWindowPanel.instance.NewWindow()
                    .SetVertical()
                    .SetTitle("Game abandoned")
                    .SetMessage("You have left the game")
                    .SetConfirmAction("Confirm", () => Debug.Log("Partida terminada"))
                    .OpenMenu();
                return;
            }
            if (_gameMode.PressedTimes == 0 && _gameMode.Time == 0) return;
            else
            {
                if (_playingGameMode != 2) gameFinished?.Invoke();
            }
            TopScores.PlayedGames++;
            double[] topScores = TopScores.GetTopScore(_playingGameMode);
            if (_gameMode.IsHigherThan((long) topScores[0], topScores[1]) && (topScores[0] != -1 && topScores[1] != -1))
            {
                if (AchievementsManager.instance.Connected)
                {
                    if (_playingGameMode == 1)
                    {
                        if (_gameMode.PressedTimes >= 100) AchievementsManager.instance.SubmitAchievement(0);
                        else if (_gameMode.PressedTimes >= 150) AchievementsManager.instance.SubmitAchievement(1);
                        else if (_gameMode.PressedTimes >= 200) AchievementsManager.instance.SubmitAchievement(2);
                        else if (_gameMode.PressedTimes >= 250) AchievementsManager.instance.SubmitAchievement(3);
                        AchievementsManager.instance.SubmitScoreToTestCPS(_gameMode.PressedTimes);
                    }
                    else if (_playingGameMode == 0)
                    {
                        AchievementsManager.instance.SubmitScoreToTimeTrial((long) _gameMode.Time * 100);
                    }
                }
                TopScores.RecordsBeated++;
                increaseRateEvents?.Invoke();
                TopScores.SetTopScore(_playingGameMode, new double[] {_gameMode.PressedTimes, _gameMode.Time});
                GameMode topScoreGameMode = CreateGameMode(_playingGameMode, false);
                topScoreGameMode.PressedTimes = (long) topScores[0];
                topScoreGameMode.Time = topScores[1];
                ModalWindowPanel.instance.NewWindow()
                    .SetVertical()
                    .SetTitle("Game finished with a\nNEW RECORD!")
                    .SetMessage(_gameMode.GetModeName(false) + "\n" + "Last record:\n" + topScoreGameMode.GetMessage() + "\nNew record:\n" + _gameMode.GetMessage())
                    .SetConfirmAction("Confirm", () => Debug.Log("Partida terminada"))
                    .OpenMenu();
                return;
            }
            ModalWindowPanel.instance.NewWindow()
                    .SetVertical()
                    .SetTitle("Game finished")
                    .SetMessage(_gameMode.GetModeName(false) + "\n" + _gameMode.GetMessage())
                    .SetConfirmAction("Confirm", () => Debug.Log("Partida terminada"))
                    .OpenMenu();
            if (topScores[0] == -1 || topScores[1] == -1) TopScores.SetTopScore(_playingGameMode, new double[] {_gameMode.PressedTimes, _gameMode.Time});
        }
    }
}