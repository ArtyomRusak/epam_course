using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AR.EPAM.Services.Exceptions
{
    public class LotServiceException : ServiceException
    {
        protected LotServiceException()
        {

        }

        public LotServiceException(string message)
            : base(message)
        {

        }

        public LotServiceException(Exception exception)
            : base(exception)
        {

        }
    }
}
