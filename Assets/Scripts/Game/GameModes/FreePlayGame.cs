using System;
using Util;

namespace Game
{
    public class FreePlayGame : Game
    {
        public FreePlayGame(long pressedTimes, double time, bool playable = true) : base(pressedTimes, time, playable)
        {
        }

        public override void StopGame()
        {
            if (TotalTouches == 0 && Time == 0) StopGame(true);
            StopGame(false);
        }

        public override void GameExecution()
        {
            if (TotalTouches >= 99999999) StopGame(false);
            Time += UnityEngine.Time.deltaTime;
        }

        public override string GetGameModeName(bool divided)
        {
            if (!divided) return "Free Play";
            return "Free\nPlay";
        }

        public override string GetMessage()
        {
            return $"Score: {TotalTouches} clicks\nTime: {TimeFormatter.FormatTime(Time)}\nCPS: {GetCPS()}/s";
        }
        
        public override double GetCPS()
        {
            return Math.Truncate((TotalTouches / Time) * 1000) / 1000;
        }
    }
}