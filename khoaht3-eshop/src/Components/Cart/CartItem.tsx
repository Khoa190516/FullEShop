import {
  Button,
  message,
  Card,
  Row,
  Col,
  Divider,
  InputNumber,
} from "antd";
import { useContext, useEffect, useState } from "react";
import { CartItemResponseModel } from "../../Models/ResponseModel/Cart";
import { CartUpdateRequestModel } from "../../Models/RequestModel/Cart";
import { ListProductContext } from "../../ContextProvider/ProductsCartContext";
interface CartItemProps {
  cartItem: CartItemResponseModel;
}

const CartItemCard: React.FC<CartItemProps> = ({ cartItem }) => {
  const {
    cart,
    products,
    addProductToCart,
    subtractProductFromCart,
    removeProductFromCart,
  } = useContext(ListProductContext);

  const [selectedCartItem, setSelectedCartItem] =
    useState<CartItemResponseModel>(cartItem);
  const [selectedQuantity, setSelectedQuantity] = useState<number>(
    cartItem.quantity
  );

  useEffect(() => {
    let selectedCartItem = cart.products.find((p) => p.id === cartItem.id);
    if (selectedCartItem !== undefined) {
      setSelectedCartItem(selectedCartItem);
    }
  }, [cart, products]);

  const handleAddToCard = async (cartItem: CartItemResponseModel) => {
    var productUpdate: CartUpdateRequestModel = {
      productId: cartItem.id,
      quantity: selectedQuantity,
      isDecrease: false,
    };
    var response = await addProductToCart(productUpdate);
    if (response !== undefined && response === true) {
      message.success("Added to cart");
    } else {
      message.error("Error adding to cart");
    }
  };

  const handleSubtractFromCard = async (cartItem: CartItemResponseModel) => {
    var productUpdate: CartUpdateRequestModel = {
      productId: cartItem.id,
      quantity: selectedQuantity,
      isDecrease: true,
    };
    var response = await subtractProductFromCart(productUpdate);
    //console.log(response);
    if (response !== undefined && response === true) {
      message.success("Subtract from cart");
    } else {
      message.error("Error adding to cart");
    }
  };

  const handleRemoveProductFromCard = async (
    cartItem: CartItemResponseModel
  ) => {
    var productUpdate: CartUpdateRequestModel = {
      productId: cartItem.id,
      quantity: 0,
      isDecrease: true,
    };
    var response = await removeProductFromCart(productUpdate);

    if (response !== undefined && response === true) {
      message.success("Removed from cart");
    } else {
      message.error("Error remove to cart");
    }
  };

  return (
    <Card
      title={
        <Row
          gutter={10}
          style={{ fontWeight: "bold", display: "flex", alignItems: "center" }}
        >
          <Col
            span={16}
            style={{
              fontSize: "17px",
              overflow: "hidden",
              textOverflow: "ellipsis",
              whiteSpace: "nowrap",
            }}
          >
            {selectedCartItem.title}
          </Col>
          <Col span={8}>
            {/* {selectedCartItem.discountPercentage > 0 && (
              <Tag
                color="red"
                style={{
                  fontSize: "17px",
                  textAlign: "center",
                  display: "flex",
                  justifyContent: "center",
                  alignItems: "center",
                  padding: "2px",
                }}
              >
                Sale {selectedCartItem.discountPercentage}%
              </Tag>
            )} */}
            <Button
              type="primary"
              danger
              onClick={() => handleRemoveProductFromCard(selectedCartItem)}
            >
              Remove
            </Button>
          </Col>
        </Row>
      }
      style={{ width: "350px", margin: "5px", height: "600px" }}
      bodyStyle={{ height: "544px" }}
    >
      <br />
      <div>
        <div>
          <img
            style={{ width: "100%", height: "200px" }}
            src={selectedCartItem.thumbnail}
            alt={selectedCartItem.title}
          />
        </div>
      </div>
      <br />
      <div
        style={{
          display: "flex",
          flexDirection: "row",
          justifyContent: "space-between",
          alignItems: "center",
        }}
      >
        <span style={{ fontSize: "20px", fontWeight: "bold" }}>
          {selectedCartItem.price.toFixed(2)}$
        </span>
      </div>
      <br />
      <div style={{ fontSize: "15px" }}>Price: {selectedCartItem.price}$</div>
      <div style={{ fontSize: "15px" }}>
        Sale off: {selectedCartItem.discountPercentage}%
      </div>
      <div style={{ fontSize: "15px" }}>
        Available Stock: {products.find((p) => p.id === cartItem.id)?.availableStock}
      </div>
      <div style={{ fontSize: "15px" }}>
        Quantity: {selectedCartItem.quantity}
      </div>
      <div style={{ fontSize: "15px" }}>
        Saved: {selectedCartItem.discountPrice}$
      </div>
      <div style={{ fontSize: "15px", fontWeight: "bold" }}>
        Total: {selectedCartItem.total}$
      </div>
      <br/>
      <div
        style={{
          width: "100%",
          justifyContent: "center",
          alignItems: "center",
        }}
      >
        <Row
          gutter={10}
          style={{
            display: "flex",
            justifyContent: "center",
            width: "100%",
            alignItems: "center",
          }}
        >
          <Col span={8}>
            <Button
              type="primary"
              onClick={() => handleSubtractFromCard(selectedCartItem)}
              style={{ fontWeight: "bold", width: "100%" }}
            >
              -
            </Button>
          </Col>
          <Col span={8}>
            <InputNumber
              min={0}
              defaultValue={selectedQuantity}
              precision={0}
              onChange={(value) =>
                setSelectedQuantity(value ?? selectedQuantity)
              }
              style={{ fontWeight: "bold", width: "100%"}}
            />
          </Col>
          <Col span={8}>
            <Button
              type="primary"
              onClick={() => handleAddToCard(selectedCartItem)}
              style={{ fontWeight: "bold", width: "100%" }}
            >
              +
            </Button>
          </Col>
        </Row>
      </div>
    </Card>
  );
};

export default CartItemCard;
