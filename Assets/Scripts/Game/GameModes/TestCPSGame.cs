using System;

namespace Game
{
    public class TestCPSGame : Game
    {
        private double _initialTime;

        public TestCPSGame(long pressedTimes, double time, bool playable = true) : base(pressedTimes, time, playable)
        {
            _initialTime = time;
        }

        public override void GameExecution()
        {
            if (Time <= 0) 
            {
                StopGame(false);
                Time = _initialTime;
            }
            Time -= UnityEngine.Time.deltaTime;
        }

        public override string GetGameModeName(bool withLineBreak)
        {
            if (!withLineBreak) return "Test CPS";
            return "Test\nCPS";
        }

        public override double GetCPS()
        {
            return Math.Truncate((TotalTouches / _initialTime) * 1000) / 1000;
        }

        public override string GetMessage()
        {
            return $"Score: {TotalTouches} clicks\nTime: {_initialTime} seconds\nCPS: {GetCPS()}/s";
        }
    }
}