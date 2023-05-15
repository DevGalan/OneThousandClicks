using System;
using Util;

namespace Game
{
    public class TimeTrialGame : Game
    {
        private long _initialTouches;

        public TimeTrialGame(long totalTouches, double time, bool playable = true) : base(totalTouches, time, playable)
        {
            _initialTouches = totalTouches;
        }

        public override void CountTouch()
        {
            TotalTouches--;
        }

        public override void GameExecution()
        {
            if (TotalTouches <= 0) 
            {
                StopGame(false);
                TotalTouches = _initialTouches - TotalTouches;
            }
            Time += UnityEngine.Time.deltaTime;
        }

        public override double GetCPS()
        {
            return Math.Truncate(((_initialTouches - TotalTouches) / Time) * 1000) / 1000;
        }

        public override string GetGameModeName(bool withLineBreak)
        {
            if (!withLineBreak) return "Time Trial";
            return "Time\nTrial";
        }

        public override string GetMessage()
        {
            return $"Score: {_initialTouches - TotalTouches} clicks\nTime: {TimeFormatter.FormatTime(Time)}\nCPS: {GetCPS()}/s";
        }

        public override bool IsBetterThan(Game game)
        {
            return Time < game.Time;
        }
    }
}