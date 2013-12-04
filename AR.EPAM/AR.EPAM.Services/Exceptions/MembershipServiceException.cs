using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AR.EPAM.Infrastructure.Guard.Exceptions;

namespace AR.EPAM.Services.Exceptions
{
    public class MembershipServiceException : ServiceException
    {
        public string ErrorMessage { get; private set; }

        public MembershipServiceException()
        {
            
        }

        public MembershipServiceException(string message)
        {
            ErrorMessage = message;
        }
    }
}
