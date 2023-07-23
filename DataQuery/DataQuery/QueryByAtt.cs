using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapGIS.GeoDataBase;
using MapGIS.GeoObjects.Att;
using MapGIS.UI.Controls;
using MapGIS.GeoMap;

namespace DataQuery
{
    public partial class QueryByAtt : Form
    {
        //定义数据源、数据库变量
        Server Svr = null;
        DataBase GDB = null;
        SFeatureCls SFCls = null;

        public QueryByAtt()
        {
            InitializeComponent();
        }

        private void QueryByAtt_Load(object sender, EventArgs e)
        {
            GetFldval.Enabled = false;
            
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
                desDBCB.Items.Add(name.ToString());
                i++;
            }

            //设置第一个被选中
            srcDBCB.SelectedIndex = -1; 
            desDBCB.SelectedIndex = -1;
 
        }

        private void srcDBCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            srcSFCB.Items.Clear();
            srcSFCB.Text = "";
            AttFieldsList.Items.Clear();

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

        private void srcSFCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetFldval.Enabled = true;
            AttFieldsList.Items.Clear();

            Fields Flds = null;
            Field Fld = null;
            ListViewItem item = null;

            //打开指定的简单要素类
            if (srcSFCB.Text == "" || srcDBCB.Text == "") 
                return;

            SFCls = new SFeatureCls(GDB);
            SFCls.Open(srcSFCB.Text, 0);         
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
            //取属性结构
            Flds = SFCls.Fields;
            if (Flds == null) return;
            //可以查看属性结构对象中的字段数目,查看正确性
            int num = Flds.Count;

            //获取属性字段
            int i = 0;
            while (i < num)
            {
                Fld = Flds.GetItem(i);
                item = AttFieldsList.Items.Add(Fld.FieldName);
                item.SubItems.Add(Fld.FieldType.ToString());
                i++;
            }
            return;
        }

        private void EnSure_Click(object sender, EventArgs e)
        {                  
            if (desDBCB.Text == ""|| SFClsName.Text=="")
            {
                MessageBox.Show("请重新检查目的数据库、目的类的名称是否全部输入");
                return;
            }        

            QueryDef QueryDef = null;
            RecordSet RecordSet = null;
            SFeatureCls desSFCls = null;
        
            //变量定义
            QueryDef = new QueryDef();

            //设置查询条件
            QueryDef.Filter = SQLquery.Text;
            RecordSet = SFCls.Select(QueryDef);

            if (RecordSet == null||RecordSet.Count == 0)
            {
                MessageBox.Show("查询结果为0条,结果未保存在目的类中");
                return;
            }
     
            GDB = Svr.OpenGDB(desDBCB.Text);

            //注意：这里必须从GDB中创建Class对象
            desSFCls = new SFeatureCls(GDB);
            int id = desSFCls.Create(SFClsName.Text, SFCls.GeomType, 0, 0, null);
            
            if (id == 0)
            {
                MessageBox.Show("创建目的类失败,有可能类重名,请重新输入");
                return;
            }
            
            //将查询结果集复制给新类
            bool rtn = desSFCls.CopySet(RecordSet);
            if (rtn)
            {
                MessageBox.Show("查询成功，结果保存在目的类中，共" + RecordSet.Count + "条记录");
            }
            else
            {
                desSFCls.Close();
                SFeatureCls.Remove(GDB, id);
                MessageBox.Show("查询失败");
            }
            return;
        }

        private void CanCle_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }

        private void GetFldval_Click(object sender, EventArgs e)
        {
            //类中所有对象的信息
            BrowseObjects browseObjects = new BrowseObjects();

            if (SFCls == null) 
                return;

            browseObjects.GetSFCls(SFCls);
            browseObjects.ShowDialog();
        }

        private void btn_SQLQuery_Click(object sender, EventArgs e)
        {
            //设置属性查询条件
            VectorLayer layer = new VectorLayer(VectorLayerType.SFclsLayer);
            layer.AttachData(SFCls);
            AttQueryDlg dlg = new AttQueryDlg(layer);
            if (dlg.ShowDialog() != DialogResult.OK)
                return;
            SQLquery.Text = dlg.SQLexpress;
        }                       
    }
}