namespace RemoteTasksAdmin
{
    partial class FrmEditorTask
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
            this.cmbWorkType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbStartMomentType = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pickerTaskTime = new System.Windows.Forms.DateTimePicker();
            this.bndTask = new System.Windows.Forms.BindingSource(this.components);
            this.lblTaskTime = new System.Windows.Forms.Label();
            this.txtDayInMonth = new System.Windows.Forms.NumericUpDown();
            this.lblDayInMonth = new System.Windows.Forms.Label();
            this.cmbDaysOfWeek = new System.Windows.Forms.ComboBox();
            this.lblDaysOfWeek = new System.Windows.Forms.Label();
            this.chkOff = new System.Windows.Forms.CheckBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.panelAtDate = new System.Windows.Forms.Panel();
            this.panelDayOfWeek = new System.Windows.Forms.Panel();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.panelDayInMonth = new System.Windows.Forms.Panel();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bndTask)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDayInMonth)).BeginInit();
            this.panelAtDate.SuspendLayout();
            this.panelDayOfWeek.SuspendLayout();
            this.panelDayInMonth.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbWorkType
            // 
            this.cmbWorkType.FormattingEnabled = true;
            this.cmbWorkType.Items.AddRange(new object[] {
            "Очистить корзину",
            "Очистить временные файлы",
            "Удалить файлы дампа памяти для системных ошибок",
            "Удалить эскизы",
            "Удалить старые файлы программы CheckDisk",
            "Удалить временные файлы инсталляций",
            "Удалить временные файлы Интернета",
            "Удалить файлы загрузки Program Files",
            "Удалить временные файлы инсталлятора Windows",
            "Удалить лог-файл обновлений Windows"});
            this.cmbWorkType.Location = new System.Drawing.Point(152, 6);
            this.cmbWorkType.Name = "cmbWorkType";
            this.cmbWorkType.Size = new System.Drawing.Size(364, 21);
            this.cmbWorkType.TabIndex = 0;
            this.cmbWorkType.SelectedIndexChanged += new System.EventHandler(this.cmbWorkType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Вид задачи:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Принцип запуска:";
            // 
            // cmbStartMomentType
            // 
            this.cmbStartMomentType.FormattingEnabled = true;
            this.cmbStartMomentType.Items.AddRange(new object[] {
            "В определённую дату-время",
            "Раз в неделю",
            "Раз в месяц"});
            this.cmbStartMomentType.Location = new System.Drawing.Point(152, 33);
            this.cmbStartMomentType.Name = "cmbStartMomentType";
            this.cmbStartMomentType.Size = new System.Drawing.Size(200, 21);
            this.cmbStartMomentType.TabIndex = 3;
            this.cmbStartMomentType.SelectedIndexChanged += new System.EventHandler(this.cmbStartMomentType_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(432, 159);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // pickerTaskTime
            // 
            this.pickerTaskTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.pickerTaskTime.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bndTask, "TaskTime", true));
            this.pickerTaskTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.pickerTaskTime.Location = new System.Drawing.Point(144, 3);
            this.pickerTaskTime.Name = "pickerTaskTime";
            this.pickerTaskTime.Size = new System.Drawing.Size(200, 20);
            this.pickerTaskTime.TabIndex = 5;
            // 
            // bndTask
            // 
            this.bndTask.DataSource = typeof(ProxyClasses.PrxTask);
            // 
            // lblTaskTime
            // 
            this.lblTaskTime.AutoSize = true;
            this.lblTaskTime.Location = new System.Drawing.Point(4, 3);
            this.lblTaskTime.Name = "lblTaskTime";
            this.lblTaskTime.Size = new System.Drawing.Size(124, 13);
            this.lblTaskTime.TabIndex = 6;
            this.lblTaskTime.Text = "Дата и время запуска:";
            // 
            // txtDayInMonth
            // 
            this.txtDayInMonth.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bndTask, "DayInMonth", true));
            this.txtDayInMonth.Location = new System.Drawing.Point(144, 3);
            this.txtDayInMonth.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.txtDayInMonth.Name = "txtDayInMonth";
            this.txtDayInMonth.Size = new System.Drawing.Size(46, 20);
            this.txtDayInMonth.TabIndex = 7;
            this.txtDayInMonth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblDayInMonth
            // 
            this.lblDayInMonth.AutoSize = true;
            this.lblDayInMonth.Location = new System.Drawing.Point(4, 6);
            this.lblDayInMonth.Name = "lblDayInMonth";
            this.lblDayInMonth.Size = new System.Drawing.Size(78, 13);
            this.lblDayInMonth.TabIndex = 8;
            this.lblDayInMonth.Text = "День месяца:";
            // 
            // cmbDaysOfWeek
            // 
            this.cmbDaysOfWeek.FormattingEnabled = true;
            this.cmbDaysOfWeek.Items.AddRange(new object[] {
            "Понедельник",
            "Вторник",
            "Среда",
            "Четверг",
            "Пятница",
            "Суббота",
            "Воскресенье"});
            this.cmbDaysOfWeek.Location = new System.Drawing.Point(144, 3);
            this.cmbDaysOfWeek.Name = "cmbDaysOfWeek";
            this.cmbDaysOfWeek.Size = new System.Drawing.Size(200, 21);
            this.cmbDaysOfWeek.TabIndex = 9;
            this.cmbDaysOfWeek.SelectedIndexChanged += new System.EventHandler(this.cmbDaysOfWeek_SelectedIndexChanged);
            // 
            // lblDaysOfWeek
            // 
            this.lblDaysOfWeek.AutoSize = true;
            this.lblDaysOfWeek.Location = new System.Drawing.Point(4, 6);
            this.lblDaysOfWeek.Name = "lblDaysOfWeek";
            this.lblDaysOfWeek.Size = new System.Drawing.Size(76, 13);
            this.lblDaysOfWeek.TabIndex = 10;
            this.lblDaysOfWeek.Text = "День недели:";
            // 
            // chkOff
            // 
            this.chkOff.AutoSize = true;
            this.chkOff.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bndTask, "Off", true));
            this.chkOff.Location = new System.Drawing.Point(152, 136);
            this.chkOff.Name = "chkOff";
            this.chkOff.Size = new System.Drawing.Size(82, 17);
            this.chkOff.TabIndex = 11;
            this.chkOff.Text = "Отключена";
            this.chkOff.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(351, 159);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 12;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // panelAtDate
            // 
            this.panelAtDate.Controls.Add(this.pickerTaskTime);
            this.panelAtDate.Controls.Add(this.lblTaskTime);
            this.panelAtDate.Location = new System.Drawing.Point(8, 57);
            this.panelAtDate.Name = "panelAtDate";
            this.panelAtDate.Size = new System.Drawing.Size(347, 28);
            this.panelAtDate.TabIndex = 13;
            // 
            // panelDayOfWeek
            // 
            this.panelDayOfWeek.Controls.Add(this.dateTimePicker1);
            this.panelDayOfWeek.Controls.Add(this.label3);
            this.panelDayOfWeek.Controls.Add(this.cmbDaysOfWeek);
            this.panelDayOfWeek.Controls.Add(this.lblDaysOfWeek);
            this.panelDayOfWeek.Location = new System.Drawing.Point(8, 92);
            this.panelDayOfWeek.Name = "panelDayOfWeek";
            this.panelDayOfWeek.Size = new System.Drawing.Size(347, 56);
            this.panelDayOfWeek.TabIndex = 14;
            this.panelDayOfWeek.Visible = false;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dateTimePicker1.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bndTask, "TaskTime", true));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePicker1.Location = new System.Drawing.Point(144, 29);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(71, 20);
            this.dateTimePicker1.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Дата и время запуска:";
            // 
            // panelDayInMonth
            // 
            this.panelDayInMonth.Controls.Add(this.dateTimePicker2);
            this.panelDayInMonth.Controls.Add(this.label4);
            this.panelDayInMonth.Controls.Add(this.txtDayInMonth);
            this.panelDayInMonth.Controls.Add(this.lblDayInMonth);
            this.panelDayInMonth.Location = new System.Drawing.Point(8, 156);
            this.panelDayInMonth.Name = "panelDayInMonth";
            this.panelDayInMonth.Size = new System.Drawing.Size(347, 56);
            this.panelDayInMonth.TabIndex = 15;
            this.panelDayInMonth.Visible = false;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dateTimePicker2.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bndTask, "TaskTime", true));
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePicker2.Location = new System.Drawing.Point(144, 29);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.ShowUpDown = true;
            this.dateTimePicker2.Size = new System.Drawing.Size(71, 20);
            this.dateTimePicker2.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Дата и время запуска:";
            // 
            // FrmEditorTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 192);
            this.Controls.Add(this.panelDayInMonth);
            this.Controls.Add(this.panelDayOfWeek);
            this.Controls.Add(this.panelAtDate);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.chkOff);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cmbStartMomentType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbWorkType);
            this.Name = "FrmEditorTask";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Task Editor";
            this.Load += new System.EventHandler(this.FrmEditorTask_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bndTask)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDayInMonth)).EndInit();
            this.panelAtDate.ResumeLayout(false);
            this.panelAtDate.PerformLayout();
            this.panelDayOfWeek.ResumeLayout(false);
            this.panelDayOfWeek.PerformLayout();
            this.panelDayInMonth.ResumeLayout(false);
            this.panelDayInMonth.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbWorkType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbStartMomentType;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.BindingSource bndTask;
        private System.Windows.Forms.DateTimePicker pickerTaskTime;
        private System.Windows.Forms.Label lblTaskTime;
        private System.Windows.Forms.NumericUpDown txtDayInMonth;
        private System.Windows.Forms.Label lblDayInMonth;
        private System.Windows.Forms.ComboBox cmbDaysOfWeek;
        private System.Windows.Forms.Label lblDaysOfWeek;
        private System.Windows.Forms.CheckBox chkOff;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Panel panelAtDate;
        private System.Windows.Forms.Panel panelDayOfWeek;
        private System.Windows.Forms.Panel panelDayInMonth;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
    }
}