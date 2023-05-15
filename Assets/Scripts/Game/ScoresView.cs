using UnityEngine;
using TMPro;
using Util;

namespace Game
{
    public class ScoresView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _gameModeText;
        [SerializeField]
        private TextMeshProUGUI _pressedTimesText;
        [SerializeField]
        private TextMeshProUGUI _timeText;
        [SerializeField]
        private TextMeshProUGUI _cPSText;

        public void SetScore(int mode)
        {
            double[] values = TopScores.GetTopScore(mode);
            Game _game = GameFactory.CreateGame(mode, (long) values[0], values[1]);
            _gameModeText.text = _game.GetGameModeName(true) + "\nRecord";
            if (values[0] == -1 || values[1] == -1) 
            {
                _pressedTimesText.transform.parent.parent.gameObject.SetActive(true);
                _pressedTimesText.text = "Not played yet";
                _timeText.transform.parent.parent.gameObject.SetActive(false);
                _cPSText.transform.parent.parent.gameObject.SetActive(false);
                return;
            }
            _pressedTimesText.text = _game.TotalTouches.ToString() + " clicks";
            _timeText.text = TimeFormatter.FormatTime(values[1]) + " millis";
            _cPSText.text = _game.GetCPS().ToString() + " clicks/s";
            _pressedTimesText.transform.parent.parent.gameObject.SetActive(mode != 0);
            _timeText.transform.parent.parent.gameObject.SetActive(mode !=  1);
            _cPSText.transform.parent.parent.gameObject.SetActive(true);
        }
    }
}