using Android.App;
using Android.Content;
using Android.Hardware.Camera2;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rebb.Deliveryman.Assets.Callbacks
{
    class CaptureSessionCallback : CameraCaptureSession.StateCallback
    {
        public CameraDevice CameraDevice { get; set; }
        public Surface Surface { get; set; }
        public CaptureSessionCallback(CameraDevice device)
        {
            CameraDevice = device;
        }
        public override void OnConfigured(CameraCaptureSession session)
        {
            CaptureRequest.Builder builder = CameraDevice.CreateCaptureRequest(CameraTemplate.Preview);
            builder.AddTarget(Surface);
            session.SetRepeatingRequest(builder.Build(), null, null);
        }

        public override void OnConfigureFailed(CameraCaptureSession session)
        {

        }
    }
}