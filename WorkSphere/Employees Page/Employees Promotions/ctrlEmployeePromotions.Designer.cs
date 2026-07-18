namespace WorkSphere.Employees_Page.Employees_Promotions
{
    partial class ctrlEmployeePromotions
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnExportAll = new System.Windows.Forms.Button();
            this.btnExportSomeData = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dgvListPromotions = new System.Windows.Forms.DataGridView();
            this.lblStateTitle = new System.Windows.Forms.Label();
            this.panelState = new System.Windows.Forms.Panel();
            this.lblStateDescription = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.cbNumberOfPages = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblNumberOfItem = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListPromotions)).BeginInit();
            this.panelState.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.btnAdd);
            this.flowLayoutPanel1.Controls.Add(this.btnEdit);
            this.flowLayoutPanel1.Controls.Add(this.btnDelete);
            this.flowLayoutPanel1.Controls.Add(this.btnExportAll);
            this.flowLayoutPanel1.Controls.Add(this.btnExportSomeData);
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1166, 65);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::WorkSphere.Properties.Resources.Add;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(1038, 8);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnAdd.Size = new System.Drawing.Size(115, 50);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Tag = "Promotions.Add";
            this.btnAdd.Text = "    اضافة";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Image = global::WorkSphere.Properties.Resources.Edit;
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(917, 8);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnEdit.Size = new System.Drawing.Size(115, 50);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Tag = "Promotions.Edit";
            this.btnEdit.Text = "    تعديل";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::WorkSphere.Properties.Resources.delete;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(796, 8);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnDelete.Size = new System.Drawing.Size(115, 50);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Tag = "Promotions.Delete";
            this.btnDelete.Text = "    حذف";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnExportAll
            // 
            this.btnExportAll.Image = global::WorkSphere.Properties.Resources.excel;
            this.btnExportAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportAll.Location = new System.Drawing.Point(675, 8);
            this.btnExportAll.Name = "btnExportAll";
            this.btnExportAll.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnExportAll.Size = new System.Drawing.Size(115, 50);
            this.btnExportAll.TabIndex = 3;
            this.btnExportAll.Tag = "Promotions.Export";
            this.btnExportAll.Text = "    الكل";
            this.btnExportAll.UseVisualStyleBackColor = true;
            this.btnExportAll.Click += new System.EventHandler(this.btnExportAll_Click);
            // 
            // btnExportSomeData
            // 
            this.btnExportSomeData.Image = global::WorkSphere.Properties.Resources.xls;
            this.btnExportSomeData.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportSomeData.Location = new System.Drawing.Point(554, 8);
            this.btnExportSomeData.Name = "btnExportSomeData";
            this.btnExportSomeData.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnExportSomeData.Size = new System.Drawing.Size(115, 50);
            this.btnExportSomeData.TabIndex = 4;
            this.btnExportSomeData.Tag = "Promotions.Export";
            this.btnExportSomeData.Text = "   الشبكة";
            this.btnExportSomeData.UseVisualStyleBackColor = true;
            this.btnExportSomeData.Click += new System.EventHandler(this.btnExportSomeData_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Location = new System.Drawing.Point(200, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(348, 50);
            this.panel1.TabIndex = 4;
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearch.Location = new System.Drawing.Point(50, 0);
            this.txtSearch.Multiline = true;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(298, 50);
            this.txtSearch.TabIndex = 6;
            this.txtSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.txtSearch, "اكتب عبارة بحث او اضغط على علامة البحث لعرض كافة البيانات");
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // btnSearch
            // 
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSearch.Image = global::WorkSphere.Properties.Resources.search;
            this.btnSearch.Location = new System.Drawing.Point(0, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnSearch.Size = new System.Drawing.Size(50, 50);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dgvListPromotions
            // 
            this.dgvListPromotions.AllowUserToAddRows = false;
            this.dgvListPromotions.AllowUserToDeleteRows = false;
            this.dgvListPromotions.AllowUserToOrderColumns = true;
            this.dgvListPromotions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvListPromotions.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvListPromotions.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvListPromotions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvListPromotions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvListPromotions.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvListPromotions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvListPromotions.Location = new System.Drawing.Point(0, 65);
            this.dgvListPromotions.Name = "dgvListPromotions";
            this.dgvListPromotions.ReadOnly = true;
            this.dgvListPromotions.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dgvListPromotions.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvListPromotions.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvListPromotions.RowHeadersWidth = 62;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvListPromotions.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvListPromotions.RowTemplate.Height = 28;
            this.dgvListPromotions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvListPromotions.Size = new System.Drawing.Size(1166, 479);
            this.dgvListPromotions.TabIndex = 2;
            // 
            // lblStateTitle
            // 
            this.lblStateTitle.BackColor = System.Drawing.Color.White;
            this.lblStateTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStateTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStateTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblStateTitle.Location = new System.Drawing.Point(0, 0);
            this.lblStateTitle.Name = "lblStateTitle";
            this.lblStateTitle.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblStateTitle.Size = new System.Drawing.Size(589, 45);
            this.lblStateTitle.TabIndex = 3;
            this.lblStateTitle.Text = "لايوجد بيانات";
            this.lblStateTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelState
            // 
            this.panelState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panelState.BackColor = System.Drawing.Color.White;
            this.panelState.Controls.Add(this.lblStateDescription);
            this.panelState.Controls.Add(this.lblStateTitle);
            this.panelState.Location = new System.Drawing.Point(289, 203);
            this.panelState.Name = "panelState";
            this.panelState.Size = new System.Drawing.Size(589, 138);
            this.panelState.TabIndex = 4;
            this.panelState.Visible = false;
            // 
            // lblStateDescription
            // 
            this.lblStateDescription.BackColor = System.Drawing.Color.White;
            this.lblStateDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStateDescription.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStateDescription.ForeColor = System.Drawing.Color.LightGray;
            this.lblStateDescription.Location = new System.Drawing.Point(0, 45);
            this.lblStateDescription.Name = "lblStateDescription";
            this.lblStateDescription.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblStateDescription.Size = new System.Drawing.Size(589, 45);
            this.lblStateDescription.TabIndex = 4;
            this.lblStateDescription.Text = "لايوجد بيانات";
            this.lblStateDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Image = global::WorkSphere.Properties.Resources.Refresh;
            this.btnRefresh.Location = new System.Drawing.Point(1103, 479);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnRefresh.Size = new System.Drawing.Size(50, 50);
            this.btnRefresh.TabIndex = 5;
            this.toolTip1.SetToolTip(this.btnRefresh, "اعادة تحميل");
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Image = global::WorkSphere.Properties.Resources.right_arrow;
            this.btnPrev.Location = new System.Drawing.Point(157, 0);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnPrev.Size = new System.Drawing.Size(50, 50);
            this.btnPrev.TabIndex = 7;
            this.toolTip1.SetToolTip(this.btnPrev, "الصفحة السابقة");
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnNext
            // 
            this.btnNext.Image = global::WorkSphere.Properties.Resources.left_arrow;
            this.btnNext.Location = new System.Drawing.Point(0, 0);
            this.btnNext.Name = "btnNext";
            this.btnNext.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnNext.Size = new System.Drawing.Size(50, 50);
            this.btnNext.TabIndex = 8;
            this.toolTip1.SetToolTip(this.btnNext, "الصفحة التالية");
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // cbNumberOfPages
            // 
            this.cbNumberOfPages.BackColor = System.Drawing.Color.White;
            this.cbNumberOfPages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNumberOfPages.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.cbNumberOfPages.FormattingEnabled = true;
            this.cbNumberOfPages.Location = new System.Drawing.Point(50, 0);
            this.cbNumberOfPages.Name = "cbNumberOfPages";
            this.cbNumberOfPages.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbNumberOfPages.Size = new System.Drawing.Size(107, 49);
            this.cbNumberOfPages.TabIndex = 7;
            this.toolTip1.SetToolTip(this.cbNumberOfPages, "عدد الصفحات");
            this.cbNumberOfPages.SelectedIndexChanged += new System.EventHandler(this.cbNumberOfPages_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.lblNumberOfItem);
            this.panel2.Controls.Add(this.cbNumberOfPages);
            this.panel2.Controls.Add(this.btnNext);
            this.panel2.Controls.Add(this.btnPrev);
            this.panel2.Location = new System.Drawing.Point(3, 479);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(308, 50);
            this.panel2.TabIndex = 6;
            // 
            // lblNumberOfItem
            // 
            this.lblNumberOfItem.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblNumberOfItem.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblNumberOfItem.ForeColor = System.Drawing.Color.Red;
            this.lblNumberOfItem.Location = new System.Drawing.Point(213, 0);
            this.lblNumberOfItem.Name = "lblNumberOfItem";
            this.lblNumberOfItem.Size = new System.Drawing.Size(95, 50);
            this.lblNumberOfItem.TabIndex = 7;
            this.lblNumberOfItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctrlEmployeePromotions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.panelState);
            this.Controls.Add(this.dgvListPromotions);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ctrlEmployeePromotions";
            this.Size = new System.Drawing.Size(1166, 544);
            this.Load += new System.EventHandler(this.ctrlEmployeePromotions_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListPromotions)).EndInit();
            this.panelState.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnExportAll;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvListPromotions;
        private System.Windows.Forms.Label lblStateTitle;
        private System.Windows.Forms.Panel panelState;
        private System.Windows.Forms.Label lblStateDescription;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cbNumberOfPages;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Label lblNumberOfItem;
        private System.Windows.Forms.Button btnExportSomeData;
    }
}
