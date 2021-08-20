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
using Rebb.Deliveryman.Assets.Enums;

namespace Rebb.Deliveryman
{
    [Activity(Label = "RegisterBasicAccount", Theme = "@style/AppTheme.NoActionBar")]
    public class RegisterActivity : AppCompatActivity
    {
        public const string RegisterStepKey = "StartRegisterStep";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_register);

            RegisterStep step = RegisterStep.RegisterBasic;
            if (Intent != null)
            {
                step = (RegisterStep)Intent.Extras.GetInt(RegisterStepKey,1);
            }
            ByStep(step);
        }


        public void ByStep(RegisterStep step)
        {
            AppCompatDialogFragment fragment = null;
            switch (step)
            {
                case RegisterStep.RegisterBasic:
                    fragment = new RegisterBasicFragment(this);
                    break;
                case RegisterStep.RegisterPassword:
                    fragment = new RegisterPasswordFragment(this, new AccountUpload());
                    break;
                case RegisterStep.RegisterEmail:
                    fragment = new ConfirmEmailFragment();
                    break;
                case RegisterStep.RegisterDocuments:
                    break;
                default:
                    fragment = new RegisterBasicFragment(this);
                    break;
            }
            SupportFragmentManager.BeginTransaction()
                .SetReorderingAllowed(true)
                .Add(Resource.Id.registerFragment, fragment, RegisterBasicFragment.TAG)
                .Commit();
        }
    }
}