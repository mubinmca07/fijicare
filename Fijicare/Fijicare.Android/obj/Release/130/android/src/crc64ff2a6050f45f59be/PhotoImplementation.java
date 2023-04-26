package crc64ff2a6050f45f59be;


public class PhotoImplementation
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Fijicare.Droid.Native.PhotoImplementation, Fijicare.Android", PhotoImplementation.class, __md_methods);
	}


	public PhotoImplementation ()
	{
		super ();
		if (getClass () == PhotoImplementation.class) {
			mono.android.TypeManager.Activate ("Fijicare.Droid.Native.PhotoImplementation, Fijicare.Android", "", this, new java.lang.Object[] {  });
		}
	}

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
