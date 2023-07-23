namespace DataQuery
{
    partial class QueryByAtt
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.GetFldval = new System.Windows.Forms.Button();
            this.CanCle = new System.Windows.Forms.Button();
            this.EnSure = new System.Windows.Forms.Button();
            this.SFClsName = new System.Windows.Forms.TextBox();
            this.SQLquery = new System.Windows.Forms.TextBox();
            this.AttFieldsList = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.desDBCB = new System.Windows.Forms.ComboBox();
            this.srcSFCB = new System.Windows.Forms.ComboBox();
            this.srcDBCB = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_SQLQuery = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GetFldval
            // 
            this.GetFldval.Location = new System.Drawing.Point(6, 204);
            this.GetFldval.Name = "GetFldval";
            this.GetFldval.Size = new System.Drawing.Size(108, 26);
            this.GetFldval.TabIndex = 34;
            this.GetFldval.Text = "浏览类属性记录";
            this.GetFldval.UseVisualStyleBackColor = true;
            this.GetFldval.Click += new System.EventHandler(this.GetFldval_Click);
            // 
            // CanCle
            // 
            this.CanCle.Location = new System.Drawing.Point(260, 412);
            this.CanCle.Name = "CanCle";
            this.CanCle.Size = new System.Drawing.Size(69, 23);
            this.CanCle.TabIndex = 32;
            this.CanCle.Text = "取消";
            this.CanCle.UseVisualStyleBackColor = true;
            this.CanCle.Click += new System.EventHandler(this.CanCle_Click);
            // 
            // EnSure
            // 
            this.EnSure.Location = new System.Drawing.Point(164, 412);
            this.EnSure.Name = "EnSure";
            this.EnSure.Size = new System.Drawing.Size(69, 23);
            this.EnSure.TabIndex = 33;
            this.EnSure.Text = "确定";
            this.EnSure.UseVisualStyleBackColor = true;
            this.EnSure.Click += new System.EventHandler(this.EnSure_Click);
            // 
            // SFClsName
            // 
            this.SFClsName.Location = new System.Drawing.Point(164, 347);
            this.SFClsName.Name = "SFClsName";
            this.SFClsName.Size = new System.Drawing.Size(165, 21);
            this.SFClsName.TabIndex = 30;
            // 
            // SQLquery
            // 
            this.SQLquery.Location = new System.Drawing.Point(164, 315);
            this.SQLquery.Name = "SQLquery";
            this.SQLquery.ReadOnly = true;
            this.SQLquery.Size = new System.Drawing.Size(112, 21);
            this.SQLquery.TabIndex = 31;
            // 
            // AttFieldsList
            // 
            this.AttFieldsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3});
            this.AttFieldsList.FullRowSelect = true;
            this.AttFieldsList.GridLines = true;
            this.AttFieldsList.Location = new System.Drawing.Point(6, 20);
            this.AttFieldsList.Name = "AttFieldsList";
            this.AttFieldsList.Size = new System.Drawing.Size(315, 178);
            this.AttFieldsList.TabIndex = 29;
            this.AttFieldsList.UseCompatibleStateImageBehavior = false;
            this.AttFieldsList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "字段名";
            this.columnHeader1.Width = 131;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "类型";
            this.columnHeader3.Width = 130;
            // 
            // desDBCB
            // 
            this.desDBCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.desDBCB.FormattingEnabled = true;
            this.desDBCB.Location = new System.Drawing.Point(164, 378);
            this.desDBCB.Name = "desDBCB";
            this.desDBCB.Size = new System.Drawing.Size(165, 20);
            this.desDBCB.TabIndex = 28;
            // 
            // srcSFCB
            // 
            this.srcSFCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.srcSFCB.FormattingEnabled = true;
            this.srcSFCB.Location = new System.Drawing.Point(164, 37);
            this.srcSFCB.Name = "srcSFCB";
            this.srcSFCB.Size = new System.Drawing.Size(165, 20);
            this.srcSFCB.TabIndex = 26;
            this.srcSFCB.SelectedIndexChanged += new System.EventHandler(this.srcSFCB_SelectedIndexChanged);
            // 
            // srcDBCB
            // 
            this.srcDBCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.srcDBCB.FormattingEnabled = true;
            this.srcDBCB.Location = new System.Drawing.Point(164, 6);
            this.srcDBCB.Name = "srcDBCB";
            this.srcDBCB.Size = new System.Drawing.Size(165, 20);
            this.srcDBCB.TabIndex = 27;
            this.srcDBCB.SelectedIndexChanged += new System.EventHandler(this.srcDBCB_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 386);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 22;
            this.label6.Text = "目的数据库：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 356);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 21;
            this.label5.Text = "目的类名称：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 318);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 23;
            this.label4.Text = "设置查询条件：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 25;
            this.label2.Text = "选择简单要素类：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 24;
            this.label1.Text = "选择数据库：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.AttFieldsList);
            this.groupBox1.Controls.Add(this.GetFldval);
            this.groupBox1.Location = new System.Drawing.Point(12, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(328, 243);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "属性字段信息";
            // 
            // btn_SQLQuery
            // 
            this.btn_SQLQuery.Location = new System.Drawing.Point(282, 315);
            this.btn_SQLQuery.Name = "btn_SQLQuery";
            this.btn_SQLQuery.Size = new System.Drawing.Size(47, 22);
            this.btn_SQLQuery.TabIndex = 36;
            this.btn_SQLQuery.Text = "......";
            this.btn_SQLQuery.UseVisualStyleBackColor = true;
            this.btn_SQLQuery.Click += new System.EventHandler(this.btn_SQLQuery_Click);
            // 
            // QueryByAtt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 444);
            this.Controls.Add(this.btn_SQLQuery);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CanCle);
            this.Controls.Add(this.EnSure);
            this.Controls.Add(this.SFClsName);
            this.Controls.Add(this.SQLquery);
            this.Controls.Add(this.desDBCB);
            this.Controls.Add(this.srcSFCB);
            this.Controls.Add(this.srcDBCB);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QueryByAtt";
            this.Text = "属性查询";
            this.Load += new System.EventHandler(this.QueryByAtt_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button GetFldval;
        private System.Windows.Forms.Button CanCle;
        private System.Windows.Forms.Button EnSure;
        private System.Windows.Forms.TextBox SFClsName;
        private System.Windows.Forms.TextBox SQLquery;
        private System.Windows.Forms.ListView AttFieldsList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ComboBox desDBCB;
        private System.Windows.Forms.ComboBox srcSFCB;
        private System.Windows.Forms.ComboBox srcDBCB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_SQLQuery;
    }
}