using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Techo_App.iOS;

[assembly: Xamarin.Forms.Dependency(typeof (ISoundIosImplementation))]
namespace Techo_App.iOS
{
    class ISoundIosImplementation : ISound
    {
        public ISoundIosImplementation() { }
        public void KeyboardClick()
        {
            UIDevice.CurrentDevice.PlayInputClick();
        }
    }
}