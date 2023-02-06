//-----------------------------------------------------------------------------
// BarcodeMappingTable.cs -- Barcode Mapping Table
//
// Author: MS Lee
// E-mail: mslee@hellerindustries.co.kr
// Tel:
//
// Edit History:
//
// 27-Jan-23  01.01.09.00   MSL Discard decimal point during gets rail setpoint.
//                              Discard decimal point during gets belt setpoint.
// 07-Feb-23  01.01.10.00   MSL Discard more than one decimal place during gets rail setpoint.
//-----------------------------------------------------------------------------
using AISIN_WFA.Models;
using AISIN_WFA.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AISIN_WFA.GUI
{
    public partial class BarcodeMappingTable : Form
    {
        public string beltSpeed = "-1";
        public string beltWidth = "-1";
        public ContextMenuStrip menuStrip = null;
        public string barcodeRecipePath = @"C:\Heller Industries\AISIN Line Comm\Bin\barcodeTable.json";
        public BarcodeMappingTable()
        {
            InitializeComponent();
            initializeTable();
        }

        public void saveParameter()
        {
            int rowCount = recipe_table.Rows.Count;
            List<barcodeRecipe> data = new List<barcodeRecipe>();
            for (int i = 0; i < rowCount - 1; i++)
            {
                if (tableValueLegal(i))
                {
                    barcodeRecipe temBar_recipe = new barcodeRecipe()
                    {
                        barcode = recipe_table.Rows[i].Cells[0].Value.ToString(),
                        recipe = recipe_table.Rows[i].Cells[1].Value.ToString(),
                        //beltWidth = beltWidth,
                        //beltSpeed = beltSpeed,
                        beltWidth = beltWidth,
                        beltSpeed = beltSpeed,
                    };
                    data.Add(temBar_recipe);
                }
                else
                {
                    return;
                }
            }
            //MessageBox.Show("BeltWidth1: ", recipe_table.Rows[0].Cells[2].Value.ToString());
            string jsonString = JsonConvert.SerializeObject(data, Formatting.Indented);
            globalFunctions.SerialToFile(jsonString, barcodeRecipePath);
            globalFunctions.initializeBarcodeRecipeTable();
            MessageBox.Show("Barcode recipe table is configured successfully.", "Barcode recipe table", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool tableValueLegal(int rowNumber)
        {
            object barcode_value = recipe_table.Rows[rowNumber].Cells[0].Value;
            object recipe_value = recipe_table.Rows[rowNumber].Cells[1].Value;
            object width_value = recipe_table.Rows[rowNumber].Cells[2].Value;
            object speed_value = recipe_table.Rows[rowNumber].Cells[3].Value;
            //MessageBox.Show("Width: " + width_value.ToString());

            // barcode value and recipe value cannot be null.
            if (barcode_value == null || recipe_value == null)
                return false;

            // width value and speed value can be null, but cannot be the illegal value.
            if (width_value == null)
            {
                beltWidth = "-1";
            }
            else
            {
                // 07-Feb-23  01.01.10.00   MSL Discard more than one decimal place during gets rail setpoint.
#if true
                if (!Regex.IsMatch(width_value.ToString(), @"^(0|([1-9]\d*))(\.\d)?$"))
                {
                    MessageBox.Show("Belt Width must be digital !\n configuration failed.", "Barcode recipe table", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
#else
                if (!Regex.IsMatch(width_value.ToString(), @"^(0|([1-9]\d*))(\.\d+)?$"))
                {
                    MessageBox.Show("Belt Width must be digital or decimal values !\nPage3 configuration failed.", "Barcode recipe table", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
#endif

                //float.TryParse(width_value.ToString(), out beltWidth);
                beltWidth = (string)width_value;
            }
            if (speed_value == null)
            {
                beltSpeed = "-1";
            }
            else
            {
                // 07-Feb-23  01.01.10.00   MSL Discard more than one decimal place during gets rail setpoint.

#if true
                if (!Regex.IsMatch(speed_value.ToString(), @"^(0|([1-9]\d*))(\.\d)?$"))
                {
                    MessageBox.Show("Belt Speed must be digital !\nConfiguration failed.", "Barcode recipe table", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
#else
                if (!Regex.IsMatch(speed_value.ToString(), @"^(0|([1-9]\d*))(\.\d+)?$"))
                {
                    MessageBox.Show("Belt Speed must be digital or decimal values !\nConfiguration failed.", "Barcode recipe table", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
#endif
                //beltSpeed = float.Parse(speed_value.ToString());
                //float.TryParse(speed_value.ToString(), out beltSpeed);
                //beltSpeed = (float)speed_value;
                beltSpeed = (string)speed_value;
            }
            return true;
        }


        private void Delete_Click(object sender, EventArgs e)
        {
            HLog.log(HLog.eLog.EVENT, $"User Mapping click Delete Button");
            int rowCount = recipe_table.Rows.Count;
            if (rowCount > 1 && recipe_table.CurrentRow != null)
                recipe_table.Rows.RemoveAt(recipe_table.CurrentRow.Index);
        }

        private void recipe_table_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.ColumnIndex > -1 && e.RowIndex > -1)
            {
                menuStrip = new ContextMenuStrip();

                menuStrip.Items.Add("Delete");
                menuStrip.Items[0].Click += new EventHandler(Delete_Click);

                // show the menu strip in the cell position
                menuStrip.Show(MousePosition.X, MousePosition.Y);
            }
            else
            {
                if (menuStrip != null)
                {
                    menuStrip.Close();
                    menuStrip = null;
                }
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.ContextMenu = new ContextMenu();
        }

        private void initializeTable()
        {
            if (File.Exists(barcodeRecipePath))
            {
                string JsonData = globalFunctions.DeserialDataFromFile(barcodeRecipePath);
                List<barcodeRecipe> list_bar_recipe = JsonConvert.DeserializeObject<List<barcodeRecipe>>(JsonData);
                int row = 0;
                foreach (barcodeRecipe bar_rec in list_bar_recipe)
                {
                    recipe_table.Rows.Add();
                    recipe_table.Rows[row].Cells[0].Value = bar_rec.barcode;
                    recipe_table.Rows[row].Cells[1].Value = bar_rec.recipe;
                    if (Convert.ToSingle(bar_rec.beltWidth) == -1)
                        recipe_table.Rows[row].Cells[2].Value = null;
                    else
                        recipe_table.Rows[row].Cells[2].Value = bar_rec.beltWidth;
                    if (Convert.ToSingle(bar_rec.beltSpeed) == -1)
                        recipe_table.Rows[row].Cells[3].Value = null;
                    else
                        recipe_table.Rows[row].Cells[3].Value = bar_rec.beltSpeed;
                    row++;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HLog.log(HLog.eLog.EVENT, $"User Click Mapping Table Save Button");
            saveParameter();
            Close();
        }
    }
}
