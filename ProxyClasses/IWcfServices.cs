using ProxyClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ProxyClasses
{
    /// <summary>
    /// Набор сервисов для работы с данными по удалённым компьютерам
    /// </summary>
    [ServiceContract()]
    public interface IWcfComputers
    {
        /// <summary>
        /// Возвращает список компьютеров, с которыми работает администратор
        /// </summary>
        /// <returns></returns>
        [OperationContract()]
        List<PrxComputer> GetList();

        /// <summary>
        /// Сохраняет в базе данных данные о компьютере
        /// </summary>
        /// <param name="prxComputer">Сериализованный объект типа PrxComputer</param>
        /// <returns></returns>
        [OperationContract()]
        int Update(PrxComputer prxComputer);

        /// <summary>
        /// Возвращает информацию о компьютере по его Id
        /// </summary>
        /// <param name="id">Id компьютера в базе данных</param>
        /// <returns></returns>
        [OperationContract()]
        PrxComputer GetItem(int id);

        /// <summary>
        /// Возвращает информацию о компьютере по его Mac-адресу
        /// </summary>
        /// <param name="macAddress">Мас-адрес компьютера</param>
        /// <returns></returns>
        [OperationContract()]
        [FaultContract(typeof(PrxSimpleException))]
        PrxComputer GetItemByMacAddress(string macAddress);
    }

    /// <summary>
    /// Набор сервисов для работы с задачами для удалённых компьютеров
    /// </summary>
    [ServiceContract()]
    public interface IWcfTasks
    {
        /// <summary>
        /// Возвращает список задач для компьютера по его Id
        /// </summary>
        /// <param name="computerId">Id компьютера в базе данных</param>
        /// <returns></returns>
        [OperationContract()]
        List<PrxTask> GetList(int computerId);

        /// <summary>
        /// Возвращает список задач для компьютера по Мас-адресу
        /// </summary>
        /// <param name="macAddress">Мас-адрес сетевой карты компьютера</param>
        /// <param name="activeOnly">Истина, если хотим получить только активные задачи, иначе возвращает все задачи</param>
        /// <returns></returns>
        [OperationContract()]
        [FaultContract(typeof(PrxSimpleException))]
        List<PrxTask> GetListByMacAddress(string MacAddress, bool activeOnly);

        /// <summary>
        /// Сохраняет задачу в базе данных сервера
        /// </summary>
        /// <param name="prxTask">Сериализованный объект, в котором передаётся задача</param>
        /// <returns></returns>
        [OperationContract()]
        [FaultContract(typeof(PrxSimpleException))]
        int Update(PrxTask prxTask);

        /// <summary>
        /// Возвращает задачу по её Id в базе данных сервера
        /// </summary>
        /// <param name="id">Id задачи в базе данных</param>
        /// <returns></returns>
        [OperationContract()]
        [FaultContract(typeof(PrxSimpleException))]
        PrxTask GetItem(int id);
    }

    /// <summary>
    /// Набор сервисов для работы с результатами выполнения задач на удалённых компьютерах
    /// </summary>
    [ServiceContract()]
    public interface IWcfTaskResults
    {
        /// <summary>
        /// Возвращает список результатов выполнения задач для компьютера с определённым Id в базе данных
        /// </summary>
        /// <param name="id">Id компьютера в базе данных</param>
        /// <param name="startDate">Дата начала периода, за который выводится список выполнения задачи</param>
        /// <param name="endDate">Дата конца периода, за который выводится список выполнения задачи</param>
        /// <returns></returns>
        [OperationContract()]
        [FaultContract(typeof(PrxSimpleException))]
        List<PrxTaskResult> GetListForComputer(int id, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Возвращает список результатов выполнения для задачи с определённым Id в базе данных
        /// </summary>
        /// <param name="id">Id задачи в базе данных</param>
        /// <param name="startDate">Дата начала периода, за который выводится список выполнения задачи</param>
        /// <param name="endDate">Дата конца периода, за который выводится список выполнения задачи</param>
        /// <returns></returns>
        [OperationContract()]
        [FaultContract(typeof(PrxSimpleException))]
        List<PrxTaskResult> GetListForTask(int id, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Возвращает список резутатов выполнения всех задач за определённый период
        /// </summary>
        /// <param name="startDate">Дата начала периода, за который выводится список выполнения задачи</param>
        /// <param name="endDate">Дата конца периода, за который выводится список выполнения задачи</param>
        /// <returns></returns>
        [OperationContract()]
        [FaultContract(typeof(PrxSimpleException))]
        List<PrxTaskResult> GetList(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Сохраняет результат выполнения задачи в базе данных сервера
        /// </summary>
        /// <param name="prxTaskResult">Информация о результате выполнения задачи</param>
        /// <returns></returns>
        [OperationContract()]
        [FaultContract(typeof(PrxSimpleException))]
        int Update(PrxTaskResult prxTaskResult);

        /// <summary>
        /// Возвращает результат выполнения задачи по его Id в базе данных
        /// </summary>
        /// <param name="id">Id результата выполнения задачи в базе данных</param>
        /// <returns></returns>
        [OperationContract()]
        [FaultContract(typeof(PrxSimpleException))]
        PrxTaskResult GetItem(int id);
    }
}