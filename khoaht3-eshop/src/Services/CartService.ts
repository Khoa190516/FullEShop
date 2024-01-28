import { UUID } from "crypto";
import apiLinks from "../Commons/ApiEndpoints";
import httpClient from "../HttpClients/HttpClient";
import { CartUpdateRequestModel } from "../Models/RequestModel/Cart";
import { ApiResponse } from "../Models/ResponseModel/ApiResponse";

const getCart = async (): Promise<ApiResponse|undefined> => {
    try{
      const response = await httpClient.get({
        url: `${apiLinks.cart.getCart}`,
      });
      return response.data as ApiResponse;
    }catch(e){
      //console.log(e)
      return undefined
    }
  };

  const updateCart = async (data: CartUpdateRequestModel): Promise<ApiResponse|undefined> => {
    try{
      const response = await httpClient.put({
        url: `${apiLinks.cart.putCart}`,
        data: JSON.stringify(data),
      });
      return response.data as ApiResponse;
    }catch(e){
      //console.log(e)
      return undefined
    }
  };

  const deleteProductFromCart = async (productId: number): Promise<ApiResponse|undefined> => {
    try{
      const response = await httpClient.delete({
        url: `${apiLinks.cart.deleteProductFromCart}${productId}`,
      });
      return response.data as ApiResponse;
    }catch(e){
      //console.log(e)
      return undefined
    }
  };
  
  const cartService = {
    getCart: getCart,
    updateCart: updateCart,
    deleteProductFromCart: deleteProductFromCart
  };
  
  export default cartService;