using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business
{
    public class UserBusiness
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool CreateUser(Usuarios usuario)
        {
            try
            {
                _unitOfWork.GenericRepository<Usuarios>().Insert(usuario);

                return _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear usuario: {ex.Message}");
                return false;
            }
        }

        public bool AsignarArea(int userId, int areaId)
        {
            var asignacionExistente = _unitOfWork.GenericRepository<AreaUsuarios>()
                .Get(au => au.IdUsuario == userId);

            if (asignacionExistente != null && asignacionExistente.Any())
            {
                return false;
            }

            var areaUsuario = new AreaUsuarios
            {
                IdArea = areaId,
                IdUsuario = userId
            };

            _unitOfWork.GenericRepository<AreaUsuarios>().Insert(areaUsuario);
            _unitOfWork.Save();
            return true;
        }

        public List<Usuarios> ListaUsuario()
        {
            return _unitOfWork.GenericRepository<Usuarios>().Get().ToList();
        }

        public bool UpdateUser(Usuarios usuario)
        {
            try
            {
                _unitOfWork.GenericRepository<Usuarios>().Update(usuario);
                _unitOfWork.Save();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
