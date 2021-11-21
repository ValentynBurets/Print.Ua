import {React, useEffect, useState} from "react";
import Table from 'react-bootstrap/Table'
import TheaderListOrder from './ComponentsEmployeeOrdersPage/TheaderListOrder'
import Tbody from './ComponentsEmployeeOrdersPage/Tbody'
import OrderListServiceGetAllOrders from './Services/OrderListServiceGetAllOrders'
import connection from '../../jsonData/ConnectionConfig.json'
import CloseButton from 'react-bootstrap/CloseButton'
import Modal from 'react-bootstrap/Modal'
import Button from 'react-bootstrap/Button'
import CancelOrderService from './Services/CancelOrderService'
import Row from 'react-bootstrap/Row'
import text from '../../jsonData/English/EmployeeProductListPage.json'
import { Container } from "react-bootstrap";
import BadRequest from "../../Components/BadRequest/BadRequest";
import errorText from '../../jsonData/English/BadRequest.json'



function EmployeeOrderPage() {

    const [numberOrder, setNumberOrder] = useState();

    const [cancelOrderState, setCancelOrderState] = useState(false);

    const toggleShow = (number) => {
        setCancelOrderState(!cancelOrderState);
        setNumberOrder(number)
    }

    const [badRequest, setBadRequest] = useState(false)

    const [isLoaded, setIsLoaded] = useState(false);

    const [shows, setShows] = useState();

    useEffect(() => {
        OrderListServiceGetAllOrders.request(connection.ServerUrl + connection.Routes.Orders).then(response => {
            if (response.data === null) {
                setBadRequest(true)
            }
            else {
                setShows(Object.values(response.data))
                setIsLoaded(true)
            }
        })
      }, []);

      const cancelOrder = () =>{
        CancelOrderService.request(connection.ServerUrl + connection.Routes.Orders, shows[numberOrder].id).then(response => {
            if (response.data === true) {
                window.location.reload();
            }
            else {
                setBadRequest(true)
                toggleShow()
            }
        })

    }

    return (
        <>
            <BadRequest show={badRequest} text={errorText.BadConnection}/>
            { !isLoaded ? null : 
                <Container style={{width:"70%"}}>
                    <Modal show={cancelOrderState} getOpenState={(e) => setCancelOrderState(e)} tabIndex='-1'>
                            <Modal.Header>
                                <Modal.Title>{text.CancelOrder}</Modal.Title>
                                <CloseButton variant="white" onClick={toggleShow}/>
                            </Modal.Header>

                            <Modal.Footer>
                                <Button variant='danger' onClick={cancelOrder}>Continue</Button>
                                <div className="vr" />
                                <Button variant='secondary' onClick={toggleShow}>
                                Cancel
                                </Button>
                            </Modal.Footer>
                    </Modal>
                    <Row>
                        <h1 className='text-center mt-3'>{text.OrderList}</h1>    
                    </Row>
                    <Row className="justify-content-md-center mx-auto mt-3 ListOfElem">
                        <Table responsive>
                            <TheaderListOrder/>
                            <Tbody 
                                bodyData={shows} 
                                cancelOrderState={cancelOrderState}
                                setCancelOrderState={setCancelOrderState}
                                toggleShow={toggleShow}
                                setCancelOrderState={setCancelOrderState}
                                cancelOrder={cancelOrderState}
                            />
                        </Table>
                    </Row>
                </Container>
            }  
        </>
    );
}

export default EmployeeOrderPage;