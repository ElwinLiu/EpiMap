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
        //��������Դ�����ݿ����
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
            
            //��������Դ
            Svr.Connect("MapGisLocal", "", "");

            dbIDs = Svr.GDBIDs;
            int i = 0;

            //��ȡ����Դ�µ��������ݿ�ID��Name
            while (i < dbIDs.Count)
            {
                int id = dbIDs[i];
                string name = Svr.GetDBName(id);
                srcDBCB.Items.Add(name.ToString());
                desDBCB.Items.Add(name.ToString());
                i++;
            }

            //���õ�һ����ѡ��
            srcDBCB.SelectedIndex = -1; 
            desDBCB.SelectedIndex = -1;
 
        }

        private void srcDBCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            srcSFCB.Items.Clear();
            srcSFCB.Text = "";
            AttFieldsList.Items.Clear();

            //Ҫ�����ݼ���id�б�
            List<int> dsIDs = null; 

            //�����ݿ�
            if (srcDBCB.Text == "") return;
            GDB = Svr.OpenGDB(srcDBCB.Text);
            if (GDB == null) return;           

            //��ȡ�������ݿ���Ҫ�����ݼ���ID�б�
            dsIDs = GDB.GetXclses(XClsType.Fds, 0);

            //������Ҫ�����ݼ�����ȡ���еļ�Ҫ�������Ϣ
            if (dsIDs != null)
            {
                int cou = dsIDs.Count;

                //��ȡҪ�����ݼ��м�Ҫ����ĸ���
                for (int i = 0; i < cou; i++)
                {
                    GetVecClsName(XClsType.SFCls, dsIDs[i]);
                }
            }

            //ֱ�ӻ�ȡ���ݿ��м�Ҫ�������Ϣ
            GetVecClsName(XClsType.SFCls, 0);

            srcSFCB.SelectedIndex = -1;
            return;
        }
        
        /// <summary>
        /// ��ȡ��Ҫ�����id��name
        /// </summary>
        /// <param name="type">ʸ��������</param>
        /// <param name="xfID">Ҫ�����ݼ�ID</param>
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

            //��ָ���ļ�Ҫ����
            if (srcSFCB.Text == "" || srcDBCB.Text == "") 
                return;

            SFCls = new SFeatureCls(GDB);
            SFCls.Open(srcSFCB.Text, 0);         
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
            //ȡ���Խṹ
            Flds = SFCls.Fields;
            if (Flds == null) return;
            //���Բ鿴���Խṹ�����е��ֶ���Ŀ,�鿴��ȷ��
            int num = Flds.Count;

            //��ȡ�����ֶ�
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
                MessageBox.Show("�����¼��Ŀ�����ݿ⡢Ŀ����������Ƿ�ȫ������");
                return;
            }        

            QueryDef QueryDef = null;
            RecordSet RecordSet = null;
            SFeatureCls desSFCls = null;
        
            //��������
            QueryDef = new QueryDef();

            //���ò�ѯ����
            QueryDef.Filter = SQLquery.Text;
            RecordSet = SFCls.Select(QueryDef);

            if (RecordSet == null||RecordSet.Count == 0)
            {
                MessageBox.Show("��ѯ���Ϊ0��,���δ������Ŀ������");
                return;
            }
     
            GDB = Svr.OpenGDB(desDBCB.Text);

            //ע�⣺��������GDB�д���Class����
            desSFCls = new SFeatureCls(GDB);
            int id = desSFCls.Create(SFClsName.Text, SFCls.GeomType, 0, 0, null);
            
            if (id == 0)
            {
                MessageBox.Show("����Ŀ����ʧ��,�п���������,����������");
                return;
            }
            
            //����ѯ��������Ƹ�����
            bool rtn = desSFCls.CopySet(RecordSet);
            if (rtn)
            {
                MessageBox.Show("��ѯ�ɹ������������Ŀ�����У���" + RecordSet.Count + "����¼");
            }
            else
            {
                desSFCls.Close();
                SFeatureCls.Remove(GDB, id);
                MessageBox.Show("��ѯʧ��");
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
            //�������ж������Ϣ
            BrowseObjects browseObjects = new BrowseObjects();

            if (SFCls == null) 
                return;

            browseObjects.GetSFCls(SFCls);
            browseObjects.ShowDialog();
        }

        private void btn_SQLQuery_Click(object sender, EventArgs e)
        {
            //�������Բ�ѯ����
            VectorLayer layer = new VectorLayer(VectorLayerType.SFclsLayer);
            layer.AttachData(SFCls);
            AttQueryDlg dlg = new AttQueryDlg(layer);
            if (dlg.ShowDialog() != DialogResult.OK)
                return;
            SQLquery.Text = dlg.SQLexpress;
        }                       
    }
}