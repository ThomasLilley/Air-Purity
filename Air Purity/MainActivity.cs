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
using Microcharts;
using SkiaSharp;
using Microcharts.Droid;

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
            string url = "http://35.234.88.236/index.php/sensors/getsensordata";
            JsonValue json = await FetchJsonAsync(url);
            ParseAndDisplay(json, "LPG", 1);
            ParseAndDisplay(json, "CO", 2);
            ParseAndDisplay(json, "SMOKE", 3);
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

        private void ParseAndDisplay(JsonValue json, string Jval, int Gval)
        {
            //TextView txt1 = FindViewById<TextView>(Resource.Id.txt1);
            //TextView txt2 = FindViewById<TextView>(Resource.Id.txt2);
            //TextView txt3 = FindViewById<TextView>(Resource.Id.txt3);
            //TextView txt4 = FindViewById<TextView>(Resource.Id.txt4);

            //foreach (JsonValue obj in json)
            //{
            //    txt1.Text = txt1.Text + "\n" + obj["time_stamp"];
            //    txt2.Text = txt2.Text + "\n" + obj["sensor_id"];
            //    txt3.Text = txt3.Text + "\n" + obj["sensor_type"];
            //    txt4.Text = txt4.Text + "\n" + obj["value"];

            //}    
            if (Gval == 1)
            {
                int maxSize = 10;
                int JsonSize = 0;
                int count = 0;
                Console.WriteLine("counting");
                foreach (JsonValue obj in json)
                {
                    if (count < maxSize)
                    {
                        JsonSize++;
                    }
                    count++;
                }

                float[] valueArray = new float[JsonSize];
                count = 0; foreach (JsonValue obj in json)
                {
                    if (count < maxSize)
                    {
                        Console.WriteLine(obj[Jval].ToString());
                        valueArray[count] = obj[Jval];

                    }
                    count++;
                }

                foreach (var item in valueArray)
                {
                    Console.WriteLine(item.ToString());
                }


                Console.WriteLine("Writing Graph");
                var entries1 = new List<Entry>();

                count = 0;
                foreach (int item in valueArray)
                {
                    Console.WriteLine(item.ToString());
                    entries1.Add(new Entry(valueArray[count])
                    {
                        Label = count.ToString(),
                        Color = SKColor.Parse("#90D585"),
                        ValueLabel = valueArray[count].ToString()
                    });
                    count++;
                }

                var chart = new LineChart()
                {
                    Entries = entries1,
                    BackgroundColor = SKColors.Transparent
                };

                var chartView = FindViewById<ChartView>(Resource.Id.chartView);
                chartView.Chart = chart;
            }
            if (Gval == 2)
            {
                int maxSize = 10;
                int JsonSize = 0;
                int count = 0;
                Console.WriteLine("counting");
                foreach (JsonValue obj in json)
                {
                    if (count < maxSize)
                    {
                        JsonSize++;
                    }
                    count++;
                }

                float[] valueArray = new float[JsonSize];
                count = 0; foreach (JsonValue obj in json)
                {
                    if (count < maxSize)
                    {
                        Console.WriteLine(obj[Jval].ToString());
                        valueArray[count] = obj[Jval];

                    }
                    count++;
                }

                foreach (var item in valueArray)
                {
                    Console.WriteLine(item.ToString());
                }
                Console.WriteLine("Writing Graph");
                var entries2 = new List<Entry>();

                count = 0;
                foreach (int item in valueArray)
                {
                    Console.WriteLine(item.ToString());
                    entries2.Add(new Entry(valueArray[count])
                    {
                        Label = count.ToString(),
                        Color = SKColor.Parse("#4e81d5"),
                        ValueLabel = valueArray[count].ToString()
                    });
                    count++;
                }

                var chart = new LineChart()
                {
                    Entries = entries2,
                    BackgroundColor = SKColors.Transparent
                };

                var chartView = FindViewById<ChartView>(Resource.Id.chartView2);
                chartView.Chart = chart;
            }
            if (Gval == 3)
            {
                int maxSize = 10;
                int JsonSize = 0;
                int count = 0;
                Console.WriteLine("counting");
                foreach (JsonValue obj in json)
                {
                    if (count < maxSize)
                    {
                        JsonSize++;
                    }
                    count++;
                }

                float[] valueArray = new float[JsonSize];
                count = 0; foreach (JsonValue obj in json)
                {
                    if (count < maxSize)
                    {
                        Console.WriteLine(obj[Jval].ToString());
                        valueArray[count] = obj[Jval];

                    }
                    count++;
                }

                foreach (var item in valueArray)
                {
                    Console.WriteLine(item.ToString());
                }

                Console.WriteLine("Writing Graph");
                var entries3 = new List<Entry>();

                count = 0;
                foreach (int item in valueArray)
                {
                    Console.WriteLine(item.ToString());
                    entries3.Add(new Entry(valueArray[count])
                    {
                        Label = count.ToString(),
                        Color = SKColor.Parse("#d5664e"),
                        ValueLabel = valueArray[count].ToString()
                    });
                    count++;
                }

                var chart = new LineChart()
                {
                    Entries = entries3,
                    BackgroundColor = SKColors.Transparent
                };

                var chartView = FindViewById<ChartView>(Resource.Id.chartView3);
                chartView.Chart = chart;
            }



        }
    }
}

