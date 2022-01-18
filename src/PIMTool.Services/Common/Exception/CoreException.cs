using ExceptionBase = System.Exception;

namespace PIMTool.Services.Common.Exception
{
    public class CoreException : ExceptionBase
    {
        private CoreException(string message, ExceptionBase innerException = null) : base(message, innerException)
        {

        }

        public short StatusCode { get; set; }

        public class DataHasBeenModifiedException : CoreException
        {
            public DataHasBeenModifiedException(string entityName, long id, string oldVersion, string newVersion,
                string message = "Can not perform action on the resource. It has been modified by other, please try again!",
                ExceptionBase innerExp = null) : base(message, innerExp)
            {
                StatusCode = 409;
                EntityId = id;
                EntityName = entityName;
                OldVersion = oldVersion;
                NewVersion = newVersion;
            }

            public string EntityName { get; private set; }
            public long EntityId { get; private set; }
            public string OldVersion { get; private set; }
            public string NewVersion { get; private set; }
        }

        public class NotFoundException : CoreException
        {
            public NotFoundException(string propName, object value,
                string message = "This resource is not available!", ExceptionBase innerExp = null) : base(message, innerExp)
            {
                PropertyName = propName;
                Value = value;
                StatusCode = 404;
            }

            public string PropertyName { get; private set; }
            public object Value { get; private set; }
        }

        public class BadRequestException : CoreException
        {
            public BadRequestException(string message = "This request is not valid!", ExceptionBase innerExp = null)
                : base(message, innerExp)
            {
                StatusCode = 400;
            }
        }

        public class UnauthorizedException : CoreException
        {
            public UnauthorizedException(string message = "You are unauthorized to perform this action!",
                ExceptionBase innerExp = null) : base(message, innerExp)
            {
                StatusCode = 401;
            }
        }

        public class ForBiddenException : CoreException
        {
            public ForBiddenException(string message = "You are forbidden to perform this action!",
                ExceptionBase innerExp = null) : base(message, innerExp)
            {
                StatusCode = 403;
            }
        }

        public class InternalErrorException : CoreException
        {
            public InternalErrorException(string message = "Internal server error has occurred",
                ExceptionBase innerExp = null) : base(message, innerExp)
            {
                StatusCode = 500;
            }
        }
    }
}
