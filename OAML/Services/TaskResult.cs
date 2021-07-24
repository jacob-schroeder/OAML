using OAML.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAML.Services
{
    public class TaskResult : ITaskResult
    {
        public bool Success => !Errors?.Any() ?? true;
        public ICollection<KeyValuePair<string, string>> Errors { get; private set; }

        public TaskResult()
        {
            Errors = new List<KeyValuePair<string, string>>();
        }

        public void AddError(string key, string value)
        {
            Errors.Add(new KeyValuePair<string, string>(key, value));
        }
    }
}
