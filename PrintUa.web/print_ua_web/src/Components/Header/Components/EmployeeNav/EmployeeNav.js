import TextData from "../../../../jsonData/English/Header.json";
import React from "react";
import { Nav } from 'react-bootstrap';
import { Link } from 'react-router-dom';

function EmployeeNav() {
    return (
        <Nav>
            <Link to="/employee_order_page" className="nav-link">{TextData.employee.employeeOrderPageLink}</Link>
        </Nav>
    );
}

export default EmployeeNav;