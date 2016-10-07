using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralServer
{
    public class TCU4ExceptionBase : Exception
    {
        public TCU4ExceptionBase(string messageFromResources, params string[] args) : base(string.Format(messageFromResources.Replace("{", "<b>{").Replace( "}", "}</b>"), args))
        {
        }
    }

    public class UpdateException : TCU4ExceptionBase
    {
        public UpdateException(string messageFromResources, params string[] args) : base(messageFromResources, args)
        {
        }
    }

    public class ItemNotFoundException : TCU4ExceptionBase
    {
        public ItemNotFoundException(string messageFromResources, params string[] args) : base(messageFromResources, args)
        {
        }
    }
}
