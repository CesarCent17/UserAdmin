using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Utils
{
    public class OperationResult<T>
    {
        public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }
    }
}
