using Android.App;
using Android.Content;
using Android.Content.PM;
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
using Rebb.Client.Core.Models;
using Rebb.Client.Core.Models.Result;
using Rebb.Deliveryman.Assets;
using Rebb.Deliveryman.Assets.Enums;
using Rebb.Deliveryman.Assets.Fragments;
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
        private ApiClient Client = Statics.ApiClient;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_splash_screen);
            ImageView imgLogo = FindViewById<ImageView>(Resource.Id.imgLogo);

            Task task = Task.Run(Background);
            if (task.Status == TaskStatus.Created)
                task.Start();
        }

        private async Task Background()
        {
            Bundle bundle = ActivityOptionsCompat.MakeCustomAnimation(this, Resource.Animation.abc_fade_in, Resource.Animation.abc_fade_out).ToBundle();
            Intent intent = new Intent(this, typeof(RegisterActivity));

            intent.PutExtra(RegisterActivity.RegisterStepKey, (int)RegisterStep.RegisterBasic);
            try
            {
                PackageInfo pInfo = PackageManager.GetPackageInfo(PackageName, 0);
                string version = pInfo.VersionName;

                Statics.ApiClient = new ApiClient(null, $"Android (Deliveryman/App {version}-{Build.VERSION.SdkInt})");
                ValidLoginResult? validation = await Client.LoginController.ValidLoginAsync(Statics.GetLogin(this));

                if (validation != null)
                {
                    if (!validation.ValidedAccount)
                    {
                        intent.PutExtra(RegisterActivity.RegisterStepKey, (int)RegisterStep.RegisterEmail);
                    }
                }
            }
            catch (Exception e)
            {
            }

            ActivityCompat.StartActivity(this, intent, bundle);
            Finish();
        }
    }
}