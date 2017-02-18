using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveUI.Helpers.Tests.Fakes
{
    public class View : IViewFor<RoutableViewModel>
    {
        public RoutableViewModel ViewModel { get; set; }

        object IViewFor.ViewModel { get; set; }
    }
}
