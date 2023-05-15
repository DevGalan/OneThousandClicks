namespace Game
{
    public abstract class Game
    {
        public delegate void FinishGameDelegate(bool left);
        private FinishGameDelegate _finishGame;

        private long _totalTouches;
        private double _time;
        
        private bool _playable;
        private bool _inGame;
        private bool _finished;

        protected Game(long pressedTimes, double time, bool playable = true)
        {
            _totalTouches = pressedTimes;
            _time = time;
            _playable = playable;
        }

        public FinishGameDelegate FinishGame { get => _finishGame; set => _finishGame = value; }
        public long TotalTouches { get => _totalTouches; set => _totalTouches = value; }
        public double Time { get => _time; set => _time = value; }
        public bool Playable { get => _playable; set => _playable = value; }
        public bool InGame { get => _inGame; set => _inGame = value; }
        public bool Finished { get => _finished; set => _finished = value; }

        public void StartGame()
        {
            if (!_playable || _finished || _inGame) return;
            _inGame = true;
        }

        public virtual void StopGame()
        {
            StopGame(true);
        }

        protected void StopGame(bool left)
        {
            if (!_inGame) return;
            _finishGame?.Invoke(left);
            _inGame = false;
            _finished = true;
        }

        public abstract void GameExecution();
        public abstract string GetGameModeName(bool withLineBreak);
        public abstract string GetMessage();
        public abstract double GetCPS();
        public virtual void CountTouch()
        {
            _totalTouches++;
        }
        public virtual bool IsBetterThan(Game game)
        {
            return TotalTouches > game._totalTouches;
        }
    }
}