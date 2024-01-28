import { HomeOutlined, ShoppingCartOutlined } from "@ant-design/icons";
import { message } from "antd";
import { jwtDecode } from "jwt-decode";
import { useContext, useEffect, useState } from "react";
import { Link, useLocation, useNavigate } from "react-router-dom";
import { JwtTokenModel } from "../../Models/ResponseModel/JwtToken";
import { ROUTE_URLS, TOKEN } from "../../Commons/Global";
import '../../Style/NavigationBar.css';

const NavigationBar: React.FC = () => {
  const location = useLocation();
  const [welcomeMessage, setWelcomeMessage] = useState<string>("");
  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);

  const displayUser = () => {
    var tokenString = localStorage.getItem(TOKEN);
    if (tokenString !== null) {
      var userToken: JwtTokenModel = jwtDecode(tokenString);
      if (userToken !== null) {
        setWelcomeMessage(`Welcome, ${userToken.unique_name}`);
        setIsLoggedIn(true);
      }
    }
  };

  const handleLogout = () => {
    localStorage.removeItem(TOKEN);
    setWelcomeMessage("");
    setIsLoggedIn(false);
    message.error("Logged out", 1).then(()=>{
      window.location.reload();
    });
  };

  useEffect(() => {
    displayUser();
  });

  return (
    <nav
      className="navigation"
      style={{ position: "fixed", top: "0", zIndex: "1", width: "100%" }}
    >
      <ul>
        <li>
          <b style={{color:"lightgreen"}}>{welcomeMessage}</b>
        </li>
        <li>
          <Link className={location.pathname === "/" ? "active" : ""} to="/">
            <HomeOutlined />{" "}
            Home
          </Link>
        </li>
        <li>
          <Link
            className={location.pathname === ROUTE_URLS.CART_PAGE ? "active" : ""}
            to={ROUTE_URLS.CART_PAGE}
          >
            <ShoppingCartOutlined /> {" "}Cart
          </Link>
        </li>
      </ul>
      <div className="navbar-title">KhoaHT3 E-Shop</div>
      <div className="navbar-login" style={{ marginRight: "20px" }}>
        {isLoggedIn === true ? (
          <Link style={{color:"red"}} onClick={() => handleLogout()} to="#">
            Log out
          </Link>
        ) : (
          <Link style={{color:"green"}} to={ROUTE_URLS.LOGIN_PAGE}>Login</Link>
        )}
      </div>
    </nav>
  );
};

export default NavigationBar;