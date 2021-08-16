using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Rebb.Deliveryman.Assets.CustomViews
{
    public class BitmapOutput : View
    {
        public Activity Activity { get; set; }
        public Bitmap Bitmap
        {
            get { return _bitmap; }
            set
            {
                _bitmap = value;
                if (Activity != null)
                {
                    Activity.RunOnUiThread(() =>
                    {
                        Invalidate();
                    });
                }
                else
                {
                    PostInvalidateDelayed(10);
                }
            }
        }
        Bitmap _bitmap;
        public BitmapOutput(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }
        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            Bitmap bitmap = Bitmap;
            Rect rect = new Rect(0, 0, Width, Height);
            Paint p = new Paint();
            p.SetStyle(Paint.Style.Fill);

            if (bitmap != null)
            {
                canvas.DrawBitmap(bitmap, 0, 0, p);
            }
        }
    }
}