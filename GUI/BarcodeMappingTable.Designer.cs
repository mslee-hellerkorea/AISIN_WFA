
namespace AISIN_WFA.GUI
{
    partial class BarcodeMappingTable
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BarcodeMappingTable));
            this.label1 = new System.Windows.Forms.Label();
            this.recipe_table = new System.Windows.Forms.DataGridView();
            this.Barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Recipe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Belt_width = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Belt_speed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.recipe_table)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(234, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(236, 17);
            this.label1.TabIndex = 24;
            this.label1.Text = "Barcode recipe mapping table setup";
            // 
            // recipe_table
            // 
            this.recipe_table.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.recipe_table.BackgroundColor = System.Drawing.SystemColors.Control;
            this.recipe_table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.recipe_table.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Barcode,
            this.Recipe,
            this.Belt_width,
            this.Belt_speed});
            this.recipe_table.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.recipe_table.Location = new System.Drawing.Point(9, 42);
            this.recipe_table.Margin = new System.Windows.Forms.Padding(2);
            this.recipe_table.Name = "recipe_table";
            this.recipe_table.RowTemplate.Height = 28;
            this.recipe_table.Size = new System.Drawing.Size(709, 149);
            this.recipe_table.TabIndex = 23;
            this.recipe_table.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.recipe_table_CellMouseDown);
            this.recipe_table.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            // 
            // Barcode
            // 
            this.Barcode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Barcode.HeaderText = "Barcode Pattern";
            this.Barcode.Name = "Barcode";
            // 
            // Recipe
            // 
            this.Recipe.HeaderText = "Recipe";
            this.Recipe.Name = "Recipe";
            // 
            // Belt_width
            // 
            this.Belt_width.HeaderText = "Rail Width (mm)";
            this.Belt_width.Name = "Belt_width";
            // 
            // Belt_speed
            // 
            this.Belt_speed.HeaderText = "Belt Speed (mm/min)";
            this.Belt_speed.Name = "Belt_speed";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 284);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(520, 39);
            this.label5.TabIndex = 30;
            this.label5.Text = resources.GetString("label5.Text");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 260);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(633, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "2. Belt Speed and Belt Width should not contain non-didital  or non-decimal value" +
    "s. Recipe name should not contain \".job\" or \".json\".";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 237);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(508, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "1.  If you only want to bind barcode with recipe, you can just leave Belt Width a" +
    "nd Belt Speed to be empty.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 207);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 24);
            this.label2.TabIndex = 27;
            this.label2.Text = "Tips:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(653, 333);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(65, 26);
            this.button1.TabIndex = 32;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // BarcodeMappingTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(727, 367);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.recipe_table);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "BarcodeMappingTable";
            this.Text = "BarcodeMappingTable";
            ((System.ComponentModel.ISupportInitialize)(this.recipe_table)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView recipe_table;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Barcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Recipe;
        private System.Windows.Forms.DataGridViewTextBoxColumn Belt_width;
        private System.Windows.Forms.DataGridViewTextBoxColumn Belt_speed;
    }
}