using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using CCWin;

namespace generate_system
{
    


    public partial class Form1 : CCSkinMain
    {

        Dictionary<List<string>, string> dir = new Dictionary<List<string>, string>();//规则字典，key字段存放前提，value字段存放结论

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int i, j;

            string path = Environment.CurrentDirectory + "\\rules1.txt";

            string tmp;

            tmp = File_tools.read_rules(path);

            var rules = tmp.Split(":".ToCharArray());

            for (i = 0; i < rules.Length; i++)//取出每一条规则放入规则字典中
            {
                var rule_pre_post = rules[i].Split(" ".ToCharArray());

                List<string> rule_pre = new List<string>();

                for (j = 0; j < rule_pre_post.Length; j++)
                {
                    if (j == rule_pre_post.Length - 1)//读到结论,把前提和结论全加入到dir中
                    {
                        dir.Add(rule_pre, rule_pre_post[j]);
                    }
                    else//读到前提
                    {
                        rule_pre.Add(rule_pre_post[j]);
                    }
                }
            }//初始化规则字典完毕

            //根据规则字典生成可供选择的事实

            List<string> l1 = new List<string>();

            foreach (var key in dir.Keys)
            {
                foreach (var pre in key)
                {
                    if (l1.Contains(pre))
                    {
                        continue;
                    }
                    else
                    {
                        l1.Add(pre);
                    }
                    
                }
            }

            int x1 = this.listBox1.Location.X;

            int y1 = this.listBox1.Location.Y-50;

            i = 0;

            foreach (var pre in l1)
            {
                CheckBox c1 = new CheckBox();

                c1.ForeColor = Color.Violet;

                c1.Text = pre;

                c1.Visible = true;
                if (i % 2 == 0)
                {
                    c1.Location = new Point(x1 , y1);
                }
                else
                {
                    c1.Location = new Point(x1 + 110, y1);
                    
                    y1 += 20;
                }


                this.listBox1.Controls.Add(c1);

                i++;
            }
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            //用户在事实上打钩，首先得到用户的输入
            List<string> user_in = new List<string>();

            List<KeyValuePair<List<string>, string>> has_checked = new List<KeyValuePair<List<string>, string>>();//每次遍历知识库，将匹配的知识加入到这个里面

            this.richTextBox1.Clear();

            this.richTextBox2.Clear();

            foreach (CheckBox item in this.listBox1.Controls)
            {
                if (item.Checked)
                {
                    user_in.Add(item.Text);

                }
            }

            int i = 1;

            while (true)
            {
                has_checked.Clear();//首先是没有一个匹配的知识

                foreach (KeyValuePair<List<string>, string> tmp in dir)//遍历一遍知识库，取出匹配的知识
                {
                    if (Tools.contains(tmp.Key, user_in))
                    {
                        has_checked.Add(tmp);
                    }
                }

                if (has_checked.Count == 0)//无匹配项，或许已经找完或许没有找到
                {
                    if (user_in.Count == 0)
                    {
                        MessageBox.Show("您没有勾选已知事实请重新选择！", "警告！", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;
                    }
                    else
                    {
                        if (i != 1)
                        {
                            this.richTextBox2.AppendText(user_in[user_in.Count - 1]);

                            MessageBox.Show("匹配成功！", "恭喜!", MessageBoxButtons.OK);

                            return;
                        }
                            
                        this.richTextBox2.AppendText("无匹配结果");

                    MessageBox.Show("查找失败请重新选择事实", "注意!", MessageBoxButtons.OK,MessageBoxIcon.Error);

                        return;


                    }

                    
                    

                }
                else//知识库中有知识与之匹配
                {
                    this.richTextBox1.AppendText("第" + i + "次有" + has_checked.Count + "条知识与事实匹配" + "\r\n");

                    foreach (KeyValuePair<List<string>, string> all in has_checked)//对于每一条匹配的知识

                    {
                        foreach (string tmp in all.Key)
                        {
                            if (user_in.Contains(tmp))
                            {
                                user_in.Remove(tmp);

                                this.richTextBox1.AppendText(tmp + "->");
                            }
                        }
                        user_in.Add(all.Value);

                        this.richTextBox1.AppendText(all.Value+"\r\n");
                    }
                }

                i++;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void skinButton2_Click(object sender, EventArgs e)
        {
            //用户在事实上打钩，首先得到用户的输入
            List<string> user_in = new List<string>();

            List<KeyValuePair<List<string>, string>> has_checked = new List<KeyValuePair<List<string>, string>>();//每次遍历知识库，将匹配的知识加入到这个里面

            this.richTextBox1.Clear();

            this.richTextBox2.Clear();

            foreach (CheckBox item in this.listBox1.Controls)
            {
                if (item.Checked)
                {
                    user_in.Add(item.Text);

                }
            }

            int i = 1;

            while (true)
            {
                has_checked.Clear();//首先是没有一个匹配的知识

                foreach (KeyValuePair<List<string>, string> tmp in dir)//遍历一遍知识库，取出匹配的知识
                {
                    if (Tools.contains(tmp.Key, user_in))
                    {
                        has_checked.Add(tmp);
                    }
                }

                if (has_checked.Count == 0)//无匹配项，或许已经找完或许没有找到
                {
                    if (user_in.Count == 0)
                    {
                        MessageBox.Show("您没有勾选已知事实请重新选择！", "警告！", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;
                    }
                    else
                    {
                        foreach (string tmp in dir.Values)
                        {
                            if (tmp == user_in[user_in.Count - 1])
                            {
                                this.richTextBox2.AppendText(user_in[user_in.Count - 1]);

                                MessageBox.Show("匹配成功！", "恭喜!", MessageBoxButtons.OK);

                                return;
                            }


                        }

                        this.richTextBox2.AppendText("无匹配结果");

                        MessageBox.Show("查找失败请重新选择事实", "注意!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;
                    }

                }
                else//知识库中有知识与之匹配
                {
                    this.richTextBox1.AppendText("第" + i + "次有" + has_checked.Count + "条知识与事实匹配" + "\r\n");

                    foreach (KeyValuePair<List<string>, string> all in has_checked)//对于每一条匹配的知识
                    {
                        foreach (string tmp in all.Key)
                        {
                            if (user_in.Contains(tmp))
                            {
                                user_in.Remove(tmp);

                                this.richTextBox1.AppendText(tmp + "->");
                            }
                        }
                        user_in.Add(all.Value);

                        this.richTextBox1.AppendText(all.Value + "\r\n");
                    }
                }

                i++;
            }

        }
    }

    class Tools//小工具类
    {
        public static bool contains(List<string> knowlege, List<string> fact)//根据用户输入的事实和知识库中已有的知识
        {
            //进行比较，如果事实包含知识则返回true

            foreach (string p in knowlege)
            {
                if (!fact.Contains(p)) return false;
            }

            return true;
        }                            
    }

    class File_tools//文件读写工具
    {
        public static string read_rules(string path)
        {
            string rules;

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader streamReader = new StreamReader(fileStream, Encoding.Default);

            rules = streamReader.ReadToEnd();

            streamReader.Close();

            fileStream.Close();

            return rules;
        }

        public static void update_rules(string path,string rules)
        {
            FileStream fileStream = new FileStream(path, FileMode.Append);

            StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.Default);

            streamWriter.Write(rules.Trim());

            streamWriter.Close();

            fileStream.Close();

        }
    }
}
