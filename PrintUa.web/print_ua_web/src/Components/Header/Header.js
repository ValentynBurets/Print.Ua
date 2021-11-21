import resources from "../../jsonData/English/StartPage.json";
import "./Header.sass";
import React from "react";
import { Navbar, Nav, Button, Row } from 'react-bootstrap';
import { useAuth } from "../AuthProvider/AuthProvider";
import { Link } from "react-router-dom";

function Header({ children, auth }) {
    const { logout } = useAuth();

    return (
        <Navbar className="bar-container" bg="dark" variant="dark">
            <Navbar.Brand className="ms-5">
                {resources.Header}
            </Navbar.Brand>
            <Nav className="ms-3">
                {children}
            </Nav>
            <Navbar.Collapse className="justify-content-end me-5" style={{ visibility: auth ? "visible" : "hidden" }}>
                <Nav>
                    <Link to="/profile" className="nav-link me-4">Profile</Link>
                </Nav>
                <Button size="sm" variant="outline-danger" onClick={() => logout()}>Log out</Button>
            </Navbar.Collapse>
        </Navbar>
    );
}

export default Header;