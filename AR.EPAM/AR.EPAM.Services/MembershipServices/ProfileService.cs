using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AR.EPAM.Core;
using AR.EPAM.Core.Entities.Membership;
using AR.EPAM.Core.Exceptions;
using AR.EPAM.Infrastructure.Guard;
using AR.EPAM.Services.Exceptions;

namespace AR.EPAM.Services.MembershipServices
{
    public class ProfileService : IService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryFactory _factoryOfRepositories;

        public ProfileService(IUnitOfWork unitOfWork, IRepositoryFactory factoryOfRepositories)
        {
            Guard.AgainstNullReference(unitOfWork, "unitOfWork");
            Guard.AgainstNullReference(factoryOfRepositories, "factoryOfRepositories");

            _unitOfWork = unitOfWork;
            _factoryOfRepositories = factoryOfRepositories;
        }

        public Profile CreateProfile(string name, string surname, string patronymic, string city, string phoneNumber, int userId)
        {
            var profile = new Profile
            {
                City = city,
                Name = name,
                Patronymic = patronymic,
                PhoneNumber = phoneNumber,
                Surname = surname,
                UserId = userId
            };

            var profileRepository = _factoryOfRepositories.GetProfileRepository();
            profileRepository.Create(profile);
            try
            {
                _unitOfWork.PreSave();
            }
            catch (Exception e)
            {
                throw new ServiceException(e);
            }
            return profile;
        }

        public void UpdateProfile(Profile profile)
        {
            var profileRepository = _factoryOfRepositories.GetProfileRepository();
            try
            {
                profileRepository.Update(profile);
            }
            catch (Exception exception)
            {
                throw new ProfileServiceException(exception.Message);
            }
        }

        public void RemoveProfile(Profile profile)
        {
            var profileRepository = _factoryOfRepositories.GetProfileRepository();
            profileRepository.Remove(profile);
        }

        public Profile GetProfileByUserId(int userId)
        {
            var profileRepository = _factoryOfRepositories.GetProfileRepository();
            try
            {
                return profileRepository.Find(e => e.UserId == userId);
            }
            catch (ArgumentNullException ex)
            {
                throw new ProfileServiceException(ex.Message);
            }
            catch (RepositoryException exception)
            {
                throw new ProfileServiceException(exception.Message);
            }
        }
    }
}
