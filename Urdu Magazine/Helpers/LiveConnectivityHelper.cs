using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Urdu_Magazine.Helpers
{
    public class LiveConnectivityHelper
    {
        public void sendNotification(List<string> userGroups,int type,string who,string profilepic,string date,string link) {


            var liveConnectivity = GlobalHost.ConnectionManager.GetHubContext<LiveConnectivity>();
            liveConnectivity.Clients.Groups(userGroups).showNotification(who, type, (profilepic == null) ? "default" : profilepic, date.ToString(),link);

            /* string message = null;
            if (type == 1)
            {
                message = who + " " + Urdu_Magazine.Resources.Resource.CommentedNotificationText;
            }
            else if (type == 2)
            {
                message = who + " " + Urdu_Magazine.Resources.Resource.CallForArticleNotificationText;
            }

            else if (type == 3)
            {
                message = who + " " + Urdu_Magazine.Resources.Resource.PublishNotificationText;
            }
            else if (type == 4)
            {
                message = who + " " + Urdu_Magazine.Resources.Resource.SubmittedNotificationText;
            }
            else if (type == 5)
            {
                message = who + " " + Urdu_Magazine.Resources.Resource.AcceptedNotificationText;
            }
            */

        }
    }
}