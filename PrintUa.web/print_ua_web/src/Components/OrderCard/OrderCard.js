import { React } from 'react'
import { useHistory } from 'react-router-dom'
import {
    Card,
    ListGroup,
    Button,
    Row,
    Col
} from 'react-bootstrap'
import PropTypes from 'prop-types'
import Coll from 'react-bootstrap/Col'
import OrderImageCarousel from "./OrderImageCarousel/OrderImageCarousel"
import TextData from '../../jsonData/English/OrderCard.json'

export default function OrderCard(props) {

    let history = useHistory()

    const viewOrder = () =>{
        history.push({
            pathname: `/view_order/${props.order.id}`
        })
    }
    
    const order = props.order
    const date = (order.creationDate.split('T'));
    const date_time = date[1].slice(0, 5)
    const date_day = date[0]

    let comment_date_time = "";
    let comment_date_day = "";

    if(order?.comments[0] != null)
    {
        const comment_date = (order?.comments[0]?.date.split('T'));
        comment_date_time = comment_date[1].slice(0, 5);
        comment_date_day = comment_date[0];
    }else
    {
        comment_date_time = "";
        comment_date_day = "";
    }
    

    return (
        <Card 
            bg="" 
            border="success" 
            className="cardTemplateStyle" 
            // onClick={viewOrder}
        >
            <Row>
                <Col className="PictureBlock">
                    <OrderImageCarousel
                        imgArray={ order?.imageArray}
                    />
                </Col>

                <Col className="d-flex align-items-center">
                    
                    <ListGroup variant="flush" className="InfoBlock">
                        <ListGroup.Item>№ {order.orderNumber}</ListGroup.Item>
                        <ListGroup.Item>{order.state}</ListGroup.Item>
                        <ListGroup.Item>{order.price} $</ListGroup.Item>
                        <ListGroup.Item>ttn: {order.ttn}</ListGroup.Item>
                    </ListGroup>
                    
                </Col>
                
                <Col >
                    <Card.Body>
                        <Card.Text className="ml-3">
                            
                        </Card.Text>
                        <Card.Text className="ml-3">
                            {order?.comments[0]?.subject}
                        </Card.Text>
                        <Card.Text className="ml-3 CommentText">
                            {order?.comments[0]?.text}
                        </Card.Text>
                    </Card.Body>
                </Col>

                <Coll className="ColBlock"> 
                    <Card.Text className="ml-3">
                        {date_day + " " + date_time}
                    </Card.Text>
                    {order.comments.date}
                    <Row>
                        <Button 
                            variant="primary" 
                            className="viewButton" 
                            onClick={viewOrder}
                        >
                            {TextData.EditText}
                        </Button>
                    </Row>
                </Coll>
            </Row>
        </Card>
    )
}

OrderCard.propTypes = {
    Picture: PropTypes.string,
    Date: PropTypes.string,
    Material: PropTypes.string,
    Format: PropTypes.string,
    Amount: PropTypes.number,
    Price: PropTypes.number
}


