﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Models.HTTP
{
    public interface IHttpEndpoint
    {
        void HandleRequest(HttpRequest rq, HttpResponse rs);
    }
}
