﻿using SistemaVenta.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.BLL.Interface
{
    public interface IRolService
    {
        Task<List<Rol>> Lista();
    }
}
