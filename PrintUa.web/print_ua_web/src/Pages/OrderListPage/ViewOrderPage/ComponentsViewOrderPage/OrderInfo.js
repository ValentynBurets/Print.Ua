import { Col, Container, Button, Card} from 'react-bootstrap'
import TextData from '../../../../jsonData/English/ViewOrderPage.json'
import Moment from 'moment';
import {React, useState, useCallback} from 'react'
import MyVerticallyCenteredModal from './ConfirmOrderRemoval';
import OrderCancel from './../Services/CancelOrder';
import connection from '../../../../jsonData/ConnectionConfig.json'
import { useHistory } from "react-router-dom";

function OrderInfo(props){

  const [modalShow, setModalShow] = useState(false);
  const currentHistory = useHistory();
  //const [OrderCancel, cancelOrder] = useState();

  function getPrice(){
    let price = 0;
    props.incomingOrder?.products.forEach(product => {
      price += product.service.cost * product.amount;
    });
    return price;
  }

  const _cancelOrder = () => {
    OrderCancel
    .request(connection.ServerUrl + connection.Routes.CancelOrder, props.incomingOrder?.id)
    .then(response => {
      if (response.data === null) {
        console.log("error");
      }
      else {
        console.log("deleted");
        currentHistory.push("/order_list");
          window.location.reload();
      }
    })
  }

    if(props.incomingOrder?.employee == null){

      return(
        <Col xs={11} sm={11} md={4} lg={3}>
        <Container className='OrderInfo'>
          <div className="InfoBlock">
            <div className="InfoLine">{TextData.State}:</div>
            <div className="InfoLine">{TextData.OrderNumber}:</div>
            <div className="InfoLine">{TextData.CreationDate}:</div>
            <div className="InfoLine">{TextData.TotalPrice}:</div>
          </div>

          <div className="DataBlock">
            <div className="InfoLine"> {props.incomingOrder?.state}</div>
            <div className="InfoLine"> {props.incomingOrder?.orderNumber}</div>
            <div className="InfoLine"> {Moment(props.incomingOrder?.creationDate).format('DD.MM.YYYY')}</div>
            <div className="InfoLine"> {getPrice()} {TextData.Currency}</div>
          </div>
          <Button onClick={() => setModalShow(true)} className="Button" id="DiscardButton" variant="danger">{TextData.Discard}</Button> 
          </Container>
          <MyVerticallyCenteredModal
            show={modalShow}
            onHide={() => setModalShow(false)}
            onAccept={_cancelOrder}
          />
        </Col> 
      )
    } else{

      return(
        <Col xs={12} sm={12} md={4} lg={3}  >
          <Container className='OrderInfo'>
          <div className="InfoBlock">
            <div className="InfoLine">{TextData.State}:</div>
            <div className="InfoLine">{TextData.OrderNumber}:</div>
            <div className="InfoLine">{TextData.CreationDate}:</div>
            <div className="InfoLine">{TextData.ttn}:</div>
            <div className="InfoLine">{TextData.TotalPrice}:</div>
            <div className="InfoLine">{TextData.EmployeeName}:</div>
          </div>

          <div className="DataBlock">
            <div className="InfoLine"> {props.incomingOrder?.state}</div>
            <div className="InfoLine"> {props.incomingOrder?.orderNumber}</div>
            <div className="InfoLine"> {Moment(props.incomingOrder?.creationDate).format('d MMM')}</div>
            <div className="InfoLine"> {props.incomingOrder?.ttn}</div>
            <div className="InfoLine"> {getPrice()} {TextData.Currency}</div>
            <div className="InfoLine"> {props.incomingOrder?.employee.surname} {props.incomingOrder?.employee.name}</div>
          </div>
          <Button onClick={() => setModalShow(true)} className="Button" id="DiscardButton" variant="danger">{TextData.Discard}</Button> 
          </Container>
          <MyVerticallyCenteredModal
            show={modalShow}
            onHide={() => setModalShow(false)}
            onAccept={_cancelOrder}
          />
        </Col> 
      )
    }
    
}

export default OrderInfo