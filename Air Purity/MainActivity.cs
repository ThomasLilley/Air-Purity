using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Provider;

using System.Json;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace Air_Purity
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);


            Button ShowJson = FindViewById<Button>(Resource.Id.btn1);

            ShowJson.Click += (object sender, EventArgs e) =>
            {
                string toast = string.Format("Getting Data");
                Toast.MakeText(this, toast, ToastLength.Long).Show();
                GetJson();
            };
        }

            protected async void GetJson()
        {
            string url = "http://35.234.88.236/index.php/sensors/getsensordata?sensorid=1";
            JsonValue json = await FetchJsonAsync(url);
            ParseAndDisplay(json);
        }

        private async Task<JsonValue> FetchJsonAsync(string url)
        {
            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";

            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                    Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

                    // Return the JSON document:
                    return jsonDoc;
                }
            }
        }

        private void ParseAndDisplay(JsonValue json)
        {
            TextView txt1 = FindViewById<TextView>(Resource.Id.txt1);
            TextView txt2 = FindViewById<TextView>(Resource.Id.txt2);
            TextView txt3 = FindViewById<TextView>(Resource.Id.txt3);
            TextView txt4 = FindViewById<TextView>(Resource.Id.txt4);

            foreach (JsonValue obj in json)
            {
                txt1.Text = txt1.Text + "\n" + obj["time_stamp"];
                txt2.Text = txt2.Text + "\n" + obj["sensor_id"];
                txt3.Text = txt3.Text + "\n" + obj["sensor_type"];
                txt4.Text = txt4.Text + "\n" + obj["value"];
                /*if (count < maxSize)
                {
                    valueArray[count] = obj["value"];
                }
                count++;*/
            }
        }

    }
}

