export interface CartResponseModel {
  id: number;
  userId: number;
  total: number;
  discountedTotal: number;
  totalProducts: number;
  totalQuantity: number;
  products: CartItemResponseModel[];
}

export interface CartItemResponseModel {
  id: number;
  title: string;
  price: number;
  quantity: number;
  total: number;
  discountPercentage: number;
  discountPrice: number;
  thumbnail: string;
}
