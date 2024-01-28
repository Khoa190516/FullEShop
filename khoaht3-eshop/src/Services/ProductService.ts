import apiLinks from "../Commons/ApiEndpoints";
import httpClient from "../HttpClients/HttpClient";
import { ApiResponse } from "../Models/ResponseModel/ApiResponse";

const getListProducts = async (): Promise<ApiResponse|undefined> => {
    try{
      const response = await httpClient.get({
        url: `${apiLinks.product.getProducts}`,
      });
      return response.data as ApiResponse;
    }catch(e){
      //console.log(e)
      return undefined
    }
  };
  
  const productService = {
    getListProducts: getListProducts,
  };
  
  export default productService;