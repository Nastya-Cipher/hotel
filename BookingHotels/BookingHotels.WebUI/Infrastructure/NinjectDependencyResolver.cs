using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using BookingHotels.Domain.Abstract;
using BookingHotels.Domain.Concrete;
using BookingHotels.WebUI.Infrastructure.Abstract;
using BookingHotels.WebUI.Infrastructure.Concrete;

namespace BookingHotels.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IHotelRepository>().To<EFHotelRepository>();
        }
    }
}