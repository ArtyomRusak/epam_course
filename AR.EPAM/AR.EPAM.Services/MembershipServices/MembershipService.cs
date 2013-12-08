using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AR.EPAM.Core;
using AR.EPAM.Core.Entities.Membership;
using AR.EPAM.Core.Exceptions;
using AR.EPAM.Infrastructure.Guard;
using AR.EPAM.Services.Exceptions;

namespace AR.EPAM.Services.MembershipServices
{
    public class MembershipService : IService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IRepositoryFactory _factoryOfRepositories;

        public MembershipService(IUnitOfWork unitOfWork, IRepositoryFactory factoryOfRepositories)
        {
            _unitOfWork = unitOfWork;
            _factoryOfRepositories = factoryOfRepositories;
        }

        public User RegisterUser(string email, string userName, string password, string roleName)
        {
            //email validation.

            var role = GetRoleByName(roleName);
            if (role == null)
            {
                throw new MembershipServiceException("Role doesn't exist");
            }

            var user = GetUserByEmail(email);
            if (user != null)
            {
                throw new MembershipServiceException("User is registered.");
            }
            user = new User { Email = email, UserName = userName };

            Guard.AgainstEmptyStringOrNull(password, "password");
            user.SetPassword(password);
            user.Roles.Add(role);

            var userRepository = _factoryOfRepositories.GetUserRepository();
            userRepository.Create(user);
            return user;
        }

        public User LoginUser(string email, string password)
        {
            //email validation.
            var user = GetUserByEmail(email);
            if (user == null)
            {
                throw new MembershipServiceException("User doesn't exist.");
            }

            user.IsLogged = true;
            UpdateUser(user);
            return user;
        }

        public void ResetPassword(string email, string password, string newPassword)
        {
            var user = GetUserByEmail(email);
            if (user == null)
            {
                throw new MembershipServiceException("User doesn't exist.");
            }
            Guard.AgainstInequalityOfValues(user.Password, (password + user.PasswordSalt).GetHashCode(), "Password is not new.");

            user.PasswordSalt = DateTime.Now.ToString();
            user.SetPassword(newPassword);
            UpdateUser(user);
        }

        public void LogoutUser(int id)
        {
            var user = GetUser(id);
            user.IsLogged = false;
            UpdateUser(user);
        }

        public User GetUser(int id)
        {
            var userRepository = _factoryOfRepositories.GetUserRepository();
            try
            {
                var user = userRepository.GetEntityById(id);
                return user;
            }
            catch (Exception e)
            {
                throw new ServiceException(e.Message);
            }
        }

        private User GetUserByEmail(string email)
        {
            Guard.AgainstEmptyStringOrNull(email, "email");

            var userRepository = _factoryOfRepositories.GetUserRepository();
            try
            {
                var user = userRepository.Find(e => e.Email == email);
                return user;
            }
            catch (ArgumentException ex)
            {
                throw new MembershipServiceException(ex.Message);
            }
            catch (RepositoryException ex)
            {
                throw new MembershipServiceException(ex.Message);
            }
        }

        public Role GetRoleByName(string roleName)
        {
            Guard.AgainstEmptyStringOrNull(roleName, "roleName");

            var roleRepository = _factoryOfRepositories.GetRoleRepository();
            try
            {
                return roleRepository.Find(e => e.Name == roleName);
            }
            catch (ArgumentException ex)
            {
                throw new ServiceException(ex);
            }
            catch (Exception ex)
            {
                throw new MembershipServiceException(ex.Message);
            }
        }

        public void UpdateUser(User user)
        {
            var userRepository = _factoryOfRepositories.GetUserRepository();
            try
            {
                userRepository.Update(user);
            }
            catch (Exception ex)
            {
                throw new MembershipServiceException(ex.Message);
            }
        }
    }
}
