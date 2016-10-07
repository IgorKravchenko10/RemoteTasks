using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RemoteTasksAdmin.DataSources;
using ProxyClasses;


namespace RemoteTasksAdmin
{
    public partial class FrmListComputers : Form
    {
        private DsComputerRows _DsComputerRows;
        private DsTaskRows _DsTaskRows;
        private DsTaskResultRows _DsTaskResultRows;
        private SrvSettings _HostSettings;

        public FrmListComputers()
        {
            InitializeComponent();
            _HostSettings = new SrvSettings(SrvSettings.HostTypeEnum.TCP, "localhost", 9025, false);
            _DsComputerRows = new DsComputerRows(_HostSettings);
            _DsTaskRows = new DsTaskRows(_HostSettings);
            _DsTaskResultRows = new DsTaskResultRows(_HostSettings);
        }

        /// <summary>
        /// Загружается форма
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmListComputers_Load(object sender, EventArgs e)
        {
            FillTree();
        }

        private enum GroupTreeNodeTypeEnum
        {
            Tasks,
            Results
        }

        private class GroupTreeNode
        {
            public GroupTreeNodeTypeEnum GroupTreeNodeType { get; set; }

            public PrxComputer Computer { get; set; }
        }

        /// <summary>
        /// Процедура обработки события, возникающая при смене выделенного узла в дереве
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                // Проверяем, есть ли объект в Теге
                if (e.Node.Tag != null)
                {
                    if (e.Node.Tag is GroupTreeNode)
                    {
                        // Получаем объект для группы задач, чтоб достать из него объект prxComputer
                        GroupTreeNode groupTreeNode = (GroupTreeNode)e.Node.Tag;
                        // Если это объект типа GroupTreeNode и это группа для задач
                        if (((GroupTreeNode)e.Node.Tag).GroupTreeNodeType == GroupTreeNodeTypeEnum.Tasks)
                        {
                            // Задаём видимым Грид с задачей
                            SetGridVisible(this.grvTasks);
                            // Загружаем список задач в источник данных
                            _DsTaskRows.LoadData(groupTreeNode.Computer);
                            // Присваиваем объекту связывания список с задачами
                            this.bndTasks.DataSource = _DsTaskRows.List;
                            // Перечитываем данные и выводим в Гриде
                            this.bndTasks.ResetBindings(false);
                        }
                        if (((GroupTreeNode)e.Node.Tag).GroupTreeNodeType == GroupTreeNodeTypeEnum.Results)
                        {
                            SetGridVisible(this.grvTaskResults);
                            _DsTaskResultRows.LoadData(groupTreeNode.Computer);
                            this.bndTaskResults.DataSource = _DsTaskResultRows.List;
                            this.bndTaskResults.ResetBindings(false);
                        }
                    }
                }
                else
                {
                    // Если в Теге пусто, значит, стоим в корне дерева. делаем список компьютеров видимым
                    SetGridVisible(this.grvComputers);
                    // Загружаем список компьютеров в Грид
                    SetComputersToGrid(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
        }

        /// <summary>
        /// Загружаем список компьютеров в Грид из источника данных
        /// </summary>
        /// <param name="needToReload">Показывает, нужно ли перечитывать данные в источник данных из wcf-сервиса</param>
        private void SetComputersToGrid(bool needToReload)
        {
            if (needToReload)
            {
                _DsComputerRows.LoadData();
            }
            this.bndComputers.DataSource = _DsComputerRows.List;
            this.bndComputers.ResetBindings(false);
        }

        /// <summary>
        /// Заполняем главное дерево
        /// </summary>
        private void FillTree()
        {
            CreateTaskNodeContextMenu();

            _DsComputerRows.LoadData();
            TreeNode rootTreeNode = new TreeNode("Remote Tasks");
            treeView1.Nodes.Add(rootTreeNode);

            foreach (PrxComputer prxComputer in _DsComputerRows.List)
            {
                TreeNode computerTreeNode = new TreeNode(prxComputer.Name);
                computerTreeNode.Tag = prxComputer;
                rootTreeNode.Nodes.Add(computerTreeNode);

                TreeNode tasksGroupTreeNode = new TreeNode("Назначенные задания");
                tasksGroupTreeNode.ContextMenu = _NodeTaskContextMenu;
                GroupTreeNode groupTreeNode = new GroupTreeNode()
                {
                    Computer = prxComputer,
                    GroupTreeNodeType = GroupTreeNodeTypeEnum.Tasks
                };
                tasksGroupTreeNode.Tag = groupTreeNode;
                computerTreeNode.Nodes.Add(tasksGroupTreeNode);

                TreeNode taskResultsGroupTreeNode = new TreeNode("Выполненные задания");
                groupTreeNode = new GroupTreeNode()
                {
                    Computer = prxComputer,
                    GroupTreeNodeType = GroupTreeNodeTypeEnum.Results
                };
                taskResultsGroupTreeNode.Tag = groupTreeNode;
                computerTreeNode.Nodes.Add(taskResultsGroupTreeNode);
            }
        }

        /// <summary>
        /// Устанавливаем видимым тот Грид, который спускаем в качестве параметра
        /// Остальные Гриды делаем невидимыми
        /// </summary>
        /// <param name="dataGridView">Грид, который хотим сделать видимым</param>
        private void SetGridVisible(DataGridView dataGridView)
        {
            this.grvComputers.Dock = DockStyle.Fill;
            this.grvTasks.Dock = DockStyle.Fill;
            this.grvTaskResults.Dock = DockStyle.Fill;
            this.grvComputers.Visible = (dataGridView == this.grvComputers);
            this.grvTasks.Visible = (dataGridView == this.grvTasks);
            this.grvTaskResults.Visible = (dataGridView == this.grvTaskResults);
        }

        private ContextMenu _NodeTaskContextMenu;
        /// <summary>
        /// Создаём контекстное меню для узлов задач
        /// </summary>
        private void CreateTaskNodeContextMenu()
        {
            _NodeTaskContextMenu = new ContextMenu();

            MenuItem createMenuItem = new MenuItem("Создать задачу");
            createMenuItem.Click += new EventHandler(NewTaskMenuItem_Click);
            _NodeTaskContextMenu.MenuItems.Add(createMenuItem);

            MenuItem deleteMenuItem = new MenuItem("Удалить задачу");
            _NodeTaskContextMenu.MenuItems.Add(deleteMenuItem);
        }

        /// <summary>
        /// Добавляем новую задачу в базу данных
        /// </summary>
        /// <param name="prxComputer">Компьютер, которому назначаем задачу</param>
        private void AddNewTask(PrxComputer prxComputer)
        {
            using (FrmEditorTask frmEditorTask = new FrmEditorTask(_DsTaskRows, prxComputer))
            {
                // Если нажата кнопка ОК в форме создания задачи
                if (frmEditorTask.ShowDialog(this) == DialogResult.OK)
                {
                    // Загружаем список задач для компьютера
                    _DsTaskRows.LoadData(prxComputer);
                    // Перечитываем элементы и обновляем их значения
                    this.bndTasks.ResetBindings(false);
                }
            };
        }

        private void EditTask(int id)
        {
            PrxTask prxTask = _DsTaskRows.GetItem(id);
            using (FrmEditorTask frmEditorTask=new FrmEditorTask(_DsTaskRows, prxTask))
            {
                // Если нажата кнопка ОК в форме создания задачи
                if (frmEditorTask.ShowDialog(this) == DialogResult.OK)
                {
                    // Загружаем список задач для компьютера
                    _DsTaskRows.LoadData(prxTask.Computer);
                    // Перечитываем элементы и обновляем их значения
                    this.bndTasks.ResetBindings(false);
                }
            };
        }

        /// <summary>
        /// Нажатие кнопки создания новой задачи в контекстном меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewTaskMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.treeView1.SelectedNode;
            if (selectedNode.Tag != null)
            {
                if (selectedNode.Tag is GroupTreeNode)
                {
                    GroupTreeNode groupTreeNode = (GroupTreeNode)selectedNode.Tag;
                    AddNewTask(groupTreeNode.Computer);
                }
            }
        }

        private void grvTasks_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                PrxTask prxTask = (PrxTask)grvTasks.CurrentRow.DataBoundItem;
                EditTask(prxTask.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
        }
    }
}
