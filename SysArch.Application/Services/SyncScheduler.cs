using MassTransit;
using SysArch.Application.Messages;
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
        private readonly IServiceBus _bus;

        public SyncScheduler(IServiceBus serviceBus)
        {
            this._bus = serviceBus;
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

            if (DateTime.Now > _nextRun)
            {
                _bus.Publish(new ExecuteUserSyncService());
                _bus.Publish(new ExecuteFileSyncService());
                _nextRun = _scheduleCalculator.CalculateNextRun(lastrun: DateTime.Now);
                autoEvent.Set();
            }


        }


    }
}
