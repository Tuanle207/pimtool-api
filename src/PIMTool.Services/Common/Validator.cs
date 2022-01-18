using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Services.Common
{
    public class Validator<T> : IValidator<T> where T : class
    {
        protected Dictionary<string, string> m_errors;
        public Dictionary<string, string> Errors
        {
            get
            {
                var returnErrors = new Dictionary<string, string>(m_errors);
                m_errors = null;
                return returnErrors;
            }
        }

        public int ErrorCount => m_errors.Count;

        public virtual bool Validate(T input)
        {
            m_errors = new Dictionary<string, string>();
            return false;
        }

        public virtual void ValidateAndThrow(T input)
        {
            m_errors = new Dictionary<string, string>();
        }

        protected void AddValidatorError(string propertyName, string error)
        {
            if (!string.IsNullOrWhiteSpace(error))
            {
                m_errors.Add(propertyName, error);
            }
        }
    }
}
