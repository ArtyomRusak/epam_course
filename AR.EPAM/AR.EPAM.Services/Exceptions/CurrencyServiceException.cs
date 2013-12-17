using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AR.EPAM.Services.Exceptions
{
    public class CurrencyServiceException : ServiceException
    {
        protected CurrencyServiceException()
        {

        }

        public CurrencyServiceException(string message)
            : base(message)
        {

        }

        public CurrencyServiceException(Exception exception)
            : base(exception)
        {

        }
    }
}
