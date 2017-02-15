using System;
using Splat;
using Xunit;
using ReactiveUI.Helpers.Tests.Fakes;

namespace ReactiveUI.Helpers.Tests
{
    public class SplatExtensionsTests
    {
        [Fact]
        public void Register_ByInterfaceWithoutDependencies_ResolvedByInterface()
        {
            var sut = new ModernDependencyResolver();

            sut.Register<ClassWithoutDependencies, IService>();

            var result = sut.GetService<IService>();
            Assert.IsType<ClassWithoutDependencies>(result);
        }

        [Fact]
        public void Register_ByInterfaceWithDependencies_ResolvedByInterface()
        {
            var sut = new ModernDependencyResolver();

            sut.Register<Dependency>();
            sut.Register<ClassWithDependencies, IService>();

            var result = sut.GetService<IService>();
            Assert.IsType<ClassWithDependencies>(result);
        }

        [Fact]
        public void Register_ByInterfaceWithMissingDependencies_ExceptionThrown()
        {
            var sut = new ModernDependencyResolver();

            sut.Register<ClassWithDependencies, IService>();

            Assert.Throws<ResolveException>(() =>
            {
                var result = sut.GetService<IService>();
            });
        }

        [Fact]
        public void Register_ClassWithoutDependencies_ResolvedByClass()
        {
            var sut = new ModernDependencyResolver();

            sut.Register<ClassWithoutDependencies>();

            var result = sut.GetService<ClassWithoutDependencies>();
            Assert.IsType<ClassWithoutDependencies>(result);
        }

        [Fact]
        public void Register_ClassWithDependencies_ResolvedByClass()
        {
            var sut = new ModernDependencyResolver();

            sut.Register<Dependency>();
            sut.Register<ClassWithDependencies>();

            var result = sut.GetService<ClassWithDependencies>();
            Assert.IsType<ClassWithDependencies>(result);
        }

        [Fact]
        public void Register_ClassWithMissingDependencies_ExceptionThrown()
        {
            var sut = new ModernDependencyResolver();

            sut.Register<ClassWithDependencies>();
            Assert.Throws<ResolveException>(() =>
            {
                var result = sut.GetService<ClassWithDependencies>();
            });
        }
        
        [Fact]
        public void RegisterLazySingleton_ByInterfaceWithoutDependencies_ResolvedByInterface()
        {
            var sut = new ModernDependencyResolver();

            sut.RegisterLazySingleton<ClassWithoutDependencies, IService>();

            var result = sut.GetService<IService>();
            Assert.IsType<ClassWithoutDependencies>(result);
        }

        [Fact]
        public void RegisterLazySingleton_ByInterfaceWithDependencies_ResolvedByInterface()
        {
            var sut = new ModernDependencyResolver();

            sut.Register<Dependency>();
            sut.RegisterLazySingleton<ClassWithDependencies, IService>();

            var result = sut.GetService<IService>();
            Assert.IsType<ClassWithDependencies>(result);
        }

        [Fact]
        public void RegisterLazySingleton_ByInterfaceWithMissingDependencies_ExceptionThrown()
        {
            var sut = new ModernDependencyResolver();

            sut.RegisterLazySingleton<ClassWithDependencies, IService>();

            Assert.Throws<ResolveException>(() =>
            {
                var result = sut.GetService<IService>();
            });
        }

        [Fact]
        public void RegisterLazySingleton_ClassWithoutDependencies_ResolvedByClass()
        {
            var sut = new ModernDependencyResolver();

            sut.RegisterLazySingleton<ClassWithoutDependencies>();

            var result = sut.GetService<ClassWithoutDependencies>();
            Assert.IsType<ClassWithoutDependencies>(result);
        }

        [Fact]
        public void RegisterLazySingleton_ClassWithDependencies_ResolvedByClass()
        {
            var sut = new ModernDependencyResolver();

            sut.Register<Dependency>();
            sut.RegisterLazySingleton<ClassWithDependencies>();

            var result = sut.GetService<ClassWithDependencies>();
            Assert.IsType<ClassWithDependencies>(result);
        }

        [Fact]
        public void RegisterLazySingleton_ClassWithMissingDependencies_ExceptionThrown()
        {
            var sut = new ModernDependencyResolver();

            sut.RegisterLazySingleton<ClassWithDependencies>();
            Assert.Throws<ResolveException>(() =>
            {
                var result = sut.GetService<ClassWithDependencies>();
            });
        }
        
        [Fact]
        public void Register_ClassWithoutDependencies_DistinctInstancesPerResolve()
        {
            var sut = new ModernDependencyResolver();

            sut.Register<ClassWithoutDependencies>();

            var result1 = sut.GetService<ClassWithoutDependencies>();
            var result2 = sut.GetService<ClassWithoutDependencies>();

            Assert.IsType<ClassWithoutDependencies>(result1);
            Assert.IsType<ClassWithoutDependencies>(result2);

            Assert.NotEqual(result1, result2);            
        }

        [Fact]
        public void RegisterLazySingleton_ClassWithoutDependencies_SameInstancesPerResolve()
        {
            var sut = new ModernDependencyResolver();

            sut.RegisterLazySingleton<ClassWithoutDependencies>();

            var result1 = sut.GetService<ClassWithoutDependencies>();
            var result2 = sut.GetService<ClassWithoutDependencies>();

            Assert.IsType<ClassWithoutDependencies>(result1);
            Assert.IsType<ClassWithoutDependencies>(result2);

            Assert.Equal(result1, result2);
        }
    }
}

