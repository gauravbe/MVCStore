using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCStore.Data.Entities;

namespace MVCStore.Data.Repository
{
    public interface IDatabaseFactory
    {
        AppEntities Get();
    }
}
