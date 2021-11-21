import "./ViewOrderPage.sass";
import {React, useState, useEffect } from 'react'
import 'bootstrap/dist/css/bootstrap.min.css'
import { Row, Button, CardGroup} from 'react-bootstrap'
import { useHistory } from 'react-router-dom'
import TextData from '../../../jsonData/English/NewOrderPage.json'
import GetOrder from './Services/LoadOrderInfo'
import ProductListBlock from './ComponentsViewOrderPage/ProductList';
import OrderInfoBlock from'./ComponentsViewOrderPage/OrderInfo';
import { useParams, BrowserRouter } from "react-router-dom";

function VeiwOrderPage(){

  const {id_order} = useParams();

  const [order, setOrder] = useState(null);

  useEffect(() => {
      GetOrder(id_order, setOrder)
    }, []);

    return(
      <Row className='Body'>
         <ProductListBlock
          incomingOrder= {order}/>
        <OrderInfoBlock
          incomingOrder= {order}
        /> 
      </Row> 
  )
}

export default VeiwOrderPage