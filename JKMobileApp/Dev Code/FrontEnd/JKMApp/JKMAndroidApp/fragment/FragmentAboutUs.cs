using Android.OS;
using Android.Views;

namespace JKMAndroidApp.fragment
{
    /// <summary>
    /// Method Name     : FragmentAboutUs
    /// Author          : Sanket Prajapati
    /// Creation Date   : 23 Jan 2018
    /// Purpose         : Fragement for About Us page
    /// Revision        : 
    /// </summary>
    public class FragmentAboutUs : Android.Support.V4.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            inflater.Inflate(Resource.Layout.layoutFragmentAbout, container, false);
            return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}