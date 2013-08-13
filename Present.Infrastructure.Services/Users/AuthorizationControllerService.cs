using System.Linq;
using Present.Core.Domain.Generic;
using Present.Domain.Users;
using Present.Infrastructure.Services.Users.Models;

namespace Present.Infrastructure.Services.Users
{
    public interface IAuthorizationControllerService
    {
        AuthorizationUserResponse ValidateUser(string userName, string password);
    }

   public class AuthorizationControllerService : IAuthorizationControllerService
    {
          #region Private members

       
        private readonly IReadOnlyRepositoryGeneric<User, int> _userRepository;
    

        #endregion

        #region Constructrs

        public AuthorizationControllerService(
           
            IReadOnlyRepositoryGeneric<User, int> userRepository)
        {
          
            _userRepository = userRepository;
            
        }

        #endregion



       public AuthorizationUserResponse ValidateUser(string userName, string password)
       {
           var user = _userRepository.Query.SingleOrDefault(x => x.UserName == userName && x.Password == password);
           if (user != null)
           {
               var response = new AuthorizationUserResponse();

               response.UserName = user.UserName;
               response.UserId = user.Id;
              

               return response;

           }

           return null;
       }
    }
}
