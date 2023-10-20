using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NodePad_C__15
{

    public partial class Find : Form
    {
        private Label label1;
        private Button button2;
        private Button button3;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private GroupBox groupBox1;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        public TextBox textBox1;
        private Button button1;
        public string undoContent { get; set; }
        public event EventHandler<FoundTextEventArgs> FoundTextEvent;
        public string searchKey { set; get; } = "";
        public bool matchCase { set; get; }
        public bool WrapAround { set; get; }
        public string Content { get; }

        public Find()
        {
            InitializeComponent();
        }

        public Find(string content)
        {
            InitializeComponent();
            Content = content;
            this.ActiveControl = textBox1;
        }

        public Find(RichTextBox richTextBox1)
        {
             InitializeComponent();
            RichTextBox1 = richTextBox1;
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Find what:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(356, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Find Next";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(356, 48);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "Cancel";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 80);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(100, 20);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "Match Case";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(12, 106);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(107, 20);
            this.checkBox2.TabIndex = 4;
            this.checkBox2.Text = "Wrap around";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(186, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(139, 56);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Deriction";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(71, 28);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(62, 20);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "Down";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 28);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(46, 20);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Up";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(97, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(228, 22);
            this.textBox1.TabIndex = 6;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Find
            // 
            this.ClientSize = new System.Drawing.Size(448, 134);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Find";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Find";
            this.Load += new System.EventHandler(this.About_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();



        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void About_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }
        // Trong form tìm kiếm
        public void UpdateText(string newText)
        {
            RichTextBox1.Text = newText;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            searchKey = textBox1.Text;
        }
        public List<int> FoundIndexes { get; private set; } = new List<int>();
        public RichTextBox RichTextBox1 { get; }

        public int count { get; set; } = 0;
        public string searchKeyword = "";
        private void button2_Click(object sender, EventArgs e)
        {
           
             searchKeyword = textBox1.Text;
            bool wrapAround = checkBox2.Checked;
            bool matchCase = checkBox1.Checked;
            bool searchUp = radioButton1.Checked;
            int start = 0;
            int index;
                while (start < RichTextBox1.TextLength)
                {
                    if(checkBox1.Checked)
                {
                     index = RichTextBox1.Find(searchKeyword, start, RichTextBoxFinds.MatchCase);

                }else
                {

                     index = RichTextBox1.Find(searchKeyword, start, RichTextBoxFinds.None);

                }

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
                if(searchUp)
                {
                    if (count >= FoundIndexes.Count())
                    {
                        count = 0;

                    }
                    OnFoundTextEvent(new FoundTextEventArgs(FoundIndexes[count++]));
                }else
                {
                    if (count < 0)
                    {
                        count = FoundIndexes.Count() -1;

                    }
                    OnFoundTextEvent(new FoundTextEventArgs(FoundIndexes[count--]));
                }
               

            }
            else
            {
                string message = $"Can not find \"{searchKey}\"";
                string title = "Notepad";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Information);
            }

            FoundIndexes.Clear();

        }
        protected virtual void OnFoundTextEvent(FoundTextEventArgs e)
        {
            FoundTextEvent?.Invoke(this, e);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
