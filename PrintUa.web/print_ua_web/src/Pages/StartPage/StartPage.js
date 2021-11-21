import "./StartPage.sass";
import resources from "../../jsonData/English/StartPage.json";
import React from "react";
import { Container, Row, Col, Button } from "react-bootstrap";
import AuthorizeBox from "./Components/AuthorizeBox/AuthorizeBox";

function StartPage() {
    return (
        <Container fluid className="start-page">
            <Row>
                <Col md={{ offset: 1 }} className="present-col">
                    <Container>
                        <Row>
                            <Col xxl={12}>
                                <div className="head-title-text-box">
                                    <p>{resources.HeadBox.Title}</p>
                                </div>
                                <div className="head-text-box">
                                    <p>{resources.HeadBox.Text}</p>
                                </div>
                            </Col>
                        </Row>
                    </Container>
                </Col>
                <Col className="authorize-col">
                    <AuthorizeBox />
                </Col>
            </Row>
        </Container >
    );
}

export default StartPage;