import apiLinks from "../Commons/ApiEndpoints";
import httpClient from "../HttpClients/HttpClient";
import { UserLoginRequestModel, UserSignUpRequestModel } from "../Models/RequestModel/User";
import { ApiResponse } from "../Models/ResponseModel/ApiResponse";

const login = async (data: UserLoginRequestModel): Promise<ApiResponse|undefined> => {
    try{
      const response = await httpClient.post({
        url: `${apiLinks.auth.postLogin}`,
        data: JSON.stringify(data),
      });
      return response.data as ApiResponse;
    }catch(e){
      //console.log(e)
      return undefined
    }
  };

  const signUp = async (data: UserSignUpRequestModel): Promise<ApiResponse|undefined> => {
    try{
      const response = await httpClient.post({
        url: `${apiLinks.auth.postSignUp}`,
        data: JSON.stringify(data),
      });
      return response.data as ApiResponse;
    }catch(e){
      //console.log(e)
      return undefined
    }
  };
  
  const authService = {
    login: login,
    signUp: signUp
  };
  
  export default authService;