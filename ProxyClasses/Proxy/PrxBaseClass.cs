using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ProxyClasses
{
    /// <summary>
    /// Тип аутентификации подключения к SQL-сервер
    /// </summary>
    [DataContract()]
    public enum AuthenticationTypeEnum : int
    {
        [EnumMember()]
        Windows = 0,
        [EnumMember()]
        SQLServer = 1
    }


    /// <summary>
    /// Базовый класс для всех Proxy классов
    /// </summary>
    [DataContract()]
    [Serializable()]
    public abstract class PrxBaseClass
    {
        /// <summary>
        /// Id объекта в базе данных
        /// </summary>
        [DataMember()]
        public int Id { get; set; }
    }

    /// <summary>
    /// Базовый класс для всех сущностей, имеющих ID и Name
    /// </summary>
    [DataContract()]
    public abstract class PrxBaseDictionary : PrxBaseClass
    {
        /// <summary>
        /// Основное текстовое значение сущности
        /// </summary>
        [DataMember()]
        public string Name { get; set; }
    }
}
