using Android.OS;
using Android.Views;

namespace JKMAndroidApp.fragment
{
    /// <summary>
    /// Method Name     : FragmentMyAccount
    /// Author          : Sanket Prajapati
    /// Creation Date   : 23 Jan 2018
    /// Purpose         : Fragement for MyAccount page
    /// Revision        : 
    /// </summary>
    public class FragmentMyAccount : Android.Support.V4.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.LayoutFragmentMyAccount, container, false);

            return view;
        }
    }
}