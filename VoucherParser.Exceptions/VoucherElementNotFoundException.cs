using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherParser.Parsing.Exceptions
{
    public class VoucherElementNotFoundException : BusinessException
    {
        public VoucherElementNotFoundException(string message) : base(message)
        {

        }
    }
}
