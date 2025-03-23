//using BLL.Contract;
//using BusinessLayer.DTOs.Users;
//using DataAccessLayer.Entities;
//using Microsoft.AspNetCore.Identity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BusinessLayer.Services
//{
//    public class UserService : IUserService
//    {
//        private readonly UserManager<User> _userManager;
//        private readonly SignInManager<User> _signInManager;

//        public UserService(UserManager<User> userManager, SignInManager<User> signInManager)
//        {
//            _userManager = userManager;
//            _signInManager = signInManager;
//        }
//        public async Task<bool> CheckPassword(User user, string Password)
//        {
//            var isPasswordValid = await _userManager.CheckPasswordAsync(user, Password);
//            return isPasswordValid;
//        }

//        public async Task<User?> FindUserByUserName(string UserName)
//        {
//            return await _userManager.FindByNameAsync(UserName);
//        }

//        public async Task<IdentityResult> RegisterUser(UserDto User)
//        {
//            var userEntity = User.ToEntity();

//            IdentityResult result = await _userManager.CreateAsync(userEntity, User.Password);

//            return result;
//        }

//        public async Task Signin(LoginUserDto User, bool IsCookiePersistant)
//        {
//            await _signInManager.SignInAsync(User.ToEntity(), IsCookiePersistant);
//        }

//        public async Task Signout()
//        {
//            await _signInManager.SignOutAsync();
//        }
//    }
//}
