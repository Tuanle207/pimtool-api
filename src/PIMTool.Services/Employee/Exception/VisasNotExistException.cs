using PIMTool.Services.Common.Exception;
using PIMTool.Services.Project;
using System.Collections.Generic;

namespace PIMTool.Services.Employee.Exception
{
    public class VisasNotExistException : DataValidationException
    {

        public VisasNotExistException() : base()
        {
            Visas = new List<string>();
        }

        public VisasNotExistException(IList<string> visas) : base()
        {
            Visas = visas;
            FormatError();
        }
        public IList<string> Visas { get; private set; }

        public void AddVisa(string visa)
        {
            Visas.Add(visa);
            Errors.Clear();
            FormatError();
        }

        private void FormatError()
        {
            string visaSeq = string.Join(", ", Visas);
            var message = $"The following visas do not exist: {visaSeq}";
            Errors.Add(nameof(ProjectEntity.Employees), message);
        }
    }
}
