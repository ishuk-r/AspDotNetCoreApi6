﻿using AspDotNetCoreApi6.Models;
using AspDotNetCoreApi6.Models.ViewModels;
using AspDotNetCoreApi6.Services.Interface;
using Microsoft.EntityFrameworkCore;
using AspDotNetCoreApi6.Enums;

namespace AspDotNetCoreApi6.Services
{
    public class UserService : IUser
    {
        private readonly MovieContext _movieContext;

        public UserService(MovieContext movieContext)
        {
            _movieContext = movieContext;
        }

        public async Task<Status> Login(Login loginModel)
        {
            var user = await _movieContext.Users.FirstOrDefaultAsync(x => x.Email == loginModel.UserName);
            if (user == null) return Status.UserNotFound;

            var decodedPassword = PasswordHelper.DecodeFrom64(user.Password);
            if (loginModel.Passsword != decodedPassword) return Status.PasswordNotMatch;

            return Status.LoginSuccess;
        }


        public async Task<Status> RegisterUser(UserModel user)
        {
            if (user == null) return Status.RegistrationFailed;

            var isEmailAlreadyExist = await _movieContext.Users.AnyAsync(x => x.Email == user.Email);
            if (isEmailAlreadyExist) return Status.EmailAlreadyExist;

            user.Password = PasswordHelper.EncodePasswordToBase64(user.Password);

            User newUser = new User()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                Mobile = user.Mobile,
                Address = user.Address
            };

            await _movieContext.Users.AddAsync(newUser);
            var result = await _movieContext.SaveChangesAsync();

            if (result == 1) return Status.RegistrationComplete;

            return Status.RegistrationFailed;
        }
    }
}
