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
        //��������Դ�����ݿ����
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
                desGDBCB.Items.Add(name.ToString());
                i++;
            }

            //���õ�һ����ѡ��
            srcDBCB.SelectedIndex = -1;
            desGDBCB.SelectedIndex = -1;
        }

        private void srcDBCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            srcSFCB.Items.Clear();
            srcSFCB.Text = "";

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

        private void btn_OnOK_Click(object sender, EventArgs e)
        {
            QueryDef QueryDef = null;
            RecordSet RcdSet = null;
            Rect rect = null;
            SFeatureCls srcSF = null;
            SFeatureCls desSF = null;
            DataBase desGDB = null;

            //������ʼ��
            QueryDef = new QueryDef();
            rect = new Rect();         

            if (desSFTx.Text == "" || desGDBCB.Text == "" || srcDBCB.Text == "" || srcSFCB.Text == "")
            {
                MessageBox.Show("��������������Ϣ");
                return;
            }

            try
            {
                //����Ҫ������ѯ�ľ��η�Χ
                rect.XMin = double.Parse(textBox1.Text.ToString());
                rect.XMax = double.Parse(textBox3.Text.ToString());
                rect.YMin = double.Parse(textBox4.Text.ToString());
                rect.YMax = double.Parse(textBox5.Text.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("���η�Χ����Ϊ����");
                return;
            }

            //��Դ���ݿ⡢Ŀ�����ݿ�
            GDB = Svr.OpenGDB(srcDBCB.Text);
            desGDB = Svr.OpenGDB(desGDBCB.Text);

            //��Դ��Ҫ����
            srcSF = new SFeatureCls(GDB);
            srcSF.Open(srcSFCB.Text, 0);

            //����Ŀ�ļ�Ҫ����
            desSF = new SFeatureCls(GDB);
            int id = desSF.Create(desSFTx.Text, srcSF.GeomType, 0, 0, null);
            if (id <= 0)
            {
                MessageBox.Show("����Ŀ����ʧ��");
                return;
            }           

            //���η�Χ��ѯ
            QueryDef.SetRect(rect, SpaQueryMode.Intersect);
            RcdSet = srcSF.Select(QueryDef);

            if (RcdSet.Count == 0)
            {
                desSF.Close();
                SFeatureCls.Remove(desGDB, id);
                MessageBox.Show("����������Ҫ�ظ���Ϊ0,��������������");
                return;
            }

            desSF.CopySet(RcdSet);
            MessageBox.Show("��ѯ�ɹ���һ��������" + RcdSet.Count + "����¼");
        }

        private void OnCanle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void srcSFCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            //��Դ���ݿ�
            GDB = Svr.OpenGDB(srcDBCB.Text);

            //��Դ��Ҫ����
            SFeatureCls  srcSF = new SFeatureCls(GDB);
            srcSF.Open(srcSFCB.Text, 0);

            Rect rect = srcSF.Range;
           
            //����Ҫ������ѯ�ľ��η�Χ
            textBox1.Text = rect.XMin.ToString();
            textBox3.Text = rect.XMax.ToString();
            textBox4.Text = rect.YMin.ToString();
            textBox5.Text = rect.YMax.ToString();

            return;
        }


    }
}