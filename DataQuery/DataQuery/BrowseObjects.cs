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
        /// 将属性字段添加到ListView1控件列中
        /// </summary>
        /// <param name="name">属性字段名称</param>
        public void FieldName(string name)
        {
            listView1.Columns.Add(name, 80, HorizontalAlignment.Center);
        }

        /// <summary>
        /// 将值添加到ListView1控件中
        /// </summary>
        /// <param name="items">控件项</param>
        /// <param name="val">值</param>
        public void ObjectVal(ListViewItem items  ,object val)
        {   
            if (val == null)
                items.SubItems.Add("");
            else 
                items.SubItems.Add(val.ToString());
            return;
        }

        /// <summary>
        /// 获取几何实体信息
        /// </summary>
        /// <param name="SFCls">简单要素类对象</param>
        public void GetSFCls(SFeatureCls SFCls)
        {
            Fields Flds = null;
            Field Fld = null;
            Record Rcd = null;
       
            //变量初始化          
            Rcd = new Record(); 
            Flds = new Fields();

            //获取属性字段名称
            Flds = SFCls.Fields;
            int num = Flds.Count;

            //在ListView1控件第一列增加“OID”字段
            if (num > 0)  FieldName("OID");
            for (int i = 0; i < num; i++)
            {
                Fld = Flds.GetItem(i);
                string name = Fld.FieldName;
                FieldName(name);
            }

            int objCount = SFCls.Count;
            
            //获取所有对象的ID，思路是根据对象的个数进行循环，若OID不存在，则OID自加继续循环直到循环objnum次
            int n = 0;
            long id = 0;
            while(n < objCount)
            {                   
                //取得ID=ID.Int的简单要素的属性  
                Rcd = SFCls.GetAtt(id);

                //取得属性结构对象中的字段数目
                if (Rcd == null)
                {
                    id++;
                    continue;
                }
                else
                    n++;
                       
                Flds = Rcd.Fields;

                //获取对象的各个属性字段的值
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