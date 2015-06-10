using SysArch.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SysArch.Application.Services
{
    public class SyncScheduler : ISyncScheduler
    {

        private Timer _executionTimer;
        private AutoResetEvent _autoEvent = new AutoResetEvent(false);
        private ISyncSchedulerCalculator _scheduleCalculator;
        private DateTime _nextRun;

        public SyncScheduler()
        {

        }

        public void Start(ISyncSchedulerCalculator scheduleCalculator)
        {
            _scheduleCalculator = scheduleCalculator;

            TimerCallback tcb = this.TimerExecutor;
            _executionTimer = new Timer(tcb, _autoEvent, 1000, 1000);
        }

        public void Stop()
        {
            _autoEvent.WaitOne(5000, false);
            _executionTimer.Dispose();
        }

        private void TimerExecutor(Object stateInfo)
        {
            AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;
            autoEvent.Set();

            if (DateTime.Now > _nextRun)
            {
                Console.WriteLine("Executing schedule");
                _nextRun = _scheduleCalculator.CalculateNextRun(lastrun: DateTime.Now);
            }
            

        }

        

    }
}
