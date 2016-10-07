using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ProxyClasses
{

    /// <summary>
    /// Состояние компьютера (Готов, Занят, Выключен)
    /// </summary>
    [DataContract()]
    public enum ComputerStateEnum : int
    {
        [EnumMember]
        Any = 0,
        [EnumMember]
        Ready = 1,
        [EnumMember]
        Off = 2,
        [EnumMember]
        Busy = 4
    }

    /// <summary>
    /// Удалённый компьютер
    /// </summary>
    [DataContract()]
    public class PrxComputer : PrxBaseDictionary
    {
        /// <summary>
        /// Mac-адрес сетевой карты компьютера
        /// </summary>
        [DataMember()]
        public string MacAddress { get; set; }

        /// <summary>
        /// Имя домена, в котором находится компьютер
        /// </summary>
        [DataMember()]
        public string DomainName { get; set; }

        /// <summary>
        /// Состояние компьютера (Готов, Занят, Выключен)
        /// </summary>
        [DataMember()]
        public ComputerStateEnum State { get; set; }

        /// <summary>
        /// Адрес компьютера в локальной сети
        /// </summary>
        [DataMember()]
        public long LocalIPAddress { get; set; }

        /// <summary>
        /// Адрес компьютера в локальной сети в стандартном формате
        /// </summary>
        public string LocalIPAddressString
        {
            get
            {
                IPAddress ip = new IPAddress(this.LocalIPAddress);
                return ip.ToString();
            }
        }

        /// <summary>
        /// Адрес компьютера в cети Интернет. Если компьютер находится в локальной сети позади маршрутизатора, это IP-адрес маршрутизатора
        /// </summary>
        [DataMember()]
        public long InternetIPAddress { get; set; }

        /// <summary>
        /// Адрес компьютера в сети Интернет в стандартном формате
        /// </summary>
        public string InternetIPAddressString
        {
            get
            {
                IPAddress ip = new IPAddress(this.InternetIPAddress);
                return ip.ToString();
            }
        }
    }
}
