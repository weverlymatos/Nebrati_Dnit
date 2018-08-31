using System;
using System.Collections.Generic;
using System.Linq;
using Dnit.Core;
using FFImageLoading.Forms.Platform;
using Foundation;
using UIKit;

namespace Dnit.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            SlideOverKit.iOS.SlideOverKit.Init();
            CachedImageRenderer.Init();

            return base.FinishedLaunching(app, options);
        }
    }
}
