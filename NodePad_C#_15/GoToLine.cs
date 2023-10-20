using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NodePad_C__15
{
    public partial class GoToLine : Form
    {
        private Label label1;
        private Button btn_goto;
        public NumericUpDown numericUpDown1;
        private ContextMenuStrip contextMenuStrip1;
        private System.ComponentModel.IContainer components;
        private Button btn_cancel;

        public int Length { get; }

        public GoToLine()
        {
            InitializeComponent();
        }

        public GoToLine(int length)
        {
            InitializeComponent();
            Length = length;

        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_goto = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Line  number:";
            // 
            // btn_goto
            // 
            this.btn_goto.Location = new System.Drawing.Point(67, 80);
            this.btn_goto.Name = "btn_goto";
            this.btn_goto.Size = new System.Drawing.Size(83, 23);
            this.btn_goto.TabIndex = 2;
            this.btn_goto.Text = "Go To";
            this.btn_goto.UseVisualStyleBackColor = true;
            this.btn_goto.Click += new System.EventHandler(this.btn_goto_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(180, 80);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(90, 23);
            this.btn_cancel.TabIndex = 3;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(15, 39);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(255, 22);
            this.numericUpDown1.TabIndex = 4;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // GoToLine
            // 
            this.AcceptButton = this.btn_goto;
            this.CancelButton = this.btn_cancel;
            this.ClientSize = new System.Drawing.Size(282, 117);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_goto);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.Name = "GoToLine";
            this.Text = "Go To Line";
            this.Load += new System.EventHandler(this.GoToLine_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void GoToLine_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;

            this.ActiveControl = numericUpDown1;
            numericUpDown1.Select(0, numericUpDown1.Text.Length);
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btn_goto_Click(object sender, EventArgs e)
        {
            if(int.Parse(this.numericUpDown1.Text) > Length + 1 || int.Parse(this.numericUpDown1.Text) == 0)
            {
                MessageBox.Show("The line number is beyond the total number of lines", "Notepad - GoTo Line", MessageBoxButtons.OK);
                numericUpDown1.Focus();
                numericUpDown1.Select(0, numericUpDown1.Text.Length);
            }
            else
            {
                this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
           
        }
    }
}
