using ProxyClasses;
using RemoteTasksAdmin.DataSources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteTasksAdmin
{
    public partial class FrmEditorTask : Form
    {
        private DsTaskRows _DsTaskRows;
        private PrxTask _DataSource;
        public PrxTask DataSource
        {
            get
            {
                return _DataSource;
            }
        }

        public FrmEditorTask(DsTaskRows dsTaskRows, PrxComputer prxComputer)
        {
            InitializeComponent();

            _DsTaskRows = dsTaskRows;
            _DataSource = new PrxTask()
            {
                Computer = prxComputer,
                TaskTime = DateTime.Now
            };

            this.bndTask.DataSource = _DataSource;
            this.bndTask.ResetBindings(false);

            this.cmbStartMomentType.Enabled = false;
            this.pickerTaskTime.Enabled = false;
            this.cmbDaysOfWeek.Enabled = false;
            this.txtDayInMonth.Enabled = false;
            this.chkOff.Enabled = false;
        }

        /// <summary>
        /// Переменная, которая отключает обработку события SelectedIndexChanged для выпадающих списков во время инициализации
        /// </summary>
        private bool _SelectedIndexChangedOff;

        public FrmEditorTask(DsTaskRows dsTaskRows, PrxTask prxTask)
        {
            InitializeComponent();

            _DsTaskRows = dsTaskRows;
            _DataSource = prxTask;

            // Отключаем обработку событий выпадающих списков
            _SelectedIndexChangedOff = true;

            this.cmbWorkType.SelectedIndex = (int)Math.Log(((int)_DataSource.WorkType), 2);
            // Включаем обработку событий выпадающих списков после завершения инициализации
            _SelectedIndexChangedOff = false;
            this.cmbStartMomentType.SelectedIndex = (int)_DataSource.StartMomentType;
            this.cmbDaysOfWeek.SelectedIndex = _DataSource.DayOfWeek-1;

            this.bndTask.DataSource = _DataSource;
            this.bndTask.ResetBindings(false);
        }

        /// <summary>
        /// При выборе типа задачи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbWorkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_SelectedIndexChangedOff) return;

            _DataSource.WorkType = (WorkEnum)(Math.Pow(2, cmbWorkType.SelectedIndex));

            string selectedItem = (string)this.cmbWorkType.SelectedItem;
            // Если выбран какой-то тип задачи, подключаем остальные элементы формы.
            if (!String.IsNullOrEmpty(selectedItem))
            {
                this.cmbStartMomentType.Enabled = true;
                this.pickerTaskTime.Enabled = true;
                this.cmbDaysOfWeek.Enabled = true;
                this.txtDayInMonth.Enabled = true;
                this.chkOff.Enabled = true;

                this.cmbStartMomentType.SelectedIndex = 0;
            }
        }

        private void FrmEditorTask_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// При выборе принципа выполнения задачи, скрываем и меняем расположение определённых панелей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbStartMomentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_SelectedIndexChangedOff) return;

            _DataSource.StartMomentType = (StartTimeEnum)cmbStartMomentType.SelectedIndex;

            this.panelAtDate.Visible = (this.cmbStartMomentType.SelectedIndex == 0);
            this.panelDayOfWeek.Visible = (this.cmbStartMomentType.SelectedIndex == 1);
            this.panelDayInMonth.Visible = (this.cmbStartMomentType.SelectedIndex == 2);

            this.panelDayOfWeek.Top = this.panelAtDate.Top;
            this.panelDayInMonth.Top = this.panelAtDate.Top;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                // Отправляем новую задачу в базу данных
                _DsTaskRows.Update(this.DataSource);
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbDaysOfWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_SelectedIndexChangedOff) return;

            // Дни недели должны сохранятся в базе данных
            // с первого по седьмой, поэтому индекс списка, который начинается с нуля, увеличиваем на 1
            _DataSource.DayOfWeek = cmbDaysOfWeek.SelectedIndex + 1;
        }
    }
}
