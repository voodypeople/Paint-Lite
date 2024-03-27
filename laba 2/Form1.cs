using System.Drawing.Drawing2D;
using System.Drawing;
using System.Drawing.Imaging;
using System;
using System.Windows.Forms;



namespace laba_2
{

    public partial class Form1 : Form
    {

        private Point mousePos1;
        private Point mousePos2;
        private DraggedFragment draggedFragment;
        private bool Checked_to_drag = false;
        //using callmet = Form1.newToolStripMenuItem_Click();
      
        int main_width, main_height;
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
            hScrollBar2.Minimum = 0;
            hScrollBar2.Maximum = 255;
            History = new List<Image>();
            main_width = this.Width; main_height = this.Height;
            picDrawingSurface.BackColor= Color.White;
            //this.picDrawingSurface.MouseWheel += picDrawingSurface_MoustWheel;

        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //main_width = this.Width; main_height = this.Height;
            checkedornot = true;
            History.Clear();
            historyCounter = 0;
            panel3.Width = main_width - main_width / 8; panel3.Height = main_height - main_height / 5;
            picDrawingSurface.Width = panel3.Width - panel3.Width / 10; picDrawingSurface.Height = panel3.Height - panel3.Height / 10;
            //Bitmap pic = new Bitmap(picDrawingSurface.Bounds.Width, picDrawingSurface.Bounds.Height);
            Bitmap pic = new Bitmap(panel3.Width - panel3.Width / 10, panel3.Height - panel3.Height / 10);
            
            picDrawingSurface.Image = pic;
            picDrawingSurface.BackColor = Color.White;
            History.Add(new Bitmap(picDrawingSurface.Image));
            label6.Text = picDrawingSurface.Bounds.Width.ToString() + ";" + picDrawingSurface.Bounds.Height.ToString();
            if (checkBox1.Checked == true) { checkBox1.Checked = false; Checked_to_drag = false; }
            //x = picDrawingSurface.Width;
            //y = picDrawingSurface.Height;
            //label6.Text = Convert.ToString(picDrawingSurface.Width-2)+ ";" + Convert.ToString( picDrawingSurface.Height-2);

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
                {
                    MessageBox.Show("Для начала создайте файл!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {

                    Image img = Image.FromFile(OP.FileName);
                    picDrawingSurface.Image = img;
                    label6.Text = picDrawingSurface.Bounds.Width.ToString() + ";" + picDrawingSurface.Bounds.Height.ToString();
                    
                    if(checkBox1.Checked == true) { checkBox1.Checked= false; Checked_to_drag = false; }
                    /*if (img.Width > x || img.Height > y)
                    {
                        MessageBox.Show("Измените размер окна или выберите другое изображение!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        picDrawingSurface.Image = img;
                    }
                    */
                }
                //image1 = new Bitmap(fd.FileName, true);
                

            }
            //picDrawingSurface.Dispose();
            //picDrawingSurface.SizeMode = PictureBoxSizeMode.StretchImage;

            //picDrawingSurface.AutoSize = true;
            
            
        }

        public void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(picDrawingSurface.Image == null)
            {
                MessageBox.Show("Сначала создайте файл!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information); return; 
            }
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
        private void picDrawingSurface_MoustWheel(object sender, MouseEventArgs e)
        {
            if(e.Delta> 0) 
            {
                picDrawingSurface.Width = picDrawingSurface.Width + 50;
                picDrawingSurface.Height = picDrawingSurface.Height + 50;
            
            }
            else
            {
                picDrawingSurface.Width = picDrawingSurface.Width - 50;
                picDrawingSurface.Height = picDrawingSurface.Height - 50;
            }
            //MessageBox.Show("wheel", "");
        }

        private void picDrawingSurface_MouseDown(object sender, MouseEventArgs e)
        {
            if (picDrawingSurface.Image == null)
            {

                MessageBox.Show("Сначала создайте новый файл!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if(picDrawingSurface.Image != null && checkBox1.Checked == true) {
                if (draggedFragment != null && !draggedFragment.Rect.Contains(e.Location))
                {
                    //уничтожаем фрагмент
                    draggedFragment = null;
                    picDrawingSurface.Invalidate();
                }
                return;
            }

            if (checkBox1.Checked == false)
            {
                if (e.Button == MouseButtons.Left)
                {
                    drawing = true;
                    oldLocation = e.Location;
                    currentPath = new GraphicsPath();
                }
            }
        }
        Rectangle GetRect(Point p1, Point p2)
        {
            var x1 = Math.Min(p1.X, p2.X);
            var x2 = Math.Max(p1.X, p2.X);
            var y1 = Math.Min(p1.Y, p2.Y);
            var y2 = Math.Max(p1.Y, p2.Y);
            return new Rectangle(x1, y1, x2 - x1, y2 - y1);
        }
        private void picDrawingSurface_MouseUp(object sender, MouseEventArgs e)
        {
            if(checkBox1.Checked == true) {

                if (mousePos1 != mousePos2)
                {
                    //создаем DraggedFragment
                    var rect = GetRect(mousePos1, mousePos2);
                    draggedFragment = new DraggedFragment() { SourceRect = rect, Location = rect.Location };
                }
                else
                {
                    //пользователь сдвинул фрагмент и отпутил мышь?
                    if (draggedFragment != null)
                    {
                        //фиксируем изменения в исходном изображении
                        draggedFragment.Fix(picDrawingSurface.Image);
                        //уничтожаем фрагмент
                        draggedFragment = null;
                        mousePos1 = mousePos2 = e.Location;
                    }
                }
                picDrawingSurface.Invalidate();
            }
            History.RemoveRange(historyCounter + 1, History.Count - historyCounter - 1);
            History.Add(new Bitmap(picDrawingSurface.Image));
            if (historyCounter + 1 < 100) historyCounter++;
            if (History.Count - 1 == 100) History.RemoveAt(0);
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
            //picDrawingSurface.Invalidate();
        }

        private void picDrawingSurface_MouseMove(object sender, MouseEventArgs e)
        {
            if(checkBox1.Checked == true) 
            {
                if (e.Button == MouseButtons.Left)
                {
                    //юзер тянет фрагмент?
                    if (draggedFragment != null)
                    {
                        //сдвигаем фрагмент
                        draggedFragment.Location.Offset(e.Location.X - mousePos2.X, e.Location.Y - mousePos2.Y);
                        mousePos1 = e.Location;
                    }
                    //сдвигаем выделенную область
                    mousePos2 = e.Location;
                    picDrawingSurface.Invalidate();
                }
                else
                {
                    mousePos1 = mousePos2 = e.Location;
                }
                return;
            }
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
            else MessageBox.Show("История пуста", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (historyCounter < History.Count - 1)
            {
                picDrawingSurface.Image = new Bitmap(History[++historyCounter]);
            }
            else MessageBox.Show("История пуста", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void hScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            Color col = currentPen.Color;
            currentPen.Color = Color.FromArgb(hScrollBar2.Value, col);

            label4.Text = Convert.ToString((100 * hScrollBar2.Value) / 255) + "%";
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void picDrawingSurface_SizeChanged(object sender, EventArgs e)
        {
            //label6.Text = picDrawingSurface.Bounds.Width.ToString() + ";" + picDrawingSurface.Bounds.Height.ToString();
            //label6.Text = Convert.ToString(picDrawingSurface.Width - 2) + ";" + Convert.ToString(picDrawingSurface.Height - 2);
        }

        private void eraserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(picDrawingSurface.Image == null)
            {
                Environment.Exit(0);
            }
            var result = MessageBox.Show("Сохранить изменения перед выходом?", "Информация", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if(result == DialogResult.Yes)
            {
                saveToolStripMenuItem_Click(null, null);
            }
            else
                Environment.Exit(0);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked && picDrawingSurface.Image != null) {
                Checked_to_drag = true;
                picDrawingSurface.Image = new Bitmap(History[historyCounter]);
            }
            //else { 
                
            //    //MessageBox.Show("Для начала создайте файл!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            //    return;
            //}
        }

        private void picDrawingSurface_Paint(object sender, PaintEventArgs e)
        {
            if (draggedFragment != null)
            {
                //рисуем вырезанное белое место
                e.Graphics.SetClip(draggedFragment.SourceRect);
                e.Graphics.Clear(Color.White);

                //рисуем сдвинутый фрагмент
                
                e.Graphics.SetClip(draggedFragment.Rect);
                e.Graphics.DrawImage(picDrawingSurface.Image, draggedFragment.Location.X - draggedFragment.SourceRect.X, draggedFragment.Location.Y - draggedFragment.SourceRect.Y);

                //рисуем рамку
                e.Graphics.ResetClip();
                ControlPaint.DrawFocusRectangle(e.Graphics, draggedFragment.Rect);
            }
            else
            {
                //если выделена область
                if (mousePos1 != mousePos2)
                    ControlPaint.DrawFocusRectangle(e.Graphics, GetRect(mousePos1, mousePos2));//рисуем рамку
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            main_width = this.Width; 
            main_height = this.Height;
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }
    }

    class DraggedFragment
    {
        public Rectangle SourceRect;//прямоугольник фрагмента в исходном изображении
        public Point Location;//положение сдвинутого фрагмента

        //прямоугольник сдвинутого фрагмента
        public Rectangle Rect
        {
            get { return new Rectangle(Location, SourceRect.Size); }
        }

        //фиксация изменений в исх изображении
        public void Fix(Image image)
        {
            using (var clone = (Image)image.Clone())
            using (var gr = Graphics.FromImage(image))
            {
                //рисуем вырезанное белое место
                gr.SetClip(SourceRect);
                //gr.Clear(Color.White);
                gr.Clear(Color.Transparent);
                //рисуем сдвинутый фрагмент
                gr.SetClip(Rect);
                gr.DrawImage(clone, Location.X - SourceRect.X, Location.Y - SourceRect.Y);
            }
        }
    }

    
}
