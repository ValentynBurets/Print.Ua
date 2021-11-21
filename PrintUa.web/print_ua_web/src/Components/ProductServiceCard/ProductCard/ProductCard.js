import React from 'react'
import {
    Card,
    Button,
    Row
} from 'react-bootstrap'


import Coll from 'react-bootstrap/Col'
//import logo_Service from '../../Images/Service_Image.png' 
import './Style.sass'

export default function ProductCard(props) {
    let currentItem = props.item

    return (
        <Card 
            bg="" 
            border="success" 
            className='CardStyle ProductCard'
            >
            {props.showImage
                ? <Card.Img
                    style={{padding:"20px"}}
                    variant="top"
                    src={"data:image/jpg;base64," + currentItem.picture}
                    alt='image'
                    /> 
                : <div/>
            }
            
            <Card.Body>
                <Card.Title>{currentItem?.name}</Card.Title>
                <Card.Text>
                    {currentItem?.material?.materialName}
                </Card.Text>
                <Card.Text>
                    {currentItem?.format?.formatName}
                </Card.Text>
                <Card.Text>
                    {currentItem?.amount} pictures
                </Card.Text>
                <Card.Text>
                    {currentItem?.cost}$ per one
                </Card.Text>
                <Button variant="primary" 
                    className="ml-2 Button"
                    onClick={() => { props.setItem(currentItem) }} 
                >
                    {props.buttonText}
                </Button>
            </Card.Body>
        </Card>
    )
}
