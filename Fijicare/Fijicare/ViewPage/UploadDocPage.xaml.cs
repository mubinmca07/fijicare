using Fijicare.Interfaces;
using Fijicare.ViewModel;
using Plugin.Media;
using System;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fijicare.ViewPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UploadDocPage : ContentPage
    {
        UploadDocViewModel uploadDocViewModel;
        public UploadDocPage()
        {
            InitializeComponent();
            uploadDocViewModel = new UploadDocViewModel(Navigation);
            uploadDocViewModel.PathList = new ObservableCollection<string>();
            this.BindingContext = uploadDocViewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Device.RuntimePlatform == Device.Android)
            {
                MessagingCenter.Subscribe<App, ObservableCollection<string>>((App)Xamarin.Forms.Application.Current, "ImagesSelected", (s, images) =>
                {
                    // listItems.FlowItemsSource = images;

                    foreach (var item in images)
                    {
                        if (uploadDocViewModel.DocList == null)
                            uploadDocViewModel.DocList = new ObservableCollection<NameAndPath>();

                        var select = (from p in uploadDocViewModel.DocList where p.ImagePath == item select p).FirstOrDefault();
                        if (select == null)
                        {
                            NameAndPath andPath = new NameAndPath();
                            andPath.ImagePath = item;
                            andPath.ImageName = System.IO.Path.GetFileName(item);
                            uploadDocViewModel.DocList.Add(andPath);
                        }

                    }

                });
            }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            
            MessagingCenter.Unsubscribe<App, ObservableCollection<string>>(this, "ImagesSelected");
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            uploadDocViewModel.Height = stalaout.HeightRequest;
            uploadDocViewModel.Orientation = StackOrientation.Horizontal;
            stalaout.HeightRequest = 80;
            uploadDocViewModel.IsVisible = true;
            if (Device.RuntimePlatform == Device.Android)
            {
                await DependencyService.Get<IMediaService>().OpenGallery();
            }
            else
            {

            }
        }


        async void Camera_Clicked(object sender, System.EventArgs e)
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                Directory = "upload",
                Name = DateTime.Now.ToString() + ".jpg"
            });

            if (file == null)
                return;



        }
    }
}