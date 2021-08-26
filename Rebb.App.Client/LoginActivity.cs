using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using AndroidX.AppCompat.App;
using AndroidX.Core.App;
using Google.Android.Material.TextField;
using Rebb.App.Client.Assets;
using Rebb.App.Client.Assets.Enums;
using Rebb.App.Client.Assets.Fragments;
using Rebb.Client.Core;
using Rebb.Client.Core.Exceptions;
using Rebb.Client.Core.Models;
using Rebb.Client.Core.Models.Result;
using System;
using System.Threading.Tasks;

namespace Rebb.App.Client
{
    [Activity(Label = "LoginActivity", Theme = "@style/AppTheme.NoActionBar", HardwareAccelerated = true)]
    public class LoginActivity : AppCompatActivity
    {
        TextInputLayout User;
        TextInputLayout Pwd;
        View btnLogin;
        public ApiClient Client { get { return Statics.ApiClient; } }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_login);

            User = FindViewById<TextInputLayout>(Resource.Id.TextInputEmail);
            Pwd = FindViewById<TextInputLayout>(Resource.Id.TextInputPassword);
            btnLogin = FindViewById(Resource.Id.btnLogin);
            btnLogin.Click += LoginClick;
            // Create your application here
        }

        public void LoginClick(object sender, EventArgs args)
        {
            string message = Resources.GetString(Resource.String.task_login);
            User.EditText.Text ??= string.Empty;
            Pwd.EditText.Text ??= string.Empty;

            LoadingTaskFragment fragment = new LoadingTaskFragment(new Task<Task>(LoginAsync))
            {
                Message = message,
            };
            fragment.Show(SupportFragmentManager, LoadingTaskFragment.TAG);
        }

        public async Task LoginAsync()
        {
            Bundle bundle = ActivityOptionsCompat.MakeCustomAnimation(this, Resource.Animation.abc_fade_in, Resource.Animation.abc_slide_out_top).ToBundle();
            Intent intent = new Intent(this, typeof(MainActivity));
            try
            {
                FirstStepResult fs = await Client.LoginController.FirstStepAsync(User.EditText.Text);
                AuthenticationResult auth = await Client.LoginController.SecondStepAsync(User.EditText.Text, fs.Key, fs.ID);
                Login login = new Login()
                {
                    AccountKey = auth.AccountKey,
                    FirstStepKey = fs.Key,
                    AuthenticationToken = auth.Token
                };

                Statics.SaveLogin(this, login, User.EditText.Text);
                Statics.ApiClient.Login = login;

                if (!auth.ValidedAccount)
                {
                    intent = new Intent(this, typeof(RegisterActivity));
                    intent.PutExtra(RegisterActivity.RegisterStepKey, (int)RegisterStep.RegisterEmail);
                    ActivityCompat.StartActivity(this, intent, bundle);
                }
            }
            catch (LoginException e)
            {
                string field = e.FieldName.ToLowerInvariant();
                if (field == nameof(Pwd).ToLowerInvariant())
                {
                    Pwd.Error = Resources.GetString(Resource.String.error_invalid_password);
                    Pwd.ErrorEnabled = true;
                }

                if (field == nameof(User).ToLowerInvariant())
                {
                    User.Error = Resources.GetString(Resource.String.error_invalid_login);
                    User.ErrorEnabled = true;
                }
            }
            catch (Exception e)
            { 
            
            }
        }
    }
}