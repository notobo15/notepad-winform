using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace NodePad_C__15
{
    public partial class Form1 : Form
    {
        public bool isSaved { get; set; } = true;
        private Find findFormInstance;
        private Replace FormReplace;

        private Stack<string> undoStack = new Stack<string>();// Lấy font hiện tại;
        public string font { get; set; } = "Arial";
        public string style { get; set; } = "Regular";
        public int size { get; set; } = 11;
        public string content { get; set; } = "";
        public float zoom { get; set; } = 1f;
        public string pathfileopen { get; set; }
        public Form1()
        {
            InitializeComponent();
            richTextBox1.MouseWheel += RichTextBox1_MouseWheel;
        }

        private void RichTextBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            bool ctrlPressed = Control.ModifierKeys.HasFlag(Keys.Control);

            if (ctrlPressed)
            {
                // Lấy giá trị delta của chuột
                int delta = e.Delta;

                // Kiểm tra xem có phải lăn lên hay lăn xuống
                if (delta > 0)
                {
                    // Lăn lên - tăng phần trăm
                    IncreaseZoom();
                }
                else
                {
                    // Lăn xuống - giảm phần trăm
                    DecreaseZoom();
                }
            }

            // Cập nhật label với giá trị phần trăm hiện tại
            //UpdateZoomLabel();
        }
        private void IncreaseZoom()
        {
            richTextBox1.ZoomFactor += 0.1f;
         

            // Đảm bảo ZoomFactor không vượt quá 500%
            if (richTextBox1.ZoomFactor > 5.0f)
            {
                richTextBox1.ZoomFactor = 5.0f;
            }
            zoom = richTextBox1.ZoomFactor;
            // Cập nhật label với giá trị phần trăm hiện tại
            UpdateZoomLabel();
        }

        private void DecreaseZoom()
        {

                if (richTextBox1.ZoomFactor <= 0.2f)
                {
                    richTextBox1.ZoomFactor = 0.1f;
                }else
                {
                    richTextBox1.ZoomFactor -= 0.1f;
                }
            zoom = richTextBox1.ZoomFactor;
            // Cập nhật label với giá trị phần trăm hiện tại
            UpdateZoomLabel();
        }
        private void UpdateZoomLabel()
        {
            // Hiển thị giá trị phần trăm trên label
            lb_scale.Text = $"Zoom: {richTextBox1.ZoomFactor * 100}%";
        }
        public void ExitApplication()
        {
            if (richTextBox1.Text.Count() > 0)
            {
                // Hiển thị hộp thoại xác nhận lưu
                var result = MessageBox.Show("Do you want to save the current document before exiting?", "Save Document", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Người dùng muốn lưu trước khi thoát
                    SaveFileDialog saveDialog = new SaveFileDialog();
                    saveDialog.Filter = "Text Document (*.txt)|*.txt|All Files (*.*)|*.*";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Lưu nội dung vào tệp tin đã chọn
                        try
                        {
                            richTextBox1.SaveFile(saveDialog.FileName, RichTextBoxStreamType.PlainText);
                            return;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error saving file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    // Thoát khỏi ứng dụng
                    this.Hide();
                    // Không cần làm trắng nội dung, vì chúng ta đã lưu
                }
                else if (result == DialogResult.Cancel)
                {
                }
                else if (result == DialogResult.No)
                {
                    // Thoát khỏi ứng dụng
                    this.Hide();
                }
            }

          
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ExitApplication();

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitApplication();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //richTextBox1.Clear();
            if (!string.IsNullOrEmpty(richTextBox1.Text))
            {
                // Hiển thị hộp thoại xác nhận lưu
                DialogResult result = MessageBox.Show("Do you want to save changes to Untitled?", "Save Document", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Người dùng muốn lưu trước khi tạo mới
                    SaveFileDialog saveDialog = new SaveFileDialog();
                    saveDialog.Filter = "Text Document (*.txt)|*.txt|All Files (*.*)|*.*";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Lưu nội dung vào tệp tin đã chọn
                        richTextBox1.SaveFile(saveDialog.FileName, RichTextBoxStreamType.PlainText);

                        // Làm trắng nội dung
                        richTextBox1.Clear();
                    }
                }
                else if (result == DialogResult.No)
                {
                    // Người dùng không muốn lưu, làm trắng nội dung
                    richTextBox1.Clear();
                }
                // Nếu người dùng chọn Cancel, không thực hiện bất kỳ thay đổi nào
            }
            else
            {
                // Nếu Notepad không có nội dung, chỉ làm trắng nội dung
                richTextBox1.Clear();
            }
        }

        private void newWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 newForm = new Form1(); // Tạo một thể hiện mới của form
            newForm.Show(); // Hiển thị form mới
        }

        private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //ExitApplication();
            if(!isSaved)
            {
                var result = MessageBox.Show("Do you want to save the current?", "Save Document", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                  
                    try
                    {
                        richTextBox1.SaveFile(pathfileopen, RichTextBoxStreamType.PlainText);
                        currentFilePath =  Path.GetFileName(pathfileopen);
                        this.Text = currentFilePath + " - Notepad";
                        isSaved = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error saving file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                  
            }
          


            }


            OpenFileDialog rishav = new OpenFileDialog();
            rishav.Title = "Open";
            rishav.Filter = "Text Document(*.txt)|*.txt|All File(*.*)|*.*";
            if (rishav.ShowDialog() == DialogResult.OK)
                richTextBox1.LoadFile(rishav.FileName, RichTextBoxStreamType.PlainText);
            richTextBox1.ZoomFactor += zoom;
            pathfileopen = rishav.FileName;
            string filename_with_ext = Path.GetFileName(rishav.FileName);
            currentFilePath = filename_with_ext; // Cập nhật tên và vị trí tệp hiện tại
            this.Text = currentFilePath + " - Notepad";

        }

        private string currentFilePath = "untitled"; // Đặt biến currentFilePath ở mức lớp
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentFilePath != "untitled")
            {
                // Nếu đã lưu tệp trước đó, thực hiện lưu nội dung vào tệp hiện tại
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(currentFilePath, false, System.Text.Encoding.UTF8))
                {
                    sw.Write(richTextBox1.Text);
                    this.Text = currentFilePath + " - Notepad";
                    isSaved = true;
                }

            }
            else
            {
                // Nếu chưa lưu tệp trước đó, sử dụng "Save As" để chọn vị trí và tên tệp
                saveAsToolStripMenuItem_Click(sender, e);
            }
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog rishav = new SaveFileDialog();
            rishav.Title = "Save As";
            rishav.Filter = "Text Document(*.txt)|*.txt|All Files(*.*)|*.*";

            if (rishav.ShowDialog() == DialogResult.OK)
            {
                // Lưu nội dung vào tệp được chọn
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(rishav.FileName, false, System.Text.Encoding.UTF8))
                {
                    sw.Write(richTextBox1.Text);
                }
                string filename_with_ext = Path.GetFileName(rishav.FileName);
                currentFilePath = filename_with_ext; // Cập nhật tên và vị trí tệp hiện tại
                this.Text = currentFilePath + " - Notepad";
                isSaved = true;
            }
        }

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PageSetupDialog pageSetupDialog = new PageSetupDialog();
            pageSetupDialog.PageSettings = new System.Drawing.Printing.PageSettings();
            pageSetupDialog.PrinterSettings = new System.Drawing.Printing.PrinterSettings();

            if (pageSetupDialog.ShowDialog() == DialogResult.OK)
            {
                // Lấy thông tin thiết lập in từ hộp thoại Page Setup
                System.Drawing.Printing.PageSettings pageSettings = pageSetupDialog.PageSettings;
                System.Drawing.Printing.PrinterSettings printerSettings = pageSetupDialog.PrinterSettings;

                // Bạn có thể sử dụng thông tin này để điều chỉnh cách in văn bản trong ứng dụng của bạn.
                // Ví dụ: Đặt giấy in, cỡ giấy, hướng in, v.v.

                // Thực hiện các xử lý thích hợp dựa trên thông tin Page Setup
            }
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(PrintPageHandler);

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }
        private void PrintPageHandler(object sender, PrintPageEventArgs e)
        {
            string text = richTextBox1.Text; // Lấy nội dung từ richTextBox1
            Font printFont = richTextBox1.Font; // Lấy font từ richTextBox1

            // Vẽ văn bản lên trang in
            e.Graphics.DrawString(text, printFont, Brushes.Black, 10, 10);
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
           
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
            isSaved = false;
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                richTextBox1.Paste();
                isSaved = false;
                richTextBox1.ForeColor = SystemColors.ControlText;
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = string.Empty;
        }

        private void searchWithBingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selectedText = richTextBox1.SelectedText;

            if (!string.IsNullOrWhiteSpace(selectedText))
            {
                // Sử dụng thư viện System.Diagnostics để mở trình duyệt web mặc định với truy vấn tìm kiếm trên Bing.
                System.Diagnostics.Process.Start("https://www.bing.com/search?q=" + Uri.EscapeDataString(selectedText));
            }
        }
        private int currentIndex = 0;
        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (findFormInstance == null || findFormInstance.IsDisposed)
            {
                findFormInstance = new Find(richTextBox1);
                findFormInstance.FoundTextEvent += FindForm_FoundTextEvent;
                findFormInstance.Show(this);
            }
            else
            {
                // Kiểm tra xem form đang ẩn hay không
                if (findFormInstance.Visible)
                {
                    // Nếu đang hiển thị, đưa form lên phía trước
                    findFormInstance.BringToFront();
                }
                else
                {
                    // Nếu đang ẩn, hiển thị lại form
                    findFormInstance.Show(this);
                }
            }

        }
        public List<int> FoundIndexes { get; private set; } = new List<int>();
        
        public void updateFoundIndexes(string selected)
        {
            FoundIndexes.Clear();
            int start = 0;
            while (start < richTextBox1.TextLength)
            {
                int index = richTextBox1.Find(selected, start, RichTextBoxFinds.MatchCase);

                if (index != -1)
                {
                    // Thêm index vào danh sách
                    FoundIndexes.Add(index);

                    // Di chuyển vị trí bắt đầu cho lần tìm kiếm tiếp theo
                    start = index + selected.Length;
                }
                else
                {
                    // Nếu không tìm thấy thì thoát khỏi vòng lặp
                    break;
                }
            }

        }
        public int countFindNext = 0;
        private void findNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selected = richTextBox1.SelectedText;
            int selectionIndex = richTextBox1.SelectionStart;
            int selectionLength = richTextBox1.SelectionLength;
            updateFoundIndexes(selected);
           for(var i = 0; i < FoundIndexes.Count(); i++)
            {
                if (FoundIndexes[i].Equals(selectionIndex))
                {
                    countFindNext = i;
                    break;
                }
            }
            countFindNext++;
            if (countFindNext >= FoundIndexes.Count())
            {
                countFindNext = 0;
            }
          

            richTextBox1.Select(FoundIndexes[countFindNext], selectionLength);
            richTextBox1.ScrollToCaret();  // Cuộn đến vị trí bôi đen
            richTextBox1.Focus();  // Tập trung vào RichTextBox
            richTextBox1.SelectionStart = FoundIndexes[countFindNext];
            

        }

        private void findPreviousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selected = richTextBox1.SelectedText;
            int selectionIndex = richTextBox1.SelectionStart;
            int selectionLength = richTextBox1.SelectionLength;
            updateFoundIndexes(selected);
            for (var i = 0; i < FoundIndexes.Count(); i++)
            {
                if (FoundIndexes[i].Equals(selectionIndex))
                {
                    countFindNext = i;
                    break;
                }
            }
            countFindNext--;
            if (countFindNext < 0)
            {
                countFindNext = FoundIndexes.Count() -1;
            }


            richTextBox1.Select(FoundIndexes[countFindNext], selectionLength);
            richTextBox1.ScrollToCaret();  // Cuộn đến vị trí bôi đen
            richTextBox1.Focus();  // Tập trung vào RichTextBox
            richTextBox1.SelectionStart = FoundIndexes[countFindNext];

        }

        private void replaceToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (FormReplace == null || FormReplace.IsDisposed)
            {
                FormReplace = new Replace(richTextBox1);
                FormReplace.FoundTextEvent += FindForm_FoundTextEvent;
                FormReplace.ReplaceTextEvent += FormReplace_FoundTextEvent;
                FormReplace.ReplaceTextEvent += FormReplaceAll_FoundTextEvent;
                FormReplace.Show(this);
            }
            else
            {
                // Kiểm tra xem form đang ẩn hay không
                if (FormReplace.Visible)
                {
                    // Nếu đang hiển thị, đưa form lên phía trước
                    FormReplace.BringToFront();
                }
                else
                {
                    // Nếu đang ẩn, hiển thị lại form
                    FormReplace.Show(this);
                }
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void timeDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dateTimeString = System.DateTime.Now.ToString();
            int currentPosition = richTextBox1.SelectionStart;

            // Chèn thời gian vào vị trí con trỏ hiện tại
            richTextBox1.Text = richTextBox1.Text.Insert(currentPosition, dateTimeString);
            richTextBox1.SelectionStart = currentPosition + dateTimeString.Length;

            richTextBox1.Focus();
        }

        private void wordWarpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // richTextBox1.WordWrap = !richTextBox1.WordWrap; // Chuyển đổi giá trị của WordWrap
            if(richTextBox1.WordWrap)
            {
                richTextBox1.WordWrap = false;
                wordWarpToolStripMenuItem.Checked = false;
            }
            else
            {
                richTextBox1.WordWrap = true;
                wordWarpToolStripMenuItem.Checked = true;
            }
           
          

        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            fontDialog.Font = richTextBox1.Font; // Đặt font mặc định là font hiện tại của RichTextBox

            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionFont = fontDialog.Font;
            }
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Tăng kích thước font lên 2 đơn vị(hoặc bất kỳ giá trị nào bạn muốn).
            richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, richTextBox1.Font.Size + 2);


        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(richTextBox1.Font.Size < 10)
            {
                richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, 9);

            }
            else
            {
                richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, richTextBox1.Font.Size - 2);

            }
        }

        private void restoreDefaultZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Đặt kích thước font trở về mặc định (ở đây là 8).
            richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, 8);
        }

        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Đường dẫn URL bạn muốn mở
            string url = "https://www.bing.com/search?q=get+help+with+notepad+in+windows&filters=guid:%224466414-en-dia%22%20lang:%22en%22&form=T00032&ocid=HelpPane-BingIA";

            // Mở đường dẫn URL trong trình duyệt web mặc định
            System.Diagnostics.Process.Start(url);
        }

        private void sendFeedBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void aboutNodePadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var about = new About();
            about.ShowDialog();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
           // undoStack.Push(richTextBox1.Text);
         
            richTextBox1.ForeColor = SystemColors.ControlText;
           // content = richTextBox1.Text;
            //  undoStack.Push(richTextBox1.Text);
            findFormInstance?.UpdateText(richTextBox1.Text);
            FormReplace?.UpdateText(richTextBox1.Text);


          
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void fontToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
         
        }

        private void zoomToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        ToolStripMenuItem menuItemPaste;
        ToolStripMenuItem menuItemCut;
        ToolStripMenuItem menuItemCopy; 
        ToolStripMenuItem menuItemDelete;
        ToolStripMenuItem SearchWithBing;
        ToolStripMenuItem menuRightToLeft; 
        private void Form1_Load(object sender, EventArgs e)
        {

            // Đối với biểu tượng lưu trong thư mục dự án
            this.Icon = new Icon("notepad.ico");
            Font currentFont = richTextBox1.SelectionFont;

            contextMenuStrip1.RenderMode = ToolStripRenderMode.System;
            // Thêm các mục menu vào ContextMenuStrip
            var menuItemUndo = new ToolStripMenuItem("Undo");
            menuItemUndo.Click += PasteToolStripMenuItem_Click;
            contextMenuStrip1.Items.Add(menuItemUndo);
            contextMenuStrip1.Items.Add(new ToolStripSeparator());
             menuItemCut = new ToolStripMenuItem("Cut");
            menuItemCut.Click += CutToolStripMenuItem_Click;
            contextMenuStrip1.Items.Add(menuItemCut);

             menuItemCopy = new ToolStripMenuItem("Copy");
            menuItemCopy.Click += CopyToolStripMenuItem_Click;
            contextMenuStrip1.Items.Add(menuItemCopy);

             menuItemPaste = new ToolStripMenuItem("Paste");
            menuItemPaste.Click += PasteToolStripMenuItem_Click;
            contextMenuStrip1.Items.Add(menuItemPaste);


            menuItemDelete = new ToolStripMenuItem("Delete");
            menuItemDelete.Click += PasteToolStripMenuItem_Click;
            contextMenuStrip1.Items.Add(menuItemDelete);

            contextMenuStrip1.Items.Add(new ToolStripSeparator());

            var menuItemSelectAll = new ToolStripMenuItem("SelectAll");
            menuItemSelectAll.Click += selectAllToolStripMenuItem_Click;
            contextMenuStrip1.Items.Add(menuItemSelectAll);

            contextMenuStrip1.Items.Add(new ToolStripSeparator());

            menuRightToLeft = new ToolStripMenuItem("Right to left Reading order");
            menuRightToLeft.Click += RightToLeft_Click;
            contextMenuStrip1.Items.Add(menuRightToLeft);

            var showUnicode = new ToolStripMenuItem("Show Unicode control charater");
            //showUnicode.Click += PasteToolStripMenuItem_Click;
            contextMenuStrip1.Items.Add(showUnicode);


            var insertUnicode = new ToolStripMenuItem("Insert Unicode control charater");
            insertUnicode.Click += PasteToolStripMenuItem_Click;
            contextMenuStrip1.Items.Add(insertUnicode);


            ToolStripMenuItem LRM = new ToolStripMenuItem("LRM");
            LRM.ShortcutKeyDisplayString = "Left-to-right mark";
            //LRM.Click += LRM_Click;
            insertUnicode.DropDownItems.Add(LRM);

            ToolStripMenuItem RLM = new ToolStripMenuItem("RLM");
            //menuItemAddLine.Click += AddLineToolStripMenuItem_Click;
            insertUnicode.DropDownItems.Add(RLM);
            ToolStripMenuItem ZWJ = new ToolStripMenuItem("ZWJ");
            //menuItemAddLine.Click += AddLineToolStripMenuItem_Click;
            insertUnicode.DropDownItems.Add(ZWJ);
            ToolStripMenuItem ZWNJ = new ToolStripMenuItem("ZWNJ");
            //menuItemAddLine.Click += AddLineToolStripMenuItem_Click;
            insertUnicode.DropDownItems.Add(ZWNJ);
            ToolStripMenuItem LRE = new ToolStripMenuItem("LRE");
            //menuItemAddLine.Click += AddLineToolStripMenuItem_Click;
            insertUnicode.DropDownItems.Add(LRE);
            ToolStripMenuItem RLE = new ToolStripMenuItem("RLE");
            //menuItemAddLine.Click += AddLineToolStripMenuItem_Click;
            insertUnicode.DropDownItems.Add(RLE);
            ToolStripMenuItem LRO = new ToolStripMenuItem("LRO");
            //menuItemAddLine.Click += AddLineToolStripMenuItem_Click;
            insertUnicode.DropDownItems.Add(LRO);
            ToolStripMenuItem RLO = new ToolStripMenuItem("RLO");
            //menuItemAddLine.Click += AddLineToolStripMenuItem_Click;
            insertUnicode.DropDownItems.Add(RLO);
            ToolStripMenuItem PDF = new ToolStripMenuItem("PDF");
            //menuItemAddLine.Click += AddLineToolStripMenuItem_Click;
            insertUnicode.DropDownItems.Add(PDF);


            ToolStripMenuItem NADS = new ToolStripMenuItem("NADS");
            //menuItemAddLine.Click += AddLineToolStripMenuItem_Click;
            insertUnicode.DropDownItems.Add(NADS);

            ToolStripMenuItem NODS = new ToolStripMenuItem("NODS");
            //menuItemAddLine.Click += AddLineToolStripMenuItem_Click;
            insertUnicode.DropDownItems.Add(NODS);

            ToolStripMenuItem ASS = new ToolStripMenuItem("ASS");
            //menuItemAddLine.Click += AddLineToolStripMenuItem_Click;
            insertUnicode.DropDownItems.Add(ASS);

            ToolStripMenuItem ISS = new ToolStripMenuItem("ISS");
            //menuItemAddLine.Click += AddLineToolStripMenuItem_Click;
            insertUnicode.DropDownItems.Add(ISS);

            ToolStripMenuItem AAFS = new ToolStripMenuItem("AAFS");
            //menuItemAddLine.Click += AddLineToolStripMenuItem_Click;
            insertUnicode.DropDownItems.Add(AAFS);

            ToolStripMenuItem IAFS = new ToolStripMenuItem("(IAFS");
            //menuItemAddLine.Click += AddLineToolStripMenuItem_Click;
            insertUnicode.DropDownItems.Add(IAFS);

            ToolStripMenuItem RS = new ToolStripMenuItem("RS");
            //menuItemAddLine.Click += AddLineToolStripMenuItem_Click;
            insertUnicode.DropDownItems.Add(RS);
            ToolStripMenuItem US = new ToolStripMenuItem("US");
            US.ShortcutKeyDisplayString = "Unit Separator (Segment separator)";
            //menuItemAddLine.Click += AddLineToolStripMenuItem_Click;
            insertUnicode.DropDownItems.Add(US);

            contextMenuStrip1.Items.Add(new ToolStripSeparator());

            var closeIME = new ToolStripMenuItem("Close IME");
            //closeIME.Click += PasteToolStripMenuItem_Click;
            contextMenuStrip1.Items.Add(closeIME);



            var Reconversion = new ToolStripMenuItem("Reconversion");
            //Reconversion.Click += PasteToolStripMenuItem_Click;
            contextMenuStrip1.Items.Add(Reconversion);
            Reconversion.Enabled = false;
            contextMenuStrip1.Items.Add(new ToolStripSeparator());

            SearchWithBing = new ToolStripMenuItem("Search with Bing...");
            SearchWithBing.Click += searchWithBingToolStripMenuItem_Click;
            contextMenuStrip1.Items.Add(SearchWithBing);



            // Gán ContextMenuStrip cho RichTextBox
            richTextBox1.ContextMenuStrip = contextMenuStrip1;
            richTextBox1.ForeColor = Color.Black;

        }

        private void RightToLeft_Click(object sender, EventArgs e)
        {
            if(richTextBox1.RightToLeft == System.Windows.Forms.RightToLeft.Yes)
            {
                richTextBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
                menuRightToLeft.Checked = false;
            }
            else
            {
                menuRightToLeft.Checked = true;
                richTextBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            }
        
        }

        private void FindForm_FoundTextEvent(object sender, FoundTextEventArgs e)
        {
            if (findFormInstance != null)
            {
                // Xử lý vị trí tìm thấy từ sự kiện và bôi đen nó trong RichTextBox
                richTextBox1.Select(e.FoundIndex, findFormInstance.textBox1.Text.Length);
            }
            if(FormReplace !=null)
            {
                richTextBox1.Select(e.FoundIndex, FormReplace.textBox1.Text.Length);

            }
            // richTextBox1.ScrollToCaret();  // Cuộn đến vị trí bôi đen
            richTextBox1.Focus();  // Tập trung vào RichTextBox
        }
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
            isSaved = false;
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
            isSaved = false;
        }


        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            int selectionStart = richTextBox1.SelectionStart;
            int line = richTextBox1.GetLineFromCharIndex(selectionStart) + 1; // Số dòng (bắt đầu từ 1)

            // Số cột là vị trí hiện tại trong dòng (1-indexed)
            int column = selectionStart - richTextBox1.GetFirstCharIndexFromLine(line - 1) + 1;

            lb_count_row_col.Text = $"Ln {line}, Col {column}";

                string selectedText = richTextBox1.SelectedText;
            if(selectedText.Count() > 0)
            {
                
                deleteToolStripMenuItem.Enabled = true;
                searchWithBingToolStripMenuItem.Enabled = true;

                deleteToolStripMenuItem.Enabled = true;
                cutToolStripMenuItem.Enabled = true;
                copyToolStripMenuItem.Enabled = true;
            }
            else
            {
                searchWithBingToolStripMenuItem.Enabled = false;
                deleteToolStripMenuItem.Enabled = false;
                deleteToolStripMenuItem.Enabled = false;
                cutToolStripMenuItem.Enabled = false;
                copyToolStripMenuItem.Enabled = false;
            }

            if(richTextBox1.Text.Length > 0)
            {
                findNextToolStripMenuItem.Enabled = true;
                findToolStripMenuItem.Enabled = true;
                findPreviousToolStripMenuItem.Enabled = true;
            }
            else
            {
                findToolStripMenuItem.Enabled = false;
                findPreviousToolStripMenuItem.Enabled = false;

                findNextToolStripMenuItem.Enabled = false;

            }
        }

        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(statusBarToolStripMenuItem.Checked)
            {
                tableLayoutPanel1.Visible = false;
                statusBarToolStripMenuItem.Checked = false;
            }else
            {
                tableLayoutPanel1.Visible = true;
                statusBarToolStripMenuItem.Checked = true;
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void goToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var gotoline = new GoToLine(richTextBox1.Lines.Length);
            gotoline.ShowDialog();
            if (int.TryParse(gotoline.numericUpDown1.Text, out int lineNumber))
            {
                // Đảm bảo số dòng nằm trong giới hạn hợp lý
                lineNumber = Math.Max(1, Math.Min(lineNumber, richTextBox1.Lines.Length));

                // Tính vị trí bắt đầu của số dòng
                int position = richTextBox1.GetFirstCharIndexFromLine(lineNumber - 1);

                // Nếu số dòng là hợp lệ, di chuyển con trỏ đến vị trí đó
                if (position >= 0)
                {
                    //richTextBox1.SelectionStart = position;
                   // richTextBox1.SelectionLength = 0;
                  //  richTextBox1.ScrollToCaret();
                }
            }
            else
            {
                // Thông báo nếu nhập liệu không phải là số
                MessageBox.Show("Vui lòng nhập một số dòng hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem Clipboard có chứa văn bản hay không
            bool clipboardHasText = Clipboard.ContainsText();

            // Cập nhật trạng thái của nút Paste
            pasteToolStripMenuItem.Enabled = clipboardHasText;
          
        }

        private void richTextBox1_MouseDown(object sender, MouseEventArgs e)
        {
            // Kiểm tra xem có phải là chuột phải không
            if (e.Button == MouseButtons.Right)
            {
                bool clipboardHasText = Clipboard.ContainsText();
                menuItemPaste.Enabled = clipboardHasText;
                string selectedText = richTextBox1.SelectedText;
                if (selectedText.Count() > 0)
                {

                    menuItemDelete.Enabled = true;
                    menuItemCut.Enabled = true;
                    menuItemCopy.Enabled = true;
                    SearchWithBing.Enabled = true;
                }
                else
                {
                    menuItemDelete.Enabled = false;
                    menuItemCut.Enabled = false;
                    menuItemCopy.Enabled = false;
                    SearchWithBing.Enabled = false;

                }

            }
        }

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.Text = "*" + currentFilePath + " - Notepad";
            isSaved = false;
            if (char.IsLetterOrDigit(e.KeyChar) || char.IsPunctuation(e.KeyChar) || char.IsWhiteSpace(e.KeyChar))
            {
                // Di chuyển con trỏ về cuối
                richTextBox1.SelectionStart = richTextBox1.TextLength;
                richTextBox1.ScrollToCaret();
            }

        }
        //
        private void FormReplace_FoundTextEvent(object sender, FoundTextEventArgs e)
        {
            int count = 0;
            richTextBox1.Select(e.FoundIndex, FormReplace.textBox1.Text.Length);
            richTextBox1.SelectedText = FormReplace.textBox2.Text;
            FormReplace.count++;
            FormReplace.count = count;
            richTextBox1.ScrollToCaret();  // Cuộn đến vị trí bôi đen
            richTextBox1.Focus();  // Tập trung vào RichTextBox
            if (count >= FoundIndexes.Count())
            {
                count = 0;

            }
            richTextBox1.SelectionStart = FormReplace.FoundIndexes[FormReplace.count];
            richTextBox1.SelectionLength = FormReplace.textBox2.Text.Length; // Đặt độ dài là 0 để thay thế không gian trắng
           // richTextBox1.
            // Gán giá trị mới cho vị trí được chọn
          
            if(e.FoundIndex == 0)
            {

                string replaceText = FormReplace.textBox2.Text;
                // Thực hiện Replace All
                foreach (int index in FormReplace.FoundIndexes)
                {
                    richTextBox1.Select(index, FormReplace.textBox1.Text.Length);
                    richTextBox1.SelectedText = replaceText;
                }
                FormReplace.FoundIndexes.Clear();

            }
        }
        private void FormReplaceAll_FoundTextEvent(object sender, FoundTextEventArgs e)
        {
         //   richTextBox1.Text = "131312 ";
        }
    }
}
