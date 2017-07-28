using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ImageCircle.Forms.Plugin.Droid;
using Firebase.Iid;
using Firebase.Messaging;
using Android.Util;
using Android.Gms.Common;
using System.Threading.Tasks;

using Techo_App.Droid.AccountManagement;
namespace Techo_App.Droid
{
    [Activity(Label = "Techo", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        string algo = "asd";
        const string TAG = "MainActivity";
        protected override void OnCreate(Bundle bundle)
        {
            //SetContentView(Resource.Layout.Main);
            /*if (!GetString(Resource.String.google_app_id).Equals("1:241880227899:android:57aba120d0d3e2a6"))
                throw new System.Exception("Invalid Json file");
            Task.Run(() =>
            {
                var instanceId = FirebaseInstanceId.Instance;
                instanceId.DeleteInstanceId();
                Android.Util.Log.Debug("TAG", "{0} {1}", instanceId.Token,  instanceId.GetToken(GetString(Resource.String.gcm_defaultSenderId), Firebase.Messaging.FirebaseMessaging.InstanceIdScope));
            });*/
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            ImageCircleRenderer.Init();
            AccountManagementImplementation.Init();

            LoadApplication(new App());
            //LoadApplication(new App());var logTokenButton = FindViewById<Button>(Resource.Id.logTokenButton);
        }
    }
}

 