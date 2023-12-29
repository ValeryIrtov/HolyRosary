using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Animation;

namespace HolyRosary.Droid
{
    [Activity(Theme = "@style/Theme.Splash",
        MainLauncher = true,
        NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.SplashLayout);
            System.Threading.ThreadPool.QueueUserWorkItem(o => LoadActivity());
        }
        private void LoadActivity()
        {
            System.Threading.Thread.Sleep(1200); // Simulate a long pause    
            RunOnUiThread(() => StartActivity(typeof(MainActivity)));
        }
        public override void OnWindowFocusChanged(bool hasFocus)
        {
            ImageView imageView = FindViewById<ImageView>(Resource.Id.loadinganimation);

            Android.Graphics.Drawables.AnimationDrawable animation = (Android.Graphics.Drawables.AnimationDrawable)imageView.Drawable;

            animation.Start();
        }
    }

}