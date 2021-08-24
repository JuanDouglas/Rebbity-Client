using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rebb.App.Client.Assets.Enums
{
    public enum RegisterStep : uint
    {
        RegisterBasic = 1,
        RegisterPassword = 2,
        RegisterEmail = 3,
        RegisterDocuments = 4
    }
}