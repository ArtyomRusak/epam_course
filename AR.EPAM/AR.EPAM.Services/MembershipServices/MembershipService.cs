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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryFactory _factoryOfRepositories;

        public MembershipService(IUnitOfWork unitOfWork, IRepositoryFactory factoryOfRepositories)
        {
            Guard.AgainstNullReference(unitOfWork, "unitOfWork");
            Guard.AgainstNullReference(factoryOfRepositories, "factoryOfRepositories");

            _unitOfWork = unitOfWork;
            _factoryOfRepositories = factoryOfRepositories;
        }

        public User RegisterUser(string email, string userName, string password)
        {
            //email validation.

            var role = GetRoleByName("Member");
            if (role == null)
            {
                throw new MembershipServiceException("Role doesn't exist");
            }

            var user = GetUserByEmail(email);
            if (user != null)
            {
                throw new MembershipServiceException("User is registered.");
            }
            user = new User { Email = email, UserName = userName, PasswordSalt = DateTime.Now.ToString(), RegisterDate = DateTime.Now };

            Guard.AgainstEmptyStringOrNull(password, "password");
            user.SetPassword(password);

            var userRepository = _factoryOfRepositories.GetUserRepository();
            userRepository.Create(user);

            user.Roles.Add(role);

            try
            {
                _unitOfWork.PreSave();
            }
            catch (Exception e)
            {
                throw new ServiceException(e);
            }
            return user;
        }

        public User LoginUser(string email, string password)
        {
            //email validation.
            var user = GetUserByEmail(email);
            if (user == null)
            {
                return null;
            }

            if (user.VerifyPassword(password))
            {
                return user;
            }
            else
            {
                throw new MembershipServiceException("Wrong password.");
            }
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

        public User GetUserById(int id)
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

        public User GetUserByEmail(string email)
        {
            var userRepository = _factoryOfRepositories.GetUserRepository();
            try
            {
                return userRepository.Find(e => e.Email == email);
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

        public User GetUserByUserName(string username)
        {
            var userRepository = _factoryOfRepositories.GetUserRepository();
            try
            {
                return userRepository.Find(e => e.UserName == username);
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

        public List<User> GetLastRegisteredUsers(int count)
        {
            var userRepository = _factoryOfRepositories.GetUserRepository();
            try
            {
                return userRepository.All().OrderByDescending(e => e.RegisterDate).Take(count).ToList();
            }
            catch (Exception ex)
            {
                throw new MembershipServiceException(ex.Message);
            }
        }

        public List<User> GetUsersByUserNameContaining(string username)
        {
            var userRepository = _factoryOfRepositories.GetUserRepository();
            try
            {
                return userRepository.Filter(e => e.UserName.Contains(username)).ToList();
            }
            catch (Exception ex)
            {
                throw new MembershipServiceException(ex.Message);
            }
        }
    }
}
