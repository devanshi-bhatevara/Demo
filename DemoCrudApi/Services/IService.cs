using DemoCrudApi.Dtos;
using DemoCrudApi.Models;

namespace DemoCrudApi.Services
{
    public interface IService
    {
        ServiceResponse<IEnumerable<DemoDto>> GetDemos();
        ServiceResponse<DemoDto> GetDemo(int id);

        ServiceResponse<string> AddDemo(Demo demo);

        ServiceResponse<string> ModifyDemo(Demo demo);

        ServiceResponse<string> RemoveDemo(int id);
    }
}
