using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class AchievementsManager : MonoBehaviour
    {
        public static AchievementsManager instance;

        private bool _connected;

        public UnityEvent<int> submitAchievement;
        public UnityEvent<long> submitScoreToTimeTrial;
        public UnityEvent<long> submitScoreToTestCPS;

        public bool Connected { get => _connected; set => _connected = value; }

        private void Awake() 
        {
            if (instance == null) instance = this;
        }

        public void SubmitAchievement(int index)
        {
            submitAchievement?.Invoke(index);
        }

        public void SubmitScoreToTestCPS(long clicks)
        {
            submitScoreToTestCPS?.Invoke(clicks);
        }

        public void SubmitScoreToTimeTrial(long time)
        {
            submitScoreToTimeTrial?.Invoke(time);
        }
    }
}