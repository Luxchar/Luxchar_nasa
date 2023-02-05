
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForm_Luc_Charlopeau.API
{
    class Config
    {
        // api key for the nasa api
        private string api_key = "RRudGx8KiFj27P7jSdfok4feDlOiLDpzB1wbdPlg";

        // url for the nasa api
        private string url = "https://api.nasa.gov/planetary/apod?api_key=";

        public string GetApiKey() // get method for the api key
        {
            return api_key;
        }

        public string GetUrl() // get method for the url
        {
            return url;
        }
    }
}
