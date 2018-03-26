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
using Android.Graphics;

namespace JKMAndroidApp.Utility
{
    public static class RandomColorGenerator
    {
        public static Color RandomColor()
        {
            Random random = new Random();
            Color randomColor = new Color(random.Next(256), random.Next(256), random.Next(256));
            return randomColor;
        }
    }
}