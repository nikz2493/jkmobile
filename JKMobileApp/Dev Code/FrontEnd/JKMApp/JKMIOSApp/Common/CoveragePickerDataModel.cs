using System;
using System.Collections.Generic;
using UIKit;

namespace JKMIOSApp.Common
{
    /// <summary>
    /// Class Name      : Extensions.
    /// Author          : Hiren Patel
    /// Creation Date   : 22 JAN 2018
    /// Purpose         : Coverage picker data model.
    /// Revision        :
    /// </summary>
    public class CoveragePickerDataModel : UIPickerViewModel
    {
        public event EventHandler<EventArgs> ValueChanged;

        /// <summary>
        /// Class Name      : Extensions.
        /// Author          : Hiren Patel
        /// Creation Date   : 22 JAN 2018
        /// Purpose         : The items to show up in the picker.
        /// Revision        :
        /// </summary>
        public List<string> Items { get; private set; }

        /// <summary>
        /// The current selected item
        /// </summary>
        public string SelectedItem
        {
            get { return Items[selectedIndex]; }
        }

        private int selectedIndex = 0;

        public void SetSelectedIndex(int index)
        {
            this.selectedIndex = index;
        }

        public CoveragePickerDataModel()
        {
            Items = new List<string>();
        }

        /// <summary>
        /// Methode Name    : GetRowsInComponent.
        /// Author          : Hiren Patel
        /// Creation Date   : 22 JAN 2018
        /// Purpose         : Called by the picker to determine how many rows are in a given spinner item.
        /// Revision        :
        /// </summary>
        /// <returns>The rows in component.</returns>
        /// <param name="pickerView">Picker view.</param>
        /// <param name="component">Component.</param>
        public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            return Items.Count;
        }

        /// <summary>
        /// Methode Name    : GetTitle.
        /// Author          : Hiren Patel
        /// Creation Date   : 22 JAN 2018
        /// Purpose         : called by the picker to get the text for a particular row in a particular.
        /// Revision        :
        /// </summary>
        /// <returns>The title.</returns>
        /// <param name="pickerView">Picker view.</param>
        /// <param name="row">Row.</param>
        /// <param name="component">Component.</param>
        public override string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            return Items[(int)row];
        }

        /// <summary>
        /// Methode Name    : GetTitle.
        /// Author          : Hiren Patel
        /// Creation Date   : 22 JAN 2018
        /// Purpose         : called by the picker to get the number of spinner items.
        /// Revision        :
        /// </summary>
        /// <returns>The component count.</returns>
        /// <param name="pickerView">Picker view.</param>
        public override nint GetComponentCount(UIPickerView pickerView)
        {
            return 1;
        }

        /// <summary>
        /// Methode Name    : Selected.
        /// Author          : Hiren Patel
        /// Creation Date   : 22 JAN 2018
        /// Purpose         : called when a row is selected in the spinner.
        /// Revision        :
        /// </summary>
        /// <returns>The selected.</returns>
        /// <param name="pickerView">Picker view.</param>
        /// <param name="row">Row.</param>
        /// <param name="component">Component.</param>
        public override void Selected(UIPickerView pickerView, nint row, nint component)
        {
            selectedIndex = (int)row;
            if (ValueChanged != null)
            {
                ValueChanged(this, new EventArgs());
            }
        }

       



        /// <summary>
        /// Methode Name    : Selected.
        /// Author          : Hiren Patel
        /// Creation Date   : 22 JAN 2018
        /// Purpose         : To change label view of UIPicker view .
        /// Revision        :
        /// </summary>
        /// <returns>The view.</returns>
        /// <param name="pickerView">Picker view.</param>
        /// <param name="row">Row.</param>
        /// <param name="component">Component.</param>
        /// <param name="view">View.</param>
        public override UIView GetView(UIPickerView pickerView, nint row, nint component, UIView view)
        {
            // https://forums.xamarin.com/discussion/27337/uipickerview-change-text-size-or-make-it-multiline
            var label = new UILabel();

            label.Text = Items[(int)row].ToString();
            label.BackgroundColor = UIColor.Clear;
            label.TextAlignment = UITextAlignment.Center;
            UIHelper.SetLabelFont(label);
            label.TextColor = UIColor.Black;

            return label;
        }
    }
}