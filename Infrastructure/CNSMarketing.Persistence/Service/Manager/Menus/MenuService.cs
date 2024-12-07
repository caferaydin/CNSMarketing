using CNSMarketing.Application.Repositories;
using CNSMarketing.Domain.Entities;
using CNSMarketing.Application.Abstraction.Service.Manager;

namespace CNSMarketing.Persistence.Service.Manager
{
    public class MenuService : GenericService<Menu, Guid, IMenuReadRepository, IMenuWriteRepository>, IMenuService
    {
        private readonly IMenuReadRepository _readRepository;
        private readonly IMenuWriteRepository _writeRepository;


        public MenuService(IMenuReadRepository readRepository, IMenuWriteRepository writeRepository) : base(readRepository, writeRepository)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }

        public async Task<IEnumerable<Menu>> GetMenusAsync()
        {
            var menus = await _readRepository.GetAllMenusAsync();

            return menus;

        }

    }
}
