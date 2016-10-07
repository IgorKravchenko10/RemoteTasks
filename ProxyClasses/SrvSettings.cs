using System;
using System.ServiceModel;

namespace ProxyClasses
{
    /// <summary>
    /// Класс, хранящий набор свойств и объектов, необходимых для запуска web-сервисов
    /// </summary>
    public class SrvSettings
    {
        /// <summary>
        /// Тип протокола, который использует сервис
        /// </summary>
        public enum HostTypeEnum
        {
            /// <summary>
            /// TCP-протокол (wcf-сервис)
            /// </summary>
            TCP = 0,
            /// <summary>
            /// Http-протокол (wcf-сервис)
            /// </summary>
            HTTP = 1,
            /// <summary>
            /// Http-протокол (web-сервис)
            /// </summary>
            Web = 2
        }

        private HostTypeEnum _HostType = HostTypeEnum.TCP;
        /// <summary>
        /// Тип протокола, который использует сервис
        /// </summary>
        public HostTypeEnum HostType
        {
            get { return _HostType; }
        }

        /// <summary>
        /// Url-префикс, который используется в адресе для вызова сервиса
        /// </summary>
        public string HostTypePrefix
        {
            get
            {
                if (_HostType == HostTypeEnum.TCP)
                {
                    return "net.tcp://";
                }
                else
                {
                    if (this.IsSecureConnection && this.HostType == HostTypeEnum.Web)
                    {
                        return "https://";
                    }
                    else
                    {
                        return "http://";
                    }
                }
            }
        }

        private bool _IsSecureConnection;
        /// <summary>
        /// Признак безопасного соединения, которое используется сервисом. Для безопасного соединения должно быть задано имя SSL-сертификата
        /// </summary>
        public bool IsSecureConnection
        {
            get { return _IsSecureConnection; }
        }

        private string _CertificateName;
        /// <summary>
        /// Имя SSL-сертификата для безопасного соединения. Для обычного соединения не используется
        /// </summary>
        public string CertificateName
        {
            get { return _CertificateName; }
        }
        /// Байндинги, которые хранят параметры запущенных wcf и web-сервисов
        /// <summary>
        /// Байндинг wcf-сервиса с TCP-протоколом
        /// </summary>
        private NetTcpBinding _TcpBinding;
        /// <summary>
        /// Байндинг wcf-сервиса с Http-протоколом
        /// </summary>
        private WSHttpBinding _HttpBinding;
        /// <summary>
        /// Байндинг web-сервиса с Http-протоколом
        /// </summary>
        private WebHttpBinding _WebBinding;

        /// <summary>
        /// Время ожидания для открытия соединения и передачи пакетов
        /// </summary>
        private const int _Timeout = 15;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="hostType">Тип протокола, который использует сервис</param>
        /// <param name="hostName">Имя хоста (компьютера), на котором запускается сервис</param>
        /// <param name="port">Порт, на котором запускается сервис</param>
        /// <param name="isSecureConnection">Признак безопасного соединения</param>
        /// <param name="certificateName">Имя SSL-сертификата безопасного соединения</param>
        public SrvSettings(HostTypeEnum hostType, string hostName, int port, bool isSecureConnection, string certificateName = "")
        {
            _IsSecureConnection = isSecureConnection;
            _CertificateName = certificateName;
            _HostType = hostType;
            _HostName = hostName;
            _Port = port;
            switch (hostType)
            {
                case HostTypeEnum.TCP:
                    _TcpBinding = new NetTcpBinding();
                    if (_IsSecureConnection)
                    {
                        _TcpBinding.Security.Mode = SecurityMode.TransportWithMessageCredential;
                        _TcpBinding.Security.Message.ClientCredentialType = MessageCredentialType.Certificate;
                    }
                    else
                    {
                        _TcpBinding.Security.Mode = SecurityMode.None;
                    }

                    _TcpBinding.MaxReceivedMessageSize = 500000000;
                    _TcpBinding.ReceiveTimeout = TimeSpan.FromMinutes(_Timeout);
                    _TcpBinding.SendTimeout = TimeSpan.FromMinutes(_Timeout);
                    _TcpBinding.OpenTimeout = TimeSpan.FromMinutes(_Timeout);
                    _TcpBinding.ReaderQuotas.MaxArrayLength = 73400320;
                    break;
                case HostTypeEnum.HTTP:
                    _HttpBinding = new WSHttpBinding();
                    _HttpBinding.Security.Mode = SecurityMode.None;
                    _HttpBinding.MaxReceivedMessageSize = 500000000;
                    _HttpBinding.ReliableSession.Enabled = true;
                    _HttpBinding.ReaderQuotas.MaxArrayLength = 73400320;
                    break;
                case HostTypeEnum.Web:
                    _WebBinding = new WebHttpBinding();

                    if (_IsSecureConnection)
                    {
                        _WebBinding.Security.Mode = WebHttpSecurityMode.Transport;
                    }
                    else
                    {
                        _WebBinding.Security.Mode = WebHttpSecurityMode.None;
                    }

                    _WebBinding.MaxReceivedMessageSize = int.MaxValue;
                    _WebBinding.ReaderQuotas.MaxArrayLength = int.MaxValue;
                    _WebBinding.MaxBufferSize = int.MaxValue;
                    break;
            }
        }

        /// <summary>
        /// Байндинг сервиса
        /// </summary>
        public System.ServiceModel.Channels.Binding Binding
        {
            get
            {
                switch (_HostType)
                {
                    case HostTypeEnum.TCP:
                        return _TcpBinding;
                    case HostTypeEnum.HTTP:
                        return _HttpBinding;
                    case HostTypeEnum.Web:
                        return _WebBinding;
                    default:
                        return null;
                }
            }
        }

        private string _HostName;
        /// <summary>
        /// Имя хоста, на котором запущен сервис
        /// </summary>
        public string HostName
        {
            get { return _HostName; }
        }

        private int _Port;
        /// <summary>
        /// Порт сервиса
        /// </summary>
        public int Port
        {
            get { return _Port; }
        }

        /// <summary>
        /// Адрес хоста, на котором расположен сервис
        /// </summary>
        public string HostServiceUrl
        {
            get { return this.HostTypePrefix + this.HostName + ":" + this.Port; }
        }

        /// <summary>
        /// Полный адрес web-сервиса вместе с суффиксом группы сервисов
        /// </summary>
        /// <param name="suffix">Суффикс группы сервисов. Выбирается из свойств-суффиксов "только для чтения"</param>
        /// <returns></returns>
        public string GetUrlString(string suffix)
        {
            return string.Format("{0}/{1}", this.HostServiceUrl, suffix);
        }

        /// <summary>
        /// Суффикс группы сервисов
        /// </summary>
        public string CommandsSuffix
        {
            get { return "Commands"; }
        }

        /// <summary>
        /// Суффикс группы сервисов
        /// </summary>
        public string ComputersSuffix
        {
            get { return "Computers"; }
        }

        public string TasksSuffix
        {
            get { return "Tasks"; }
        }

        public string TaskResultsSuffix
        {
            get { return "TaskResults"; }
        }
    }
}