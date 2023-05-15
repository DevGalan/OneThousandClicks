using System;
using TMPro;

namespace Game
{
    public class FreePlay : GameMode
    {
        public FreePlay(TextMeshProUGUI pressedTimesText, TextMeshProUGUI timeText, long pressedTimes = 0, double time = 0) 
                : base(pressedTimesText, timeText, pressedTimes, time)
        {
        }

        public override void CountTouch()
        {
            PressedTimes++;
        }

        public override void StopGame()
        {
            StopGame(false);
        }

        public override void GameExecution()
        {
            if (PressedTimes == 99999999) StopGame(false);
            Time += UnityEngine.Time.deltaTime;
        }

        public override string GetModeName(bool divided)
        {
            if (!divided) return "Free Play";
            return "Free\nPlay";
        }

        public override double GetCPS()
        {
            return Math.Truncate((PressedTimes / Time) * 1000) / 1000;
        }

        public override string GetMessage()
        {
            return $"Score: {PressedTimes} clicks\nTime: {GetTime(Time)}\nCPS: {GetCPS()}/s";
        }
    }
}