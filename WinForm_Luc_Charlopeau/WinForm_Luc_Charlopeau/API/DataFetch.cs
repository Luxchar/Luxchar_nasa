using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WinForm_Luc_Charlopeau.Core;
using WinForm_Luc_Charlopeau.MVVM.ViewModel;

namespace WinForm_Luc_Charlopeau.API
{
    class DataFetch : ObservableObject
    {
        // PROPERTIES TO BIND THE DATA TO THE VIEW ASYNCHRONOUSELY
        public Image ImageVM { get; set; }
        private object _currentImage;

        public object CurrentImage
        {
            get { return _currentImage; } // return the current view
            set
            { // set the current view to the value passed in the parameter
                _currentImage = value;
                OnPropertyChanged();
            }
        }

        public Title TitleVM { get; set; }
        private object _currentTitle;

        public object CurrentTitle
        {
            get { return _currentTitle; } // return the current view
            set
            { // set the current view to the value passed in the parameter
                _currentTitle = value;
                OnPropertyChanged();
            }
        }

        public Explanation ExplanationVM { get; set; }
        private object _currentExplanation;

        public object CurrentExplanation
        {
            get { return _currentExplanation; } // return the current view
            set
            { // set the current view to the value passed in the parameter
                _currentExplanation = value;
                OnPropertyChanged();
            }
        }

        public DataFetch()
        {
            // run FetchDataAsync in a new thread to avoid blocking the ui 
            Task.Run(async () => await FetchDataAsync());

            // refresh the ui with the inotifypropertychanged interface
            CurrentImage = BGImage;
        }

        // METHODS TO FETCH THE DATA
        public System.Drawing.Bitmap BGImage { get; set; }
        public string Explanation { get; set; }
        public string Title { get; set; }

        public async Task<DataFetch> FetchDataAsync() // get the image from the nasa api
        {
            // get the api key and the url from the config class
            Config config = new Config();
            string api_key = config.GetApiKey();
            string url = config.GetUrl();

            (string title, string explanation, string image_url) = await GetAPODInfoAsync();

            await SetImageAsync(image_url, title, api_key, url);
            SetExplanation(explanation);
            SetTitle(title);

            // refresh the ui with the inotifypropertychanged interface
            CurrentImage = BGImage;
            CurrentExplanation = Explanation;
            CurrentTitle = Title;

            return this;
        }

        private async Task SetImageAsync(string image_url, string title, string api_key, string url) // download an image from the nasa api
        {
            // create a new web client
            System.Net.WebClient client2 = new System.Net.WebClient();

            // check if image is already downloaded
            if (File.Exists(title))
            {
                // set bg image asynchronoulsy
                BGImage = new System.Drawing.Bitmap((title)); // create a new bitmap from the image
                return;
            }

            // download the image
            await client2.DownloadFileTaskAsync(image_url, title); // download the image in the images folder

            BGImage = new System.Drawing.Bitmap((title)); // create a new bitmap from the image
        }

        private async Task<(string, string, string)> GetAPODInfoAsync() // get the info of the APOD image
        {
            // get the api key and the url from the config class
            Config config = new Config();
            string api_key = config.GetApiKey();
            string url = config.GetUrl();

            // create a new web client
            System.Net.WebClient client = new System.Net.WebClient();

            // get the json from the nasa api
            string json = await client.DownloadStringTaskAsync(url + api_key);

            // parse the json
            var parsedJson = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json); // dynamic = any type of object

            // get the title and the explanation of the image
            string title = parsedJson.title;
            string explanation = parsedJson.explanation;
            string image_url = parsedJson.url;
            return (title, explanation, image_url);
        }

        private void SetTitle(string title) // set the title of the image
        {
            Title = title;
        }

        private void SetExplanation(string explanation)
        {
            // add skip line every 25 letters in explanation
            for (int i = 0; i < explanation.Length; i++)
            {
                if (i % 110 == 0)
                {
                    explanation = explanation.Insert(i, "\n");
                }
            }

            Explanation = explanation;
        }
    }
}