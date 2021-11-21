import { React, useState, useEffect } from 'react';
import { useLocation } from "react-router-dom";
import Header from './Header/Header'
import { Container } from 'react-bootstrap'
import { useAuth } from "../Components/AuthProvider/AuthProvider";
import CustomerNav from './Header/Components/CustomerNav/CustomerNav';
import AdminNav from './Header/Components/AdminNav/AdminNav';
import EmployeeNav from './Header/Components/EmployeeNav/EmployeeNav';

export default function Layout(props) {
    const location = useLocation();
    const { user, login } = useAuth();
    const [isTryAuth, setIsTryAuth] = useState(false);

    useEffect(() => {
        login();
        setIsTryAuth(true);
    }, [location]);

    if (isTryAuth) {
        let userNav;

        switch (user.role) {
            case "Customer":
                userNav = <CustomerNav />
                break;
            case "Employee":
                userNav = <EmployeeNav />
                break;
            case "Admin":
                userNav = <AdminNav />
                break;
            default:
                userNav = null;
        }

        return (
            <div className="h-100">
                <Header auth={user.auth}>
                    {userNav}
                </Header>
                <Container className="h-100 p-0" fluid={true}>
                    {props.children}
                </Container>
            </div>
        );
    }

    return null;
}
