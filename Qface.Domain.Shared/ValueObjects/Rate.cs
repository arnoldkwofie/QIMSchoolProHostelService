using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qface.Domain.Shared.ValueObjects
{
    public class Rate
    {
        public decimal Selling { get; private set; }
        public decimal Buying { get; private set; }
        private Rate() { } //For EF Core
        private Rate(decimal selling, decimal buying)
        {
            Selling = selling;
            Buying = buying;
        }

        public static Rate Create(decimal selling = 1, decimal buying = 1)
        {
            return new Rate(selling, buying);
        }

    }
}
