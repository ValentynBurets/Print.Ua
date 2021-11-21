using Data.EF;
using Data.Interface.UnitOfWork;
using Data.Repository;
using Domain.Entity;
using Domain.Repository.OrderManagement;
using Domain.Repository.ProductManagement;
using Domain.Repository.UserManagement;
using System.Threading.Tasks;

namespace Data.UnitOfWork
{
    public class OrderUnitOfWork : IOrderUnitOfWork
    {
        private readonly DomainDbContext _dbContext;
        private IOrderRepository _orderRepository;
        private IOrderCommentRepository _orderCommentRepository;
        private IProductRepository _productRepository;
        private IProductServiceFormatRepository _productServiceFormatRepository;
        private IProductServiceRepository _productServiceRepository;
        private IProductServiceMaterialRepository _productServiceMaterialRepository;
        private IEmployeeRepository _employeeRepository;
        private ICustomerRepository _customerRepository;
        private IAdminRepository _adminRepository;


        public OrderUnitOfWork(DomainDbContext orderDbContext)
        {
            _dbContext = orderDbContext;
        }

        public IOrderRepository OrderRepository => _orderRepository ??= new OrderRepository(_dbContext);

        public IOrderCommentRepository OrderCommentRepository => 
            _orderCommentRepository ??= new OrderCommentRepository(_dbContext);

        public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_dbContext);
        
        public IProductServiceFormatRepository ProductServiceFormatRepository => 
            _productServiceFormatRepository ??= new ProductServiceFormatRepository(_dbContext);
        
        public IProductServiceRepository ProductServiceRepository => 
            _productServiceRepository ??= new ProductServiceRepository(_dbContext);
        
        public IProductServiceMaterialRepository ProductServiceMaterialRepository => 
            _productServiceMaterialRepository ??= new ProductServiceMaterialRepository(_dbContext);
        
        public IEmployeeRepository EmployeeRepository => _employeeRepository ??= new EmployeeRepository(_dbContext);
        public ICustomerRepository CustomerRepository => _customerRepository ??= new CustomerRepository(_dbContext);
        public IAdminRepository AdminRepository => _adminRepository ??= new AdminRepository(_dbContext);

        public async Task<int> Save() => await _dbContext.SaveChangesAsync();
    }
}
