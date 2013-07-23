using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace OTS.WebLib.utilities
{
    /// <summary>
    /// ListControlUtilities : cong cu ho tro lam viec voi control loai listcontrol
    /// Innotech
    /// </summary>
    public class ListControlUtilities
    {

        /// <summary>
        /// chon cho dropdownlist, listbox, listcheckbox, list option
        /// </summary>
        /// <param name="control"></param>
        /// <param name="textValue"></param>
        public static void selectItemHavingValue(ListControl control, string textValue)
        {
            control.SelectedIndex = -1;
            ListItem li = control.Items.FindByValue(textValue);

            if (li != null)
            {
                li.Selected = true;
            }

        }

        /// <summary>
        /// chon cho dropdownlist, listbox, listcheckbox, list option
        /// </summary>
        /// <param name="control"></param>
        /// <param name="textValue"></param>
        public static void selectItemHavingText(ListControl control, string textContent)
        {
            control.SelectedIndex = -1;
            ListItem li = control.Items.FindByText(textContent);

            if (li != null)
            {
                li.Selected = true;
            }

        }

        /// <summary>
        /// Load du lieu cho : CheckListBox, DropDownList, RadioButtonList, BulletList ..
        /// tu : Array, ArrayList ..
        /// doi tuong:  string
        /// 
        /// string[] TestItems = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
        /// loadListControlFromCollection(TestItems, ListBox1);
        /// LoadListControlFromCollection(TestItems, CheckBoxList1);
        /// LoadListControlFromCollection(TestItems, DropDownList1);
        /// LoadListControlFromCollection(TestItems, RadioButtonList1);
        /// LoadListControlFromCollection(TestItems, BulletedList1);        
        /// </summary>
        /// <param name="data"></param>
        /// <param name="listControlControl"></param>
        public static void loadListControlFromCollection(ICollection data, ListControl listControlControl)
        {
            foreach (object item in data)
            {
                listControlControl.Items.Add(item.ToString());
            }
        }
    }
}
