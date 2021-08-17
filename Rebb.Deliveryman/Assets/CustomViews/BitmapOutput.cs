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

namespace Rebb.Deliveryman.Assets.CustomViews
{
    public class BitmapOutput : View
    {
        public Bitmap Bitmap
        {
            get { return _bitmap; }
            set
            {
                _bitmap = value;
                Invalidate();
            }
        }
        Bitmap _bitmap;
        public BitmapOutput(Context context, IAttributeSet attrs) : base(context, attrs)
        {

        }
        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);
            Paint p = new Paint();
            if (_bitmap != null)
            {
                canvas.DrawBitmap(_bitmap, 0, 0, p);
            }
        }
        protected override void OnDraw(Canvas canvas)
        {
            Paint p = new Paint();
            if (_bitmap != null)
            {
                canvas.DrawBitmap(_bitmap, 0, 0, p);
            }
        }
    }
}