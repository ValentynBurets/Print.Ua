import {React, useEffect, useState} from "react";
import Table from 'react-bootstrap/Table'
import Button from 'react-bootstrap/Button'
import Modal from 'react-bootstrap/Modal'
import TheaderListOrder from './ComponentsAdminServicePage/TheaderListOrder'
import Tbody from './ComponentsAdminServicePage/Tbody'
import OrderListServiceGetAllOrders from './Services/OrderListServiceGetAllOrders'
import connection from '../../jsonData/ConnectionConfig.json'
import DeleteOrderService from './Services/DeleteOrderService'
import text from '../../jsonData/English/AdminServiceListPage.json'
import BadRequest from "../../Components/BadRequest/BadRequest";
import errorText from '../../jsonData/English/BadRequest.json'
import { Container } from "react-bootstrap";
import Row from 'react-bootstrap/Row'
import { Link } from 'react-router-dom'
import Col from 'react-bootstrap/Col'
import './AdminServicePage.css'

function AdminServicePage() {

    const [numberService, setNumberService] = useState("");

    const [badRequest, setBadRequest] = useState(false)

    const [cancelServiceState, setCancelServiceState] = useState(false);

    const toggleShow = () => {
        setCancelServiceState(!cancelServiceState);
    }

    const toggleShowWithNumber = (number) => {
        setCancelServiceState(!cancelServiceState);
        setNumberService(number)
    }

    const [isLoaded, setIsLoaded] = useState(false);

    const [shows, setShows] = useState();

    useEffect(() => {
        OrderListServiceGetAllOrders.request(connection.ServerUrl + connection.Routes.ProductService).then(response => {
            if (response.data === null) {
                setBadRequest(true)
            }
            else {
                setShows(Object.values(response.data))
                setIsLoaded(true)
            }
        })
      }, []);

      const deleteService = () =>{
        DeleteOrderService.request(connection.ServerUrl + connection.Routes.ProductService, shows[numberService].id).then(response => {
            if (response.data === true) {
                toggleShow()
                window.location.reload();
            }
            else {
                setBadRequest(true)
            }
        })

    }

    return(
            <>
                <BadRequest show={badRequest} text={errorText.BadConnection}/>
                { !isLoaded ? null : <>
                        <Modal show={cancelServiceState} getOpenState={(e) => setCancelServiceState(e)} tabIndex='-1'>
                            <Modal.Header>
                                <Modal.Title className=""><div className="grid align-self-center">{text.AreYouShure}</div></Modal.Title>
                                <Button className='btn-close' color='none' onClick={toggleShow}></Button>
                            </Modal.Header>

                            <Modal.Body>{text.Warning}</Modal.Body>
                            <Modal.Footer>
                                <Button variant="danger" onClick={deleteService}>Continue</Button>
                                <Button variant="secondary" onClick={toggleShow}>
                                Cancel
                                </Button>
                            </Modal.Footer>
                        </Modal>
                        <Row>
                            <h1 className='text-center mt-3'>{text.ServiceList}</h1>    
                        </Row>
                        <Container style={{width:"60%"}} className="mt-3 ListOfElem">
                            <Table responsive style={{ textAlign: "center"}}>
                                <TheaderListOrder/>
                                <Tbody 
                                    bodyData={shows} 
                                    toggleShowWithNumber={toggleShowWithNumber}
                                />
                            </Table>
                            <Row className="justify-content-center">
                                <Col md="4 text-center">
                                    <Link to={`/admin/new_service/`}><i className="bi fs-1 bi-plus-square-fill link"></i></Link>
                                </Col>
                            </Row>
                        </Container>
                    </>
                }
            </>
        
    )
}

export default AdminServicePage;