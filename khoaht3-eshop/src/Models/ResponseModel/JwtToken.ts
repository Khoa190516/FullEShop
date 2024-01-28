export interface JwtTokenModel {
    nameid: string
    email: string
    unique_name: string
    role: string
    nbf: number
    exp: number
    iat: number
  }