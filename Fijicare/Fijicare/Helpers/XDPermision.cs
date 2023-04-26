using Fijicare.Interfaces;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Fijicare.Helpers
{
  public static  class XDPermision
    { 
        public static async void CheckSMSPermision(Permission permissionType)
        {


            //var CheckSMSPermision = DependencyService.Get<Ipermision>();
            //CheckSMSPermision?.CheckPermision();

            StringBuilder msg = new StringBuilder();
            msg.AppendLine("Permission denied for ");
            bool isDenied = false;

            PermissionStatus status = PermissionStatus.Unknown;

            PermissionStatus permissionStatus = PermissionStatus.Unknown;
            permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(permissionType);
            if (permissionStatus != PermissionStatus.Granted) //This always return Granted even if i explicitly revoke from device

            {
                Dictionary<Permission, PermissionStatus> results = await CrossPermissions.Current.RequestPermissionsAsync(permissionType);
                status = results[permissionType];
                if (status != PermissionStatus.Granted)
                {
                    msg.AppendLine(permissionType.ToString());
                    isDenied = true;
                }
            }

            if (isDenied)
            {
                bool result = await Application.Current.MainPage.DisplayAlert("Permission Denied Please go to setting and allow.", msg.ToString(), "Yes", "No");
                if (result)
                {
                    //user wants to exit
                    //Terminate application
                    try
                    {
                        DependencyService.Get<ISettingsService>().OpenSettings();
                    }
                    catch (Exception ex)
                    {

                      
                    }
                  
                }
                else
                {
                    //Dont do anything
                }
            }
            return;


        }
    }
}
