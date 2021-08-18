﻿using Android.App;
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
using Rebb.Client.Core.Models;
using Rebb.Client.Core.Models.Result;
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

            Task task = Task.Run(Background);
            if (task.Status == TaskStatus.Created)
                task.Start();
        }

        private async Task Background()
        {
            try
            {
                ApiClient client = new ApiClient(null, "Rebb DeliveryMan Android App");
                //ValidLoginResult? validation = await client.LoginController.ValidLoginAsync(new Login()
                //{
                //    FirstStepKey = string.Empty,
                //    AccountKey = string.Empty,
                //    AuthenticationToken = string.Empty
                //});
            }
            catch (Exception e)
            {
            }
            Bundle bundle = ActivityOptionsCompat.MakeCustomAnimation(this, Resource.Animation.abc_fade_in, Resource.Animation.abc_fade_out).ToBundle();
            Intent intent = new Intent(this, typeof(RegisterActivity));
            Finish();
            ActivityCompat.StartActivity(this, intent, bundle);
        }
    }
}