using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysArch.Core
{
    public static class CommandHeaderExtensions
    {
        public static Action<IDictionary<string, object>> ApplyCommandHeaders(this Command cmd)
        {
            return headers =>
            {
                headers.Add("CommandId", cmd.CommandId);
            };
        }
    }

}
