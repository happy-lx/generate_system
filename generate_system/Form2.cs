using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCWin;

namespace generate_system
{
    public partial class Form2 : CCSkinMain
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string text = File_tools.read_rules(Environment.CurrentDirectory + "\\rules1.txt");



            var l1 = text.Split(":".ToCharArray());

            this.richTextBox2.Clear();

            foreach (var tmp in l1)
            {
                this.richTextBox2.AppendText(tmp + "\r\n");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = Environment.CurrentDirectory + "\\rules1.txt";

            string text = ":" + this.richTextBox1.Text;

            try
            {
                File_tools.update_rules(path, text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }

            MessageBox.Show("添加成功!", "成功", MessageBoxButtons.OK);
            
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            string path = Environment.CurrentDirectory + "\\rules1.txt";

            string text = ":" + this.richTextBox1.Text;

            try
            {
                File_tools.update_rules(path, text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }

            MessageBox.Show("添加成功!", "成功", MessageBoxButtons.OK);
        }

        private void skinButton2_Click(object sender, EventArgs e)
        {
            string text = File_tools.read_rules(Environment.CurrentDirectory + "\\rules1.txt");



            var l1 = text.Split(":".ToCharArray());

            this.richTextBox2.Clear();

            foreach (var tmp in l1)
            {
                this.richTextBox2.AppendText(tmp + "\r\n");
            }
        }
    }
}
