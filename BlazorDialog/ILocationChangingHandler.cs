using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDialog
{
    public interface ILocationChangingHandler
    {
        void RegisterLocationChangingHandler(Dialog dialog);
        void RemoveLocationChangingHandler(Dialog dialog);
    }
}
