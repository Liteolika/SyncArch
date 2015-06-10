using SysArch.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysArch.Application.Services
{
    public class SyncSchedulerCalculator : ISyncSchedulerCalculator
    {

        public DateTime CalculateNextRun(DateTime lastrun)
        {
            return lastrun.AddSeconds(5);
        }
    }
}
