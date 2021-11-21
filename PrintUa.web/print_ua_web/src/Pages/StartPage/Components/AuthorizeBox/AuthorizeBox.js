import "./AuthorizeBox.sass";
import resources from "../../../../jsonData/English/StartPage.json";
import React from "react";
import { Container, Tabs, Tab } from "react-bootstrap";
import LoginForm from "./Components/LoginForm/LoginForm";
import RegisterForm from "./Components/RegisterForm/RegisterForm";

function AuthorizeBox() {
    return (
        <Container className="authorize-box">
            <p className="fw-bolder fs-4">{resources.AuthorizeBox.Title}</p>
            <Tabs defaultActiveKey="loginTab">
                <Tab className="p-3 pb-2" eventKey="loginTab" title={resources.AuthorizeBox.LoginTab.Title}>
                    <LoginForm />
                </Tab>
                <Tab className="p-3 pb-2" eventKey="registerTab" title={resources.AuthorizeBox.RegisterTab.Title}>
                    <RegisterForm />
                </Tab>
            </Tabs>
        </Container>
    );
}

export default AuthorizeBox;