using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AR.EPAM.Core;
using AR.EPAM.Core.Entities.Auction;
using AR.EPAM.Core.Entities.Membership;
using AR.EPAM.Core.Exceptions;
using AR.EPAM.Infrastructure.Guard;
using AR.EPAM.Services.Exceptions;

namespace AR.EPAM.Services.AuctionServices
{
    public class CommentService : IService
    {
        #region [Private members]

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryFactory _factoryOfRepositories;

        #endregion


        #region [Ctor's]

        public CommentService(IUnitOfWork unitOfWork, IRepositoryFactory factoryOfRepositories)
        {
            Guard.AgainstNullReference(unitOfWork, "unitOfWork");
            Guard.AgainstNullReference(factoryOfRepositories, "factoryOfRepositories");

            _unitOfWork = unitOfWork;
            _factoryOfRepositories = factoryOfRepositories;
        }

        #endregion

        #region [CommentService's members]

        public Comment CreateComment(string description, int userId, int lotId)
        {
            var comment = new Comment
            {
                Date = DateTime.Now,
                Description = description,
                UserId = userId,
                LotId = lotId
            };

            var commentRepository = _factoryOfRepositories.GetCommentRepository();
            commentRepository.Create(comment);
            _unitOfWork.PreSave();
            return comment;
        }

        public void RemoveComment(Comment comment)
        {
            var commentRepository = _factoryOfRepositories.GetCommentRepository();
            commentRepository.Remove(comment);
        }

        public Comment GetCommentById(long commentId)
        {
            var commentRepository = _factoryOfRepositories.GetCommentRepository();
            try
            {
                return commentRepository.GetEntityById(commentId);
            }
            catch (ArgumentNullException e)
            {
                throw new CommentServiceException(e.Message);
            }
            catch (RepositoryException ex)
            {
                throw new CommentServiceException(ex.Message);
            }
        }

        #endregion
    }
}
