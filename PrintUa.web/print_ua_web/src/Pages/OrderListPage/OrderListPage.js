import "./OrderListPage.sass";
import {React, useState, useEffect } from 'react'
import 'bootstrap/dist/css/bootstrap.min.css'
import { Container, Button, Row} from 'react-bootstrap'
import { useHistory } from 'react-router-dom'
import OrdersDeck from '../../Components/OrderCard/OrdersDeck'
import GetOrders from './Services/LoadOrdersService'
import TextData from '../../jsonData/English/OrderList.json'
import Loading from '../../Components/Loading/Loading'
import "./Style.sass"


function OrderListPage(){

    const [data, saveData] = useState({
        isLoading: true,
        requests: null,
        inProgress: null,
    })

    const [orders, setOrders] = useState(null)

    let history = useHistory()

    const newOrder = () => {
        history.push({
            pathname: '/new_order'
        })
    }

    useEffect(() => {
        GetOrders(setOrders)
        
    }, [setOrders])

    useEffect(() => 
        {
            if(orders != null)
                saveData({isLoading: false})
        }, [orders]
    );

    return (
        <Container>
            {(data.isLoading) ? (
                <Loading/>
            ):(
                <Container>
                 <div className='OrderHeader'>
                    <div>My orders</div>
                    
                 </div> 
                <OrdersDeck orders={orders}/>   
                
            </Container>
            )}
            <Button variant='primary' className='Button' id='ButtonNewOrder' onClick={newOrder}>Create new</Button>
        </Container>
    )
}
export default OrderListPage
