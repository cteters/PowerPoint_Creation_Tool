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
        private string CX = ""; // identifier of the Programmable Search Engine
        private string APIKEY = ""; // API key
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

            string json1;
            var request1 = WebRequest.Create("https://www.googleapis.com/customsearch/v1?key=" + APIKEY + "&cx=" + CX + "&q=" + text);
            HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();
            json1 = new StreamReader(response1.GetResponseStream()).ReadToEnd();

            dynamic foo = JObject.Parse(json1);
            thumbnails = new List<string>();

            for (int i = 0; i < 9; i++)
            {
                try
                {
                    thumbnails.Add(foo.items[i].pagemap.cse_image[0].src.ToString());
                } 
                catch (Exception e)
                {
                    Console.WriteLine("cse_image not listed");
                }
            }

            string json2;
            var request2 = WebRequest.Create("https://www.googleapis.com/customsearch/v1?key=" + APIKEY + "&cx=" + CX + "&q=" + text + "&start=11");
            HttpWebResponse response2 = (HttpWebResponse)request2.GetResponse();
            json2 = new StreamReader(response2.GetResponseStream()).ReadToEnd();

            dynamic foo2 = JObject.Parse(json2);

            for (int i = 0; i < 9; i++)
            {
                try
                {
                    thumbnails.Add(foo2.items[i].pagemap.cse_image[0].src.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("cse_image not listed");
                }
            }

            imagePopulate();
        }

        public void imagePopulate()
        {
            for (int i = 0; i < thumbnails.Count; i++)
            {
                try
                {
                    BitmapImage original = new BitmapImage();
                    original.BeginInit();

                    original.UriSource = new Uri(thumbnails[i]);

                    original.DecodePixelHeight = 150;
                    original.EndInit();

                    Image final = new Image();
                    final.Source = original;

                    //Object[] itemSelections = new Object[] { new System.Windows.Controls.CheckBox(), final };
                    //imageList.Items.Add(itemSelections);
                    imageList.Items.Add(final);
                    imageList.Items.Add(new CheckBox());
                }
                catch (Exception e)
                {
                    Console.WriteLine("unknown url caught for single image");
                }
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