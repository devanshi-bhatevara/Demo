using DemoCrudApi.Models;

namespace DemoCrudApi.Data
{
    public interface IRepository
    {
        IEnumerable<Demo> GetAll();

        Demo? GetDemo(int id);

        bool Insert(Demo demo);

        bool Update(Demo demo);

        bool Delete(int id);

        bool DemoExist(string name);

        bool DemoExist(int id, string name);
    }
}
