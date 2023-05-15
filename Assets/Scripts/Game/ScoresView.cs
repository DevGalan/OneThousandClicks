using UnityEngine;
using TMPro;

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
            GameMode _gameMode;
            if (mode == 0) _gameMode = new TimeTrial(null, null, (long) values[0], values[1]);
            else if (mode == 1) _gameMode = new TestCPS(null, null, (long) values[0], 15);
            else _gameMode = new FreePlay(null, null, (long) values[0], values[1]);
            _gameModeText.text = _gameMode.GetModeName(true) + "\nRecord";
            if (values[0] == -1 || values[1] == -1) 
            {
                _pressedTimesText.transform.parent.parent.gameObject.SetActive(true);
                _pressedTimesText.text = "Not played yet";
                _timeText.transform.parent.parent.gameObject.SetActive(false);
                _cPSText.transform.parent.parent.gameObject.SetActive(false);
                return;
            }
            _pressedTimesText.text = _gameMode.PressedTimes.ToString() + " clicks";
            _timeText.text = _gameMode.GetTime(values[1]) + " millis";
            _cPSText.text = _gameMode.GetCPS().ToString() + " clicks/s";
            _pressedTimesText.transform.parent.parent.gameObject.SetActive(mode != 0);
            _timeText.transform.parent.parent.gameObject.SetActive(mode !=  1);
            _cPSText.transform.parent.parent.gameObject.SetActive(true);
        }
    }
}