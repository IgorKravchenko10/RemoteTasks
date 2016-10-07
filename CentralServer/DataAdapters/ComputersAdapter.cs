using CentralServer.DataObjects;
using ProxyClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralServer.DataAdapters
{
    public class ComputersAdapter
    {

        public static List<PrxComputer> GetList(CsContext csContext)
        {
            // Объявляем запрос к контексту базы данных
            // Который является LINQ TO SQL запросом и при выполнении будет транслирован
            // в SQL-запрос к серверу
            // Полученный результирующй набор будет транслирован в коллекцию объектов типа PrxComputer
            IQueryable<PrxComputer> computersQuery = from qr in csContext.Computers
                                                     select new PrxComputer
                                                     {
                                                         Id = qr.ComputerId,
                                                         Name = qr.Name,
                                                         MacAddress=qr.MacAddress,
                                                         DomainName=qr.DomainName,
                                                         InternetIPAddress = qr.InternetIPAddress,
                                                         LocalIPAddress = qr.LocalIPAddress,
                                                         State = (ComputerStateEnum)qr.State
                                                     };

            List<PrxComputer> computers = computersQuery.ToList();
            return computers;
        }

        /// <summary>
        /// Сохраняем данные из объекта prxComputer в базе данных. Компьютер в базе данных находим по его Мас-адресу
        /// </summary>
        /// <param name="csContext">Контекст базы данных</param>
        /// <param name="prxComputer">Объект, из которого производим сохранение</param>
        /// <returns>Возвращаем сущность типа Computer, которая была сохранена в базе данных через csContext</returns>
        public static Computer UpdateByMacAddress(CsContext csContext, PrxComputer prxComputer)
        {
            Computer computer;
            try
            {
                computer = GetItem(csContext, prxComputer.MacAddress);
            }
            catch(ItemNotFoundException ex)
            {
                computer = new Computer();
                csContext.Computers.Add(computer);
            }
            
            computer.Name = prxComputer.Name;
            computer.MacAddress = prxComputer.MacAddress;
            computer.DomainName = prxComputer.DomainName;
            computer.InternetIPAddress = prxComputer.InternetIPAddress;
            computer.LocalIPAddress = prxComputer.LocalIPAddress;
            computer.State = (int)prxComputer.State;

            csContext.SaveChanges();

            return computer;
        }
        
        /// <summary>
        /// Возвращаем данные о компьютере в виде сущности Computer из базы данных
        /// </summary>
        /// <param name="csContext">Контекст базы данных</param>
        /// <param name="id">Id компьютера в таблице базы данных</param>
        /// <returns></returns>
        public static Computer GetItem(CsContext csContext, int id)
        {
            //IQueryable<Computer> computersQuery = from qr in csContext.Computers where qr.ComputerId == prxComputer.Id select qr;
            //List<Computer> computers = computersQuery.ToList();
            //Computer computer2 = null;
            //if (computers.Count > 0)
            //{
            //    computer2 = computers[0];
            //}

            //Выполняет LINQ-запрос и возвращает первый элемент коллекции, если это возможно, то есть, в коллекции что-то имеется
            //Если коллекция пустая, вернёт null
            Computer computer = (from qr in csContext.Computers where qr.ComputerId == id select qr).FirstOrDefault();
            return computer;
        }       
        
        public static Computer GetItem(CsContext csContext, string macAddress)
        {
            Computer computer = (from qr in csContext.Computers where qr.MacAddress == macAddress select qr).FirstOrDefault();
            if (computer == null)
            {
                throw new ItemNotFoundException(Resources.Messages.ComputerNotFoundByMacAddress, macAddress); 
            }
            return computer;
        } 
    }
}
