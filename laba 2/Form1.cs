using System.Drawing.Drawing2D;
using System.Drawing;
using System.Drawing.Imaging;

namespace laba_2
{

    public partial class Form1 : Form
    {
        //using callmet = Form1.newToolStripMenuItem_Click();
        private int x, y;
        bool drawing;
        GraphicsPath currentPath = null;
        Point oldLocation;
        public Image resizeimg;
        public Pen currentPen;
        Color historyColor;
        List<Image> History;
        int historyCounter;
        bool checkedornot = false;
        public Form1()
        {
            InitializeComponent();
            drawing = false;
            currentPen = new Pen(Color.Black);
            
            History = new List<Image>();
            

        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkedornot = true;
            History.Clear();
            historyCounter = 0;
            Bitmap pic = new Bitmap(picDrawingSurface.Width, picDrawingSurface.Height);
            picDrawingSurface.Image = pic;
            History.Add(new Bitmap(picDrawingSurface.Image));
            x = picDrawingSurface.Width;
            y = picDrawingSurface.Height;
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }

       
        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            picDrawingSurface.Visible = true;
            OpenFileDialog OP = new OpenFileDialog();
            OP.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif|PNG Image | *.png";
             OP.Title = "Open an Image File";
                        OP.FilterIndex = 1; //По умолчанию будет выбрано первое расширение *.jpg
                                            //И, когда пользователь укажет нужный путь к картинке, ее нужно будет загрузить в PictureBox:
            if (OP.ShowDialog() != DialogResult.Cancel)
            {


                if (checkedornot == false)
                    MessageBox.Show("Для начала создайте файл!");
                else
                {
                    
                    Image img = Image.FromFile(OP.FileName);
                    if (img.Width > x || img.Height > y)
                    {
                        MessageBox.Show("Измените размер окна или выберите другое изображение!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        picDrawingSurface.Image = img;
                    }
                }
                //image1 = new Bitmap(fd.FileName, true);
                

            }
            //picDrawingSurface.Dispose();
            //picDrawingSurface.SizeMode = PictureBoxSizeMode.StretchImage;

            //picDrawingSurface.AutoSize = true;
            
            
        }

        public void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveDlg = new SaveFileDialog();
            SaveDlg.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif|PNG Image | *.png";
            SaveDlg.Title = "Save an Image File";
            SaveDlg.FilterIndex = 4; //По умолчанию будет выбрано последнее расширение

            SaveDlg.ShowDialog();
            if (SaveDlg.FileName != "") //Если введено не пустое имя
            {
                System.IO.FileStream fs =
                (System.IO.FileStream)SaveDlg.OpenFile();
                switch (SaveDlg.FilterIndex)
                {
                    case 1:
                        this.picDrawingSurface.Image.Save(fs, ImageFormat.Jpeg);
                        break;
                    case 2:
                        this.picDrawingSurface.Image.Save(fs, ImageFormat.Bmp);
                        break;
                    case 3:
                        this.picDrawingSurface.Image.Save(fs, ImageFormat.Gif);
                        break;
                    case 4:
                        this.picDrawingSurface.Image.Save(fs, ImageFormat.Png);
                        break;
                }
                fs.Close();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About form = new About();
            form.Owner = this;
            form.ShowDialog();

        }

        private void picDrawingSurface_MouseDown(object sender, MouseEventArgs e)
        {
            if (picDrawingSurface.Image == null)
            {

                MessageBox.Show("Сначала создайте новый файл!");
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                drawing = true;
                oldLocation = e.Location;
                currentPath = new GraphicsPath();
            }
        }

        private void picDrawingSurface_MouseUp(object sender, MouseEventArgs e)
        {
            History.RemoveRange(historyCounter + 1, History.Count - historyCounter - 1);
            History.Add(new Bitmap(picDrawingSurface.Image));
            if (historyCounter + 1 < 10) historyCounter++;
            if (History.Count - 1 == 10) History.RemoveAt(0);
            drawing = false;
            try
            {
                currentPath.Dispose();
            }
            catch { };
            drawing = false;
            try
            {
                currentPath.Dispose();
            }
            catch { };
        }

        private void picDrawingSurface_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawing)
            {
                Graphics g = Graphics.FromImage(picDrawingSurface.Image);
                currentPath.AddLine(oldLocation, e.Location);
                g.DrawPath(currentPen, currentPath);
                oldLocation = e.Location;
                g.Dispose();
                picDrawingSurface.Invalidate();
            }

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            //resizeimg.Size = Size(100, 100);
            

        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            /*if (picDrawingSurface.Image == null)
            {
                MessageBox.Show("Сначала создайте файл!");
            }
            else
            {
                if (trackBar1.Value == 3)
                {
                    if (picDrawingSurface.Image == null)
                    {
                        return;
                    }
                    Bitmap imgbitmap = new Bitmap(resizeimg);
                    Image resizeimg2 = resizeImage(imgbitmap, picDrawingSurface.Size);

                    picDrawingSurface.Image = resizeimg2;
                    label1.Text = "100%";
                }
                if (trackBar1.Value == 2)
                {
                    if (picDrawingSurface.Image == null)
                    {
                        return;
                    }
                    Bitmap imgbitmap = new Bitmap(resizeimg);
                    Image resizeimg2 = resizeImage(imgbitmap, picDrawingSurface.Size / 2);

                    picDrawingSurface.Image = resizeimg2;
                    label1.Text = "50%";
                }
                if (trackBar1.Value == 1)
                {
                    if (picDrawingSurface.Image == null)
                    {
                        return;
                    }
                    Bitmap imgbitmap = new Bitmap(resizeimg);
                    Image resizeimg2 = resizeImage(imgbitmap, picDrawingSurface.Size / 3);

                    picDrawingSurface.Image = resizeimg2;
                    label1.Text = "33%";
                }
                if (trackBar1.Value == 0)
                {
                    if (picDrawingSurface.Image == null)
                    {
                        return;
                    }
                    Bitmap imgbitmap = new Bitmap(resizeimg);
                    Image resizeimg2 = resizeImage(imgbitmap, picDrawingSurface.Size / 4);

                    picDrawingSurface.Image = resizeimg2;
                    label1.Text = "25%";
                }
            }
            */
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            //MyDialog.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            //MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.
            

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                currentPen.Color = MyDialog.Color;
                //currentPen.DashCap = System.Drawing.Drawing2D.DashCap.Round;
                //currentPen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
            }
        }

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            currentPen.Width = hScrollBar1.Value;
            label3.Text = hScrollBar1.Value.ToString() + "%";
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (History.Count != 0 && historyCounter != 0)
            {
                picDrawingSurface.Image = new Bitmap(History[--historyCounter]);
            }
            else MessageBox.Show("История пуста");
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (historyCounter < History.Count - 1)
            {
                picDrawingSurface.Image = new Bitmap(History[++historyCounter]);
            }
            else MessageBox.Show("История пуста");
        }

        private void dotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            solidToolStripMenuItem.Checked = false;
            dashToolStripMenuItem.Checked = false;
            dotToolStripMenuItem.Checked = true;
        }

        private void dashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;

            solidToolStripMenuItem.Checked = false;
            dotToolStripMenuItem.Checked = false;
            dashToolStripMenuItem.Checked = true;
            
        }

        private void solidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            dotToolStripMenuItem.Checked = false;
            dashToolStripMenuItem.Checked = false;
            solidToolStripMenuItem.Checked = true;
        }

        private void picDrawingSurface_Click(object sender, EventArgs e)
        {

        }

        private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e)
        {

        }

        private void toolStripContainer1_BottomToolStripPanel_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void OnApplicationExit(object sender, EventArgs e)
        {
            Form3 form = new Form3();
            form.Owner = this;
            form.ShowDialog();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }
    }

    
    
}