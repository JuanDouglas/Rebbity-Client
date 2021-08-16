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

namespace Rebb.Deliveryman
{
    [Activity(Label = "RegisterBasicAccount", Theme = "@style/AppTheme.NoActionBar")]
    public class RegisterBasicActivity : AppCompatActivity
    {
        View buttonNext;
        TextInputLayout Email;
        TextInputLayout Password;
        TextInputLayout ConfirmPassword;
        TextInputLayout PhoneNumber;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_register_basic);

            buttonNext = FindViewById(Resource.Id.btnNext);
            buttonNext.Click += NextClick;
            // Create your application here
        }
        public void NextClick(object sender, EventArgs args)
        {
            new LoadingTaskFragment(Task.Run(() =>
            {
               
            }))
            {
                Message = "Conectando aos servidores"}.Show(SupportFragmentManager, LoadingTaskFragment.TAG);
        }

        private void ShowError(string name, string error)
        {
            TextInputLayout layout;
            switch (name.ToLowerInvariant())
            {
                default:
                    break;
            }
        }
    }
}