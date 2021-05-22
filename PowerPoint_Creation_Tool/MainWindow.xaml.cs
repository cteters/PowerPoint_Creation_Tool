using System;
using System.Windows;
using Microsoft.Office.Interop.PowerPoint;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Core;
using System.Windows.Forms;
using System.Text.RegularExpressions;

// solution starting point:
// https://stackoverflow.com/questions/26372020/how-to-programmatically-create-a-powerpoint-from-a-list-of-images


namespace PowerPointWPF
{
    public partial class MainWindow : Window
    {
        private Microsoft.Office.Interop.PowerPoint.Application pptApplication;
        private Slides slides;
        private _Slide slide;
        private TextRange objText;
        private CustomLayout customLayout;
        public List<string> selected;
        private Presentation pptPresentation;

        public MainWindow()
        {
            InitializeComponent();

            pptApplication = new Microsoft.Office.Interop.PowerPoint.Application();

            // Create the Presentation File
            pptPresentation = pptApplication.Presentations.Add(MsoTriState.msoTrue);
            customLayout = pptPresentation.SlideMaster.CustomLayouts[PpSlideLayout.ppLayoutText];
        }

        private void PhotosClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Documents.TextRange textRange = new System.Windows.Documents.TextRange
                (
                    Text.Document.ContentStart, // TextPointer to the start of content in the RichTextBox.          
                    Text.Document.ContentEnd    // TextPointer to the end of content in the RichTextBox.
                );

            string convertedText = Regex.Replace(textRange.Text, "\\s+", "+");

            ImageSearch newWindow = new ImageSearch(convertedText);
            newWindow.ShowDialog();
            selected = newWindow.getSelected();
        }

        private void SubmitClick(object sender, RoutedEventArgs e)
        {
            // Create new Slide
            slides = pptPresentation.Slides;
            slide = slides.AddSlide(1, customLayout);

            // Add title
            objText = slide.Shapes[1].TextFrame.TextRange;
            System.Windows.Documents.TextRange titleRange = new System.Windows.Documents.TextRange
            (
                Title.Document.ContentStart, // TextPointer to the start of content in the RichTextBox.          
                Title.Document.ContentEnd    // TextPointer to the end of content in the RichTextBox.
            );
            objText.Text = titleRange.Text;

            // Add text
            objText = slide.Shapes[2].TextFrame.TextRange;
            System.Windows.Documents.TextRange textRange = new System.Windows.Documents.TextRange
            (
                Text.Document.ContentStart, // TextPointer to the start of content in the RichTextBox.          
                Text.Document.ContentEnd    // TextPointer to the end of content in the RichTextBox.
            );
            objText.Text = textRange.Text;

            int numPics = 0;
            if (selected != null)
            {
                numPics = selected.Count();
            }

            for (int i = 0; i < numPics && i < 3; i++)
            {
                slide.Shapes.AddPicture(selected[i], MsoTriState.msoFalse, MsoTriState.msoTrue, ((i + 1) * 200), 300, 175, 175);
            }
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}