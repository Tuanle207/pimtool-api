using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Services.Common
{
    public interface IValidator<T> where T : class
    {
        Dictionary<string, string> Errors { get; }
        int ErrorCount { get; }
        bool Validate(T input);
        void ValidateAndThrow(T input);
    }
}
