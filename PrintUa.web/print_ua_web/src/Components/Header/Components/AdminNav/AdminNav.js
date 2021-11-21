import TextData from "../../../../jsonData/English/Header.json";
import React from "react";
import { Nav } from 'react-bootstrap';
import { Link } from 'react-router-dom';

function AdminNav() {
    return (
        <Nav>
            <Link to="/user_list" className="nav-link">{TextData.admin.userListPageLink}</Link>
            <Link to="/admin/service" className="nav-link">{TextData.admin.serviceListPageLink}</Link>
        </Nav>
    );
}

export default AdminNav;