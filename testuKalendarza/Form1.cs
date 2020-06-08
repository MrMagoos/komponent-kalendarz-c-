using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testuKalendarza
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            createCustoCalendar1.BtnRightButtonClicked(button3, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            createCustoCalendar1.BtnTodayButtonClicked(button2, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            createCustoCalendar1.BtnLeftButtonClicked(button1, e);
        }
    }
}
