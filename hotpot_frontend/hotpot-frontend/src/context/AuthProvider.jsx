import { useState } from "react";
import { getUser } from "../utils/tokenHelper";
import AuthContext from "./AuthContext";

function AuthProvider({children}){
    const[user,setUser]=useState(getUser());
    return(
        <AuthContext.Provider value={{user,setUser}}>
            {children}
        </AuthContext.Provider>
    );
};
export default AuthProvider;