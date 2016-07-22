using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace STVCReader
{
    public partial class About_STVCReader : Form
    {
        public About_STVCReader()
        {
            InitializeComponent();
        }

        private void About_STVCReader_Load(object sender, EventArgs e)
        {
            
            this.textBox1.Text = "STVCReader Version: 1.0 (2016/03/02 19:17:16)";
            this.textBox4.Text = "Boyce_Hong(Boyce_Hong@asus.com)";
            this.textBox5.Text = "Copyright (c) 2016";
            this.textBox2.Text = "Tip:Try to drag and drop a file to main form ^-^";
        }

    }
}
