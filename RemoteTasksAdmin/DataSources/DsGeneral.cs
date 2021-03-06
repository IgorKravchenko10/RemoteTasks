﻿using ProxyClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace RemoteTasksAdmin.DataSources
{

    /// <summary>
    /// Класс, отвечающий за реализацию обмена данными с Wcf-сервисами (фабрики и каналы)
    /// </summary>
    /// <typeparam name="IType"></typeparam>
    public class ChannelProvider<IType>
    {
        public System.ServiceModel.ChannelFactory<IType> Factory;
        public IType Channel;
        public SrvSettings HostSettings;
        public string ServiceURI;

        /// <summary>
        /// Создаёт объект типа ChannelProvider для обмена данными с сервисом через интерфейс определённого типа
        /// </summary>
        /// <param name="hostSettings">Объект, хранящий набор свойств и объектов, необходимых для запуска web-сервисов</param>
        /// <param name="ServiceSuffix">Часть адреса хостинга, который соответствует Wcf-сервису</param>
        public ChannelProvider(SrvSettings hostSettings, string ServiceSuffix)
        {
            this.HostSettings = hostSettings;
            // Получаем полный адрес сервиса, склеивая адрес сервиса из HostSettings и его суффикс
            this.ServiceURI = hostSettings.GetUrlString(ServiceSuffix);
        }

        /// <summary>
        /// Создаём канал для работы с Wcf-сервисом
        /// </summary>
        public void ChannelCreate()
        {
            // На всякий случай закроем канал, если он вдруг был открыт
            ChannelClose();
            // Создаём фабрику каналов, байндинг передаём из HostSettings
            Factory = new System.ServiceModel.ChannelFactory<IType>(this.HostSettings.Binding);
            //Открываем канал с помощью фабрики. Адрес формируем на основе адреса из HostSettings и суффикса, полученного в конструкторе
            Channel = Factory.CreateChannel(new System.ServiceModel.EndpointAddress(this.ServiceURI));
            // Поскольку по умолчанию максимальное кол-во объектов при передаче через wcf-сервис
            // ограничего значением 32768, для нас этого мало
            // Увеличим данное значение до максимального
            foreach (System.ServiceModel.Description.OperationDescription operation in Factory.Endpoint.Contract.Operations)
            {
                System.ServiceModel.Description.DataContractSerializerOperationBehavior behavior = operation.Behaviors.Find<System.ServiceModel.Description.DataContractSerializerOperationBehavior>();

                if ((behavior != null))
                {
                    behavior.MaxItemsInObjectGraph = 2147483647;
                }
            }
        }

        /// <summary>
        /// Закрываем канал для работы с wcf-сервисом
        /// </summary>
        public void ChannelClose()
        {
            try
            {
                if (Factory != null && Factory.State != System.ServiceModel.CommunicationState.Closed)
                    Factory.Close();
                //если вылет по ошибке, значит канала все равно нет
            }
            catch (Exception ex)
            {
            }
        }
    }

    /// <summary>
    /// Источник данных для работы со списком удалённых компьютеров
    /// </summary>
    public class DsComputerRows
    {
        private ChannelProvider<IWcfComputers> _ChannelProvider;
        /// <summary>
        /// Список удалённых компьютеров
        /// </summary>
        public List<PrxComputer> List;

        public DsComputerRows(SrvSettings hostSettings)
        {
            _ChannelProvider = new ChannelProvider<IWcfComputers>(hostSettings, hostSettings.ComputersSuffix);
            List = new List<PrxComputer>();
        }

        /// <summary>
        /// Загружаем список компьютеров в переменную List
        /// </summary>
        public void LoadData()
        {
            _ChannelProvider.ChannelCreate();
            List<PrxComputer> tmpList = _ChannelProvider.Channel.GetList();
            _ChannelProvider.ChannelClose();
            List.Clear();
            List.AddRange(tmpList);
        }
    }

    /// <summary>
    /// Источник данных для работы с задачами удалённых компьютеров
    /// </summary>
    public class DsTaskRows
    {
        private ChannelProvider<IWcfTasks> _ChannelProvider;
        /// <summary>
        /// Список задач для удалённых компьютеров
        /// </summary>
        public List<PrxTask> List;

        public DsTaskRows(SrvSettings hostSettings)
        {
            _ChannelProvider = new ChannelProvider<IWcfTasks>(hostSettings, hostSettings.TasksSuffix);
            List = new List<PrxTask>();
        }

        /// <summary>
        /// Загружаем список задач для определённого компьютера
        /// </summary>
        /// <param name="prxComputer">Удалённый компьютер, для которого нужно загрузить список задач</param>
        public void LoadData(PrxComputer prxComputer)
        {
            _ChannelProvider.ChannelCreate();
            List<PrxTask> tmpList = _ChannelProvider.Channel.GetList(prxComputer.Id);
            _ChannelProvider.ChannelClose();
            List.Clear();
            List.AddRange(tmpList);

            foreach(PrxTask item in List)
            {
                item.Computer = prxComputer;
            }
        }

        /// <summary>
        /// Отправляем задачу в базу данных
        /// </summary>
        /// <param name="prxTask"></param>
        /// <returns></returns>
        public int Update(PrxTask prxTask)
        {
            _ChannelProvider.ChannelCreate();
            int taskId=_ChannelProvider.Channel.Update(prxTask);
            _ChannelProvider.ChannelClose();
            return taskId;
        }

        public PrxTask GetItem(int id)
        {
            _ChannelProvider.ChannelCreate();
            PrxTask prxTask = _ChannelProvider.Channel.GetItem(id);
            _ChannelProvider.ChannelClose();
            return prxTask;
        }
    }

    public class DsTaskResultRows
    {
        private ChannelProvider<IWcfTaskResults> _ChannelProvider;
        public List<PrxTaskResult> List;

        public DsTaskResultRows(SrvSettings hostSettings)
        {
            _ChannelProvider = new ChannelProvider<IWcfTaskResults>(hostSettings, hostSettings.TaskResultsSuffix);
            List = new List<PrxTaskResult>();
        }

        public void LoadData(PrxComputer prxComputer)
        {
            _ChannelProvider.ChannelCreate();
            List<PrxTaskResult> loadedList = _ChannelProvider.Channel.GetListForComputer(prxComputer.Id, DateTime.MinValue, DateTime.MaxValue);
            _ChannelProvider.ChannelClose();
            List.Clear();
            List.AddRange(loadedList);
        }
    }
}