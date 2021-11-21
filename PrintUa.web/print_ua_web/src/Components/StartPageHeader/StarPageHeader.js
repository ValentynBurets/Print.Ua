import resources from "../../jsonData/English/StartPage.json";
import React from "react";
import { Navbar } from 'react-bootstrap';
import { Link } from 'react-router-dom';

function StarPageHeader() {
    return (
        <Navbar bg="dark" variant="dark">
            <Navbar.Brand className="ms-5">
                <Link to="/" className="navbar-brand fs-3">{resources.Header}</Link>
            </Navbar.Brand>
        </Navbar>
    );
}

export default StarPageHeader;