using TMPro;
using UnityEngine;

namespace Game
{
    public abstract class GameMode
    {
        public delegate void FinishGame(bool left);
        public FinishGame finishGame;
        private TextMeshProUGUI _pressedTimesText;
        private TextMeshProUGUI _timeText;
        private long _pressedTimes;
        private double _time;
        private bool _inGame;
        private bool _finished;

        protected GameMode(TextMeshProUGUI pressedTimesText, TextMeshProUGUI timeText, long pressedTimes = 0, double time = 0)
        {
            _pressedTimesText = pressedTimesText;
            _timeText = timeText;
            PressedTimes = pressedTimes;
            Time = time;
            _inGame = false;
        }

        public long PressedTimes 
        { 
            get => _pressedTimes; 
            set 
            {
                _pressedTimes = value;
                if (_pressedTimes < 0) _pressedTimes = 0;
                if (_pressedTimesText == null) return;
                _pressedTimesText.text = _pressedTimes.ToString();
            }
        }

        public double Time 
        { 
            get => _time; 
            set 
            {
                _time = value;
                if (_time < 0) _time = 0;
                if (_timeText == null) return;
                _timeText.text = GetTime(_time);
            }
        }

        public bool InGame { get => _inGame; }
        public bool Finished { get => _finished; }

        public void StartGame()
        {
            _inGame = true;
        }

        public virtual void StopGame()
        {
            StopGame(true);
        }

        public string GetTime(double time)
        {
            int millis = (int) (time % 1 * 100);
            int secs = (int) time % 60;
            int mins = (int) time / 60;
            int hours = (int) time / 3600;
            return (hours <= 9 ? "0" : "") + hours + ":" + (mins <= 9 ? "0" : "") + mins + ":"
                        + (secs <= 9 ? "0" : "") + secs + ":" + (millis <= 9 ? "0" : "") + millis;
        }

        protected void StopGame(bool left)
        {
            finishGame?.Invoke(left);
            _finished = true;
            _inGame = false;
        }

        public abstract string GetModeName(bool divided);

        public abstract void CountTouch();

        public abstract void GameExecution();

        public abstract string GetMessage();

        public virtual bool IsHigherThan(long score, double time)
        {
            return PressedTimes > score;
        }

        public abstract double GetCPS();
    }
}