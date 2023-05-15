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
                TotalTouches = _initialTouches - TotalTouches;
                StopGame(false);
            }
            Time += UnityEngine.Time.deltaTime;
        }

        public override double GetCPS()
        {
            return Math.Truncate(((TotalTouches) / Time) * 1000) / 1000;
        }

        public override string GetGameModeName(bool withLineBreak)
        {
            if (!withLineBreak) return "Time Trial";
            return "Time\nTrial";
        }

        public override string GetMessage()
        {
            return $"Score: {TotalTouches} clicks\nTime: {TimeFormatter.FormatTime(Time)}\nCPS: {GetCPS()}/s";
        }

        public override bool IsBetterThan(Game game)
        {
            return Time < game.Time;
        }
    }
}