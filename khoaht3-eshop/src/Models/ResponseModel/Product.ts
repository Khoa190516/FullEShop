export interface ProductResponseModel {
  id: number;
  title: string;
  description: string;
  price: number;
  stock: number;
  availableStock: number;
  discountPercentage: number;
  thumbnail: string;
  categoryId: number;
  categoryName: string;
  branchId: number;
  branchName: string;
  images: string[];
  rating: number;
}
