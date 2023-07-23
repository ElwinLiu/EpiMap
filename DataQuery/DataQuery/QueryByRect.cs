using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapGIS.GeoDataBase;
using MapGIS.GeoObjects.Geometry;

namespace DataQuery
{
    public partial class QueryByRect : Form
    {
        //定义数据源、数据库变量
        Server Svr = null;
        DataBase GDB = null;

        public QueryByRect()
        {
            InitializeComponent();
        }

        private void QueryByRect_Load(object sender, EventArgs e)
        {
            Svr = new Server();
            List<int> dbIDs = new List<int>();

            //连接数据源
            Svr.Connect("MapGisLocal", "", "");

            dbIDs = Svr.GDBIDs;
            int i = 0;

            //获取数据源下的所有数据库ID和Name
            while (i < dbIDs.Count)
            {
                int id = dbIDs[i];
                string name = Svr.GetDBName(id);
                srcDBCB.Items.Add(name.ToString());
                desGDBCB.Items.Add(name.ToString());
                i++;
            }

            //设置第一个被选中
            srcDBCB.SelectedIndex = -1;
            desGDBCB.SelectedIndex = -1;
        }

        private void srcDBCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            srcSFCB.Items.Clear();
            srcSFCB.Text = "";

            //要素数据集的id列表
            List<int> dsIDs = null;

            //打开数据库
            if (srcDBCB.Text == "") return;
            GDB = Svr.OpenGDB(srcDBCB.Text);
            if (GDB == null) return;

            //获取地理数据库中要素数据集的ID列表
            dsIDs = GDB.GetXclses(XClsType.Fds, 0);

            //若存在要素数据集，则取其中的简单要素类的信息
            if (dsIDs != null)
            {
                int cou = dsIDs.Count;

                //获取要素数据集中简单要素类的个数
                for (int i = 0; i < cou; i++)
                {
                    GetVecClsName(XClsType.SFCls, dsIDs[i]);
                }
            }

            //直接获取数据库中简单要素类的信息
            GetVecClsName(XClsType.SFCls, 0);

            srcSFCB.SelectedIndex = -1;
            return;
        }

        /// <summary>
        /// 获取简单要素类的id和name
        /// </summary>
        /// <param name="type">矢量类类型</param>
        /// <param name="xfID">要素数据集ID</param>
        public void GetVecClsName(XClsType type, int dsID)
        {
            List<int> sfclsIDs = null;

            sfclsIDs = GDB.GetXclses(type, dsID);
            if (sfclsIDs == null) return;

            int cou = sfclsIDs.Count;
            for (int i = 0; i < cou; i++)
            {
                string name = GDB.GetXclsName(XClsType.SFCls, sfclsIDs[i]);
                srcSFCB.Items.Add(name);
            }
            return;
        }

        private void btn_OnOK_Click(object sender, EventArgs e)
        {
            QueryDef QueryDef = null;
            RecordSet RcdSet = null;
            Rect rect = null;
            SFeatureCls srcSF = null;
            SFeatureCls desSF = null;
            DataBase desGDB = null;

            //变量初始化
            QueryDef = new QueryDef();
            rect = new Rect();         

            if (desSFTx.Text == "" || desGDBCB.Text == "" || srcDBCB.Text == "" || srcSFCB.Text == "")
            {
                MessageBox.Show("请输入完整的信息");
                return;
            }

            try
            {
                //设置要用来查询的矩形范围
                rect.XMin = double.Parse(textBox1.Text.ToString());
                rect.XMax = double.Parse(textBox3.Text.ToString());
                rect.YMin = double.Parse(textBox4.Text.ToString());
                rect.YMax = double.Parse(textBox5.Text.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("矩形范围必须为数字");
                return;
            }

            //打开源数据库、目的数据库
            GDB = Svr.OpenGDB(srcDBCB.Text);
            desGDB = Svr.OpenGDB(desGDBCB.Text);

            //打开源简单要素类
            srcSF = new SFeatureCls(GDB);
            srcSF.Open(srcSFCB.Text, 0);

            //创建目的简单要素类
            desSF = new SFeatureCls(GDB);
            int id = desSF.Create(desSFTx.Text, srcSF.GeomType, 0, 0, null);
            if (id <= 0)
            {
                MessageBox.Show("创建目的类失败");
                return;
            }           

            //矩形范围查询
            QueryDef.SetRect(rect, SpaQueryMode.Intersect);
            RcdSet = srcSF.Select(QueryDef);

            if (RcdSet.Count == 0)
            {
                desSF.Close();
                SFeatureCls.Remove(desGDB, id);
                MessageBox.Show("符合条件的要素个数为0,不保存在新类中");
                return;
            }

            desSF.CopySet(RcdSet);
            MessageBox.Show("查询成功，一共查找了" + RcdSet.Count + "条记录");
        }

        private void OnCanle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void srcSFCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            //打开源数据库
            GDB = Svr.OpenGDB(srcDBCB.Text);

            //打开源简单要素类
            SFeatureCls  srcSF = new SFeatureCls(GDB);
            srcSF.Open(srcSFCB.Text, 0);

            Rect rect = srcSF.Range;
           
            //设置要用来查询的矩形范围
            textBox1.Text = rect.XMin.ToString();
            textBox3.Text = rect.XMax.ToString();
            textBox4.Text = rect.YMin.ToString();
            textBox5.Text = rect.YMax.ToString();

            return;
        }


    }
}