using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


namespace PowerPointWPF
{
    /// <summary>
    /// Interaction logic for ImageSearch.xaml
    /// </summary>


    // strongly typed object for json parsing, not currently utilized
    public class Thumbnail
    {
        public string src { get; set; }
    }

    // strongly typed object for json parsing, not currently utilized
    public class CseThumbnail
    {
        public string src { get; set; }
    }

    // strongly typed object for json parsing, not currently utilized
    public class Pagemap
    {
        public List<CseThumbnail> cse_thumbnail { get; set; }
        public List<Thumbnail> thumbnail { get; set; }
    }

    public partial class ImageSearch : Window
    {
        private string CX = "";
        private string APIKEY = "";
        private string text;
        private List<string> thumbnails;
        private List<string> selected;
        //private Object[] itemSelections;
        public ImageSearch()
        {
            InitializeComponent();
        }

        public ImageSearch(string text)
        {
            InitializeComponent();
            string json;

            var request = WebRequest.Create("https://www.googleapis.com/customsearch/v1?key=" + APIKEY + "&cx=" + CX + "&q=" + text);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            json = new StreamReader(response.GetResponseStream()).ReadToEnd();
            JObject data = JObject.Parse(json);

            dynamic foo = JObject.Parse(json);
            thumbnails = new List<string>();

            for (int i = 0; i < 8; i++)
            {
                thumbnails.Add(foo.items[i].pagemap.cse_image[0].src.ToString());
            }

            imagePopulate();
        }

        public void imagePopulate()
        {
            for (int i = 0; i < thumbnails.Count; i++)
            {
                BitmapImage original = new BitmapImage();
                original.BeginInit();
                original.UriSource = new Uri(thumbnails[i]);
                original.DecodePixelWidth = 200;
                original.EndInit();

                Image final = new Image();
                final.Source = original;

                //Object[] itemSelections = new Object[] { new System.Windows.Controls.CheckBox(), final };
                //imageList.Items.Add(itemSelections);
                imageList.Items.Add(final);
                imageList.Items.Add(new CheckBox());
            }
        }

        public List<string> getSelected()
        {
            return selected;
        }

        private void SubmitClick(object sender, RoutedEventArgs e)
        {
            selected = new List<string>();

            for (int i = 1; i < imageList.Items.Count; i += 2)
            {
                //Object[] itemSelection = new Object[] { imageList.Items.GetItemAt(i) };
                //System.Windows.Controls.CheckBox checkbox = (System.Windows.Controls.CheckBox)itemSelection[0];
                imageList.Items.GetItemAt(i);
                CheckBox checkbox = (CheckBox)imageList.Items.GetItemAt(i);
                if (checkbox.IsChecked == true)
                {
                    Image image = (Image)imageList.Items.GetItemAt(i - 1);
                    selected.Add(image.Source.ToString());
                }
            }

            Close();
        }

        private void CancleClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}