using AutoMapper;
using Loyalty.Core.IRepositories;
using Loyalty.Data;
using Loyalty.Data.Entities;

namespace Loyalty.Core.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private MyDbContext _context;


        public RoleRepository(MyDbContext context, IMapper mapper)
        {

            _context = context;
        }
        public async Task<bool> Add(Role entity)
        {
            try
            {
                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> Detele(Guid id)
        {

            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return false;
            }

            try
            {
                _context.Remove(role);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }





        }

        public async Task<List<Role>> GetAll()
        {
            return _context.Roles.ToList();
        }

        public async Task<Role> GetById(Guid id)
        {
            var role = await _context.Roles.FindAsync(id);
            return role;

        }

        public async Task<bool> Update(Role entity)
        {
            try
            {
                var role = await _context.Roles.FindAsync(entity.Id);
                role.Description = entity.Description;
                role.Name = entity.Name;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
