using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Techo_App.Droid;
using Xamarin.Forms;
using Firebase.Iid;

[assembly: Dependency(typeof(IFirebaseAndroidcs))]
namespace Techo_App.Droid
{
    public class IFirebaseAndroidcs : IFirebase
    {
        public string getFirebaseUserId()
        {
            //var instanceId = FirebaseInstanceId.Instance;
            //instanceId.DeleteInstanceId();
            String iid = FirebaseInstanceId.Instance.Token;
            ///return Resource.String.gcm_defaultSenderId.ToString();
            return iid;
        }
    }
}