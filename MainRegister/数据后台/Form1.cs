using MainRegister;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 数据后台
{
    public partial class Form1 : Form
    {
        private List<MainRegister.Datas.Account> CurrentSource;
        public Form1()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
        }
        private void Load()
        {
            try
            {
                CurrentSource = FormAccount.Database.GetAccounts();
                dataGridView1.DataSource = CurrentSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show("刷新数据遇到错误:" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

          
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Load();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (CurrentSource!=null&& CurrentSource.Count>= dataGridView1.CurrentRow.Index)
            {
                FormAccount.Database.DeleteAccount(CurrentSource[dataGridView1.CurrentRow.Index]);
                MessageBox.Show("删除成功!");
                Load();
            }
            
        }
    }
}
