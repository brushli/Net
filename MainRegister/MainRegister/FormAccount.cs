using MainRegister.Datas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MainRegister
{
    public partial class FormAccount : Form
    {
        #region 移动窗体
        private bool moving = false;
        private int mouseDownX;
        private int mouseDownY;
        #endregion
        public FormAccount()
        {
            InitializeComponent();
            MouseDown += FormAccount_MouseDown; ;
            MouseMove += FormAccount_MouseMove; ;
            MouseUp += FormAccount_MouseUp;
            try
            {
                richTextBox1.Size = new System.Drawing.Size(Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["提示框宽"]),
                Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["提示框高"]));
                BackgroundImage = Image.FromFile(Path.Combine(System.IO.Directory.GetCurrentDirectory(), "背景图.jpg"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载软件遇到错误:" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormAccount_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                moving = false;
        }

        private void FormAccount_MouseMove(object sender, MouseEventArgs e)
        {
            if (moving)
                Location = new Point(
                    Location.X + (e.X - mouseDownX),
                    Location.Y + (e.Y - mouseDownY));
        }

        private void FormAccount_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            moving = true;
            mouseDownX = e.X;
            mouseDownY = e.Y;
        }

        private void btnCancel_MouseHover(object sender, EventArgs e)
        {
            btnCancel.Image = 注册提交.Properties.Resources.关闭_hover;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void btnCommit_Click(object sender, EventArgs e)
        {
            Account account = new Account
            {
                Name = textBox1.Text,
                Area = textBox2.Text,
                CreateDate = DateTime.Now
            };
            try
            {
                //Database.SaveAccountAsync(account);
                if (!string.IsNullOrWhiteSpace(richTextBox1.Text))
                {
                    richTextBox1.Text=$"{account.Area}-角色名：{account.Name }-恭喜-激活成功！\r\n"+ richTextBox1.Text;
                }
                else
                {
                    richTextBox1.AppendText($"{account.Area}-角色名：{account.Name }-恭喜-激活成功！");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("注册遇到错误:" +ex.InnerException?.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
