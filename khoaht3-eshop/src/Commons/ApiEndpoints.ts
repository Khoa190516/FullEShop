// const port = "5000"
// const port = "7241"
const port = "5000"

const baseUrl = `http://184.72.175.233:${port}/api/`

const apiLinks = {
    auth: {
        postLogin: `${baseUrl}Customers/login`,
        postSignUp: `${baseUrl}Customers/sign-up`
    },
    product:{
        getProducts: `${baseUrl}Products`,
        putProductById: `${baseUrl}Products/`,
    },
    cart:{
        getCart: `${baseUrl}Carts`,
        putCart: `${baseUrl}Carts`,
        deleteProductFromCart: `${baseUrl}Carts/`,
    },  
}

export default apiLinks