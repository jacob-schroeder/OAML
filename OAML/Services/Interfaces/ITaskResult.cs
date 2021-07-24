using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAML.Services.Interfaces
{
    public interface ITaskResult
    {
        bool Success { get; }

        ICollection<KeyValuePair<string, string>> Errors { get; }

        void AddError(string key, string value);
    }
}
