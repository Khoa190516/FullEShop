import { Button, Carousel, message, Tag, Card, Row, Col, Rate } from "antd";
import { StarOutlined } from "@ant-design/icons";
import { ProductResponseModel } from "../../Models/ResponseModel/Product";
import { useContext, useEffect, useState } from "react";
import { CartUpdateRequestModel } from "../../Models/RequestModel/Cart";
import { ListProductContext } from "../../ContextProvider/ProductsCartContext";
import { AuthContext } from "../../ContextProvider/AuthContext";
import Roles from "../../Commons/Enums";

interface ProductProps {
  product: ProductResponseModel;
}

const ProductCard: React.FC<ProductProps> = ({ product }) => {
  const { products, addProductToCart } = useContext(ListProductContext);
  const { authenticated } = useContext(AuthContext);
  const [selectedProduct, setSelectedProduct] =
    useState<ProductResponseModel>();

  useEffect(() => {
    let selectedProduct = products.find((p) => p.id === product.id);
    if (selectedProduct !== undefined) {
      setSelectedProduct(selectedProduct);
      //console.log(selectedProduct.id);
    }
  }, [products]);

  const handleAddToCard = async (product: ProductResponseModel) => {
    if (
      authenticated === undefined ||
      authenticated === null ||
      authenticated.role === Roles.Guest
    ) {
      message.error("Please login to add product to cart", 2);
      return;
    }

    var productUpdate: CartUpdateRequestModel = {
      productId: product.id,
      isDecrease: false,
      quantity: 1,
    };
    var isUpdated = await addProductToCart(productUpdate);
    if (isUpdated === true) {
      message.success("Added To Cart From Product Card", 2);
    } else {
      message.error("Error Adding To Cart From Product Card", 2);
    }
  };

  return selectedProduct === undefined ? (
    <h2>No Product Found</h2>
  ) : (
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
            {selectedProduct.title}
          </Col>
          <Col span={8}>
            {selectedProduct.discountPercentage > 0 && (
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
                Sale {selectedProduct.discountPercentage}%
              </Tag>
            )}
          </Col>
        </Row>
      }
      style={{ width: "350px", margin: "5px", height: "600px" }}
      bodyStyle={{ height: "544px" }}
    >
      <div>
        <Tag color="green">{selectedProduct.categoryName}</Tag>
        <Tag color="cyan">{selectedProduct.branchName}</Tag>
      </div>
      <br />
      <div>
        <Carousel autoplay infinite style={{ width: "100%", height: "200px" }}>
          {selectedProduct.images.map((image, index) => {
            return (
              <div key={index}>
                <img
                  key={index}
                  style={{ width: "100%", height: "200px" }}
                  src={image}
                  alt={selectedProduct.title}
                />
              </div>
            );
          })}
        </Carousel>
      </div>
      <br />
      <div style={{ fontSize: "15px" }}>{selectedProduct.description}</div>
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
          {selectedProduct.price.toFixed(2)}$
        </span>
      </div>
      <br />
      <div style={{ fontSize: "15px" }}>
        Rating: {""}
        <Rate disabled allowHalf defaultValue={selectedProduct.rating} />
      </div>
      <div style={{ fontSize: "15px" }}>
        Stock: {selectedProduct.availableStock}
      </div>
      <div
        style={{
          marginBottom: "5px",
          position: "absolute",
          bottom: "18px",
          right: "25px",
        }}
      >
        <Button
          type="primary"
          onClick={() => handleAddToCard(selectedProduct)}
          style={{ fontWeight: "bold" }}
        >
          Add To Card
        </Button>
      </div>
    </Card>
  );
};

export default ProductCard;
