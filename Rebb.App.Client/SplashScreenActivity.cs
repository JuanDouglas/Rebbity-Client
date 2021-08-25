using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Core.App;
using Rebb.App.Client.Assets;
using Rebb.App.Client.Assets.Enums;
using Rebb.Client.Core;
using Rebb.Client.Core.Models.Result;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rebb.App.Client
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Theme = "@style/AppTheme.NoActionBar")]
    public class SplashScreenActivity : AppCompatActivity
    {
        public ApiClient Client { get { return Statics.ApiClient; } }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.splash_screen);

            Task task = Task.Run(Background);
            if (task.Status == TaskStatus.Created)
                task.Start();
        }

        public async Task Background()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Bundle bundle = ActivityOptionsCompat.MakeCustomAnimation(this, Resource.Animation.abc_fade_in, Resource.Animation.abc_fade_out).ToBundle();
            Intent intent = new Intent(this, typeof(StartActivity));

            Statics.ApiClient = new ApiClient(Statics.GetLogin(this), this, Statics.AppName);
            ValidLoginResult? validation = await Client.LoginController.ValidLoginAsync(Client.Login);

            if (validation != null)
            {
                if (validation.ValidLogin)
                {
                    intent = new Intent(this, typeof(MainActivity));
                    if (!validation.ValidedAccount)
                    {
                        intent = new Intent(this, typeof(RegisterActivity));
                        intent.PutExtra(RegisterActivity.RegisterStepKey, (int)RegisterStep.RegisterEmail);
                    }
                }
            }
            stopWatch.Stop();

            TimeSpan minTime = TimeSpan.FromMilliseconds(400);
            if (stopWatch.Elapsed < minTime)
            {
                Thread.Sleep((int)(minTime.TotalMilliseconds - stopWatch.Elapsed.TotalMilliseconds));
            }

            ActivityCompat.StartActivity(this, intent, bundle);
            Finish();
        }
    }
}