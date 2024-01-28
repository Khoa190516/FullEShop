import { createContext, useState } from "react"
import { jwtDecode } from "jwt-decode"
import { JwtTokenModel } from "../Models/ResponseModel/JwtToken"
import Roles from "../Commons/Enums"
import { TOKEN } from "../Commons/Global"

type Props = {
    children?: React.ReactNode
}

export interface User {
    role: string
}

var currAuthenticated: User = {
    role: Roles.Guest
}

const getCurrentUser = () =>{
    var tokenString = localStorage.getItem(TOKEN)
    if(tokenString!==null){
        var jwtUser = jwtDecode<JwtTokenModel>(tokenString)
        if(jwtUser!==null){
            currAuthenticated.role = jwtUser.role
        }
    }
}

type IAuthContext = {
    authenticated: User | undefined,
    setAuthenticated: (newState: User | undefined) => void
}

getCurrentUser()

const initialValue: IAuthContext = {
    authenticated: currAuthenticated,
    setAuthenticated: () => { }
}

const AuthContext = createContext<IAuthContext>(initialValue)

const AuthProvider = ({ children }: Props) => {
    const [authenticated, setAuthenticated] = useState(initialValue.authenticated)

    return (
        <AuthContext.Provider value={{ authenticated, setAuthenticated }}>
            {children}
        </AuthContext.Provider>
    )
}

export { AuthContext, AuthProvider }