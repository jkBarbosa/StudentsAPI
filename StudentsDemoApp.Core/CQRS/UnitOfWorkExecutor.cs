using System.Threading.Tasks;

namespace Core.CQRS
{
    public class UnitOfWorkExecutor<T> : ICommandExecutor<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommandExecutor<T> _inner;

        public UnitOfWorkExecutor(ICommandExecutor<T> inner, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _inner = inner;
        }

        public async Task ExecuteAsync(T command)
        {

            using (_unitOfWork.Start())
            {
                try
                {
                    await Task.Yield();
                    await _inner.ExecuteAsync(command);
                    _unitOfWork.Commit();
                }
                catch
                {
                    _unitOfWork.Rollback();
                    throw;
                }
            }
        }
    }
}
