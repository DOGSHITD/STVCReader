using STVCReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class STVCReader : Form
    {
        public STVCReader()
        {
            InitializeComponent();
            this.Text = "STVCReader V1.0";
            this.MaximizeBox = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Please select files(*.bin)|*.bin";
            opf.ShowReadOnly = true;
            opf.ShowDialog();
            if (!string.IsNullOrEmpty(opf.FileName))
            {
                this.dataGridView1.Rows.Clear();
                FileInfo fi = new FileInfo(opf.FileName);
                this.Text = "STVCReader V1.0" + " - " + opf.FileName;
                this.textBox2.Text = Path.GetFileName(opf.FileName) + " (Size: " + (fi.Length/1024).ToString() + " Kb)";             
                if (fi.Length >= 4096) {
                    int ContextFlag = 0;
                    byte[] outarray = new byte[0x10000];
                    FileStream fs = new FileStream(opf.FileName, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
                    fs.Read(outarray, 0,(int)fi.Length);   //Current GPNV_STVC size is 4k.          
                    for (int i = 0; i < (fi.Length/36); i++)
                    {

                        string tempvar = Encoding.ASCII.GetString(outarray, i * 36, 35);
                        //if (String.Compare(tempvar1, "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF")==0)
                        if ((outarray[i * 36] == 0xFF) && (outarray[i * 36 + 1] == 0xFF) && (outarray[i * 36 + 2] == 0xFF))
                            continue;
                        ContextFlag = 1;
                        string opstr = tempvar.Split('\0')[0];
                        int index = this.dataGridView1.Rows.Add();
                        this.dataGridView1.Rows[index].Cells[0].Value = i + 1;
                        this.dataGridView1.Rows[index].Cells[1].Value = opstr;
                        uint count = outarray[35 + 36 * i];
                        this.dataGridView1.Rows[index].Cells[2].Value = count;

                    }
                    if (ContextFlag == 0)
                    {
                        //MessageBox.Show("No option recorded.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
           
        }

        private void STVCReader_DragDrop(object sender, DragEventArgs e)
        {
            string[] filePath = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in filePath)
            {
                if (!string.IsNullOrEmpty(file))
                {
                    this.dataGridView1.Rows.Clear();
                    FileInfo fi = new FileInfo(file);
                    this.Text = "STVCReader V1.0" + " - " + file;
                    this.textBox2.Text = Path.GetFileName(file) + " (Size: " + (fi.Length / 1024).ToString() + " Kb)";
                                     
                    if (fi.Length >= 4096)
                    {
                        int ContextFlag = 0;
                        byte[] outarray = new byte[0x10000];
                        FileStream fs = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
                        fs.Read(outarray, 0, (int)fi.Length);   //Current GPNV_STVC size is 4k.          
                        for (int i = 0; i < (fi.Length / 36); i++)
                        {
                            string tempvar = Encoding.ASCII.GetString(outarray, i * 36, 35);
                            //if (String.Compare(tempvar1, "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF")==0)
                            if ((outarray[i * 36] == 0xFF) && (outarray[i * 36 + 1] == 0xFF) && (outarray[i * 36 + 2] == 0xFF))
                                continue;
                            ContextFlag = 1;
                            string opstr = tempvar.Split('\0')[0];//ÿ
                            int index = this.dataGridView1.Rows.Add();
                            this.dataGridView1.Rows[index].Cells[0].Value = i + 1;
                            this.dataGridView1.Rows[index].Cells[1].Value = opstr;
                            uint count = outarray[35 + 36 * i];
                            this.dataGridView1.Rows[index].Cells[2].Value = count;

                        }
                        if (ContextFlag == 0) {                           
                           // MessageBox.Show("No option recorded.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }

        }

        private void STVCReader_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void STVCReader_HelpButtonClicked(Object sender, CancelEventArgs e)
        {
            
            About_STVCReader fm = new About_STVCReader();
            fm.StartPosition = FormStartPosition.CenterScreen;
            fm.Show();
            //MessageBox.Show("STVCReader Version: 1.0 (2016/03/02 19:17:16) \n           Boyce_Hong(Boyce_Hong@asus.com)\n                         Copyright (c) 2016",
            //    "About STVCReader");
        }
    }
}
