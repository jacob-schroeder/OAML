using OAML.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAML.Services
{
    public class ServiceResult<T> : TaskResult, ITaskResult
    {
        public ServiceResult() : base()
        {

        }

        public T Result { get; private set; }

        public void SetResult(T NewResult)
        {
            Result = NewResult;
        }
    }
}
