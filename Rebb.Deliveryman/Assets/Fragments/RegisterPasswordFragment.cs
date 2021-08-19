using Android.Animation;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.TextField;
using Rebb.Client.Core;
using Rebb.Client.Core.Exceptions;
using Rebb.Client.Core.Models;
using Rebb.Client.Core.Models.Result;
using Rebb.Client.Core.Models.Upload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rebb.Deliveryman.Assets.Fragments
{
    public class RegisterPasswordFragment : AppCompatDialogFragment
    {
        public AccountUpload Account { get; set; }
        public AppCompatActivity CompatActivity { get; set; }
        public ApiClient Client { get { return Statics.ApiClient; } }
        public const string TAG = "RegisterPasswordFragment";

        TextInputLayout Password;
        TextInputLayout ConfirmPassword;
        View btnNext;
        public RegisterPasswordFragment(AppCompatActivity activity, AccountUpload account)
        {
            Account = account;
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
            View view = inflater.Inflate(Resource.Layout.content_register_password, container, false);
            btnNext = view.FindViewById(Resource.Id.btnNext);
            Password = view.FindViewById<TextInputLayout>(Resource.Id.TextInputPassword);
            ConfirmPassword = view.FindViewById<TextInputLayout>(Resource.Id.TextInputPassword);

            ProgressBar progressBar = CompatActivity.FindViewById<ProgressBar>(Resource.Id.registerProgress);
            ObjectAnimator progressAnimator = ObjectAnimator.OfInt(progressBar, "progress", 0, 2500);
            progressAnimator.SetDuration(500);
            progressAnimator.Start();

            btnNext.Click += NextClick;
            return view;
        }

        private void NextClick(object sender, EventArgs args)
        {
            Account.ConfirmPassword = ConfirmPassword.EditText.Text;
            Account.Password = Password.EditText.Text;

            try
            {
                Task task = SendAccount();
                string message = Resources.GetString(Resource.String.text_send_account);
                var loadingTaskFragment = new LoadingTaskFragment(task)
                {
                    Message = message
                };
                loadingTaskFragment.Show(CompatActivity.SupportFragmentManager, LoadingTaskFragment.TAG);
            }
            catch (Exception e)
            {

            }
        }

        public async Task SendAccount()
        {
            try
            {
            AccountResult result = await Client.AccountController.CreateAccount(Account);

            }
            catch (ValidationErrorsException e)
            {
                bool isActualPage = true;
                foreach (var item in e.Errors.Errors)
                {
                    if (
                        item.Key.ToLowerInvariant() != "ConfirmPassword".ToLowerInvariant() &&
                        item.Key.ToLowerInvariant() != "Password".ToLowerInvariant()) {
                        isActualPage = false;
                        break;
                    }
                        
                }

                if (!isActualPage)
                {

                }
            }
            catch (Exception e)
            {
                Log.Error("SendMessageErrorMessage", e.Message);
                Log.Error("SendMessageErrorStackTrace", e.StackTrace);
            }
        }
    }
}