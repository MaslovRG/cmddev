﻿using FileSyncSDK.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileSync
{
    public partial class Report : Form
    {
        public Report()
        {
            InitializeComponent();
        }

        public void ShowStatus(string status)
        {
            reportBox.Text += status;
        }
    }
}
