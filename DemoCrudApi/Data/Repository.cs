using DemoCrudApi.Models;

namespace DemoCrudApi.Data
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _appDbContext;

        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;   
        }

        public IEnumerable<Demo> GetAll()
        {

            List<Demo> demos = _appDbContext.Demos.ToList();
            return demos;
        }

        public Demo? GetDemo(int id)
        {
            var demo = _appDbContext.Demos
                .FirstOrDefault(c => c.Id == id);
            return demo;
        }

        public bool Insert(Demo demo)
        {
            var result = false;
            if (demo != null)
            {
                _appDbContext.Demos.Add(demo);
                _appDbContext.SaveChanges();
                result = true;
            }
            return result;
        }

        public bool Update(Demo demo)
        {
            var result = false;
            if (demo != null)
            {
                _appDbContext.Demos.Update(demo);
                _appDbContext.SaveChanges();
                result = true;
            }
            return result;
        }

        public bool Delete(int id)
        {
            var result = false;
            var demo = _appDbContext.Demos.Find(id);
            if (demo != null)
            {
                _appDbContext.Demos.Remove(demo);
                _appDbContext.SaveChanges();
                result = true;
            }
            return result;
        }

        public bool DemoExist(string name)
        {
            var demo = _appDbContext.Demos.FirstOrDefault(c => c.Name == name);
            if (demo != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DemoExist(int id, string name)
        {
            var demo = _appDbContext.Demos.FirstOrDefault(c => c.Name == name && c.Id != id);
            if (demo != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
