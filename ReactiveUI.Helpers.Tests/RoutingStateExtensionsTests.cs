using ReactiveUI.Helpers.Tests.Fakes;
using Splat;
using System;
using Xunit;

namespace ReactiveUI.Helpers.Tests
{
    public class RoutingStateExtensionsTests
    {
        [Fact]
        public void Execute_RegisteredViewModelType_ViewModelResolved()
        {
            Locator.CurrentMutable = new ModernDependencyResolver();
            Locator.CurrentMutable.Register<RoutableViewModel>();

            var command = ReactiveCommand.Create<IRoutableViewModel, IRoutableViewModel>(vm => vm);

            command.Execute<RoutableViewModel>().Subscribe((result) =>
            {
                Assert.IsType<RoutableViewModel>(result);
            });
        }

        [Fact]
        public void Execute_RegisteredViewModelFactoryType_ViewModelCreated()
        {
            Locator.CurrentMutable = new ModernDependencyResolver();
            Locator.CurrentMutable.Register<RoutableViewModelFactory>();

            var command = ReactiveCommand.Create<IRoutableViewModel, IRoutableViewModel>(vm => vm);

            command.Execute<RoutableViewModelFactory>((f) => f.Create())
            .Subscribe((result) =>
            {
                Assert.IsType<RoutableViewModel>(result);
            });
        }

        [Fact]
        public void Execute_NotRegisteredViewModelType_ExceptionThrown()
        {
            Locator.CurrentMutable = new ModernDependencyResolver();

            var command = ReactiveCommand.Create<IRoutableViewModel, IRoutableViewModel>(vm => vm);

            Assert.Throws<ResolveException>(() =>
            {
                command.Execute<RoutableViewModel>();
            });
        }

        [Fact]
        public void Execute_NotRegisteredRoutableViewModelFactoryType_ExceptionThrown()
        {
            Locator.CurrentMutable = new ModernDependencyResolver();

            var command = ReactiveCommand.Create<IRoutableViewModel, IRoutableViewModel>(vm => vm);

            Assert.Throws<ResolveException>(() =>
            {
                command.Execute<RoutableViewModelFactory>((factoryResult) => null);
            });
        }
    }
}
