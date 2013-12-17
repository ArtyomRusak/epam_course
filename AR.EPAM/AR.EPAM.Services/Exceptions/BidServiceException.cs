using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AR.EPAM.Services.Exceptions
{
    public class BidServiceException : ServiceException
    {
        protected BidServiceException()
        {

        }

        public BidServiceException(string message)
            : base(message)
        {

        }

        public BidServiceException(Exception exception)
            : base(exception)
        {

        }
    }
}
