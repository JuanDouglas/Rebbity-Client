using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Rebb.Client.Core.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rebb.Client.Core.Controllers
{
    class NotificationsController : ApiController
    {
        protected internal override string DefaultHost => base.DefaultHost + "/Notifications";
    }
}