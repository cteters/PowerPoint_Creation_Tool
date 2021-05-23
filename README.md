# PowerPoint Creation Tool
## _C# coding exercise_

The actual use case for this program is to improve PowerPoint development efficiency by minimizing the need to search for images during the initial creation of slides. It takes in the text given values of the title and text box and generates image search results that can be added to each slide. It gives suggestions of images from a google search API, based on the contents of the information the user provides. 

## ToDO

- Optimize the search results by parsing a *Flowdocument* for bold text by using a regex argument.
- Format pictures displayed on the image search results screen by using a *DataTemplate*.
- Optimize the json search through use of *strongly typed objects*.
- Better format images in PowerPoint by dynamically scaling the values.
- Build test cases.
- Clean up code.

## Installation

Clone or download the repository and update these two lines of code in *ImageSearch.xaml.cs* with actual values:

```sh
        private string CX = ""; // identifier of the Programmable Search Engine
        private string APIKEY = ""; // API key
```
Both the CX and API key can be obtained for free from google's [Search Console API](https://developers.google.com/webmaster-tools).

## References

Here is a list of external references that were included in this project:

| References | Resource inofmraiton |
| ------ | ------ |
| Newtonsoft.Json | https://www.newtonsoft.com/json |


## License

MIT
