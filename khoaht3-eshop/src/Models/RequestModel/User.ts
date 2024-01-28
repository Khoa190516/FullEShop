export interface UserLoginRequestModel {
  userName: string;
  password: string;
}

export interface UserSignUpRequestModel extends UserLoginRequestModel {
  email: string;
  fullName: string;
}

export interface UserSignUpFormModel extends UserSignUpRequestModel {
  confirmPassword: string;
}