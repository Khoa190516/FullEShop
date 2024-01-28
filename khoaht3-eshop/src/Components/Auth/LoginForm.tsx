import { Form, Input, Button, message, Row, Col, Modal } from "antd";
import authService from "../../Services/AuthService";
import { jwtDecode } from "jwt-decode";
import { useNavigate } from "react-router-dom";
import { AuthContext, User } from "../../ContextProvider/AuthContext";
import { useContext, useState } from "react";
import Roles from "../../Commons/Enums";
import { TOKEN } from "../../Commons/Global";
import { UserLoginRequestModel } from "../../Models/RequestModel/User";
import { JwtTokenModel } from "../../Models/ResponseModel/JwtToken";
import GenericModal from "../Generic/GenericModal";
import SignUpForm from "./SignUpForm";

const LoginForm = () => {
  const { setAuthenticated } = useContext(AuthContext);
  const [isSignInLoading, setIsSignInLoading] = useState(false);
  const [isSignUpModalVisible, setIsSignUpModalVisible] = useState(false);
  const navigate = useNavigate();

  const authorizedNavigate = (role: string) => {
    switch (role) {
      case Roles.Admin: {
        navigate("/");
        break;
      }
      case Roles.Customers: {
        navigate("/");
        break;
      }
      default: {
        navigate("/error");
        break;
      }
    }
  };

  const onLoginFinish = async (values: UserLoginRequestModel) => {
    setIsSignInLoading(true);
    var response = await authService.login(values);
    if (response !== undefined) {
      //console.log(response);
      setIsSignInLoading(false);
      var token = response.result as string;
      localStorage.setItem(TOKEN, token);

      var jwtUser: JwtTokenModel = jwtDecode(token);
      //console.log(jwtUser);
      let newUser: User = {
        role: jwtUser.role,
      };

      setAuthenticated(newUser);
      message.success(
        `Login success, hello ${jwtUser.role}, ${jwtUser.unique_name}`,
        2
      );
      authorizedNavigate(newUser.role);
    } else {
      setIsSignInLoading(false);
      message.error("Login failed");
      return;
    }
  };

  const onLoginFinishFailed = (errorInfo: any) => {
    message.error("Login failed ");
    console.log("Login failed:", errorInfo)
  };

  return (
    <div
      style={{
        backgroundColor: "white",
        width: "auto",
        maxWidth: "40%",
        margin: "5%",
        padding: "2% 5% 2% 5%",
        borderRadius: "15px",
      }}
    >
      <h2 style={{ paddingLeft: "10%", fontSize: "30px", marginBottom: "5%" }}>
        KhoaHT3 EShop - Sign Up
      </h2>
      <Form
        name="basic"
        id="loginForm"
        onFinish={onLoginFinish}
        onFinishFailed={onLoginFinishFailed}
        autoComplete="off"
        size="large"
      >
        <Form.Item<UserLoginRequestModel>
          label={<p>Username</p>}
          name="userName"
          rules={[{ required: true, message: "Please input your username" }]}
        >
          <Input placeholder="Username" />
        </Form.Item>

        <Form.Item<UserLoginRequestModel>
          label={<p>Password</p>}
          name="password"
          rules={[{ required: true, message: "Please input your password!" }]}
        >
          <Input.Password />
        </Form.Item>

        <Row
          gutter={10}
          style={{ display: "flex", justifyContent: "flex-end" }}
        >
          <Col span={8}>
            <Form.Item wrapperCol={{ offset: 8, span: 16 }}>
              <Button
                key="loginSubmit"
                loading={isSignInLoading}
                form="loginForm"
                type="primary"
                htmlType="submit"
              >
                Đăng nhập
              </Button>
            </Form.Item>
          </Col>
          <Col span={1} />
          <Col span={8}>
            <Button type="link" onClick={() => setIsSignUpModalVisible(true)}>
              Đăng ký
            </Button>
          </Col>
        </Row>
      </Form>
      <GenericModal
        title="Đăng ký"
        isOpen={isSignUpModalVisible}
        style={{width:"max-content", minWidth:"40%"}}
        onClose={() => {
          setIsSignUpModalVisible(false);
        }}
        footer={[
          <Button
            key="signUpCancel"
            onClick={() => setIsSignUpModalVisible(false)}
          >
            Hủy
          </Button>,
          <Button
            key="signUpSubmit"
            loading={isSignInLoading}
            type="primary"
            htmlType="submit"
            form="signUpForm"
          >
            Đăng ký
          </Button>,
        ]}
        childComponent={<SignUpForm />}
      />
    </div>
  );
};

export default LoginForm;
