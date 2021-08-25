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

namespace Rebb.App.Client.Assets.Fragments
{
    public class RegisterPasswordFragment : RegisterFragment
    {
        public AccountUpload Account { get; set; }
        public const string TAG = "RegisterPasswordFragment";

        TextInputLayout Password;
        TextInputLayout ConfirmPassword;
        View btnNext;
        bool loading;
        public RegisterPasswordFragment(AppCompatActivity activity, AccountUpload account) : base(activity)
        {
            Account = account;
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
            View view = inflater.Inflate(Resource.Layout.fragment_register_password, container, false);
            btnNext = view.FindViewById(Resource.Id.btnNext);
            Password = view.FindViewById<TextInputLayout>(Resource.Id.TextInputPassword);
            ConfirmPassword = view.FindViewById<TextInputLayout>(Resource.Id.TextInputPassword);

            string emp = string.Empty;
            if (Account != null)
            {
                Password.EditText.Text = Account.Password ?? emp;
                ConfirmPassword.EditText.Text = Account.ConfirmPassword ?? emp;
            }

            AlterProgress(2500);
            btnNext.Click += NextClick;
            return view;
        }

        private void NextClick(object sender, EventArgs args)
        {
            if (loading)
                return;

            Account.ConfirmPassword = ConfirmPassword.EditText.Text;
            Account.Password = Password.EditText.Text;

            try
            {
                string message = Resources.GetString(Resource.String.task_send_account);
                var loadingTaskFragment = new LoadingTaskFragment(new Task<Task>(SendAccount))
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
            loading = true;
            try
            {
                AccountResult result = await Client.AccountController.CreateAccount(Account);
                Client.Login = await Client.LoginController.LoginAsync(Account.Email, Account.Password);
                await Client.AccountController.SendEmailConfirmation(Client.Login);

                ConfirmEmailFragment fragment = new ConfirmEmailFragment(CompatActivity, Account.Email);
                CompatActivity.SupportFragmentManager.BeginTransaction()
                              .SetReorderingAllowed(true)
                              .SetCustomAnimations(Resource.Animation.abc_slide_in_top, Resource.Animation.abc_fade_out)
                              .Replace(Resource.Id.registerFragment, fragment, RegisterBasicFragment.TAG)
                              .Commit();

            }
            catch (ValidationErrorsException e)
            {
                bool isActualPage = true;
                foreach (var item in e.Errors.Errors)
                {
                    if (
                        item.Key.ToLowerInvariant() != nameof(ConfirmPassword).ToLowerInvariant() &&
                        item.Key.ToLowerInvariant() != nameof(Password).ToLowerInvariant())
                    {
                        isActualPage = false;
                        break;
                    }

                }

                if (!isActualPage)
                {
                    RegisterBasicFragment fragment = new RegisterBasicFragment(e.Errors, CompatActivity, Account);
                    CompatActivity.SupportFragmentManager.BeginTransaction()
                                  .SetReorderingAllowed(true)
                                  .SetCustomAnimations(Resource.Animation.abc_slide_in_top, Resource.Animation.abc_fade_out)
                                  .Replace(Resource.Id.registerFragment, fragment, RegisterBasicFragment.TAG)
                                  .Commit();
                }
                else
                {
                    ShowErrors(e.Errors);
                }

            }
            catch (Exception e)
            {
                Log.Error("SendMessageErrorMessage", e.Message);
                Log.Error("SendMessageErrorStackTrace", e.StackTrace);
            }
            finally
            {
                loading = false;
            }
        }

        public void ShowErrors(ValidationErrorsResult validationErrorsResult)
        {
            var errors = validationErrorsResult.Errors;
            foreach (var keyUper in errors.Keys)
            {
                TextInputLayout input = null;
                string key = keyUper.ToLowerInvariant();
                if (key == nameof(Password).ToLowerInvariant())
                    input = Password;

                if (key == nameof(ConfirmPassword).ToLowerInvariant())
                    input = ConfirmPassword;

                if (input != null)
                {
                    string[] inputErrors = errors[keyUper];
                    CompatActivity.RunOnUiThread(() =>
                    {
                        input.Error = inputErrors[0];
                        input.ErrorEnabled = true;
                    });
                }
            }
        }
    }
}