using Android.Content;
using Android.Content.PM;
using Android.OS;
using Rebb.Client.Core.Controllers;
using Rebb.Client.Core.Controllers.Base;
using Rebb.Client.Core.Models;

namespace Rebb.Client.Core
{
#nullable disable
    /// <summary>
    /// Classe do cliente da API.
    /// </summary>
    public class ApiClient
    {
        public string UserAgent { get { return ApiController.UserAgent; } set { ApiController.UserAgent = value; } }
        public Login Login { get { return LoguedController.Login; } set { LoguedController.Login = value; } }
        public LoginController LoginController { get; set; }
        public AccountController AccountController { get; set; }
        public DeliverymanController DeliveryManController { get; set; }
#nullable disable
#nullable enable
        public ApiClient(Login? login, string userAgent)
#nullable disable
        {
            Login = login;
            UserAgent = userAgent;
            AccountController = new AccountController();
            LoginController = new LoginController();
            DeliveryManController = new DeliverymanController();
        }
        public ApiClient(Login? login, Context context, string appName) : this(login, string.Empty)
        {
            PackageInfo pInfo = context.PackageManager.GetPackageInfo(context.PackageName, 0);
            string version = pInfo.VersionName;

            UserAgent = $"Android ({appName} {version}-{Build.VERSION.SdkInt})";
        }
    }
}
