using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qface.Application.Shared.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string UserId { get; }

        bool IsAuthenticated { get; }
    }

    public interface IAutoCodeGenerator
    {
        public Task<(string NewCode, long LastNumber)> GenerateCode(string name, CancellationToken cancellation);
        public Task<decimal> GenerateAccountCode(int accountGroupId, CancellationToken cancellation);
    }
}
