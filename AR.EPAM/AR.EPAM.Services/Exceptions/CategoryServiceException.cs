using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AR.EPAM.Services.Exceptions
{
    public class CategoryServiceException : ServiceException
    {
        protected CategoryServiceException()
        {

        }

        public CategoryServiceException(string message)
            : base(message)
        {

        }

        public CategoryServiceException(Exception exception)
            : base(exception)
        {

        }
    }
}
