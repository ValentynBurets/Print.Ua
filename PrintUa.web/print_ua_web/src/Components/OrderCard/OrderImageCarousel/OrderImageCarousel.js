import {React, useState } from 'react'
import Carousel from "react-bootstrap/Carousel"
import Image from "react-bootstrap/Image"
import './Style.sass' 
import picture from '../../Images/picture.png'

function OrderImageCarousel(props){
    const [index,setIndex]=useState(0);
    const handleSelect = (selectedIndex, e) => {
        setIndex(selectedIndex);
    };


    //console.log(props.imgArray)
    return (
        <Carousel 
            className="carouselStyle" 
            variant={"dark"} 
            indicators={true}   
            interval={3000} 
            slide={true} 
            touch={true} 
            activeIndex={index} 
            onSelect={handleSelect} 
        >
            {props.imgArray.map((img, i)=>{
                // if(img.picture == ''){
                //     img.picture = split(picture)
                // }
                    
                return(
            <Carousel.Item key={i} >
                <Image  
                    key={img.id} 
                    fluid={true} 
                    className="CardImageStyle"
                    //className={"img-responsive center-block w-100"}  
                    src={"data:image/jpg;base64," + img.picture}
                    alt='picture'
                />
            </Carousel.Item>)})}
        </Carousel>
    );
}

// const split = (string) =>{
//     const words = string.split('base64');
//     //console.log(words[1]);
//     return words[1].substring(1);
// }

export default OrderImageCarousel 