package crc64ff2a6050f45f59be;


public class FixedNavigationRenderer
	extends crc643f46942d9dd1fff9.ListViewRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onDetachedFromWindow:()V:GetOnDetachedFromWindowHandler\n" +
			"";
		mono.android.Runtime.register ("Fijicare.Droid.Native.FixedNavigationRenderer, Fijicare.Android", FixedNavigationRenderer.class, __md_methods);
	}


	public FixedNavigationRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == FixedNavigationRenderer.class) {
			mono.android.TypeManager.Activate ("Fijicare.Droid.Native.FixedNavigationRenderer, Fijicare.Android", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
		}
	}


	public FixedNavigationRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == FixedNavigationRenderer.class) {
			mono.android.TypeManager.Activate ("Fijicare.Droid.Native.FixedNavigationRenderer, Fijicare.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
		}
	}


	public FixedNavigationRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == FixedNavigationRenderer.class) {
			mono.android.TypeManager.Activate ("Fijicare.Droid.Native.FixedNavigationRenderer, Fijicare.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
		}
	}


	public void onDetachedFromWindow ()
	{
		n_onDetachedFromWindow ();
	}

	private native void n_onDetachedFromWindow ();

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
