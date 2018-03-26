using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using JKMIOSApp.Common;
using JKMPCL.Common;
using JKMPCL.Model;
using JKMPCL.Services;
using JKMPCL.Services.Estimate;
using UIKit;
namespace JKMIOSApp.Common
{
    public class AcknowledgementViewModel
    {
        public UIView UIVIew { get; set; }
        public UIView ParentUIView { get; set; }
        public Int32  Index { get; set; }
        public Int32? ParentIndex { get; set; }
        public nfloat? Height { get; set; }
    }
}
