using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.TextField;
using Rebb.Deliveryman.Assets.Fragments;
using Rebb.Client.Core;
using System;
using System.Threading;
using System.Threading.Tasks;
using Android.Text;
using Android.Text.Style;
using Android.Graphics;
using AndroidX.Core.App;

namespace Rebb.Deliveryman
{
    [Activity(Label = "RegisterBasicAccount", Theme = "@style/AppTheme.NoActionBar")]
    public class RegisterBasicActivity : AppCompatActivity
    {
        View buttonNext;
        TextView txvCondicoesTerm;
        TextInputLayout Email;
        TextInputLayout Password;
        TextInputLayout ConfirmPassword;
        TextInputLayout PhoneNumber;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_register_basic);

            buttonNext = FindViewById(Resource.Id.btnNext);
            buttonNext.Click += NextClick;

            txvCondicoesTerm = (TextView)FindViewById(Resource.Id.txvTermo);
            string textmain = "Li e concordo com os Termos e Condições de Uso. Os termos estarão disponivel para consulta dentro do app.";
            SpannableString ss = new SpannableString(textmain);
            ForegroundColorSpan fcsBlue = new ForegroundColorSpan(Color.Red);
            ss.SetSpan(fcsBlue, 21, 46, SpanTypes.User);
            txvCondicoesTerm.SetText(ss, TextView.BufferType.Normal);
            // Create your application here
        }
        public void NextClick(object sender, EventArgs args)
        {
<<<<<<< HEAD
            Bundle bundle = ActivityOptionsCompat.MakeCustomAnimation(this, Resource.Animation.abc_fade_in, Resource.Animation.abc_fade_out).ToBundle();
            Intent intent = new Intent(this, typeof(RegisterPasswordActivity));
            ActivityCompat.StartActivity(this, intent, bundle);
            //new LoadingTaskFragment(Task.Run(() =>
            //{

            //}))
            //{ Message = "Conectando aos servidores" }.Show(SupportFragmentManager, LoadingTaskFragment.TAG);
=======
            new LoadingTaskFragment(Task.Run(() =>
            {
               
            }))
            {
                Message = "Conectando aos servidores"}.Show(SupportFragmentManager, LoadingTaskFragment.TAG);
>>>>>>> 2b06f69195ec21cd6e32eca1d8e0e9a7bda702b7
        }

        private void ShowError(string name, string error)
        {
            TextInputLayout layout;
            switch (name.ToLowerInvariant())
            {
                default:
                    break;
            }
        }
    }
}