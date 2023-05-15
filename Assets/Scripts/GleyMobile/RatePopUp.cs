using UnityEngine;

public class RatePopUp : MonoBehaviour
    {
        public static RatePopUp instance;

        private void Awake()
        {
            if (instance == null) instance = this;
        }

        public void ShowPopUp()
        {
            RateGame.Instance.ShowRatePopup();
        }

        public void IncreaseCustomEvents()
        {
            RateGame.Instance.IncreaseCustomEvents();
        }
    }