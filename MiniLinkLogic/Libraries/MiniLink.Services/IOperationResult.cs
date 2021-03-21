using System.Collections.Generic;

namespace MiniLinkLogic.Libraries.MiniLink.Services
{
    public interface IOperationResult
    {
       
        List<string> Errors { get; set; }
        bool Success { get; set; }

        void AddError(string error);
    }
}