import "./App.css";
import { AuthProvider } from "./ContextProvider/AuthContext";
import { ListProductProvider } from "./ContextProvider/ProductsCartContext";
import AppRoutes from "./Routes/AppRoutes";

function App() {
  return (
    <AuthProvider children={<ListProductProvider children={<AppRoutes />} />} />
  );
}

export default App;
