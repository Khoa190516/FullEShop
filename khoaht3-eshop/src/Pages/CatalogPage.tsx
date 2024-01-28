import React, { useContext, useEffect } from "react";
import { message } from "antd";
import ProductCard from "../Components/Catalog/ProductCard";
import Search, { SearchProps } from "antd/es/input/Search";
import { ListProductContext } from "../ContextProvider/ProductsCartContext";

const CatalogPage: React.FC = () => {
  const { products, reloadProduct } = useContext(ListProductContext);

  const onSearch: SearchProps["onSearch"] = (value) => {
    if (value !== "") {
      const filteredProducts = products.filter((product) =>
        product.title.toLowerCase().includes(value.toLowerCase())
      );
      if (filteredProducts.length > 0) {
        products.length = 0;
        filteredProducts.forEach((product) => {
          products.push(product);
        });
      } else {
        message.error("Product not found");
      }
    } else {
      reloadProduct();
    }
  };

  return (
    <div>
      <br />
      <div
        style={{
          width: "100%",
        }}
      >
        {products !== undefined && products.length > 0 && (
          <div
            style={{
              width:"100%",
              display: "flex",
              justifyContent: "center",
              alignItems: "center",
            }}
          >
            {/* <div
              style={{
                width: "100%",
                display: "flex",
                justifyContent: "center",
              }}
            >
              <Search
                placeholder="Search..."
                allowClear
                style={{ width: "300px" }}
                onChange={(e) => onSearch(e.target.value)}
              />
            </div> */}
            <div
              style={{
                width: "90%",
                display: "flex",
                marginLeft:"10%",
                justifyContent: "flex-start",
                flexWrap: "wrap",
              }}
            >
              {products.map((product) => (
                <ProductCard key={product.id} product={product} />
              ))}
            </div>
          </div>
        )}
      </div>
    </div>
  );
};

export default CatalogPage;
