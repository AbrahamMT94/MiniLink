using System;
using System.Collections.Generic;
using System.Text;

namespace MiniLinkLogic.Libraries.MiniLink.Services
{
    public class OperationResult<T> : IOperationResult
    {
        public OperationResult(T entry)
        {
            Success = true;
            Entry = entry;
            Errors = new List<string>();
        }
        public OperationResult(string errorMessage)
        {          
            Errors = new List<string>();
            AddError(errorMessage);
        }

        public bool Success { get; set; }
        public T Entry { get; set; }
        public List<string> Errors { get; set; }

        public void AddError(string error)
        {
            Success = false;
            Errors.Add(error);
        }

       
    }
}
