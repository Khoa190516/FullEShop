import React, { useContext, useEffect } from "react";
import CartItemCard from "../Components/Cart/CartItem";
import { ListProductContext } from "../ContextProvider/ProductsCartContext";
import { AuthContext } from "../ContextProvider/AuthContext";
import { Button } from "antd";

const CartPage: React.FC = () => {
  const { cart, reloadCart } = useContext(ListProductContext);
  const { authenticated } = useContext(AuthContext);

  useEffect(() => {
    reloadCart();
  }, [authenticated]);

  return (
    <div style={{ width: "100%" }}>
      {cart === undefined ||
      cart.products === undefined ||
      cart.products.length <= 0 ? (
        <div
          style={{
            width: "100%",
            display: "flex",
            justifyContent: "center",
            alignItems: "center",
            fontSize: "20px",
            color:"white",
          }}
        >
          <b>No products in the cart.</b>
        </div>
      ) : (
        <div style={{ width: "100%" }}>
          <div
            style={{
              display: "flex",
              justifyContent: "center",
              alignItems: "center",
              fontSize: "20px",
              fontWeight: "bold",
              color:"white",
              position: "fixed",
              zIndex:"1000",
              top:"30px",
              left:"0px",
              margin:"20px 0px 0px 0px",
              width:"fit-content",
              padding: "10px 20px 10px 5px",
            }}
          >
           Total: {cart.total}$
          </div>
          <div
            style={{
              width: "90%",
              display: "flex",
              justifyContent: "flex-start",
              flexWrap: "wrap",
              marginLeft: "10%",
              marginTop: "100px",
            }}
          >
            {cart.products.map((cartItem) => (
              <CartItemCard key={cartItem.id} cartItem={cartItem} />
            ))}
          </div>
        </div>
      )}
    </div>
  );
};

export default CartPage;
