using KATA_CACIB.Models;

namespace KATA_CACIB.DATA
{
    public class ServiceMock
    {
        public List<Service> GetMockServices()
        {
            // Hardcoded list of mock services
            return new List<Service>
            {
                new Service { Id = 1, Name = "S1" ,  Actions = new List<MyAction>
                                                        {
                                                            new MyAction { Id = 1, Name = "ACTION1" },
                                                            new MyAction { Id = 2, Name = "ACTION2" }
                                                        }},
                new Service { Id = 2, Name = "S2" , Actions = new List<MyAction>
                                                        {
                                                            new MyAction { Id = 1, Name = "ACTION3" },
                                                            new MyAction { Id = 2, Name = "ACTION4" }
                                                        }} ,
                new Service { Id = 3, Name = "S3" , Actions = new List<MyAction>
                                                        {
                                                            new MyAction { Id = 1, Name = "ACTION5" },
                                                            new MyAction { Id = 2, Name = "ACTION6" }
                                                        } }
            };
        }
    }
}
