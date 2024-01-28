import { createContext, useContext, useEffect, useState } from "react";
import productService from "../Services/ProductService";
import { ProductResponseModel } from "../Models/ResponseModel/Product";
import { message } from "antd";
import { CartResponseModel } from "../Models/ResponseModel/Cart";
import { CartUpdateRequestModel } from "../Models/RequestModel/Cart";
import cartService from "../Services/CartService";
import { AuthContext } from "./AuthContext";
import { TOKEN } from "../Commons/Global";

interface Props {
  children?: React.ReactNode;
}

export interface ProductsCartContextType {
  cart: CartResponseModel;
  products: ProductResponseModel[];
  addProductToCart: (productUpdate: CartUpdateRequestModel) => Promise<boolean>;
  subtractProductFromCart: (
    productUpdate: CartUpdateRequestModel
  ) => Promise<boolean>;
  removeProductFromCart: (
    productUpdate: CartUpdateRequestModel
  ) => Promise<boolean>;
  
  reloadProduct: () => void;
  reloadCart: () => void;
}

const initialValue: ProductsCartContextType = {
  cart: {
    id: 0,
    discountedTotal: 0,
    products: [],
    total: 0,
    totalProducts: 0,
    totalQuantity: 0,
    userId: 0,
  },
  products: [],
  reloadProduct: async () => {},
  reloadCart: async () => {},
  addProductToCart: async () => {
    return false;
  },
  subtractProductFromCart: async () => {
    return false;
  },
  removeProductFromCart: async () => {
    return false;
  },
};

const fetchProducts = async () => {
  try {
    const response = await productService.getListProducts();
    if (response !== undefined) {
      if (response.result.items !== undefined) {
        initialValue.products = response.result.items as ProductResponseModel[];
        return response.result.items as ProductResponseModel[];
      }
      return [];
    } else {
      message.error("Error fetching products");
    }
  } catch (error) {
    console.error("Error fetching products:", error);
  }
};

const fetchCart = async () => {
  try {
    if (localStorage.getItem(TOKEN) === null) {
      return undefined;
    }
    const response = await cartService.getCart();
    if (response !== undefined) {
      if (response.result !== undefined) {
        //console.log(response.result);
        initialValue.cart = response.result as CartResponseModel;
        return response.result as CartResponseModel;
      }
      return undefined;
    } else {
      //message.error("Error fetching cart");
    }
  } catch (error) {
    console.error("Error fetching cart:", error);
  }
};

const ListProductContext = createContext<ProductsCartContextType>(initialValue);

const ListProductProvider = ({ children }: Props) => {
  const [listProduct, setListProduct] = useState(initialValue.products);
  const [cartProvider, setCartProvider] = useState(initialValue.cart);

  const addProductToCart = async (productUpdate: CartUpdateRequestModel) => {
    var response = await cartService.updateCart(productUpdate);
    //console.log(response);
    if (response !== undefined && response.isSuccess) {
      reloadProduct();
      reloadCart();
      return true;
    } else {
      return false;
    }
  };

  const subtractProductFromCart = async (
    productUpdate: CartUpdateRequestModel
  ) => {
    productUpdate = { ...productUpdate, isDecrease: true };
    var response = await cartService.updateCart(productUpdate);
    //console.log(response);
    if (response !== undefined && response.isSuccess) {
      reloadProduct();
      reloadCart();
      return true;
    } else {
      return false;
    }
  };

  const removeProductFromCart = async (
    productUpdate: CartUpdateRequestModel
  ) => {
    var response = await cartService.deleteProductFromCart(productUpdate.productId);
    if (response !== undefined && response.isSuccess) {
      reloadProduct();
      reloadCart();
      return true;
    } else {
      return false;
    }
  };

  const reloadProduct = async () => {
    var products = await fetchProducts();
    if (products !== undefined) {
      setListProduct(products);
    }
  };

  const reloadCart = async () => {
    var cart = await fetchCart();
    if (cart !== undefined) {
      setCartProvider(cart);
    }
  };

  useEffect(() => {
    const prepareData = async () => {
      try {
        const [products, cart] = await Promise.all([
          fetchProducts(),
          fetchCart(),
        ]);

        const listProductsResponse = products as ProductResponseModel[];
        const cartResponse = cart as CartResponseModel;

        setListProduct(listProductsResponse);
        setCartProvider(cartResponse);
      } catch (error) {
        console.log(error);
      }
    };
    prepareData();
  }, []);

  return (
    <ListProductContext.Provider
      value={{
        cart: cartProvider,
        products: listProduct,
        addProductToCart,
        subtractProductFromCart: subtractProductFromCart,
        reloadProduct: reloadProduct,
        reloadCart: reloadCart,
        removeProductFromCart: removeProductFromCart,
      }}
    >
      {children}
    </ListProductContext.Provider>
  );
};

export { ListProductContext, ListProductProvider };
