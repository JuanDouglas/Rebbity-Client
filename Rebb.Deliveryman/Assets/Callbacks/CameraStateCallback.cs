using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Hardware.Camera2;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Rebb.Deliveryman.Assets.CustomViews;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rebb.Deliveryman.Assets.Callbacks
{
    public class CameraStateCallback : CameraDevice.StateCallback, ImageReader.IOnImageAvailableListener
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public CameraDeviceControl Control { get; set; }
        BitmapOutput resultView;
        public CameraStateCallback(BitmapOutput target, in CameraDeviceControl control)
        {
            resultView = target ?? throw new NullReferenceException(nameof(target));
            Control = control ?? throw new NullReferenceException(nameof(control));
        }
        public override void OnDisconnected(CameraDevice camera)
        {
           
        }

        public override void OnError(CameraDevice camera, [GeneratedEnum] CameraError error)
        {
           
        }

        public void OnImageAvailable(ImageReader reader)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Image image = reader.AcquireNextImage();
            var buffer = image.GetPlanes()[0].Buffer;

            buffer.Rewind();
            int capacity = buffer.Capacity();
            byte[] imageBytes = new byte[capacity];
            buffer.Get(imageBytes);

            using MemoryStream ms = new MemoryStream(imageBytes);
            Bitmap bmp = BitmapFactory.DecodeStream(ms);
            ms.Flush();
            ms.Close();

            using MemoryStream compressStream = new MemoryStream();
            bmp.Compress(Bitmap.CompressFormat.Jpeg, 0, compressStream);

            compressStream.Flush();
            compressStream.Close();

            stopwatch.Stop();
            Log.Debug("ImageRenderTime", stopwatch.Elapsed.ToString());
            stopwatch.Reset();

            stopwatch.Start();


            if (Control.LensFacing == LensFacing.Front)
            {
                RotateBitmap(bmp, 270);
            }
            else if (Control.LensFacing == LensFacing.Back)
            {
                RotateBitmap(bmp, 90);
            }

            resultView.Bitmap = bmp;
            resultView.Invalidate();
            stopwatch.Stop();

            Log.Debug("ImageSetTime", stopwatch.Elapsed.ToString());
            if (image != null)
            {
                image.Close();
            }
        }

        public override void OnOpened(CameraDevice camera)
        {
            var reader = ImageReader.NewInstance(Height, Width, ImageFormatType.Jpeg, 50);
            reader.SetOnImageAvailableListener(this, null);
            CaptureSessionCallback captureSessionCallback = new CaptureSessionCallback(camera) { Surface = reader.Surface };

            Control.Device = camera;
            camera.CreateCaptureSession(new List<Surface>() { reader.Surface }, captureSessionCallback, null);
        }

        public static Bitmap RotateBitmap(Bitmap source, float angle)
        {
            Matrix matrix = new Matrix();
            matrix.PostRotate(angle);
            return Bitmap.CreateBitmap(source, 0, 0, source.Width, source.Height, matrix, true);
        }
    }
    public class CameraDeviceControl {
        public CameraDevice Device { get; set; }
        public CameraCharacteristics Characteristics { get; set; }
        public LensFacing LensFacing { get; set; }
    }
}