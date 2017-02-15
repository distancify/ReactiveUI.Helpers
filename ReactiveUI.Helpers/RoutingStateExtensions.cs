using System;
using Splat;

namespace ReactiveUI.Helpers
{
    public static class RoutingStateExtensions
    {
        public static IObservable<IRoutableViewModel> Execute<TFactory>(this ReactiveCommand<IRoutableViewModel, IRoutableViewModel> cmd,
           Func<TFactory, IRoutableViewModel> viewModelInstantiator)
        {
            var factoryInstance = Locator.Current.GetService<TFactory>();
            if(factoryInstance == null)
            {
                throw new ResolveException($"Unable to resolve type {typeof(TFactory).Name}");
            }

            return cmd.Execute(viewModelInstantiator.Invoke(factoryInstance));
        }

        public static IObservable<IRoutableViewModel> Execute<TViewModel>(this ReactiveCommand<IRoutableViewModel, IRoutableViewModel> cmd)
            where TViewModel : IRoutableViewModel
        {
            var viewModelInstance = Locator.Current.GetService<TViewModel>();
            if(viewModelInstance == null)
            {
                throw new ResolveException($"Unable to resolve type {typeof(TViewModel).Name}");
            }

            return cmd.Execute(viewModelInstance);
        }
    }
}
