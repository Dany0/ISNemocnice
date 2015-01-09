using ISNemocniceKlient.NemocniceDataServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISNemocniceKlient
{
    public static class IONemocnice
    {
        private static dbNemocniceEntities _dbNemocniceContext;

        public static dbNemocniceEntities dbNemocniceContext
        {
            get { return _dbNemocniceContext != null ? _dbNemocniceContext : _dbNemocniceContext = new dbNemocniceEntities(new Uri("http://localhost:16980/NemocniceDataService.svc/")); }
            set { _dbNemocniceContext = value; }
        }
    }
}
