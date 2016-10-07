namespace RemoteTasksAdmin
{
    partial class FrmListComputers
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmListComputers));
            this.grvComputers = new System.Windows.Forms.DataGridView();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grvTaskResults = new System.Windows.Forms.DataGridView();
            this.grvTasks = new System.Windows.Forms.DataGridView();
            this.StartDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Off = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.новаяЗадачаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьЗадачуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.idDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.endTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.durationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deletedFilesNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deletedFilesSizeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deletedFilesPathDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.resultDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.failReasonDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bndTaskResults = new System.Windows.Forms.BindingSource(this.components);
            this.idDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.computerNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bndTasks = new System.Windows.Forms.BindingSource(this.components);
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.localIPAddressDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.internetIPAddressDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bndComputers = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grvComputers)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvTaskResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvTasks)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bndTaskResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bndTasks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bndComputers)).BeginInit();
            this.SuspendLayout();
            // 
            // grvComputers
            // 
            this.grvComputers.AllowUserToAddRows = false;
            this.grvComputers.AllowUserToDeleteRows = false;
            this.grvComputers.AutoGenerateColumns = false;
            this.grvComputers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvComputers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.stateDataGridViewTextBoxColumn,
            this.localIPAddressDataGridViewTextBoxColumn,
            this.internetIPAddressDataGridViewTextBoxColumn});
            this.grvComputers.DataSource = this.bndComputers;
            this.grvComputers.Location = new System.Drawing.Point(3, 3);
            this.grvComputers.Name = "grvComputers";
            this.grvComputers.ReadOnly = true;
            this.grvComputers.Size = new System.Drawing.Size(331, 131);
            this.grvComputers.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView1.Location = new System.Drawing.Point(12, 12);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(190, 420);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.grvTaskResults);
            this.panel1.Controls.Add(this.grvTasks);
            this.panel1.Controls.Add(this.grvComputers);
            this.panel1.Location = new System.Drawing.Point(208, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(936, 420);
            this.panel1.TabIndex = 2;
            // 
            // grvTaskResults
            // 
            this.grvTaskResults.AllowUserToAddRows = false;
            this.grvTaskResults.AllowUserToDeleteRows = false;
            this.grvTaskResults.AutoGenerateColumns = false;
            this.grvTaskResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvTaskResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn2,
            this.taskDataGridViewTextBoxColumn,
            this.startTimeDataGridViewTextBoxColumn,
            this.endTimeDataGridViewTextBoxColumn,
            this.durationDataGridViewTextBoxColumn,
            this.deletedFilesNumberDataGridViewTextBoxColumn,
            this.deletedFilesSizeDataGridViewTextBoxColumn,
            this.deletedFilesPathDataGridViewTextBoxColumn,
            this.resultDataGridViewTextBoxColumn,
            this.failReasonDataGridViewTextBoxColumn});
            this.grvTaskResults.DataSource = this.bndTaskResults;
            this.grvTaskResults.Location = new System.Drawing.Point(4, 278);
            this.grvTaskResults.Name = "grvTaskResults";
            this.grvTaskResults.ReadOnly = true;
            this.grvTaskResults.Size = new System.Drawing.Size(330, 131);
            this.grvTaskResults.TabIndex = 2;
            // 
            // grvTasks
            // 
            this.grvTasks.AllowUserToAddRows = false;
            this.grvTasks.AllowUserToDeleteRows = false;
            this.grvTasks.AutoGenerateColumns = false;
            this.grvTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvTasks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn1,
            this.nameDataGridViewTextBoxColumn1,
            this.computerNameDataGridViewTextBoxColumn,
            this.StartDescription,
            this.Off});
            this.grvTasks.DataSource = this.bndTasks;
            this.grvTasks.Location = new System.Drawing.Point(3, 140);
            this.grvTasks.Name = "grvTasks";
            this.grvTasks.ReadOnly = true;
            this.grvTasks.Size = new System.Drawing.Size(331, 131);
            this.grvTasks.TabIndex = 1;
            this.grvTasks.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grvTasks_CellDoubleClick);
            // 
            // StartDescription
            // 
            this.StartDescription.DataPropertyName = "StartDescription";
            this.StartDescription.HeaderText = "Момент запуска";
            this.StartDescription.Name = "StartDescription";
            this.StartDescription.ReadOnly = true;
            this.StartDescription.Width = 250;
            // 
            // Off
            // 
            this.Off.DataPropertyName = "Off";
            this.Off.HeaderText = "Отключена";
            this.Off.Name = "Off";
            this.Off.ReadOnly = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новаяЗадачаToolStripMenuItem,
            this.удалитьЗадачуToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(158, 48);
            // 
            // новаяЗадачаToolStripMenuItem
            // 
            this.новаяЗадачаToolStripMenuItem.Name = "новаяЗадачаToolStripMenuItem";
            this.новаяЗадачаToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.новаяЗадачаToolStripMenuItem.Text = "Новая задача";
            // 
            // удалитьЗадачуToolStripMenuItem
            // 
            this.удалитьЗадачуToolStripMenuItem.Name = "удалитьЗадачуToolStripMenuItem";
            this.удалитьЗадачуToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.удалитьЗадачуToolStripMenuItem.Text = "Удалить задачу";
            // 
            // idDataGridViewTextBoxColumn2
            // 
            this.idDataGridViewTextBoxColumn2.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn2.HeaderText = "Id результата";
            this.idDataGridViewTextBoxColumn2.Name = "idDataGridViewTextBoxColumn2";
            this.idDataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // taskDataGridViewTextBoxColumn
            // 
            this.taskDataGridViewTextBoxColumn.DataPropertyName = "TaskName";
            this.taskDataGridViewTextBoxColumn.HeaderText = "Задача";
            this.taskDataGridViewTextBoxColumn.Name = "taskDataGridViewTextBoxColumn";
            this.taskDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // startTimeDataGridViewTextBoxColumn
            // 
            this.startTimeDataGridViewTextBoxColumn.DataPropertyName = "StartTime";
            this.startTimeDataGridViewTextBoxColumn.HeaderText = "Начало выполнения";
            this.startTimeDataGridViewTextBoxColumn.Name = "startTimeDataGridViewTextBoxColumn";
            this.startTimeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // endTimeDataGridViewTextBoxColumn
            // 
            this.endTimeDataGridViewTextBoxColumn.DataPropertyName = "EndTime";
            this.endTimeDataGridViewTextBoxColumn.HeaderText = "Завершение выполнения";
            this.endTimeDataGridViewTextBoxColumn.Name = "endTimeDataGridViewTextBoxColumn";
            this.endTimeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // durationDataGridViewTextBoxColumn
            // 
            this.durationDataGridViewTextBoxColumn.DataPropertyName = "Duration";
            this.durationDataGridViewTextBoxColumn.HeaderText = "Продолжительность, мс";
            this.durationDataGridViewTextBoxColumn.Name = "durationDataGridViewTextBoxColumn";
            this.durationDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // deletedFilesNumberDataGridViewTextBoxColumn
            // 
            this.deletedFilesNumberDataGridViewTextBoxColumn.DataPropertyName = "DeletedFilesNumber";
            this.deletedFilesNumberDataGridViewTextBoxColumn.HeaderText = "Удалено файлов";
            this.deletedFilesNumberDataGridViewTextBoxColumn.Name = "deletedFilesNumberDataGridViewTextBoxColumn";
            this.deletedFilesNumberDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // deletedFilesSizeDataGridViewTextBoxColumn
            // 
            this.deletedFilesSizeDataGridViewTextBoxColumn.DataPropertyName = "DeletedFilesSize";
            this.deletedFilesSizeDataGridViewTextBoxColumn.HeaderText = "Размер удалённых файлов";
            this.deletedFilesSizeDataGridViewTextBoxColumn.Name = "deletedFilesSizeDataGridViewTextBoxColumn";
            this.deletedFilesSizeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // deletedFilesPathDataGridViewTextBoxColumn
            // 
            this.deletedFilesPathDataGridViewTextBoxColumn.DataPropertyName = "DeletedFilesPath";
            this.deletedFilesPathDataGridViewTextBoxColumn.HeaderText = "Путь для удаления";
            this.deletedFilesPathDataGridViewTextBoxColumn.Name = "deletedFilesPathDataGridViewTextBoxColumn";
            this.deletedFilesPathDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // resultDataGridViewTextBoxColumn
            // 
            this.resultDataGridViewTextBoxColumn.DataPropertyName = "Result";
            this.resultDataGridViewTextBoxColumn.HeaderText = "Результат";
            this.resultDataGridViewTextBoxColumn.Name = "resultDataGridViewTextBoxColumn";
            this.resultDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // failReasonDataGridViewTextBoxColumn
            // 
            this.failReasonDataGridViewTextBoxColumn.DataPropertyName = "FailReason";
            this.failReasonDataGridViewTextBoxColumn.HeaderText = "Причина неудачи";
            this.failReasonDataGridViewTextBoxColumn.Name = "failReasonDataGridViewTextBoxColumn";
            this.failReasonDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bndTaskResults
            // 
            this.bndTaskResults.DataSource = typeof(ProxyClasses.PrxTaskResult);
            // 
            // idDataGridViewTextBoxColumn1
            // 
            this.idDataGridViewTextBoxColumn1.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn1.HeaderText = "Id задачи";
            this.idDataGridViewTextBoxColumn1.Name = "idDataGridViewTextBoxColumn1";
            this.idDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // nameDataGridViewTextBoxColumn1
            // 
            this.nameDataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn1.HeaderText = "Задача";
            this.nameDataGridViewTextBoxColumn1.Name = "nameDataGridViewTextBoxColumn1";
            this.nameDataGridViewTextBoxColumn1.ReadOnly = true;
            this.nameDataGridViewTextBoxColumn1.Width = 250;
            // 
            // computerNameDataGridViewTextBoxColumn
            // 
            this.computerNameDataGridViewTextBoxColumn.DataPropertyName = "ComputerName";
            this.computerNameDataGridViewTextBoxColumn.HeaderText = "Компьютер";
            this.computerNameDataGridViewTextBoxColumn.Name = "computerNameDataGridViewTextBoxColumn";
            this.computerNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bndTasks
            // 
            this.bndTasks.DataSource = typeof(ProxyClasses.PrxTask);
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "Id компьютера";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Компьютер";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // stateDataGridViewTextBoxColumn
            // 
            this.stateDataGridViewTextBoxColumn.DataPropertyName = "State";
            this.stateDataGridViewTextBoxColumn.HeaderText = "State";
            this.stateDataGridViewTextBoxColumn.Name = "stateDataGridViewTextBoxColumn";
            this.stateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // localIPAddressDataGridViewTextBoxColumn
            // 
            this.localIPAddressDataGridViewTextBoxColumn.DataPropertyName = "LocalIPAddressString";
            this.localIPAddressDataGridViewTextBoxColumn.HeaderText = "LocalIPAddress";
            this.localIPAddressDataGridViewTextBoxColumn.Name = "localIPAddressDataGridViewTextBoxColumn";
            this.localIPAddressDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // internetIPAddressDataGridViewTextBoxColumn
            // 
            this.internetIPAddressDataGridViewTextBoxColumn.DataPropertyName = "InternetIPAddressString";
            this.internetIPAddressDataGridViewTextBoxColumn.HeaderText = "InternetIPAddress";
            this.internetIPAddressDataGridViewTextBoxColumn.Name = "internetIPAddressDataGridViewTextBoxColumn";
            this.internetIPAddressDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bndComputers
            // 
            this.bndComputers.DataSource = typeof(ProxyClasses.PrxComputer);
            // 
            // FrmListComputers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1156, 444);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.treeView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmListComputers";
            this.Text = "Remote Tasks Admin";
            this.Load += new System.EventHandler(this.FrmListComputers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grvComputers)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvTaskResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grvTasks)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bndTaskResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bndTasks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bndComputers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grvComputers;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView grvTasks;
        private System.Windows.Forms.BindingSource bndComputers;
        private System.Windows.Forms.BindingSource bndTasks;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem новаяЗадачаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьЗадачуToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn computerNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartDescription;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Off;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn localIPAddressDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn internetIPAddressDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridView grvTaskResults;
        private System.Windows.Forms.BindingSource bndTaskResults;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn startTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn endTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn durationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn deletedFilesNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn deletedFilesSizeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn deletedFilesPathDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn resultDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn failReasonDataGridViewTextBoxColumn;
    }
}

