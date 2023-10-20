using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NodePad_C__15
{
    public partial class Replace : Form
    {
        public TextBox textBox1;
        private CheckBox checkBox2;
        private CheckBox checkBox1;
        private Button button3;
        private Button button2;
        private Label label1;
        public TextBox textBox2;
        private Label label2;
        private Button button4;
        private Button button5;
        private Button button1;

        public RichTextBox RichTextBox1 { get; }
        public event EventHandler<FoundTextEventArgs> FoundTextEvent;
        public event EventHandler<FoundTextEventArgs> ReplaceTextEvent;
        public event EventHandler<FoundTextEventArgs> ReplaceAllTextEvent;
        public Replace()
        {
            InitializeComponent();
        }


        public Replace(RichTextBox richTextBox1)
        {
            InitializeComponent();
            RichTextBox1 = richTextBox1;
        }

        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(103, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(217, 20);
            this.textBox1.TabIndex = 12;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(10, 125);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(88, 17);
            this.checkBox2.TabIndex = 11;
            this.checkBox2.Text = "Wrap around";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(10, 99);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(83, 17);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.Text = "Match Case";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(336, 41);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(90, 23);
            this.button3.TabIndex = 9;
            this.button3.Text = "Replace";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(336, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Find Next";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Find what:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(103, 45);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(217, 20);
            this.textBox2.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Replace with:";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(336, 99);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(89, 23);
            this.button4.TabIndex = 15;
            this.button4.Text = "Cancel";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(336, 70);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(90, 23);
            this.button5.TabIndex = 16;
            this.button5.Text = "Replace All";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // Replace
            // 
            this.ClientSize = new System.Drawing.Size(437, 167);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Replace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Replace";
            this.Load += new System.EventHandler(this.About_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        public void UpdateText(string newText)
        {
            RichTextBox1.Text = newText;
        }
        private void About_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        protected virtual void OnFoundTextEvent(FoundTextEventArgs e)
        {
            FoundTextEvent?.Invoke(this, e);
        }
        public List<int> FoundIndexes { get; set; } = new List<int>();
        public int count { get; set; } = 0;

        private void button2_Click(object sender, EventArgs e)
        {
            FoundIndexes.Clear();
            string searchKeyword = textBox1.Text;
            bool wrapAround = checkBox2.Checked;
            bool matchCase = checkBox1.Checked;
            int start = 0;

            while (start < RichTextBox1.TextLength)
            {
                int index = RichTextBox1.Find(searchKeyword, start, RichTextBoxFinds.MatchCase);

                if (index != -1)
                {
                    // Thêm index vào danh sách
                    FoundIndexes.Add(index);

                    // Di chuyển vị trí bắt đầu cho lần tìm kiếm tiếp theo
                    start = index + searchKeyword.Length;
                }
                else
                {
                    // Nếu không tìm thấy thì thoát khỏi vòng lặp
                    break;
                }
            }


            if (FoundIndexes.Any())
            {
                // Tìm thấy từ khóa, chọn và làm nổi bật
                // textBox1.Select(index, searchKeyword.Length);
                textBox1.Focus();
                // MessageBox.Show($"Keyword '{searchKeyword}' found at index {startIndex}.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (count >= FoundIndexes.Count())
                {
                    count = 0;

                }
                OnFoundTextEvent(new FoundTextEventArgs(FoundIndexes[count++]));


            }
            else
            {
                string message = $"Can not find \"{searchKeyword}\"";
                string title = "Notepad";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Information);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string searchKeyword = textBox1.Text;

            if (FoundIndexes.Any())
            {
                // Tìm thấy từ khóa, chọn và làm nổi bật
                // textBox1.Select(index, searchKeyword.Length);
                textBox1.Focus();
                // MessageBox.Show($"Keyword '{searchKeyword}' found at index {startIndex}.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (count >= FoundIndexes.Count())
                {
                    count = 0;

                }
                // count++;
                OnReplace2TextEvent(new FoundTextEventArgs(FoundIndexes[--count]));

            }
            else
            {
                string message = $"Can not find \"{searchKeyword}\"";
                string title = "Notepad";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Information);
            }

            FoundIndexes.Clear();
        }


        protected virtual void OnReplaceTextEvent(FoundTextEventArgs e)
        {
            FoundTextEvent?.Invoke(this, e);
        }


        protected virtual void OnReplace2TextEvent(FoundTextEventArgs e)
        {
            ReplaceTextEvent?.Invoke(this, e);
        }
        protected virtual void OnReplaceAllTextEvent(FoundTextEventArgs e)
        {
            ReplaceTextEvent?.Invoke(this, e);
        }
        private void button5_Click(object sender, EventArgs e)
        {

            string replaceText = textBox2.Text;
            // Thực hiện Replace All
         

            if (FoundIndexes.Any())
            {    // Lặp qua tất cả các index đã tìm thấy và thực hiện thay thế
                foreach (int index in FoundIndexes)
                {
                    RichTextBox1.Select(index, textBox1.Text.Length);

                    // Thực hiện replace
                    RichTextBox1.SelectedText = replaceText;

                    // Cập nhật lại vị trí con trỏ
                    RichTextBox1.SelectionStart = index + replaceText.Length;
                }
                /*

                  for (int index = 0; index <FoundIndexes.Count(); index++)
                 {
                     if(index != 0)
                     {
                         index += replaceText.Length;
                     }
                     // Chọn vị trí cần replace
                     RichTextBox1.Select(index, textBox1.Text.Length);

                     // Thực hiện replace
                     RichTextBox1.SelectedText = replaceText;


                 }


                 */
            }
            else
            {
                string message = $"Can not find \"{textBox1.Text}\"";
                string title = "Notepad";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Information);
            }
            FoundIndexes.Clear();
        }
    }

}
