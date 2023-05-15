using System;
using TMPro;

namespace Game
{
    public class TestCPS : GameMode
    {
        private double _initialTime;

        public TestCPS(TextMeshProUGUI pressedTimesText, TextMeshProUGUI timeText, long pressedTimes = 0, double time = 0) 
                : base(pressedTimesText, timeText, pressedTimes, time)
        {
            _initialTime = time;
        }

        public override void CountTouch()
        {
            PressedTimes++;
        }

        public override void GameExecution()
        {
            if (Time == 0) StopGame(false);
            Time -= UnityEngine.Time.deltaTime;
        }

        public override string GetModeName(bool divided)
        {
            if (!divided) return "Test CPS";
            return "Test\nCPS";
        }

        public override double GetCPS()
        {
            return Math.Truncate((PressedTimes / _initialTime) * 1000) / 1000;
        }

        public override string GetMessage()
        {
            return $"Score: {PressedTimes} clicks\nTime: {_initialTime} seconds\nCPS: {GetCPS()}/s";
        }
    }
}