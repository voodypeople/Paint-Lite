using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba_2
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            label1.Text = "Данное приложение позволяет\n" +
                "редактировать изображения\n" +
                "и рисовать их с помощью пера.\n\n" +
                "В программе присутствуют горячие\n" +
                "клавиши, находящиеся в меню.\n\n" +
                "Есть возможность изменять\n" +
                "цвет, ширину пера, толщину\n" +
                "и его прозрачность.";


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
