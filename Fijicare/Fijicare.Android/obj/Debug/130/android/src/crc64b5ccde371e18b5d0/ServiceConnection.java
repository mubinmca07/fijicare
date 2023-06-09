package crc64b5ccde371e18b5d0;


public class ServiceConnection
	extends androidx.browser.customtabs.CustomTabsServiceConnection
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCustomTabsServiceConnected:(Landroid/content/ComponentName;Landroidx/browser/customtabs/CustomTabsClient;)V:GetOnCustomTabsServiceConnected_Landroid_content_ComponentName_Landroidx_browser_customtabs_CustomTabsClient_Handler\n" +
			"n_onServiceDisconnected:(Landroid/content/ComponentName;)V:GetOnServiceDisconnected_Landroid_content_ComponentName_Handler\n" +
			"";
		mono.android.Runtime.register ("Android.Support.CustomTabs.Chromium.SharedUtilities.ServiceConnection, Xamarin.Auth", ServiceConnection.class, __md_methods);
	}


	public ServiceConnection ()
	{
		super ();
		if (getClass () == ServiceConnection.class) {
			mono.android.TypeManager.Activate ("Android.Support.CustomTabs.Chromium.SharedUtilities.ServiceConnection, Xamarin.Auth", "", this, new java.lang.Object[] {  });
		}
	}


	public void onCustomTabsServiceConnected (android.content.ComponentName p0, androidx.browser.customtabs.CustomTabsClient p1)
	{
		n_onCustomTabsServiceConnected (p0, p1);
	}

	private native void n_onCustomTabsServiceConnected (android.content.ComponentName p0, androidx.browser.customtabs.CustomTabsClient p1);


	public void onServiceDisconnected (android.content.ComponentName p0)
	{
		n_onServiceDisconnected (p0);
	}

	private native void n_onServiceDisconnected (android.content.ComponentName p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
