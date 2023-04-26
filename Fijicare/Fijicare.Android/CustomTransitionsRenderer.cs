//using Fijicare.Droid;
//using Xamarin.Forms;

//[assembly: ExportRenderer(typeof(NavigationPage), typeof(CustomTransitionsRenderer))]
//namespace Fijicare.Droid
//{
//    public class CustomTransitionsRenderer : Xamarin.Forms.Platform.Android.AppCompat.NavigationPageRenderer
//    {
//        public CustomTransitionsRenderer() : base()
//        {

//        }
//        //SwitchContentAsync
//        protected override void SetupPageTransition(Android.Support.V4.App.FragmentTransaction transaction, bool isPush)
//        {
//            if (isPush)
//                transaction.SetCustomAnimations(Resource.Animator.move_in_left, 0);
//            else //prevView enter:
//                transaction.SetCustomAnimations(Resource.Animator.slide_in_right, 0);
//        }
//    }
//}