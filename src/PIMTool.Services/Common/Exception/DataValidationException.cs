using System.Collections.Generic;
using ExceptionBase = System.Exception;

namespace PIMTool.Services.Common.Exception
{
    public class DataValidationException : ExceptionBase
    {
        protected DataValidationException()
        {
            Errors = new Dictionary<string, string>();
        }

        public DataValidationException(string propertyName, string message) : this()
        {
            Errors.Add(propertyName, message);
        }

        public DataValidationException(Dictionary<string, string> errors)
        {
            Errors = errors;
        }

        public Dictionary<string, string> Errors { get; private set; }
    }
}
