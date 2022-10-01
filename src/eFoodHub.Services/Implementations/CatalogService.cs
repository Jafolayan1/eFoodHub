using eFoodHub.Entities;
using eFoodHub.Repositories;
using eFoodHub.Repositories.Interfaces;
using eFoodHub.Services.Interfaces;

namespace eFoodHub.Services.Implementations
{
    public class CatalogService : IDisposable, ICatalogService
    {
        private readonly IRepository<Item> _itemRepo;
        private readonly IRepository<Category> _categoryRepo;
        private readonly IRepository<ItemType> _itemTypeRepo;
        private readonly ApplicationDbContext _context;

        public CatalogService(IRepository<Item> itemRepo, IRepository<ItemType> itemTypeRepo, IRepository<Category> categoryRepo, ApplicationDbContext context)
        {
            _itemTypeRepo = itemTypeRepo;
            _itemRepo = itemRepo;
            _categoryRepo = categoryRepo;
            _context = context;
        }

        public void AddItem(Item item)
        {
            _itemRepo.Add(item);
            _itemRepo.SaveChanges();
        }

        public void DeletItem(int id)
        {
            _itemRepo.DeleteById(id);
            _itemRepo.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IEnumerable<Category> GetCategories()
        {
            return _categoryRepo.GetAll();
        }

        public Item GetItem(int id)
        {
            return _itemRepo.GetById(id);
        }

        public IEnumerable<Item> GetItems()
        {
            return _itemRepo.GetAll().OrderBy(item => item.CategoryId).ThenBy(item => item.ItemTypeId);
        }

        public IEnumerable<ItemType> GetItemTypes()
        {
            return _itemTypeRepo.GetAll();
        }

        public void UpdateItem(Item item)
        {
            _itemRepo.Update(item);
            _itemRepo.SaveChanges();
        }
    }
}