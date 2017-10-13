using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Xamarin.Forms.Maps;

namespace TrainingXamarin.Views.MapPage
{
    public class MapViewModel : BaseViewModel
    {
        const string strAutoCompleteGoogleApi = "https://maps.googleapis.com/maps/api/place/autocomplete/json?input=";
        const string strGoogleApiKey = "AIzaSyDzWRcdiGV-J8uPIpHizrFjr-aSbMHfZbc";
        const string strGeoCodingUrl = "https://maps.googleapis.com/maps/api/geocode/json";
        private ContentPage _contentPage;
        private ObservableCollection<string> itemsSource;
        private Position positionChanged;
        private string location;

        public MapViewModel(ContentPage contentPage)
        {
            _contentPage = contentPage;
            TextChanged = new Command(OnTextChanged);
            ItemChanged = new Command(OnItemChanged);
            AcceptClick = new Command(OnAcceptClick);
        }

        async Task<string> fnDownloadString(string strUrl)
        {
            HttpClient client = new HttpClient();
            string strResultData;

            var response = await client.GetAsync(strUrl);
            if (response.IsSuccessStatusCode)
            {
                strResultData = await response.Content.ReadAsStringAsync();
            }
            else
            {
                strResultData = "Exception";
            }
            return strResultData;
        }

        async void OnAcceptClick() {
            if(location == null || location == string.Empty) {
                await _contentPage.DisplayAlert("Warning", "Please Enter Location", "OK");
                return;
            }
            MessagingCenter.Send(location, "TODO");
            await _contentPage.Navigation.PopAsync();
        }

        async void OnTextChanged(object newValue)
        {
            try
            {
                string json = await fnDownloadString(strAutoCompleteGoogleApi + (string)newValue + "&key=" + strGoogleApiKey);

                if (json == "Exception")
                {
                    return;
                }
                GoogleMapPlace objMap = JsonConvert.DeserializeObject<GoogleMapPlace>(json);
                string[] strPredictiveText = new string[objMap.predictions.Count];
                int index = 0;
                foreach (Prediction objPred in objMap.predictions)
                {
                    strPredictiveText[index] = objPred.description;
                    index++;
                }
                ItemsSource = new ObservableCollection<string>(strPredictiveText);
            }
            catch
            {
                await _contentPage.DisplayAlert("Warning", "Unable to process at this moment!!!", "OK");
            }
        }

        async void OnItemChanged(object newValue)
        {
            location = (string)newValue;
            string json = await fnDownloadString(strGeoCodingUrl + "?address=" + (string)newValue);
            if (json == "Exception")
            {
                return;

            }
            GeoMap objGeo = JsonConvert.DeserializeObject<GeoMap>(json);
            PositionChanged = new Position(objGeo.results[0].geometry.location.lat, objGeo.results[0].geometry.location.lng);
        }

        public ICommand TextChanged
        {
            get; set;
        }

        public ICommand ItemChanged
        {
            get; set;
        }

        public ICommand AcceptClick
        {
            get; set;
        }


        public ObservableCollection<string> ItemsSource
        {
            get
            {
                return itemsSource;
            }
            set
            {
                itemsSource = value;
                pushPropertyChanged("ItemsSource");
            }
        }

        public Position PositionChanged
        {
            get
            {
                return positionChanged;
            }
            set
            {
                positionChanged = value;
                pushPropertyChanged("PositionChanged");
            }
        }
    }
}
