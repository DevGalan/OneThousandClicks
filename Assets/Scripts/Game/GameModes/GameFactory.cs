namespace Game
{
    public static class GameFactory
    {
        public static Game CreateGame(int gameMode)
        {
            switch (gameMode)
            {
                case 0: 
                    return new TimeTrialGame(1000, 0, true);
                case 1: 
                    return new TestCPSGame(0, 15, true);
                case 2: 
                    return new FreePlayGame(0, 0, true);
                default: 
                    return new TimeTrialGame(1000, 0, true);
            }
        }

        public static Game CreateGame(int gameMode, long initialTouches, double initialTime)
        {
            switch (gameMode)
            {
                case 0: 
                    return new TimeTrialGame(initialTouches, initialTime, false);
                case 1: 
                    return new TestCPSGame(initialTouches, initialTime, false);
                case 2: 
                    return new FreePlayGame(initialTouches, initialTime, false);
                default: 
                    return new TimeTrialGame(initialTouches, initialTime, false);
            }
        }
    }
}