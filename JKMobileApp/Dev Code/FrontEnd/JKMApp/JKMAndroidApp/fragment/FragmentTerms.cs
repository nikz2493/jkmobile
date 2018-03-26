using Android.OS;
using Android.Views;
using Android.Webkit;

namespace JKMAndroidApp.fragment
{
    /// <summary>
    /// Method Name     : FragmentTerms
    /// Author          : Sanket Prajapati
    /// Creation Date   : 23 Jan 2018
    /// Purpose         : Fragement for Terms layout
    /// Revision        : 
    /// </summary>
    public class FragmentTerms : Android.Support.V4.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
          View view=  inflater.Inflate(Resource.Layout.LayoutFragmentTerms, container, false);
           
            WebView  webview = view.FindViewById<WebView>(Resource.Id.webView1);
          
            webview.LoadUrl(StringResource.PrivacyPolicyUrl);
            return view;
           
        }
    }
}