using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveUI.Helpers.Tests.Fakes
{
    public class RoutableViewModel : ReactiveObject, IRoutableViewModel
    {
        public IScreen HostScreen
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string UrlPathSegment
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
