using System.Threading.Tasks;

namespace Data.Interface.UnitOfWork.Base
{
    public interface IUnitOfWorkBase
    {
        Task<int> Save();
    }
}
