using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Core.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rebb.App.Client
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class StartActivity : AppCompatActivity
    {
        View btnRegister;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_start);
            btnRegister = FindViewById(Resource.Id.btnStartRegister);
            btnRegister.Click += RegisterClick;
            // Create your application here
        }
        private void RegisterClick(object sender, EventArgs args)
        {
            Bundle bundle = ActivityOptionsCompat.MakeCustomAnimation(this, Resource.Animation.abc_fade_in, Resource.Animation.abc_fade_out).ToBundle();
            Intent intent = new Intent(this, typeof(RegisterActivity));
            ActivityCompat.StartActivity(this, intent, bundle);
        }
    }
}