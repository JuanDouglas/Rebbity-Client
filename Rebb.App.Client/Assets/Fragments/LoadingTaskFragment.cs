using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Rebb.App.Client.Assets.Fragments
{
    public class LoadingTaskFragment : AppCompatDialogFragment
    {
        public const string TAG = "LoadingTaskTag";
        public const int AnimationDuration = 250;
        public Task Task { get; set; }
        public ProgressBar LoadingProgressBar { get; set; }
        public TextView MessageTextView { get; set; }
        public ViewGroup MainLayout { get; set; }
        public ViewGroup CardLayout { get; set; }
        public View Background { get; set; }
        public bool Exitable { get; set; }

        public bool Loading { get; set; }

        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                if (Message == null)
                {
                    _message = string.Empty;
                }

                if (MessageTextView == null)
                    return;

                if (Message == string.Empty)
                {
                    MessageTextView.Visibility = ViewStates.Gone;
                }
                else
                {
                    if (MessageTextView.Visibility != ViewStates.Visible)
                    {
                        MessageTextView.Visibility = ViewStates.Visible;
                    }

                    MessageTextView.Text = _message;
                }
            }
        }
        private string _message;
        Stopwatch stopWatch;
        public LoadingTaskFragment(Task task) : base()
        {
            Task = task ?? throw new ArgumentNullException(nameof(task));
            stopWatch = new Stopwatch();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            Dialog.Window.SetBackgroundDrawable(new ColorDrawable(Color.Transparent));

            View view = inflater.Inflate(Resource.Layout.dialog_loading, container, false);
            LoadingProgressBar = view.FindViewById<ProgressBar>(Resource.Id.loadingProgressBar);
            MessageTextView = view.FindViewById<TextView>(Resource.Id.loadingMessage);
            MainLayout = view.FindViewById<ViewGroup>(Resource.Id.loadingMainLayout);
            CardLayout = view.FindViewById<ViewGroup>(Resource.Id.loadingCardLayout);
            Background = view.FindViewById<ViewGroup>(Resource.Id.loadingBackground);

            Loading = true;
            Message ??= string.Empty;
            MessageTextView.Text = Message;
            MainLayout.Click += MainLayoutClick;
            Task.ContinueWith((Task task) =>
            {
                TimeSpan minTime = TimeSpan.FromMilliseconds(AnimationDuration * 2);
                if (stopWatch.Elapsed < minTime)
                {
                    Thread.Sleep((int)(minTime.TotalMilliseconds - stopWatch.Elapsed.TotalMilliseconds));
                }

                Loading = false;
                Dismiss();
            });

            if (Task.Status == TaskStatus.Created)
                Task.Start();

            TranslateAnimation translateAnimation = new TranslateAnimation(0, 0, 350, 0) { Duration = AnimationDuration, Interpolator = new AccelerateDecelerateInterpolator() };
            AlphaAnimation alphaAnimation = new AlphaAnimation(0, 1) { Duration = AnimationDuration };

            CardLayout.StartAnimation(translateAnimation);
            Background.StartAnimation(alphaAnimation);

            translateAnimation.Start();
            alphaAnimation.Start();
            return view;
        }
        public override int Theme => Resource.Style.AlertDialog_AppCompat_Light;
        public void MainLayoutClick(object sender, EventArgs args)
        {
            if (Exitable)
            {
                Dismiss();
            }
        }
        public override void Dismiss()
        {
            try
            {
                TranslateAnimation translateAnimation = new TranslateAnimation(0, 0, 0, 350) { Duration = AnimationDuration, Interpolator = new AccelerateInterpolator() };
                AlphaAnimation alphaAnimation = new AlphaAnimation(1, 0) { Duration = AnimationDuration * 2 };

                alphaAnimation.AnimationEnd += (object sender, Animation.AnimationEndEventArgs args) => { base.Dismiss(); };

                Activity.RunOnUiThread(()=> {

                    CardLayout.StartAnimation(translateAnimation);
                    Background.StartAnimation(alphaAnimation);
                });

                translateAnimation.Start();
                alphaAnimation.Start();
            }
            catch (Exception e)
            {

                throw;
            }

        }
    }
}