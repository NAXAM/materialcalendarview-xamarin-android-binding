using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;

namespace Demo
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button btnPick1;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            btnPick1 = FindViewById<Button>(Resource.Id.btnPick);

            btnPick1.Click += BtnPick1_Click;
        }

        private void BtnPick1_Click(object sender, System.EventArgs e)
        {
            CustomDatePicker datePicker = new CustomDatePicker(this);
            datePicker.Show();
        }
    }
}

