using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace ProxyClasses
{
    [ServiceContract()]
    public interface IWebCommands
    {
        [OperationContract()]
        [WebGet(UriTemplate = "TestService?userName={userName}", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string TestService(string userName);

        [OperationContract()]
        [WebGet(UriTemplate = "GetMyComputerData", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        PrxComputer GetMyComputerData();
    }

    [ServiceContract()]
    public interface IWebComputers
    {
        [OperationContract()]
        [WebGet(UriTemplate = "GetList", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        List<PrxComputer> GetList();
    }
}
