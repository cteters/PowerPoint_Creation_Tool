# PowerPoint Creation Tool
## _C# coding exercise_

This is a very simple program to aid PowerPoint development by generating slides from user input. It gives suggestions of images to use from the internet, based on the contents of the information the user provides for each. An actual use case for this program is to improve efficiency and save time not having to search for images for every slide being made for a presentations.

## ToDO

- Optimize the search results by parsing Flowdocument with regex for bold text.
- Format pictures displayed on the image search results screen by using a DataTemplate.
- Optimize json search through use of strongly typed objects.
- Better format images in PowerPoint by dynamic scaling.
- Build test cases.
- Clean up code.

## Resources:
- [AngularJS] - HTML enhanced for web apps!

## Installation

Clone or download the repository and update these two lines of code in ImageSearch.xaml.cs with actual values:

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
