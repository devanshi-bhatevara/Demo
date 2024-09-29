using DemoCrudApi.Data;
using DemoCrudApi.Dtos;
using DemoCrudApi.Models;
using System.Diagnostics.Metrics;

namespace DemoCrudApi.Services
{
    public class Service : IService
    {
        private readonly IRepository _repository;
        public Service(IRepository repository)
        {
            _repository = repository;          
        }

        public ServiceResponse<IEnumerable<DemoDto>> GetDemos()
        {
            var response = new ServiceResponse<IEnumerable<DemoDto>>();
            var demos = _repository.GetAll();
            if (demos != null && demos.Any())
            {
                List<DemoDto> demoDto = new List<DemoDto>();

                foreach (var demo in demos)
                {
                    demoDto.Add(new DemoDto()
                    {
                        Id = demo.Id,
                        Name = demo.Name,
                        Age = demo.Age          
                    });
                }
                response.Data = demoDto;
            }
            else
            {
                response.Success = false;
                response.Message = "No record found";
            }
            return response;
        }

        public ServiceResponse<DemoDto> GetDemo(int id)
        {
            var response = new ServiceResponse<DemoDto>();
            var existingDemo = _repository.GetDemo(id);
            if (existingDemo != null)
            {
                var demo = new DemoDto()
                {
                    Id = id,
                    Name = existingDemo.Name,
                    Age = existingDemo.Age,
                };
                response.Data = demo;
            }
            
            else
            {
                response.Success = false;
                response.Message = "Something went wrong,try after sometime";
            }
            return response;
        }

        public ServiceResponse<string> AddDemo(Demo demo)
        {
            var response = new ServiceResponse<string>();
            if (_repository.DemoExist(demo.Name))
            {
                response.Success = false;
                response.Message = "Demo Already Exist";
                return response;
            }

            var result = _repository.Insert(demo);
            if (result)
            {
                response.Data = demo.Id.ToString();
                response.Success = true;
                response.Message = "Demo Saved Successfully";
            }
            else
            {
                response.Success = false;
                response.Message = "Something went wrong. Please try later";
            }
            return response;
        }


        public ServiceResponse<string> ModifyDemo(Demo demo)
        {
            var response = new ServiceResponse<string>();
            var message = string.Empty;
            if (_repository.DemoExist(demo.Id, demo.Name))
            {
                response.Success = false;
                response.Message = "Demo already exists.";
                return response;

            }

            var existingdemo = _repository.GetDemo(demo.Id);
            var result = false;
            if (existingdemo != null)
            {
                existingdemo.Name = demo.Name;
                existingdemo.Age = demo.Age;
                result = _repository.Update(existingdemo);
            }
            if (result)
            {
                response.Message = "demo updated successfully.";
            }
            else
            {
                response.Success = false;
                response.Message = "Something went wrong,try after sometime";
            }
            return response;

        }

        public ServiceResponse<string> RemoveDemo(int id)
        {
            var response = new ServiceResponse<string>();
            var result = _repository.Delete(id);

            if (result)
            {
                response.Message = "demo deleted successfully";
            }
            else
            {
                response.Success = false;
                response.Message = "Something went wrong";
            }

            return response;
        }

    }
}
