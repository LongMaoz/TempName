﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCWin;
using WindowsFormsApp1.Model;
using Newtonsoft.Json.Linq;
using WindowsFormsApp1.Controller;
using CompanyTaskClass.Company;
using System.Net;
using CompanyTaskClass.Tool;
using System.Text.RegularExpressions;
using CompanyTaskClass.Model;

namespace WindowsFormsApp1.View
{
    public partial class MainFrm : Skin_Mac, IFrom
    {
        private JObject Userinfo;
        private List<CompanyTask> list;
        public MainFrm()
        {
            InitializeComponent();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            Initialize();
        }

        public void Initialize(JObject jObject)
        {
            this.Userinfo = jObject;
            this.GropBox.Text = $@"当前登录帐号：{Userinfo["user"]["CompanyName"]}";
            //获取信息
            list  = MainBll.GetCompanys(Userinfo);
            this.DgrView.DataSource = list;
            this.DgrView.Columns["LoginName"].Visible = false;
            this.DgrView.Columns["PassWord"].Visible = false;
            this.DgrView.Columns["CompanyType"].Visible = false;
            this.DgrView.Columns["ID"].Visible = false;
            this.DgrView.Columns["UserID"].Visible = false;
            this.DgrView.Columns["Cookies"].Visible = false;
            this.DgrView.Columns["FromCompany"].Visible = false;
            this.Show();
        }

        public void Initialize()
        {
            this.Text = @"签约厂商";
            this.LblBand.Text = @"绑定厂商:";
        }

        private void MainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void DgrView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1 && DgrView.Columns[e.ColumnIndex].HeaderText == @"操作")
            {
                var temp = list[e.RowIndex];
                var result = new XingXingTaskTool().GetList(temp);
                if(result == null)
                {
                    new Code().Show();
                }
            }
        }
    }
}
