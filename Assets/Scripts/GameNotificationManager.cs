using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class GameNotificationManager : MonoBehaviour
{
    // Start is called before the first frame update
  
    void Start()
    {



        if (Application.platform == RuntimePlatform.Android)
        {
            if (RunningManager.notified == false)
            {
                //Debug.Log("Sal");
                //Remove all notifications
                AndroidNotificationCenter.CancelAllDisplayedNotifications();


                //Create Notif Channel
                var channel = new AndroidNotificationChannel()
                {
                    Id = "channel_id",
                    Name = "Default Channel",
                    Importance = Importance.Default,
                    Description = "Reminder",
                };
                AndroidNotificationCenter.RegisterNotificationChannel(channel);


                //NOTIF
                var notification = new AndroidNotification();
                notification.Title = "Wiggle Worm";
                notification.Text = "Come on! Don't give up! Come and make a new highscore!";
                notification.FireTime = System.DateTime.Now.AddHours(6);

                
                notification.SmallIcon = "small1";
                notification.LargeIcon = "huge1";

                var id = AndroidNotificationCenter.SendNotification(notification, "channel_id");



                if (AndroidNotificationCenter.CheckScheduledNotificationStatus(id) == NotificationStatus.Scheduled)
                {
                    AndroidNotificationCenter.CancelAllNotifications();
                    AndroidNotificationCenter.SendNotification(notification, "channel_id");
                }


                RunningManager.notified = true;
            }
        }
        
    }

    //NO UPDATE FOR YOU!
    // Update is called once per frame
    /*void Update()
    {
        
    }*/
}
