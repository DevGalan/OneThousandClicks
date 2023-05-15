using System;
using TMPro;

namespace Game
{
    public class TimeTrial : GameMode
    {
        private long _initialPressedTime;

        public TimeTrial(TextMeshProUGUI pressedTimesText, TextMeshProUGUI timeText, long pressedTimes = 0, double time = 0) 
                : base(pressedTimesText, timeText, pressedTimes, time)
        {
            _initialPressedTime = pressedTimes;
        }

        public override void CountTouch()
        {
            PressedTimes--;
        }

        public override void GameExecution()
        {
            if (PressedTimes == 0) StopGame(false);
            Time += UnityEngine.Time.deltaTime;
        }

        public override double GetCPS()
        {
            return Math.Truncate(((_initialPressedTime - PressedTimes) / Time) * 1000) / 1000;
        }

        public override string GetModeName(bool divided)
        {
            if (!divided) return "Time Trial";
            return "Time\nTrial";
        }

        public override bool IsHigherThan(long score, double time)
        {
            return Time < time;
        }

        public override string GetMessage()
        {
            return $"Score: {_initialPressedTime - PressedTimes} clicks\nTime: {GetTime(Time)}\nCPS: {GetCPS()}/s";
        }
    }
}