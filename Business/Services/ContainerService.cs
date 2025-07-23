using eShift.Business.Interface;
using eShift.Model;
using eShift.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShift.Business.Services
{
    class ContainerService : IContainerService
    {
        private readonly IContainerRepository _containerRepo;

        public ContainerService(IContainerRepository containerRepo)
        {
            _containerRepo = containerRepo;
        }

        public void AddContainer(Container container)
        {
            if (string.IsNullOrWhiteSpace(container.ContainerNo) ||
                container.CapacityKg <= 0)
                throw new Exception("Missing or invalid container data.");

            _containerRepo.AddContainer(container);
        }

        public Container GetContainerById(int containerId)
        {
            var container = _containerRepo.GetContainerById(containerId);
            if (container == null)
                throw new Exception($"Container ID {containerId} not found.");

            return container;
        }

        public List<Container> GetAllContainers()
        {
            return _containerRepo.GetAllContainers();
        }

        public bool UpdateContainer(Container container)
        {
            if (string.IsNullOrWhiteSpace(container.ContainerNo) ||
                container.CapacityKg <= 0)
                throw new Exception("Invalid container data for update.");

            return _containerRepo.UpdateContainer(container);
        }

        public void DeleteContainer(int containerId)
        {
            _containerRepo.DeleteContainer(containerId);
        }
    }
}
