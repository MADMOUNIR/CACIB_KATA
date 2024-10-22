using KATA_CACIB.DATA;
using KATA_CACIB.Models;
using KATA_CACIB.ServiceRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KATA_CACIB.Controllers
{
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly ServiceMock _serviceMock;
        private readonly ServiceManager _serviceManager;

        // Inject the mock service
        public ServicesController(ServiceMock serviceMock , ServiceManager serviceManager)
        {
            _serviceMock = serviceMock;
            _serviceManager = serviceManager;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetMockServiceList()
        {
            // Call the mock service to get a list of services
            List<Service> services = _serviceMock.GetMockServices();
            return Ok(services);
        }

        [HttpGet("{ServcieName}")]
        [Authorize]
        public IActionResult GetServiceByName(string ServcieName)
        {
            // Call the mock service to get a list of services
           var service = _serviceMock.GetMockServices().Where(x => x.Name == ServcieName).FirstOrDefault();
            
            if(service == null) { return NotFound(); }

            return Ok(service);
        }

        [HttpPost("{ServcieName}")]
        [Authorize]
        public IActionResult LaunchServiceByName(string ServcieName , [FromBody] MyAction action)
        {
            // Call the mock service to get a list of services
            var service = _serviceMock.GetMockServices().Where(x => x.Name == ServcieName).FirstOrDefault();

            if (service == null) { return NotFound(); }

            if(service.Actions.Select(x =>x.Name).ToList().Contains(action.Name))
            {
                // Call the service and get the UUID
                var uuid = _serviceManager.CallService(action.Name);

                // Return the UUID to the client
                return Ok(new { uuid });

            }

            return NotFound("Action not found in service "); 
        }

        
    }
}
