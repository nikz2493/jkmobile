using System;
using System.Collections.Generic;
using UIKit;

namespace JKMIOSApp
{
	public partial class WizardTopViewController : UIViewController
	{
		public WizardTopViewController (IntPtr handle) : base (handle)
		{
		}
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ShowCurretStep(6);
        }
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }
        private class Step
        {
            public UIImageView imageStep { get; set; }
            public UIView viewSeparator { get; set; }
            public Int32 Index { get; set; }
        }

        /// <summary>
        /// Method Name     : GetStepList
        /// Author          : Hiren Patel
        /// Creation Date   : 29 Dec 2017
        /// Purpose         : To get steplist
        /// Revision        : 
        /// </summary>
        private List<Step> GetStepList()
        {
            List<Step> stepsList = new List<Step>();
            Step step = new Step();
            step.imageStep = imgStep1;
            step.viewSeparator = viewSeparator1;
            step.Index = 1;
            stepsList.Add(step);

            step = new Step();
            step.imageStep = imgStep2;
            step.viewSeparator = viewSeparator2;
            step.Index = 2;
            stepsList.Add(step);

            step = new Step();
            step.imageStep = imgStep3;
            step.viewSeparator = viewSeparator3;
            step.Index = 3;
            stepsList.Add(step);

            step = new Step();
            step.imageStep = imgStep4;
            step.viewSeparator = viewSeparator4;
            step.Index = 4;
            stepsList.Add(step);

            step = new Step();
            step.imageStep = imgStep5;
            step.viewSeparator = viewSeparator5;
            step.Index = 5;
            stepsList.Add(step);

            step = new Step();
            step.imageStep = imgStep6;
            step.viewSeparator = viewSeparator6;
            step.Index = 6;
            stepsList.Add(step);

            step = new Step();
            step.imageStep = imgStep7;
            step.viewSeparator = viewSeparator7;
            step.Index = 7;
            stepsList.Add(step);

            step = new Step();
            step.imageStep = imgStep8;
            step.Index = 8;
            stepsList.Add(step);
            return stepsList;
        }

        public void ShowCurretStep(Int32 stepIndex)
        {
           
            Int32 counter = 1;
            foreach(Step objStep in GetStepList())
            {
                if(objStep.Index<=stepIndex)
                {
                    objStep.imageStep.Image = UIImage.FromFile("completed.png");
                    if (objStep.viewSeparator != null)
                    {
                        objStep.viewSeparator.Layer.BackgroundColor = UIColor.FromRGB(127, 219, 216).CGColor;
                    }
                    if (objStep.Index == stepIndex)
                    {
                        objStep.imageStep.Image = UIImage.FromFile(string.Format("{0}_active.png", counter));
                    }
                }
                else
                {
                    objStep.imageStep.Image = UIImage.FromFile(string.Format("{0}_disable.png", counter));
                   
                    if (objStep.viewSeparator != null)
                    {
                        //Separator background color should be change
                    }
                }
                counter++;
            }
        }



	}
}
