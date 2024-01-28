import { Form, Input, message } from "antd";
import {
  UserSignUpFormModel,
  UserSignUpRequestModel,
} from "../../Models/RequestModel/User";
import authService from "../../Services/AuthService";

const SignUpForm = () => {
  const onSignUpFinish = async (values: UserSignUpFormModel) => {
    if (values.password !== values.confirmPassword) {
      message.error("Password and confirm password not match", 2);
      return;
    }

    var signUpRequest: UserSignUpRequestModel = {
      fullName: values.fullName,
      userName: values.userName,
      email: values.email,
      password: values.password,
    };

    var response = await authService.signUp(signUpRequest);
    if (response !== undefined) {
      message.success("Sign up success", 2).then(() => {
        window.location.reload();
      });
    } else {
      message.error("Sign up failed", 2);
      return;
    }
  };

  return (
    <Form id="signUpForm" onFinish={onSignUpFinish} name="basic">
      <Form.Item<UserSignUpFormModel> label="Full name" name="fullName">
        <Input placeholder="Full name" required />
      </Form.Item>
      <Form.Item<UserSignUpFormModel> label="Username" name="userName">
        <Input placeholder="Username" required />
      </Form.Item>
      <Form.Item<UserSignUpFormModel> label="Email" name="email">
        <Input type="email" placeholder="Email" required />
      </Form.Item>
      <Form.Item<UserSignUpFormModel> label="Password" name="password">
        <Input.Password placeholder="Password" required />
      </Form.Item>
      <Form.Item<UserSignUpFormModel>
        label="Confirm password"
        name="confirmPassword"
      >
        <Input.Password placeholder="Confirm password" required />
      </Form.Item>
    </Form>
  );
};

export default SignUpForm;
