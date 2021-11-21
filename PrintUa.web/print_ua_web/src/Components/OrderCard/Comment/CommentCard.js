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
import '../../Components/TemplateStyle.sass' 

export default function CommentCard(props) {

    const comment = props.comment

    // console.log(order)
    // return (
    //     <Card 
    //         bg="" 
    //         border="success" 
    //         className="templateStyle" 
    //     >
    //         <Row>
    //             <Col>
    //                 <Card.Body>
    //                     <Card.Title>
    //                         {props.CurrentState}
    //                     </Card.Title>
    //                 </Card.Body>
    //                 <Card.Body>
    //                     <Card.Title>
    //                         {props.CurrentState}
    //                     </Card.Title>
    //                 </Card.Body>
    //             </Col>
    //             <Col>
    //                 Comments
    //             </Col>
    //             <Coll>
    //                 <Row>
    //                     <Button 
    //                         variant="primary" 
    //                         className="buttonStyle" 
    //                         onClick={props.viewOrder}
    //                     >
    //                         {TextData.EditText}
    //                     </Button>
    //                 </Row>
    //             </Coll>
    //         </Row>
    //     </Card>
    // )
}