using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoMouse
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetFixedPositionAndShowDate();
        }
        //选择：滚动或者粘贴
        private static Boolean boolGun=false;
        private static Boolean boolZhantie=false;
        private static Boolean boolGunShang = false;
        
        private static int count = -100;//默认下滚动
        private int state = -1;//上下
        private int time = 5000;
        private int timeNums = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern int mouse_event(int mouseevent, int dx, int dy, int cButtons, int dwExtraInfo);
        [System.Runtime.InteropServices.DllImport("User32")]
        public extern static void SetCursorPos(int x, int y);
        const int MOUSEEVENTF_MOVE = 0x0001;            //移动鼠标 
        const int MOUSEEVENTF_LEFTDOWN = 0x0002;        //模拟鼠标左键按下 
        const int MOUSEEVENTF_LEFTUP = 0x0004;          //模拟鼠标左键抬起 
        const int MOUSEEVENTF_RIGHTDOWN = 0x0008;       //模拟鼠标右键按下 
        const int MOUSEEVENTF_RIGHTUP = 0x0010;         //模拟鼠标右键抬起 
        const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;      //模拟鼠标中键按下 
        const int MOUSEEVENTF_MIDDLEUP = 0x0040;        //模拟鼠标中键抬起 
        const int MOUSEEVENTF_ABSOLUTE = 0x8000;        //标示是否采用绝对坐标 
        const int MOUSEEVENTF_WHEEL = 0x800;            //模拟鼠标滑轮移动

        //开始
        private void button1_Click(object sender, EventArgs e)
        {
            if (false == boolGun && false == boolZhantie && false == boolGunShang)
            {
                MessageBox.Show("请选择操作类型？ ", "提示!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            }
                       
            timer1.Enabled = true;
            try {                
                //获取时间间隔 
                timer1.Interval = 1000*int.Parse(double.Parse(textBox1.Text).ToString());
            } catch {
                //默认时间
                timer1.Interval = 5000;
            }
        }

        //停止
        private void button2_Click(object sender, EventArgs e)
        {
            if (false == boolGun && false == boolZhantie && false == boolGunShang)
            {
                MessageBox.Show("请选择操作类型~", "警告!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            timer1.Enabled = false;
           // textBox4.Text = "次数";
            //timer1.Enabled = false;
        } 

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //1 开始滚动
            if (false == boolGun && false == boolZhantie && false == boolGunShang) return;
            if (true == boolGun)//像下滚
            {
                if (state == 1)
                {
                    count = 100;//
                }
                else
                {
                    count = -100;//负数代表向下移动
                }

                //滚动鼠标一次
                gunDong();
            }
            //1.1 向上滚
            if (true == boolGunShang)
            {
                if (state == 1)
                {
                    count = -100;//
                }
                else
                {
                    count = 100;//正数代表向上移动
                }
                //滚动鼠标一次
                gunDong();
            }
            //2 粘贴 并回车
            if (true == boolZhantie) {
                // 实例化一个 KeyEventArgs 对象
                /*KeyEventArgs keyControl = new KeyEventArgs(Keys.Control);
                KeyCVDown(sender,keyControl);*/
                KeyCVDown2(sender,e, timeNums);

                //this.KeyDown += new System.Windows.Forms.KeyEventHandler(KeyCVDown);
            }
         //计数
            timeNums++;
            textBox4.Text = "" + timeNums;

       /*     if (timer1.Enabled ==true) {
                timeNums++;
                textBox4.Text = "" + timeNums;
            } */         
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        //鼠标滚动
        private void gunDong() {
            try
            {
                mouse_event(MOUSEEVENTF_WHEEL, 0, 0, count, 0);   //控制鼠标滑轮滚动，count代表滚动的值，负数代表向下，正数代表向上，如-100代表向下滚动100的y坐标
            }
            catch (Exception eee)
            {
                timer1.Enabled = false;
                MessageBox.Show("引发此异常的原因是您输入的滚轮数值为非法格式\n您可以单击“确定”并检查输入信息后重试\n详细错误信息为：" + eee.Message, "错误提示", MessageBoxButtons.OK);
            }
        }

        //1 执行粘贴和回车
        private void KeyCVDown(object sender, KeyEventArgs e)
        {
            //if (e.Control && e.KeyCode == Keys.V)
            if (e.Control)
            {
                // 执行粘贴操作
                Clipboard.SetText("test11");
                SendKeys.SendWait("^v"); // 发送Ctrl+V组合键
                SendKeys.SendWait("{ENTER}"); // 发送回车键                
            }
        }
        //2 执行粘贴和回车
        private void KeyCVDown2(object sender, EventArgs e,int num)
        {            
            // 获取剪贴板内容
            string clipboardText = Clipboard.GetText();
            if ("" == clipboardText|| null == clipboardText) clipboardText = "剪切板没有内容!";

            Clipboard.SetText(clipboardText+ num);
            SendKeys.SendWait("^v"); // 发送Ctrl+V组合键
            Clipboard.SetText(clipboardText );
            // 将剪贴板内容插入到文本框中
            //textBox1.Text = clipboardText;
            // 模拟按下回车键
            SendKeys.SendWait("{ENTER}");
        }

        //滚动选中 下滚
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            boolGun = radioButton2.Checked;
        }
        //向上滚
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            boolGunShang = radioButton3.Checked;
        }

        // 粘贴是否选中
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            boolZhantie=radioButton1.Checked;
        }
        private void tanhchuang(object sender, EventArgs e)
        {
            MessageBox.Show("这是一个弹窗", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void SetFixedPositionAndShowDate()
        {
            label1.Location = new Point(208, 8);
            label1.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
