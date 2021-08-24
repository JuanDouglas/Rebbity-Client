using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Rebb.Client.Core;
using Rebb.Client.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rebb.App.Client.Assets
{
    internal static class Statics
    {
        public static ApiClient ApiClient { get; set; }
        public const string LoginPreferences = "LoginSharedPreferences";
        public const string AppName = "Rebbity Consumer"; 
        public static void SaveLogin(Context context, Login login, string email)
        {
            ISharedPreferences preferences = context.GetSharedPreferences(LoginPreferences, FileCreationMode.Private);
            ISharedPreferencesEditor editor = preferences.Edit();
            editor.PutString(Login.FirstStepKeyHeader, login.FirstStepKey);
            editor.PutString(Login.AuthenticationTokenHeader, login.AuthenticationToken);
            editor.PutString(Login.AccountKeyHeader, login.AccountKey);
            editor.PutString("email", email);
            editor.Commit();
        }

        public static Login? GetLogin(Context context)
        {
            ISharedPreferences preferences = context.GetSharedPreferences(LoginPreferences, FileCreationMode.Private);
            string accountKey = preferences.GetString(Login.AccountKeyHeader, null);
            string fsKey = preferences.GetString(Login.AuthenticationTokenHeader, null);
            string authToken = preferences.GetString(Login.FirstStepKeyHeader, null);

            if (accountKey == null && fsKey == null && authToken == null)
                return null;


            return new Login
            {
                AccountKey = accountKey,
                FirstStepKey = fsKey,
                AuthenticationToken = authToken
            };
        }
    }
}