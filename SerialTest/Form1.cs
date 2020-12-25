using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string str;//用来临时存储i大写的十六进制格式字符串
            for (int i = 0; i < 256; i++)
            {
                str = i.ToString("x").ToUpper();
                if(str.Length == 1)
                {
                    str = "0" + str;//如果是一位的（0xA）,此时为了对齐，在数据前加一个字符“0”
                }
                comboBox1.Items.Add("0x" + str);//统一添加"0x"
            }
            comboBox1.Text = "0x00";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string data = comboBox1.Text;//存储当前下拉框的内容
            string convertdata = data.Substring(2, 2);//把字符分开
            byte[] buffer = new byte[1];//数据一个字节足矣
            buffer[0] = Convert.ToByte(convertdata, 16);//将字符串转化为byte型变量
            try//防止出错
            {
                serialPort1.Open();
                serialPort1.Write(buffer, 0, 1);
                serialPort1.Close();
            }
            catch //如果出错
            {
                if (serialPort1.IsOpen)
                    serialPort1.Close();
                MessageBox.Show("端口错误", "错误");
            }
        }
    }
}
