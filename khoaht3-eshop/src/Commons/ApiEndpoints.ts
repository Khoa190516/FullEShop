// const port = "5000"
// const port = "7241"
const port = "5000"

const baseUrl = `http://54.162.225.95:${port}/api/`

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