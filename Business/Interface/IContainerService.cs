using eShift.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShift.Business.Interface
{
    interface IContainerService
    {
        void AddContainer(Container container);
        Container GetContainerById(int containerId);
        List<Container> GetAllContainers();
        bool UpdateContainer(Container container);
        void DeleteContainer(int containerId);
    }
}
