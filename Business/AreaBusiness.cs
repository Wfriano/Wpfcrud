using Entities;
using Repository;
using System.Collections.Generic;

namespace Business
{
    public class AreaBusiness
    {
        private readonly IUnitOfWork _unitOfWork;

        public AreaBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Areas> ListaAreas()
        {
            return _unitOfWork.GenericRepository<Areas>().Get();
        }
    }
}
