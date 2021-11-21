import { Col, Container, Button} from 'react-bootstrap'
import TextData from '../../../jsonData/English/ViewOrderPage.json'
import Moment from 'moment';
import { React, useState, useCallback } from 'react';
import MyVerticallyCenteredModal from './ConfirmOrderRemoval';
import DownloadOrderService from '../Services/DownloadOrderService'
import fileDownload from 'js-file-download'
import connection from '../../../jsonData/ConnectionConfig.json'
import text from '../../../jsonData/English/EmployeeProductListPage.json'
import CancelOrderService from '../Services/CancelOrderService'
import { useHistory } from "react-router-dom";
import TTNModalInput from './TTNModalInput';

function OrderInfo(props){

  const [modalShow, setModalShow] = useState(false);

  const [addTTNState, setAddTTNState] = useState(false);

  const toggleShowAddTTN = () => {
      setAddTTNState(!addTTNState);
  }

  console.log("INCOMING ORDeR")
  console.log(props.incomingOrder)

  const currentHistory = useHistory();

  const cancelOrder = () =>{

    CancelOrderService.request(connection.ServerUrl + connection.Routes.Orders, props.id_product).then(response => {
        if (response.data === true) {
            currentHistory.push("/employee_order_page");
        }
        else {
            props.setBadRequest(true)
            setModalShow(false)
        }
    })
  }

  const memoizedDownloadOrderService = useCallback(() =>
  DownloadOrderService.request(connection.ServerUrl + connection.Routes.Orders, props.id_product).then(response => {
      if (response.data === null) {
        props.setBadRequest(true)
      }
      else {

          let info_order = Object.values(response.data).slice(0,6)

          let product = []

          Object.values(response.data)[7].map((elem)=>{

              var a = document.createElement("a");
              a.href = "data:image/jpg;base64," + elem.picture; //Image Base64 Goes here
              a.download = `${info_order[0]}_${elem.id}.jpg`; //File name Here
              a.click(); //Downloaded file
              a.remove()

              var curentProduct = []

              curentProduct.push(elem.id)
              curentProduct.push(elem.amount)
              curentProduct.push(elem.service)
              curentProduct.push(`${info_order[0]}_${elem.id}.jpg`)
              product.push(curentProduct)

          })

          info_order.push(product)

          fileDownload(new Blob([JSON.stringify(info_order)], {type: "application/json"}), 'out.json')
          props.setSendRequest(!props.sendRequest)
      }
  }),[])

  function getPrice(){
    let price = 0;
    props.incomingOrder?.products.forEach(product => {
      price += product.service.cost * product.amount;
    });
    return price;
  }

      return(
        <> 
        <TTNModalInput 
          setBadRequest={props.setBadRequest}
          addTTNState={addTTNState}
          setSendRequest={props.setSendRequest}
          sendRequest={props.sendRequest}
          toggleShowAddTTN={toggleShowAddTTN}
          id_product={props.id_product}
        />
        <Col xs={12} sm={12} md={4} lg={3} >
          <Container className='OrderInfo'>
          <div className="InfoBlock">
            <div className="InfoLine text-start">{text.OrderNumber}:</div>
            <div className="InfoLine text-start">{text.CreationDate}:</div>
            <div className="InfoLine text-start">{text.TTN}:</div>
            <div className="InfoLine text-start">{text.TotalPrice}:</div>
            <Button onClick={() => setModalShow(true)} className="Button" id="DiscardButton" variant="danger">{text.Discard}</Button> 
          </div>

          <div className="DataBlock">
            <div className="InfoLine text-start"> {props.incomingOrder?.orderNumber}</div>
            <div className="InfoLine text-start"> {Moment(props.incomingOrder?.creationDate).format('d MMM')}</div>
            <div className="InfoLine text-start"> {props.incomingOrder?.ttn}</div>
            <div className="InfoLine text-start"> {getPrice()} {TextData.Currency}</div>
            <Button className="Button" onClick={toggleShowAddTTN} id="EditButton">{text.AddTTN}</Button>
          </div>
          <Button className="Button" onClick={memoizedDownloadOrderService} disabled={props.incomingOrder?.state !== 'New' ? true : false}>{text.Download}</Button> 
          </Container>
          <MyVerticallyCenteredModal
            show={modalShow}
            onHide={() => setModalShow(false)}
            onAccept={cancelOrder}
          />
        </Col> 
        </>
      )
    
}

export default OrderInfo