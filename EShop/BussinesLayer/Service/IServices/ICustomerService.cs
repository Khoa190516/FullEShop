using BussinesLayer.RequestModels.Customer;
using BussinesLayer.ResponseModels.ApiResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Service.IServices
{
    public interface ICustomerService
    {
        Task<ApiResponse> RegisterUser(UserRegisterModel userRegisterModel);
        Task<ApiResponse> LoginUser(UserLoginModel userLoginModel);
    }
}
