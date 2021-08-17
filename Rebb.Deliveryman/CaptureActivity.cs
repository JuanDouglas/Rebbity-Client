using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Hardware.Camera2;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Rebb.Deliveryman.Assets.Callbacks;
using Rebb.Deliveryman.Assets.CustomViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rebb.Deliveryman
{
    [Activity(Label = "CaptureActivity", Theme = "@style/AppTheme.NoActionBar", ScreenOrientation = ScreenOrientation.Portrait)]
    public class CaptureActivity : AppCompatActivity
    {
        private const int RequestCameraCode = 007;
        string front;
        string back;
        BitmapOutput resultView;
        CameraDeviceControl control;
        CameraStateCallback callback;
        CameraManager manager;
        bool frontCamera;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_capture);

            DisplayMetrics metrics = new DisplayMetrics();
            WindowManager.DefaultDisplay.GetRealMetrics(metrics);

            control = new CameraDeviceControl();
            manager = (CameraManager)GetSystemService(CameraService);
            resultView = FindViewById<BitmapOutput>(Resource.Id.imgResultCapture);

            callback = new CameraStateCallback(resultView, control) { Width = metrics.HeightPixels, Height = metrics.WidthPixels };
           
            ImageView imgview = FindViewById<ImageView>(Resource.Id.imgSwitchCamera);
            imgview.Click += new EventHandler((object sender,EventArgs args)=> { SwitchCamera(); });
            if (CheckSelfPermission(Manifest.Permission.Camera) == Permission.Granted)
            {
                CameraIsPermited();
            }
            else
            {
                CameraNotPermited();
            }

            // Create your application here
        }
        private void CameraNotPermited()
        {
            RequestPermissions(new string[] { Manifest.Permission.Camera }, RequestCameraCode);
        }

        private void CameraIsPermited()
        {
            var ids = manager.GetCameraIdList();
            foreach (string id in ids)
            {
                var info = manager.GetCameraCharacteristics(id);
                var lensFacing = info.Get(CameraCharacteristics.LensFacing);
                LensFacing facing = (LensFacing)Convert.ToInt32(lensFacing.ToString());

                switch (facing)
                {
                    case LensFacing.Back:
                        back = id;
                        break;
                    case LensFacing.External:
                        break;
                    case LensFacing.Front:
                        front = id;
                        break;
                    default:
                        break;
                }
            }
            control.LensFacing = LensFacing.Front;

            manager.OpenCamera(front, callback, null);
        }
        public void SwitchCamera()
        {
            if (control.Device != null)
            {
                control.Device.Close();
            }

            if (frontCamera)
            {
                frontCamera = false;
                control.LensFacing = LensFacing.Back;
                manager.OpenCamera(back, callback, null);
            }
            else
            {
                frontCamera = true;
                control.LensFacing = LensFacing.Front;
                manager.OpenCamera(front, callback, null);
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            if (requestCode == RequestCameraCode)
            {
                for (int i = 0; i < permissions.Length; i++)
                {
                    if (permissions[i] == Manifest.Permission.Camera)
                    {
                        if (grantResults[i] == Permission.Granted)
                        {
                            CameraIsPermited();
                        }
                        else
                        {
                        }
                    }
                }
            }
        }



    }
}