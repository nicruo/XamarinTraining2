using Foundation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UIKit;

namespace XamarinTraining2.iOS
{
    public class HomeViewController : UIViewController
    {
        UIButton button;
        private int counter = 0;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.White;

            button = new UIButton(UIButtonType.System);
            button.Frame = new CoreGraphics.CGRect(0, 50, 400, 200);
            button.SetTitle("hello world", UIControlState.Normal);
            View.Add(button);

            button.SetTitle("Olá mundo!", UIControlState.Normal);
            button.TouchUpInside += Button_Click;
        }

        private async void Button_Click(object sender, System.EventArgs e)
        {
            ShowToast(this, "button clicked");
            await Task.Delay(1000);
            button.SetTitle(counter++ + " volta(s)", UIControlState.Normal);

            Character result = await Task.Run(async () =>
            {
                HttpClient httpClient = new HttpClient();

                HttpResponseMessage resultMessage = await httpClient.GetAsync("https://rickandmortyapi.com/api/character/1");

                string resultContent = await resultMessage.Content.ReadAsStringAsync();

                Character character = JsonConvert.DeserializeObject<Character>(resultContent);

                return character;
            });

            button.SetTitle("Result = " + result, UIControlState.Normal);
        }

        private async void ShowToast(UIViewController controller, string message)
        {
            UIAlertController alertController = new UIAlertController();
            alertController.Message = message;
            controller.PresentViewController(alertController, true, null);
            await Task.Delay(2000);
            alertController.DismissViewController(true, null);
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