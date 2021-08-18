using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.TextField;
using Rebb.Deliveryman.Assets.Fragments;
using Rebb.Client.Core;
using System;
using System.Threading;
using System.Threading.Tasks;
using Android.Text;
using Android.Text.Style;
using Android.Graphics;
using AndroidX.Core.App;
using Rebb.Client.Core.Models;
using Rebb.Client.Core.Models.Upload;

namespace Rebb.Deliveryman
{
    [Activity(Label = "RegisterBasicAccount", Theme = "@style/AppTheme.NoActionBar")]
    public class RegisterActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_register);

            SupportFragmentManager.BeginTransaction()
                .SetReorderingAllowed(true)
                .Add(Resource.Id.registerFragment, new RegisterBasicFragment(this), RegisterBasicFragment.TAG)
                .Commit();
           
        }
      
    }
}