using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TXTextControl;

namespace LCEMREditor
{
    public partial class Form1 : TXTextControl.Windows.Forms.Ribbon.RibbonForm
    {
        public Form1()
        {
            InitializeComponent();
            TXTextControl.Windows.Forms.Ribbon.RibbonGroup ribbonGroup= new TXTextControl.Windows.Forms.Ribbon.RibbonGroup();
            TXTextControl.Windows.Forms.Ribbon.RibbonButton ribbonButton1 = new TXTextControl.Windows.Forms.Ribbon.RibbonButton();
            ribbonButton1.Text = "自定义窗口";
            ribbonButton1.Click += RibbonButton1_Click;
            ribbonGroup.RibbonItems.Add(ribbonButton1);
            //ribbonTab1.RibbonGroups.Add(ribbonGroup);
            CreatePatientInfoGroup();
            m_rbtnOpenFile.Click += M_rbtnOpenFile_Click;
            m_rbtnSaveFile.Click += M_rbtnSaveFile_Click;
        }

        private void M_rbtnSaveFile_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void M_rbtnOpenFile_Click(object sender, EventArgs e)
        {
            Load();
        }

        private void CreatePatientInfoGroup()
        {
            // Create a new ribbon group for patient information
            TXTextControl.Windows.Forms.Ribbon.RibbonGroup patientInfoGroup = new TXTextControl.Windows.Forms.Ribbon.RibbonGroup();
            patientInfoGroup.Text = "患者信息";

            // Create and add text boxes for patient information
            patientInfoGroup.RibbonItems.Add(CreateLabel("姓名："));
            patientInfoGroup.RibbonItems.Add(CreateTextBox("张三"));

            patientInfoGroup.RibbonItems.Add(CreateLabel("科室："));
            patientInfoGroup.RibbonItems.Add(CreateTextBox("内科"));

            patientInfoGroup.RibbonItems.Add(CreateLabel("床号："));
            patientInfoGroup.RibbonItems.Add(CreateTextBox("001"));

            patientInfoGroup.RibbonItems.Add(CreateLabel("患者ID："));
            patientInfoGroup.RibbonItems.Add(CreateTextBox("1001"));

            patientInfoGroup.RibbonItems.Add(CreateLabel("住院号："));
            patientInfoGroup.RibbonItems.Add(CreateTextBox("2001"));

            patientInfoGroup.RibbonItems.Add(CreateLabel("职业："));
            patientInfoGroup.RibbonItems.Add(CreateTextBox("教师"));

            patientInfoGroup.RibbonItems.Add(CreateLabel("出生地："));
            patientInfoGroup.RibbonItems.Add(CreateTextBox("北京"));

            patientInfoGroup.RibbonItems.Add(CreateLabel("性别："));
            patientInfoGroup.RibbonItems.Add(CreateTextBox("男"));

            patientInfoGroup.RibbonItems.Add(CreateLabel("出生日期："));
            patientInfoGroup.RibbonItems.Add(CreateTextBox("1980-01-01"));

            patientInfoGroup.RibbonItems.Add(CreateLabel("婚姻状况："));
            patientInfoGroup.RibbonItems.Add(CreateTextBox("已婚"));

            patientInfoGroup.RibbonItems.Add(CreateLabel("入院时间："));
            patientInfoGroup.RibbonItems.Add(CreateTextBox(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));

            patientInfoGroup.RibbonItems.Add(CreateLabel("民族："));
            patientInfoGroup.RibbonItems.Add(CreateTextBox("汉族"));

            patientInfoGroup.RibbonItems.Add(CreateLabel("地址："));
            patientInfoGroup.RibbonItems.Add(CreateTextBox("北京市海淀区"));

            patientInfoGroup.RibbonItems.Add(CreateLabel("电话："));
            patientInfoGroup.RibbonItems.Add(CreateTextBox("010-12345678"));

            patientInfoGroup.RibbonItems.Add(CreateLabel("联系人："));
            patientInfoGroup.RibbonItems.Add(CreateTextBox("李四"));

            patientInfoGroup.RibbonItems.Add(CreateLabel("联系人电话："));
            patientInfoGroup.RibbonItems.Add(CreateTextBox("010-87654321"));

            // Add the patient information group to the ribbon
            //ribbonTab2.RibbonGroups.Add(patientInfoGroup);
        }

        private TXTextControl.Windows.Forms.Ribbon.RibbonTextBox CreateTextBox(string text)
        {
            TXTextControl.Windows.Forms.Ribbon.RibbonTextBox textBox = new TXTextControl.Windows.Forms.Ribbon.RibbonTextBox();
            textBox.Text = text;
            textBox.Width = 150;
            return textBox;
        }

        private TXTextControl.Windows.Forms.Ribbon.RibbonLabel CreateLabel(string text)
        {
            TXTextControl.Windows.Forms.Ribbon.RibbonLabel label = new TXTextControl.Windows.Forms.Ribbon.RibbonLabel();
            label.Text = text;
            label.Width = 50;
            return label;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void RibbonButton1_Click(object sender, EventArgs e)
        {
            // 在编辑器中获取 Form Field
            //int currentPosition = textControl1.Selection.Start;
            //int nearestFieldPosition = -1;
            //int nearestFieldDistance = int.MaxValue;

            //foreach (FormField field in textControl1.FormFields)
            //{
            //    int fieldPosition = field.Start;
            //    int distance = Math.Abs(currentPosition - fieldPosition);

            //    if (distance < nearestFieldDistance)
            //    {
            //        nearestFieldPosition = fieldPosition;
            //        nearestFieldDistance = distance;
            //    }
            //}
            FormField field=null;
            //textControl1.FormFields[0].ContainsInputPosition
            foreach (FormField item in textControl1.FormFields)
            {
                if (item.ContainsInputPosition)
                {
                    field = item;
                    break;
                }
            }
            //创建一个自定义窗口来编辑 Form Field 的属性
            using (Form form = new Form())
            {
                //添加一个文本框用来编辑 Form Field 的名字
                TextBox nameTextBox = new TextBox();
                nameTextBox.Text = field.Name;
                nameTextBox.Dock = DockStyle.Top;
                form.Controls.Add(nameTextBox);

                //添加一个下拉框用来选择 Form Field 的类型
                ComboBox typeComboBox = new ComboBox();
                typeComboBox.Items.Add("Text");
                typeComboBox.Items.Add("CheckBox");
                typeComboBox.Items.Add("DropDownList");
                typeComboBox.SelectedItem = field.GetType().ToString();
                typeComboBox.Dock = DockStyle.Top;
                form.Controls.Add(typeComboBox);

                //显示窗口并等待用户点击保存
                if (form.ShowDialog() == DialogResult.OK)
                {
                    //保存属性
                    field.Name = nameTextBox.Text;
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void textControl1_FormFieldSelectionChanged(object sender, SelectionFormFieldEventArgs e)
        {

        }
        private void Load()
        {
            textControl1.Load();
        }
        private void Save()
        {
            var saveDiag=textControl1.Save();
            if (saveDiag==DialogResult.OK)
            {
            }
        }
        private void textControl1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Insert:   // Toggle insertion mode
                    if (e.Control || e.Alt || e.Shift) { break; }
                    //ToggleInsertionMode();
                    break;

                case Keys.A:        // Ctrl + A: Select all
                    if (!e.Control || e.Alt || e.Shift) { break; }
                    textControl1.SelectAll();
                    break;

                case Keys.S:        // Ctrl + S: Save
                    if (!e.Control || e.Alt || e.Shift) { break; }
                    Save();
                    break;

                case Keys.O:        // Ctrl + O: Open
                    if (!e.Control || e.Alt || e.Shift) { break; }
                    Load();
                    break;

                case Keys.F:        // Ctrl + F: Search
                    if (!e.Control || e.Alt || e.Shift) { break; }
                    textControl1.Find();
                    break;

                case Keys.P:        // Ctrl + P: Print
                    if (!e.Control || e.Alt || e.Shift) { break; }
                    if (textControl1.CanPrint)
                    {
                        textControl1.Print("打印出来的文档");
                    }
                    break;
            }
        }
    }
}
