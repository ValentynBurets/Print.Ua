import React from 'react'
import { Container } from 'react-bootstrap'
import ProductCard from '../ProductCard/ProductCard';
import './style.sass'

function ProductDeck(props){
    //console.log(props)
    return(
        <Container className="container-fluid py-2 ScrollableTable" >
            <Container className="d-flex flex-row flex-nowrap">
                {props.items?.map((items) => (
                    <ProductCard
                        key={items.id} 
                        item={items}
                        setItem={(value)=>{props.setItem(value)}}
                        buttonText={props.buttonText}
                        showImage={props?.showImage}/>
                ))}
            </Container>
        </Container>
    )
}

export default ProductDeck
