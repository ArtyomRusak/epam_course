using System;

namespace AR.EPAM.Core.Exceptions
{
    public class RepositoryException : Exception
    {
        protected RepositoryException()
        {

        }

        public RepositoryException(string message)
            : base(message)
        {

        }

        public RepositoryException(Exception ex)
            : base("Inner Exception", ex)
        {

        }
    }
}
