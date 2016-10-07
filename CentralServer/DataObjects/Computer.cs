using ProxyClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralServer.DataObjects
{
    /// <summary>
    /// Класс-сущность для хранения данных удалённого компьютера
    /// </summary>
    public class Computer
    {
        /// <summary>
        /// Id компьютера в базе данных
        /// </summary>
        [Key()]
        public int ComputerId { get; set; }

        /// <summary>
        /// Имя компьютера
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Mac-адрес сетевой карты
        /// </summary>
        [Index("IndexMacAddress", IsUnique = true)]
        [MaxLength(64)]
        public string MacAddress { get; set; }


        /// <summary>
        /// Имя домена в котором расположен компьютер
        /// </summary>
        public string DomainName { get; set; }

        /// <summary>
        /// Состояние компьютера (Готов, Занят, Выключен)
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// Адрес компьютера в локальной сети
        /// </summary>
        public long LocalIPAddress { get; set; }

        /// <summary>
        /// Адрес компьютера в cети Интернет. Если компьютер находится в локальной сети позади маршрутизатора, это IP-адрес маршрутизатора
        /// </summary>
        public long InternetIPAddress { get; set; }

        /// <summary>
        /// Перечень задач, которые должен выполнить данный компьютер
        /// </summary>
        public virtual ICollection<Task> Tasks { get; set; } = new HashSet<Task>();

        /// <summary>
        /// Возвращает копию объекта Computer в виде объекта PrxComputer
        /// </summary>
        /// <returns></returns>
        public PrxComputer CopyToProxy()
        {
            PrxComputer prxComputer = new PrxComputer()
            {
                Id = this.ComputerId,
                Name = this.Name,
                MacAddress = this.MacAddress,
                DomainName=this.DomainName,
                InternetIPAddress = this.InternetIPAddress,
                LocalIPAddress = this.LocalIPAddress,
                State = (ComputerStateEnum)this.State
            };
            return prxComputer;
        }
    }
}
