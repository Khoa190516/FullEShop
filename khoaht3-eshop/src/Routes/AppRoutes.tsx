import { BrowserRouter, Route, Routes } from "react-router-dom";
import { ROUTE_URLS } from "../Commons/Global";
import CatalogPage from "../Pages/CatalogPage";
import LoginPage from "../Pages/LoginPage";
import ProtectedRoute from "../ProtectedRoutes/ProtectedRoute";
import Roles from "../Commons/Enums";
import CartPage from "../Pages/CartPage";
import ErrorPage from "../Pages/ErrorPage";
import UnauthorizedPage from "../Pages/UnauthorizedPage";
import DefaultLayoutPage from "../LayoutPages/DefaultLayoutPage";

const AppRoutes = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route
          path={ROUTE_URLS.HOME_PAGE}
          element={<DefaultLayoutPage children={<CatalogPage />} />}
        />
        <Route
          path={ROUTE_URLS.LOGIN_PAGE}
          element={<DefaultLayoutPage children={<LoginPage />} />}
        />

        <Route
          path={ROUTE_URLS.CART_PAGE}
          element={<ProtectedRoute role={[Roles.Admin, Roles.Customers]} />}
        >
          <Route
            path=""
            element={<DefaultLayoutPage children={<CartPage />} />}
          />
        </Route>

        <Route
          path={ROUTE_URLS.ERROR_PAGE}
          element={<DefaultLayoutPage children={<ErrorPage />} />}
        />
        <Route
          path={ROUTE_URLS.UNAUTHORIZED_PAGE}
          element={<DefaultLayoutPage children={<UnauthorizedPage />} />}
        />
      </Routes>
    </BrowserRouter>
  );
};

export default AppRoutes;
