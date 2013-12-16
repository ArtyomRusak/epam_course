using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AR.EPAM.Services.Exceptions
{
    public class SectionServiceException : ServiceException
    {
        protected SectionServiceException()
        {

        }

        public SectionServiceException(string message)
            : base(message)
        {

        }

        public SectionServiceException(Exception exception)
            : base(exception)
        {

        }
    }
}
