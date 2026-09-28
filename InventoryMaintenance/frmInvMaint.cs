using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryMaintenance
{
    public partial class frmInvMaint : Form
    {
        // Alex Eisenmann
        public frmInvMaint()
        {
            InitializeComponent();
        }

        private InvItemList invItems = new InvItemList();

        // Alex Eisenmann
        private void frmInvMaint_Load(object sender, EventArgs e)
        {
            invItems.Changed += new InvItemList.ChangeHandler(HandleChange);
            invItems.Fill();
            FillItemListBox();
        }

        // Alex Eisenmann
        private void FillItemListBox()
        {
            InvItem item;
            lstItems.Items.Clear();
            for (int i = 0; i < invItems.Count; i++)
            {
                item = invItems[i];
                lstItems.Items.Add(item.GetDisplayText());
            }
        }

        // Alex Eisenmann
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmNewItem newItemForm = new frmNewItem();
            InvItem invItem = newItemForm.GetNewItem();
            if (invItem != null)
            {
                Debug.WriteLine($"Item type: {invItem.GetType()}");
                Debug.WriteLine($"Item is InvItem: {invItem is InvItem}");
                Debug.WriteLine($"Item is IDisplayable: {invItem is IDisplayable}");

                invItems += invItem;
            }
        }

        // Alex Eisenmann
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int i = lstItems.SelectedIndex;
            if (i != -1)
            {
                InvItem invItem = invItems[i];
                string message = "Are you sure you want to delete "
                    + invItem.Description + "?";
                DialogResult button =
                    MessageBox.Show(message, "Confirm Delete",
                    MessageBoxButtons.YesNo);
                if (button == DialogResult.Yes)
                {
                    invItems -= invItem;
                }
            }
        }

        // Alex Eisenmann
        private void HandleChange(InvItemList invItems)
        {
            invItems.Save();
            FillItemListBox();
        }

        // Alex Eisenmann
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
