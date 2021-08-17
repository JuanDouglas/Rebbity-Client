using Android.App;
using Android.Content;
using Android.Hardware;
using Android.Hardware.Camera2;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Core.App;
using Rebb.Client.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rebb.Deliveryman
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Theme = "@style/AppTheme.NoActionBar")]
    public class SplashScreen : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_splash_screen);
            ImageView imgLogo = FindViewById<ImageView>(Resource.Id.imgLogo);
            RotateAnimation animation = new RotateAnimation(0, 0.5f, 0, 0)
            {
                Interpolator = new BounceInterpolator(),
                Duration = (TimeSpan.TicksPerSecond / TimeSpan.TicksPerMillisecond) * 2
            };
            animation.AnimationEnd += AnimationEnd;
            imgLogo.StartAnimation(animation);
            animation.Start();
            Task task = Task.Run(Background);
            if (task.Status == TaskStatus.Created)
                task.Start();
        }
        private void AnimationEnd(object sender, EventArgs args)
        {
<<<<<<< HEAD
            Bundle bundle = ActivityOptionsCompat.MakeCustomAnimation(this, Resource.Animation.abc_fade_in, Resource.Animation.abc_fade_out).ToBundle();
            Intent intent = new Intent(this, typeof(RegisterBasicActivity));
            ActivityCompat.StartActivity(this, intent, bundle);
=======
            
>>>>>>> 2b06f69195ec21cd6e32eca1d8e0e9a7bda702b7
        }

        private async Task Background() 
        {
            ApiClient.Start(null,"Rebb DeliveryMan Android App");
            Bundle bundle = ActivityOptionsCompat.MakeCustomAnimation(this, Resource.Animation.abc_fade_in, Resource.Animation.abc_fade_out).ToBundle();
            Intent intent = new Intent(this, typeof(CaptureActivity));
            ActivityCompat.StartActivity(this, intent, bundle);
        }
    }
}