using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataQuery
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }


        private void 属性查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QueryByAtt QBA = new QueryByAtt();
            QBA.ShowDialog();
        }

        private void 空间查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QueryByRect QBR = new QueryByRect();
            QBR.ShowDialog();
        }

    }
}