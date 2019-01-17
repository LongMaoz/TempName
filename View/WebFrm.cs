﻿using CompanyTaskClass.Interface;
using CompanyTaskClass.Model;
using CompanyTaskClass.Tool;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApp1.Model;

namespace WindowsFormsApp1.View
{
    [ComVisible(true)]
    public partial class WebFrm : Form, IFrom
    {
        private ICompanyTaskTool<CompanyTask> companyTaskTool;
        private CompanyTask companyTask;
        private int RowIndex;
        public delegate void MainForm(CompanyTask companyTask, int rowIndex);
        public event MainForm UpdateDgrView;
        public WebFrm()
        {
            InitializeComponent();
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void Initialize(ICompanyTaskTool<CompanyTask> companyTaskTool,CompanyTask companyTask,int rowIndex)
        {
            this.companyTaskTool = companyTaskTool;
            this.companyTask = companyTask;
            this.RowIndex = rowIndex;
            this.Show();
        }

        private void WebFrm_Load(object sender, EventArgs e)
        {
            webBrowser1.ObjectForScripting = this;
            this.webBrowser1.Navigate(companyTaskTool.GetVerificationCode()["URL"].ToString()+companyTask.ID);
        }

        public void LoginState(string result)
        {
            JObject @object = JsonConvert.DeserializeObject<JObject>(result);
            companyTask.SetCookies("thor", @object["thor"].ToString());
            if (!string.IsNullOrWhiteSpace(@object["thor"].ToString()))
            {
                this.UpdateDgrView(companyTask, RowIndex);
                this.Close();
            }else{
                MessageBox.Show(@object["msg"].ToString());
            }
        }

        public void CallingJavaSrcipt(string functionName, object[] @object)
        {
            webBrowser1.Document.InvokeScript(functionName, @object);
        }
    }
}