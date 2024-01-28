using BussinesLayer.DTO;
using BussinesLayer.RequestModels.Customer;
using BussinesLayer.ResponseModels.ApiResponseModel;
using BussinesLayer.Service.IServices;
using BussinesLayer.Util.Hash;
using BussinesLayer.Util.JwtHelper;
using DataAccessLayer.IRepositories;
using DataAccessLayer.UnitOfWork;
using DomainLayer.Commons;
using DomainLayer.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Service.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtHelper _jwtHelper;

        public CustomerService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _jwtHelper = new JwtHelper(_configuration);
        }

        public async Task<ApiResponse> RegisterUser(UserRegisterModel userRegisterModel)
        {
            bool isEmailExisted = await _unitOfWork.CustomerRepository
                .GetAsync(x => x.Email == userRegisterModel.Email || x.UserName == userRegisterModel.UserName) != null;
            
            if(isEmailExisted)
            {
                return new ApiResponse().SetBadRequest(userRegisterModel.Email + " is already existed");   
            }

            var newCustomer = new Customer()
            {
                Email = userRegisterModel.Email,
                UserName = userRegisterModel.UserName,
                FirstName = userRegisterModel.FullName.Split(' ').First(),
                LastName = userRegisterModel.FullName.Split(' ').Last(),
                Password = HashHandler.HashPassword(userRegisterModel.Password),
            };

            await _unitOfWork.CustomerRepository.AddAsync(newCustomer);
            await _unitOfWork.SaveChangeAsync();
            return new ApiResponse().SetOk("Register successfully");
        }

        public async Task<ApiResponse> LoginUser(UserLoginModel userLoginModel)
        {
            var customer = await _unitOfWork.CustomerRepository
                .GetAsync(x => x.UserName == userLoginModel.UserName);

            if(customer == null)
            {
                return new ApiResponse().SetBadRequest("Username is not existed");
            }

            if(!HashHandler.VerifyPassword(userLoginModel.Password, customer.Password))
            {
                return new ApiResponse().SetBadRequest("Password is not correct");
            }

            var token = _jwtHelper.CreateToken(new JwtTokenModel()
            {
                UserId = customer.Id,
                Email = customer.Email,
                FullName = customer.FirstName + " " + customer.LastName,
                RoleName = Commons.CUSTOMER,
                RoleId = 3,
            });

            return new ApiResponse().SetOk(token);
        }       
    }
}
