using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysArch.Application.Services
{
    public interface ISyncSchedulerCalculator
    {
        DateTime CalculateNextRun(DateTime lastrun);
    }
}
