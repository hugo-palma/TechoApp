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

[assembly: Xamarin.Forms.Dependency (typeof (ISoundImplementation))]
namespace Techo_App.Droid
{
    public class ISoundImplementation : ISound
    {
        public ISoundImplementation() { }
        public View root;
        public void KeyboardClick()
        {
            root = ((Activity)Xamarin.Forms.Forms.Context).Window.DecorView;
            if (root != null)
            {
                root.PlaySoundEffect(SoundEffects.Click);
            }
        }
    }
}