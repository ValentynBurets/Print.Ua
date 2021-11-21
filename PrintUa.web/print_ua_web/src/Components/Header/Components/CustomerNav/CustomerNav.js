import TextData from "../../../../jsonData/English/Header.json";
import React from "react";
import { Nav } from 'react-bootstrap';
import { Link } from 'react-router-dom';

function CustomerNav() {
    return (
        <Nav>
            <Link to="/order_list" className="nav-link">{TextData.customer.orderListPageLink}</Link>
        </Nav>
    );
}

export default CustomerNav;