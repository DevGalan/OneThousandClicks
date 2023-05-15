using System;
using UnityEngine;

namespace Game
{
    public class NotificationsManager : MonoBehaviour
    {
        private void Awake()
        {
            GleyNotifications.Initialize();
        }

        private void Start() 
        {
            int random = UnityEngine.Random.Range(0, 10);
            if (random < 9)
            {
                GleyNotifications.SendNotification("Lets try to beat your limits!", "Come on and hit a new record!",
                    new TimeSpan(3, 0, 0), "icon_logo_small", "icon_logo_large");
            }
            int diasAFK = UnityEngine.Random.Range(1, 4);
            GleyNotifications.SendNotification("Are you still alive?", "Stop making nothing and beat your limits like a BEAST!",
                    new TimeSpan(diasAFK * 24 - 1, 30, 0), "icon_logo_small", "icon_logo_large");
            diasAFK = UnityEngine.Random.Range(7, 15);
            GleyNotifications.SendNotification("Only losers will ignore this notification", "Prove to the world that you are not one of them and beat yout limits!",
                    new TimeSpan(diasAFK * 24 - 1, 30, 0), "icon_logo_small", "icon_logo_large");
        }
    }
}