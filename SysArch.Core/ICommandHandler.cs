﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysArch.Core
{
    public interface ICommandHandler
    { }

    public interface ICommandHandler<T> : ICommandHandler
    {
        void Handle(T cmd);
    }
}
