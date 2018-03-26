using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.View;
using Android.Util;

namespace JKMAndroidApp.Utility
{
    public class NonSwipeableViewPager : ViewPager
    {
        private bool mEnabled;

        public NonSwipeableViewPager(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            this.mEnabled = true;
        }

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            if (this.mEnabled)
                return base.OnInterceptTouchEvent(ev);

            return false;
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            if (this.mEnabled)
                return base.OnTouchEvent(e);

            return false;
        }

        public void SetPagingEnabled(bool enabled)
        {
            this.mEnabled = enabled;
        }
    }
}