using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.TextField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rebb.App.Client.Assets.Fragments
{
    public class ConfirmEmailFragment : RegisterFragment
    {
        TextInputLayout ConfirmationCode;
        TextView sendAgainText;
        View btnNext;
        string email;
        public ConfirmEmailFragment(AppCompatActivity activity, string email) : base(activity)
        {
            this.email = email ?? throw new NullReferenceException(nameof(email));
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
            View view = inflater.Inflate(Resource.Layout.fragment_confirm_email, container, false);
            ConfirmationCode = view.FindViewById<TextInputLayout>(Resource.Id.TextInputConfirmationCode);
            sendAgainText = view.FindViewById<TextView>(Resource.Id.txtSendAgain);
            btnNext = view.FindViewById(Resource.Id.btnNext);

            sendAgainText.Click += SendAgainClick;
            btnNext.Click += NextClick;
            AlterProgress(5000);
            return view;
        }

        public void NextClick(object sender, EventArgs args)
        {
            LoadingTaskFragment loadTask = new LoadingTaskFragment(new Task<Task>(NextStepAsync));
            loadTask.Show(CompatActivity.SupportFragmentManager, LoadingTaskFragment.TAG);
        }

        public async Task NextStepAsync()
        {
            string text = ConfirmationCode.EditText.Text;
            bool confirmed = await Client.AccountController.ConfirmEmail(Convert.ToInt32(text), email, Client.Login);
            if (!confirmed)
            {
                return;
            }
        }

        public void SendAgainClick(object sender, EventArgs args)
        {
            LoadingTaskFragment loadTask = new LoadingTaskFragment(new Task<Task>(SendConfirmationAsync));
            loadTask.Show(CompatActivity.SupportFragmentManager, LoadingTaskFragment.TAG);
        }

        public async Task SendConfirmationAsync()
        {
            await Client.AccountController.SendEmailConfirmation(Client.Login);
        }
    }
}