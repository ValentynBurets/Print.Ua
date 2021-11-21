using Data.Interface.UnitOfWork.Base;
using Domain.Entity;
using Domain.Repository.OrderManagement;
using Domain.Repository.ProductManagement;
using Domain.Repository.UserManagement;

namespace Data.Interface.UnitOfWork
{
    public interface IOrderUnitOfWork : IUnitOfWorkBase
    {
        IOrderRepository OrderRepository { get; }
        IOrderCommentRepository OrderCommentRepository { get; }
        IProductRepository ProductRepository { get; }
        IProductServiceFormatRepository ProductServiceFormatRepository { get; }
        IProductServiceRepository ProductServiceRepository { get; }
        IProductServiceMaterialRepository ProductServiceMaterialRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IAdminRepository AdminRepository { get; }
    }
}
