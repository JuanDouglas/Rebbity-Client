using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Core.App;
using System;

namespace Rebb.App.Client
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class StartActivity : AppCompatActivity
    {
        View btnRegister;
        View btnLogin;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_start);
            btnRegister = FindViewById(Resource.Id.btnStartRegister);
            btnRegister.Click += RegisterClick;

            btnLogin = FindViewById(Resource.Id.btnStartLogin);
            btnLogin.Click += LoginClick;
            // Create your application here
        }
        private void RegisterClick(object sender, EventArgs args)
        {
            Bundle bundle = ActivityOptionsCompat.MakeCustomAnimation(this, Resource.Animation.abc_fade_in, Resource.Animation.abc_fade_out).ToBundle();
            Intent intent = new Intent(this, typeof(RegisterActivity));
            ActivityCompat.StartActivity(this, intent, bundle);
        }
        private void LoginClick(object sender, EventArgs args) {
            Bundle bundle = ActivityOptionsCompat.MakeCustomAnimation(this, Resource.Animation.abc_fade_in, Resource.Animation.abc_fade_out).ToBundle();
            Intent intent = new Intent(this, typeof(LoginActivity));
            ActivityCompat.StartActivity(this, intent, bundle);
        }
    }
}