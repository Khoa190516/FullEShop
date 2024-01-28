using BussinesLayer.RequestModels.CartProduct;
using BussinesLayer.ResponseModels.ApiResponseModel;
using BussinesLayer.ResponseModels.Cart;
using BussinesLayer.Service.IServices;
using DataAccessLayer.UnitOfWork;
using DomainLayer.Entities;

namespace BussinesLayer.Service.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse> GetCartByUserId(Guid userId)
        {
            var response = new ApiResponse();

            var cart = await _unitOfWork.CartRepository.GetCartWithProductIdsByUserId(userId);

            if (cart == null || cart.CartProducts == null)
            {
                if (cart is not null)
                {
                    cart.CartProducts = new List<CartProduct>();
                }
                else
                {
                    cart = new Cart();
                    var user = await _unitOfWork.CustomerRepository.GetAsync(customer => customer.Id == userId);
                    if (user is not null)
                    {
                        cart.Customer = user;
                        await _unitOfWork.CartRepository.AddAsync(cart);
                    }
                }
                await _unitOfWork.SaveChangeAsync();
                return response.SetOk(new CartResponseModel()
                {
                    Id = cart.Id,
                    UserId = cart.Customer.Id,
                });
            }

            var listProductIdsInCart = cart.CartProducts.Select(x => x.ProductId).ToList();

            var fullInfoProductsInCart = await _unitOfWork
                .ProductRepository
                .GetListProductWithFullInfoByIds(listProductIdsInCart);

            fullInfoProductsInCart ??= new();

            CartResponseModel cartResponse = new()
            {
                Id = cart.Id,
                UserId = cart.Customer.Id,
                TotalProducts = cart.CartProducts.Count(),
                TotalQuantity = cart.CartProducts.Sum(cartProduct => cartProduct.Quantity),
                Products = cart.CartProducts.Select(cartItem => new CartItemResponseModel()
                {
                    DiscountPercentage = cartItem.Product.DiscountPercentage,
                    DiscountPrice = cartItem.Product.Price * ((decimal)cartItem.Product.DiscountPercentage / 100),
                    Id = cartItem.ProductId,
                    Price = cartItem.Product.Price,
                    Quantity = cartItem.Quantity,
                    Thumbnail = cartItem.Product.Thumbnail,
                    Title = cartItem.Product.Title,
                    Total = cartItem.Quantity * cartItem.Product.Price * (decimal)(1 - cartItem.Product.DiscountPercentage / 100)
                }).ToList()
            };

            var totalDiscount = 0;
            var total = 0;

            foreach (var item in cartResponse.Products)
            {
                totalDiscount += (int)(item.Price * item.Quantity * ((decimal)item.DiscountPercentage / 100));
                total += (int)(item.Price * item.Quantity * (decimal)(1 - item.DiscountPercentage / 100));
            }

            cartResponse.DiscountedTotal = totalDiscount;

            cartResponse.Total = total;

            response.SetOk(cartResponse);

            return response;
        }

        public async Task<ApiResponse> UpdateProductToCart(Guid userId, CartProductUpdateModel productAdd)
        {
            var isStockAvailable = await _unitOfWork.ProductRepository.GetProductAvailableStock(productAdd.ProductId) >= productAdd.Quantity;
            if (!isStockAvailable || productAdd.Quantity < 1)
            {
                return new ApiResponse().SetBadRequest("Product quantity is invalid");
            }
            // Get current cart
            var cart = await _unitOfWork.CartRepository.GetCartWithProductIdsByUserId(userId);
            if (cart is null) // If cart is null => create new cart and add new product to cart
            {
                cart = new Cart();

                cart.CartProducts ??= new List<CartProduct>()
                {
                    new CartProduct(){
                        ProductId = productAdd.ProductId,
                        Quantity = productAdd.Quantity
                    }
                };

                var user = await _unitOfWork.CustomerRepository.GetAsync(customer => customer.Id == userId);
                if (user is not null)
                {
                    cart.Customer = user;
                    await _unitOfWork.CartRepository.AddAsync(cart);
                }
            }
            else // If cart is exist, check if product is exist in cart => update quantity, else => add new product to cart
            {
                cart.CartProducts ??= new List<CartProduct>();

                var existedProduct = cart.CartProducts.FirstOrDefault(product => product.ProductId == productAdd.ProductId);
                if (existedProduct is not null)
                {
                    existedProduct.Quantity += productAdd.Quantity;
                }
                else
                {
                    cart.CartProducts.Add(new CartProduct()
                    {
                        ProductId = productAdd.ProductId,
                        Quantity = productAdd.Quantity
                    });
                }
            }
            await _unitOfWork.SaveChangeAsync();
            return new ApiResponse().SetOk(true);
        }

        public async Task<ApiResponse> DecreaseProductFromCart(Guid userId, CartProductUpdateModel productAdd)
        {
            // Get current cart
            var cart = await _unitOfWork.CartRepository.GetCartWithProductIdsByUserId(userId);
            if (cart is not null && cart.CartProducts is not null) // If cart is null => create new cart and add new product to cart
            {
                var existedProduct = cart.CartProducts.FirstOrDefault(product => product.ProductId == productAdd.ProductId);
                if (existedProduct is not null)
                {
                    if (existedProduct.Quantity <= productAdd.Quantity)
                    {
                        cart.CartProducts.Remove(existedProduct);
                    }
                    else
                    {
                        existedProduct.Quantity -= productAdd.Quantity;
                    }
                    await _unitOfWork.SaveChangeAsync();
                    return new ApiResponse().SetOk(true);
                }
            }
            return new ApiResponse().SetBadRequest("Product is not exist in cart");
        }

        public async Task<ApiResponse> RemoveProductFromCart(Guid userId, Guid productId)
        {
            var cart = await _unitOfWork.CartRepository.GetCartWithProductIdsByUserId(userId);
            if(cart != null && cart.CartProducts != null && cart.CartProducts.Count > 0)
            {
                var existedProduct = cart.CartProducts.FirstOrDefault(product => product.ProductId == productId);
                if (existedProduct is not null)
                {
                    cart.CartProducts.Remove(existedProduct);
                    await _unitOfWork.SaveChangeAsync();
                    return new ApiResponse().SetOk(true);
                }
            }
            return new ApiResponse().SetNotFound();
        }
    }
}
