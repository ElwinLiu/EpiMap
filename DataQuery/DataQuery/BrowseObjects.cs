using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapGIS.GeoDataBase;
using MapGIS.GeoObjects.Att;

namespace DataQuery
{
    public partial class BrowseObjects : Form
    {
        public BrowseObjects()
        {
            InitializeComponent();
        }

        /// <summary>
        /// �������ֶ���ӵ�ListView1�ؼ�����
        /// </summary>
        /// <param name="name">�����ֶ�����</param>
        public void FieldName(string name)
        {
            listView1.Columns.Add(name, 80, HorizontalAlignment.Center);
        }

        /// <summary>
        /// ��ֵ��ӵ�ListView1�ؼ���
        /// </summary>
        /// <param name="items">�ؼ���</param>
        /// <param name="val">ֵ</param>
        public void ObjectVal(ListViewItem items  ,object val)
        {   
            if (val == null)
                items.SubItems.Add("");
            else 
                items.SubItems.Add(val.ToString());
            return;
        }

        /// <summary>
        /// ��ȡ����ʵ����Ϣ
        /// </summary>
        /// <param name="SFCls">��Ҫ�������</param>
        public void GetSFCls(SFeatureCls SFCls)
        {
            Fields Flds = null;
            Field Fld = null;
            Record Rcd = null;
       
            //������ʼ��          
            Rcd = new Record(); 
            Flds = new Fields();

            //��ȡ�����ֶ�����
            Flds = SFCls.Fields;
            int num = Flds.Count;

            //��ListView1�ؼ���һ�����ӡ�OID���ֶ�
            if (num > 0)  FieldName("OID");
            for (int i = 0; i < num; i++)
            {
                Fld = Flds.GetItem(i);
                string name = Fld.FieldName;
                FieldName(name);
            }

            int objCount = SFCls.Count;
            
            //��ȡ���ж����ID��˼·�Ǹ��ݶ���ĸ�������ѭ������OID�����ڣ���OID�ԼӼ���ѭ��ֱ��ѭ��objnum��
            int n = 0;
            long id = 0;
            while(n < objCount)
            {                   
                //ȡ��ID=ID.Int�ļ�Ҫ�ص�����  
                Rcd = SFCls.GetAtt(id);

                //ȡ�����Խṹ�����е��ֶ���Ŀ
                if (Rcd == null)
                {
                    id++;
                    continue;
                }
                else
                    n++;
                       
                Flds = Rcd.Fields;

                //��ȡ����ĸ��������ֶε�ֵ
                ListViewItem items = null;
                items = listView1.Items.Add(id.ToString());

                for (int i = 0; i < num; i++)
                {
                    Fld = Flds.GetItem(i);
                    string name = Fld.FieldName;
                    object val = Rcd.get_FldVal(name);
                    ObjectVal(items, val);
                }
                id++;
            }
            return;
        }

        private void BrowseObjects_Load(object sender, EventArgs e)
        {
            
        }
    }
}