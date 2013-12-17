using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AR.EPAM.Services.Exceptions
{
    public class ProfileServiceException : ServiceException
    {
        protected ProfileServiceException()
        {

        }

        public ProfileServiceException(string message)
            : base(message)
        {

        }

        public ProfileServiceException(Exception exception)
            : base(exception)
        {

        }
    }
}
