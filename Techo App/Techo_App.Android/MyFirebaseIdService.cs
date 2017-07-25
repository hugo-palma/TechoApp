﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Iid;

namespace Techo_App.Droid
{
    [Service]
    [IntentFilter(new [] { "com.google.firebase.INSTANCE_ID_EVENT"})]
    class MyFirebaseIdService :FirebaseInstanceIdService
    {
        public override void OnTokenRefresh()
        {
            base.OnTokenRefresh();
            var instanceId = FirebaseInstanceId.Instance;
            var regid = FirebaseInstanceId.Instance.Token;
            if (regid == null)
            {
                regid = instanceId.GetToken(GetString(Resource.String.gcm_defaultSenderId), Firebase.Messaging.FirebaseMessaging.InstanceIdScope);

                Android.Util.Log.Debug("Refreshed Token:",regid);
            }
            else
            {
                Android.Util.Log.Debug("Refreshed Token:", FirebaseInstanceId.Instance.Token);
            }
        }
    }
}