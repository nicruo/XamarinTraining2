using Android.App;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace XamarinTraining2.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Button button;
        private int counter = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            button = FindViewById<Button>(Resource.Id.button);
            button.Text = "Olá mundo!";
            button.Click += Button_Click;
        }

        private async void Button_Click(object sender, System.EventArgs e)
        {
            Toast.MakeText(this, "button clicked", ToastLength.Long).Show();
            await Task.Delay(1000);
            button.Text = counter++ + " volta(s)";

            Character result = await Task.Run(async () =>
            {
                HttpClient httpClient = new HttpClient();
                
                HttpResponseMessage resultMessage = await httpClient.GetAsync("https://rickandmortyapi.com/api/character/1");
                
                string resultContent = await resultMessage.Content.ReadAsStringAsync();

                Character character = JsonConvert.DeserializeObject<Character>(resultContent);

                return character;
            });

            button.Text = "Result = " + result;
        }
    }

    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }

        public override string ToString()
        {
            return $"Id: {Id} Name: {Name} Status: {Status}";
        }
    }
}