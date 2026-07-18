namespace WorkSphere.Leave_Types
{
    partial class ctrlEmployeeAbsences
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnExportAll = new System.Windows.Forms.Button();
            this.btnExportSomData = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dgvEmployeeAbsences = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbNumberOfPages = new System.Windows.Forms.ComboBox();
            this.lblNumberOfItems = new System.Windows.Forms.Label();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.panelState = new System.Windows.Forms.Panel();
            this.lblStateDescription = new System.Windows.Forms.Label();
            this.lblStateTitle = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployeeAbsences)).BeginInit();
            this.panel2.SuspendLayout();
            this.panelState.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanel1.Controls.Add(this.btnAdd);
            this.flowLayoutPanel1.Controls.Add(this.btnEdit);
            this.flowLayoutPanel1.Controls.Add(this.btnDelete);
            this.flowLayoutPanel1.Controls.Add(this.btnExportAll);
            this.flowLayoutPanel1.Controls.Add(this.btnExportSomData);
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1166, 62);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::WorkSphere.Properties.Resources.Add;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(1048, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(115, 50);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Tag = "Absences.Add";
            this.btnAdd.Text = "    اضافة";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Image = global::WorkSphere.Properties.Resources.Edit;
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(927, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(115, 50);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Tag = "Absences.Edit";
            this.btnEdit.Text = "    تعديل";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::WorkSphere.Properties.Resources.delete;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(811, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(110, 50);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Tag = "Absences.Delete";
            this.btnDelete.Text = "    حذف";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnExportAll
            // 
            this.btnExportAll.Image = global::WorkSphere.Properties.Resources.excel;
            this.btnExportAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportAll.Location = new System.Drawing.Point(695, 3);
            this.btnExportAll.Name = "btnExportAll";
            this.btnExportAll.Size = new System.Drawing.Size(110, 50);
            this.btnExportAll.TabIndex = 3;
            this.btnExportAll.Tag = "Absences.Export";
            this.btnExportAll.Text = "    الكل";
            this.btnExportAll.UseVisualStyleBackColor = true;
            this.btnExportAll.Click += new System.EventHandler(this.btnExportAll_Click);
            // 
            // btnExportSomData
            // 
            this.btnExportSomData.Image = global::WorkSphere.Properties.Resources.xls;
            this.btnExportSomData.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportSomData.Location = new System.Drawing.Point(574, 3);
            this.btnExportSomData.Name = "btnExportSomData";
            this.btnExportSomData.Size = new System.Drawing.Size(115, 50);
            this.btnExportSomData.TabIndex = 4;
            this.btnExportSomData.Tag = "Absences.Export";
            this.btnExportSomData.Text = "   الشبكة";
            this.btnExportSomData.UseVisualStyleBackColor = true;
            this.btnExportSomData.Click += new System.EventHandler(this.btnExportSomData_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Location = new System.Drawing.Point(268, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 50);
            this.panel1.TabIndex = 5;
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtSearch.Location = new System.Drawing.Point(49, 0);
            this.txtSearch.Multiline = true;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(251, 50);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // btnSearch
            // 
            this.btnSearch.Image = global::WorkSphere.Properties.Resources.search;
            this.btnSearch.Location = new System.Drawing.Point(0, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(50, 50);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dgvEmployeeAbsences
            // 
            this.dgvEmployeeAbsences.AllowUserToAddRows = false;
            this.dgvEmployeeAbsences.AllowUserToDeleteRows = false;
            this.dgvEmployeeAbsences.AllowUserToOrderColumns = true;
            this.dgvEmployeeAbsences.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEmployeeAbsences.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvEmployeeAbsences.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEmployeeAbsences.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvEmployeeAbsences.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmployeeAbsences.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEmployeeAbsences.EnableHeadersVisualStyles = false;
            this.dgvEmployeeAbsences.GridColor = System.Drawing.Color.LightGray;
            this.dgvEmployeeAbsences.Location = new System.Drawing.Point(0, 62);
            this.dgvEmployeeAbsences.Name = "dgvEmployeeAbsences";
            this.dgvEmployeeAbsences.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEmployeeAbsences.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvEmployeeAbsences.RowHeadersVisible = false;
            this.dgvEmployeeAbsences.RowHeadersWidth = 62;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvEmployeeAbsences.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvEmployeeAbsences.RowTemplate.Height = 28;
            this.dgvEmployeeAbsences.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEmployeeAbsences.Size = new System.Drawing.Size(1166, 482);
            this.dgvEmployeeAbsences.TabIndex = 1;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Image = global::WorkSphere.Properties.Resources.Refresh;
            this.btnRefresh.Location = new System.Drawing.Point(1103, 476);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(50, 50);
            this.btnRefresh.TabIndex = 7;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.cbNumberOfPages);
            this.panel2.Controls.Add(this.lblNumberOfItems);
            this.panel2.Controls.Add(this.btnPrev);
            this.panel2.Controls.Add(this.btnNext);
            this.panel2.Location = new System.Drawing.Point(3, 476);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(297, 50);
            this.panel2.TabIndex = 8;
            // 
            // cbNumberOfPages
            // 
            this.cbNumberOfPages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNumberOfPages.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbNumberOfPages.FormattingEnabled = true;
            this.cbNumberOfPages.Location = new System.Drawing.Point(56, 1);
            this.cbNumberOfPages.Name = "cbNumberOfPages";
            this.cbNumberOfPages.Size = new System.Drawing.Size(110, 46);
            this.cbNumberOfPages.TabIndex = 9;
            this.cbNumberOfPages.SelectedIndexChanged += new System.EventHandler(this.cbNumberOfPages_SelectedIndexChanged);
            // 
            // lblNumberOfItems
            // 
            this.lblNumberOfItems.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblNumberOfItems.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfItems.ForeColor = System.Drawing.Color.Red;
            this.lblNumberOfItems.Location = new System.Drawing.Point(229, 0);
            this.lblNumberOfItems.Name = "lblNumberOfItems";
            this.lblNumberOfItems.Size = new System.Drawing.Size(68, 50);
            this.lblNumberOfItems.TabIndex = 10;
            this.lblNumberOfItems.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnPrev
            // 
            this.btnPrev.Image = global::WorkSphere.Properties.Resources.right_arrow;
            this.btnPrev.Location = new System.Drawing.Point(163, 0);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(60, 50);
            this.btnPrev.TabIndex = 9;
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnNext
            // 
            this.btnNext.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnNext.Image = global::WorkSphere.Properties.Resources.left_arrow;
            this.btnNext.Location = new System.Drawing.Point(0, 0);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(60, 50);
            this.btnNext.TabIndex = 9;
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // panelState
            // 
            this.panelState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panelState.BackColor = System.Drawing.Color.White;
            this.panelState.Controls.Add(this.lblStateDescription);
            this.panelState.Controls.Add(this.lblStateTitle);
            this.panelState.Location = new System.Drawing.Point(282, 197);
            this.panelState.Name = "panelState";
            this.panelState.Size = new System.Drawing.Size(602, 150);
            this.panelState.TabIndex = 9;
            // 
            // lblStateDescription
            // 
            this.lblStateDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStateDescription.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStateDescription.ForeColor = System.Drawing.Color.DarkGray;
            this.lblStateDescription.Location = new System.Drawing.Point(0, 65);
            this.lblStateDescription.Name = "lblStateDescription";
            this.lblStateDescription.Size = new System.Drawing.Size(602, 59);
            this.lblStateDescription.TabIndex = 1;
            this.lblStateDescription.Text = "لايوجد بيانات";
            this.lblStateDescription.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblStateTitle
            // 
            this.lblStateTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStateTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStateTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblStateTitle.Location = new System.Drawing.Point(0, 0);
            this.lblStateTitle.Name = "lblStateTitle";
            this.lblStateTitle.Size = new System.Drawing.Size(602, 65);
            this.lblStateTitle.TabIndex = 0;
            this.lblStateTitle.Text = "لايوجد بيانات";
            this.lblStateTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ctrlEmployeeAbsences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.panelState);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dgvEmployeeAbsences);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ctrlEmployeeAbsences";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(1166, 544);
            this.Load += new System.EventHandler(this.ctrlEmployeeAbsences_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployeeAbsences)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panelState.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnExportAll;
        private System.Windows.Forms.Button btnExportSomData;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvEmployeeAbsences;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.ComboBox cbNumberOfPages;
        private System.Windows.Forms.Label lblNumberOfItems;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Panel panelState;
        private System.Windows.Forms.Label lblStateDescription;
        private System.Windows.Forms.Label lblStateTitle;
    }
}
