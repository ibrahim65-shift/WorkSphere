namespace WorkSphere.System_Records_Page
{
    partial class ctrlSystemRecords
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblNumberOfItem = new System.Windows.Forms.Label();
            this.cbNumberOfPages = new System.Windows.Forms.ComboBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnRefresh = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblStateDescription = new System.Windows.Forms.Label();
            this.panelState = new System.Windows.Forms.Panel();
            this.lblStateTitle = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnExportAll = new System.Windows.Forms.Button();
            this.btnExportSomeData = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvListSystemRecords = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panelState.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListSystemRecords)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblNumberOfItem
            // 
            this.lblNumberOfItem.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblNumberOfItem.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblNumberOfItem.ForeColor = System.Drawing.Color.Red;
            this.lblNumberOfItem.Location = new System.Drawing.Point(173, 0);
            this.lblNumberOfItem.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNumberOfItem.Name = "lblNumberOfItem";
            this.lblNumberOfItem.Size = new System.Drawing.Size(77, 54);
            this.lblNumberOfItem.TabIndex = 7;
            this.lblNumberOfItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbNumberOfPages
            // 
            this.cbNumberOfPages.BackColor = System.Drawing.Color.White;
            this.cbNumberOfPages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNumberOfPages.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.cbNumberOfPages.FormattingEnabled = true;
            this.cbNumberOfPages.Location = new System.Drawing.Point(41, 0);
            this.cbNumberOfPages.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbNumberOfPages.Name = "cbNumberOfPages";
            this.cbNumberOfPages.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbNumberOfPages.Size = new System.Drawing.Size(88, 49);
            this.cbNumberOfPages.TabIndex = 7;
            this.toolTip1.SetToolTip(this.cbNumberOfPages, "عدد الصفحات");
            this.cbNumberOfPages.SelectedIndexChanged += new System.EventHandler(this.cbNumberOfPages_SelectedIndexChanged);
            // 
            // btnNext
            // 
            this.btnNext.Image = global::WorkSphere.Properties.Resources.left_arrow;
            this.btnNext.Location = new System.Drawing.Point(0, 0);
            this.btnNext.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnNext.Name = "btnNext";
            this.btnNext.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnNext.Size = new System.Drawing.Size(41, 49);
            this.btnNext.TabIndex = 8;
            this.toolTip1.SetToolTip(this.btnNext, "الصفحة التالية");
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Image = global::WorkSphere.Properties.Resources.right_arrow;
            this.btnPrev.Location = new System.Drawing.Point(128, 0);
            this.btnPrev.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnPrev.Size = new System.Drawing.Size(41, 49);
            this.btnPrev.TabIndex = 7;
            this.toolTip1.SetToolTip(this.btnPrev, "الصفحة السابقة");
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Image = global::WorkSphere.Properties.Resources.Refresh;
            this.btnRefresh.Location = new System.Drawing.Point(896, 402);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnRefresh.Size = new System.Drawing.Size(41, 42);
            this.btnRefresh.TabIndex = 10;
            this.toolTip1.SetToolTip(this.btnRefresh, "اعادة تحميل");
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearch.Location = new System.Drawing.Point(41, 0);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtSearch.Multiline = true;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(242, 42);
            this.txtSearch.TabIndex = 6;
            this.txtSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.txtSearch, "اكتب عبارة بحث او اضغط على علامة البحث لعرض كافة البيانات");
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // lblStateDescription
            // 
            this.lblStateDescription.BackColor = System.Drawing.Color.White;
            this.lblStateDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStateDescription.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStateDescription.ForeColor = System.Drawing.Color.LightGray;
            this.lblStateDescription.Location = new System.Drawing.Point(0, 59);
            this.lblStateDescription.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStateDescription.Name = "lblStateDescription";
            this.lblStateDescription.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblStateDescription.Size = new System.Drawing.Size(479, 38);
            this.lblStateDescription.TabIndex = 4;
            this.lblStateDescription.Text = "لاتوجد بيانات";
            this.lblStateDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelState
            // 
            this.panelState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panelState.BackColor = System.Drawing.Color.White;
            this.panelState.Controls.Add(this.lblStateDescription);
            this.panelState.Controls.Add(this.lblStateTitle);
            this.panelState.Location = new System.Drawing.Point(234, 162);
            this.panelState.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panelState.Name = "panelState";
            this.panelState.Size = new System.Drawing.Size(479, 134);
            this.panelState.TabIndex = 9;
            this.panelState.Visible = false;
            // 
            // lblStateTitle
            // 
            this.lblStateTitle.BackColor = System.Drawing.Color.White;
            this.lblStateTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStateTitle.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.lblStateTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblStateTitle.Location = new System.Drawing.Point(0, 0);
            this.lblStateTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStateTitle.Name = "lblStateTitle";
            this.lblStateTitle.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblStateTitle.Size = new System.Drawing.Size(479, 59);
            this.lblStateTitle.TabIndex = 3;
            this.lblStateTitle.Text = "لاتوجد بيانات";
            this.lblStateTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSearch
            // 
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSearch.Image = global::WorkSphere.Properties.Resources.search;
            this.btnSearch.Location = new System.Drawing.Point(0, 0);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnSearch.Size = new System.Drawing.Size(41, 42);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::WorkSphere.Properties.Resources.delete;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(825, 7);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnDelete.Size = new System.Drawing.Size(106, 42);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Tag = "SystemRecords.Delete";
            this.btnDelete.Text = "    حذف";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnExportAll
            // 
            this.btnExportAll.Image = global::WorkSphere.Properties.Resources.excel;
            this.btnExportAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportAll.Location = new System.Drawing.Point(728, 7);
            this.btnExportAll.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnExportAll.Name = "btnExportAll";
            this.btnExportAll.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnExportAll.Size = new System.Drawing.Size(93, 42);
            this.btnExportAll.TabIndex = 3;
            this.btnExportAll.Tag = "SystemRecords.Export";
            this.btnExportAll.Text = "    الكل";
            this.btnExportAll.UseVisualStyleBackColor = true;
            this.btnExportAll.Click += new System.EventHandler(this.btnExportAll_Click);
            // 
            // btnExportSomeData
            // 
            this.btnExportSomeData.Image = global::WorkSphere.Properties.Resources.xls;
            this.btnExportSomeData.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportSomeData.Location = new System.Drawing.Point(607, 7);
            this.btnExportSomeData.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnExportSomeData.Name = "btnExportSomeData";
            this.btnExportSomeData.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnExportSomeData.Size = new System.Drawing.Size(117, 42);
            this.btnExportSomeData.TabIndex = 4;
            this.btnExportSomeData.Tag = "SystemRecords.Export";
            this.btnExportSomeData.Text = "   الشبكة";
            this.btnExportSomeData.UseVisualStyleBackColor = true;
            this.btnExportSomeData.Click += new System.EventHandler(this.btnExportSomeData_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Location = new System.Drawing.Point(320, 7);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(283, 42);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.Controls.Add(this.lblNumberOfItem);
            this.panel2.Controls.Add(this.cbNumberOfPages);
            this.panel2.Controls.Add(this.btnNext);
            this.panel2.Controls.Add(this.btnPrev);
            this.panel2.Location = new System.Drawing.Point(5, 397);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(250, 54);
            this.panel2.TabIndex = 11;
            // 
            // dgvListSystemRecords
            // 
            this.dgvListSystemRecords.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvListSystemRecords.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvListSystemRecords.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvListSystemRecords.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvListSystemRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvListSystemRecords.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvListSystemRecords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvListSystemRecords.EnableHeadersVisualStyles = false;
            this.dgvListSystemRecords.Location = new System.Drawing.Point(3, 60);
            this.dgvListSystemRecords.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dgvListSystemRecords.Name = "dgvListSystemRecords";
            this.dgvListSystemRecords.ReadOnly = true;
            this.dgvListSystemRecords.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dgvListSystemRecords.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvListSystemRecords.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvListSystemRecords.RowHeadersWidth = 62;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvListSystemRecords.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvListSystemRecords.RowTemplate.Height = 28;
            this.dgvListSystemRecords.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvListSystemRecords.Size = new System.Drawing.Size(941, 394);
            this.dgvListSystemRecords.TabIndex = 8;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.btnDelete);
            this.flowLayoutPanel1.Controls.Add(this.btnExportAll);
            this.flowLayoutPanel1.Controls.Add(this.btnExportSomeData);
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 4);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(941, 56);
            this.flowLayoutPanel1.TabIndex = 7;
            // 
            // ctrlSystemRecords
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.panelState);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dgvListSystemRecords);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ctrlSystemRecords";
            this.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Size = new System.Drawing.Size(947, 458);
            this.Load += new System.EventHandler(this.ctrlSystemRecords_Load);
            this.panelState.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListSystemRecords)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblNumberOfItem;
        private System.Windows.Forms.ComboBox cbNumberOfPages;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblStateDescription;
        private System.Windows.Forms.Panel panelState;
        private System.Windows.Forms.Label lblStateTitle;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnExportAll;
        private System.Windows.Forms.Button btnExportSomeData;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvListSystemRecords;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}
