using Fijicare.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Fijicare.Helpers
{
   public static class NavigationPageHelper
    {
       

      

        public static TransitionType _TransitionType { get; set; }

        public static async void NavigationTo(Page _Page, TransitionType _transitionType, INavigation _Navigation)
        {
            try
            {
                _TransitionType = _transitionType;
                List<Page> existingPages = _Navigation.NavigationStack.ToList();
                Page PageIsist = (from p in existingPages
                                 where p.ToString().Contains(Convert.ToString(_Page))
                                 select p).FirstOrDefault();
                await _Navigation.PushAsync(_Page);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error on loading N navigation {0}", ex.Message);

            }

        }
        public static async void NavigationToxx(Page _Page, TransitionType _transitionType,INavigation _Navigation)            
        {
            try
            {
                _TransitionType = _transitionType;
                List<Page> existingPages = _Navigation.NavigationStack.ToList();
                Page PageIsist = (from p in existingPages
                                 where p.ToString().Contains(Convert.ToString(_Page))
                                 select p).FirstOrDefault();
                string pp = Convert.ToString(_Page);
                if (PageIsist == null)
                {
                    //var page1 = App.Current.MainPage.Navigation.NavigationStack.LastOrDefault() as PageName;
                    await _Navigation.PushAsync(_Page);
                    if ("Fijicare.ViewPage.ClassSelectionViewPage".Equals(Convert.ToString(_Page)))
                    {
                        foreach (Page item1 in _Navigation.NavigationStack.Reverse())
                        {
                            if (!item1.ToString().Equals(Convert.ToString(_Page)))
                                _Navigation.RemovePage(item1);
                        }
                    }                       

                    
                }
                else
                {
                   


                    foreach (Page item in _Navigation.NavigationStack.Reverse())
                    {

                        if (!item.ToString().Equals(Convert.ToString(_Page)))
                        {
                            
                            _Navigation.RemovePage(item);
                        }
                        else
                        {
                            _Navigation.RemovePage(item);
                            await _Navigation.PushAsync(_Page);
                            break;
                        }
                    }
                    

                }

                //var existingPages = _Navigation.ModalStack.ToList();
                //var PageIsist = (from p in existingPages
                //                 where p.ToString().Contains(Convert.ToString(_Page))
                //                 select p).FirstOrDefault();
                //if (PageIsist == null)
                //{                  
                //    await _Navigation.PushModalAsync(_Page);
                //}





            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error on loading N navigation {0}", ex.Message);

            }
           
        }


        public static async void NavigationBack(INavigation _Navigation)
        {
            try
            {
                await _Navigation.PopAsync();
                //await _Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error on loading backpage navigation {0}", ex.Message);
            }
          
        }

        public static  void NavigationStackRemove(Page _Page, INavigation _Navigation)
        {
             _Navigation.RemovePage(_Page);
        }
        
        public static void NavigationInsertBefore()
        {

        }
    }
}
