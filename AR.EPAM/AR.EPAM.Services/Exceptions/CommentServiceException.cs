using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AR.EPAM.Services.Exceptions
{
    public class CommentServiceException : ServiceException
    {
        protected CommentServiceException()
        {

        }

        public CommentServiceException(string message)
            : base(message)
        {

        }

        public CommentServiceException(Exception exception)
            : base(exception)
        {

        }
    }
}
