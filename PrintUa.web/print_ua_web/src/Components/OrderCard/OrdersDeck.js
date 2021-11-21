import { Container, Button} from 'react-bootstrap'
import OrderCard from './OrderCard'

export default function OrdersDeck(props){
    return(
        <Container className='OrderList' >
            {props.orders?.map((order) => (
                <OrderCard 
                    key={order.id} 
                    order={order}/>
            ))}
        </Container>
    )   
}


