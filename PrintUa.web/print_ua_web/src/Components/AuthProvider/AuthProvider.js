import { React, useState, useEffect, useContext } from "react";
import AuthProviderContext from "./AuthProviderContext";

function useAuthProvide() {
    const [user, setUser] = useState(
        {
            auth: false,
            role: "",
        });

    let login = () => {
        const token = localStorage.getItem("token");

        if (Boolean(token) && token !== "undefined" && token !== "null") {
            let data = token.split(".")[1];
            let decodedData = JSON.parse(window.atob(data));

            let role = decodedData["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

            let receivedUser = { auth: true, role };

            setUser(receivedUser);
            return receivedUser;
        }

        return user;
    }

    let logout = () => {
        setUser({ auth: false, role: "" });
        localStorage.removeItem("token");
    }

    return { user, login, logout };
}

export function useAuth() {
    return useContext(AuthProviderContext);
}



function AuthProvider({ children }) {

    const authUser = useAuthProvide();

    return (
        <AuthProviderContext.Provider value={authUser}>
            {children}
        </AuthProviderContext.Provider>
    );
}

export default AuthProvider;

