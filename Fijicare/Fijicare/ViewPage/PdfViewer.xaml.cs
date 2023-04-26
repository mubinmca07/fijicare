using Fijicare.ViewModel;
using Xamarin.Forms;

namespace Fijicare.ViewPage
{
    public partial class PdfViewer : ContentPage
    {
        
        private string FileName;
        public PdfViewer()
        {
            InitializeComponent();           
        }

        PdfViewModel _PdfViewModel;
        public PdfViewer(string fileName)
        {
            InitializeComponent();

            if (_PdfViewModel == null)
                _PdfViewModel = new PdfViewModel(Navigation);

            this.BindingContext = _PdfViewModel;
            this.FileName = fileName;
            string fullpath = path + "/" + FileName;
            WebViewPdf.Uri = fullpath;
           
        }
        string path = DependencyService.Get<Fijicare.Interfaces.IContentPath>().GetContentPath();
        protected override void OnAppearing()
        {        
           
            base.OnAppearing();
            string fullpath = path + "/" + FileName;             
           // Device.OpenUri(new Uri(fullpath));
            //var uri =  new  Uri("http://127.0.0.1:8084/"+FileName);
            //WebViewPdf.Uri = fullpath;
        }

        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}
