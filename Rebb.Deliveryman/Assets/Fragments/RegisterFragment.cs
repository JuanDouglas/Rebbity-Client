using Android.Animation;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Rebb.Client.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rebb.Deliveryman.Assets.Fragments
{
    public abstract class RegisterFragment : AppCompatDialogFragment
    {
        public AppCompatActivity CompatActivity { get; set; }
        public ApiClient Client { get { return Statics.ApiClient; } }

        public RegisterFragment(AppCompatActivity activity) : base()
        {
            CompatActivity = activity;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        protected internal virtual void AlterProgress(int progress)
        {
            ProgressBar progressBar = CompatActivity.FindViewById<ProgressBar>(Resource.Id.registerProgress);
            ObjectAnimator progressAnimator = ObjectAnimator.OfInt(progressBar, "progress", progressBar.Progress, progress);
            progressAnimator.SetDuration(500);
            progressAnimator.Start();
        }
    }
}