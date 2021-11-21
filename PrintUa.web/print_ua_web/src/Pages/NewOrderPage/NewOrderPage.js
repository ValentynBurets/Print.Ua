import React, { useState, useEffect, useRef } from 'react'
import './Style.sass' 
import { Container, Col, Row, Button, Form } from 'react-bootstrap'
import { useHistory } from 'react-router-dom'
import UploadImage from './Components/UploadImage'
import TextData from '../../jsonData/English/NewOrderPage.json'
import saveOrder from './Services/SaveOrder'
import LoadServiceData from './Services/DataLoading'
import ProductDeck from '../../Components/ProductServiceCard/ProductDeck/ProductDeck'
import Loading from '../../Components/Loading/Loading'
import Overlay from 'react-bootstrap/Overlay'
import Tooltip from 'react-bootstrap/Tooltip'

function NewOrderPage(){
    let history = useHistory()

    const [data, saveData] = useState({
        isLoading: true,
        requests: null,
        inProgress: null,
    })

    const [showComment, setShowComment] = useState(false);
    const targetComment = useRef(null);

    const [productPrice, setProductPrice] = useState(0);
    const [orderPrice, setOrderPrice] = useState(0);
    const [services, setServices] = useState();
    
    //#region 
    //current product state

    const productInitialState = 
        {
            fileString: "",
            selectedService: null,
            comment: "",
            amount: 0
        }

    const [product, setProduct] = useState(productInitialState);
    
    function setProductPicture(fileString) {
        setProduct(prevState => ({ ...prevState, fileString }))
    }
    function setProductService(selectedService) {
        setProduct(prevState => ({ ...prevState, selectedService }))
    }
    //end current pruct state
    //#endregion 

    const [myProductArray, updateMyProductArray] = useState([]);
    const [myCommentArray, updateMyCommentArray] = useState([]);

    const commentTextArea = useRef(null);
    
    function addCommentHandleInputChange(e) {
        const value = commentTextArea.current.value
        //updateMyCommentArray( arr => [...arr, value]);
        updateMyCommentArray(value);
        //commentTextArea.current.value = "";
        setShowComment(!showComment);
    };

    const setCencelProduct = (product) =>{
        console.log(product)
        alert('delete product')
        updateMyProductArray(myProductArray.filter(item => item.id !== product.id))
    }

    useEffect(() => {
        LoadServiceData(setServices)
    }, [setServices])

    useEffect(()=>
        {
            if(services != null)
                (saveData({isLoading: false}))
        },
        [services]
    )

    const handleInputChange = (event) => {
        const { name, value } = event.target
        setProduct((prev)=>({ ...prev, [name]: value }));      
    }

    const RecalculateProductPrice = () => {
        if(product == null || product.selectedService == null ) return;
        let tempPrice = product.selectedService.cost * product.amount;
        setProductPrice(tempPrice);
        console.log(product)
        console.log(productPrice);
    }

    useEffect(RecalculateProductPrice, [product]);// eslint-disable-line react-hooks/exhaustive-deps

    const RecalculateOrderPrice = () => {
        if(myProductArray == null) return;
        
        let tempPrice = 0
        myProductArray.forEach(element => {
            tempPrice += element.amount*element.cost;    
        });
        setOrderPrice(tempPrice);

    }
    useEffect(RecalculateOrderPrice, [myProductArray]);// eslint-disable-line react-hooks/exhaustive-deps

    const addProduct = () => {
        updateMyProductArray( arr => [...arr, {
            id: "id" + Math.random().toString(16).slice(2),
            picture: product.fileString.toString(),
            serviceId: product.selectedService.id.toString(),
            name: product.selectedService.name,
            material: product.selectedService.material,
            format: product.selectedService.format,
            cost: product.selectedService.cost,
            amount: parseInt(product.amount)
        }]);
//      console.log(myProductArray);
    }

    useEffect(()=>{setProduct(productInitialState); setProductPrice(0)}, [myProductArray])

    const newOrder = () => {
        console.log(myProductArray);
        saveOrder(myProductArray, myCommentArray);
        history.push({
            pathname: '/order_list'
        })
    }
    
    return (
        <Container>
            {(data.isLoading) ? (
                <Loading/>
            ) : (
            <Container>

                <Row className='OrderHeader'>
                    <div>{TextData.NewOrder}</div>    
                </Row>

                <Row className="containerStyle">
                    <Col>
                        <UploadImage 
                            fileString = {product.fileString} 
                            setFileString={(value) => {setProductPicture(value)}}
                        />
                    </Col>
                    
                    <Col className="ServicesBox">
                        <Container>
                            <Row className='HeaderText'>
                                <div >{TextData.Services}</div>
                            </Row>
                            
                            <Row>
                                <ProductDeck
                                    items={services} 
                                    setItem={(value) => {setProductService(value)}}
                                    buttonText = {TextData.Select}
                                />
                            </Row>    
                            <Row className='AdditionalTextBlock'>
                                <Col className='AdditionalTextL'>
                                    <div>
                                        <label>{TextData.Amount}</label>
                                        <input 
                                           className="numberInputStyle"
                                            type="number" 
                                            id="amount" 
                                           name="amount" 
                                           min="1" max="1000"
                                           onChange={handleInputChange} 
                                        /> 
                                    </div>
                                </Col>
                                <Col className='AdditionalTextR'>
                                    <div className="TotalPrice">
                                        <label> {productPrice 
                                            ? TextData.TotalPrice + productPrice + '$' ?? 'price' 
                                            : TextData.SelectServiceAmount}
                                        </label>
                                    </div>
                                </Col>
                            </Row>
                        </Container>
                    </Col>
                </Row>

                <Row>
                    <Col>
                        <Button className='ProductButton' variant='primary' onClick={newOrder}>
                            {TextData.Create}
                        </Button>
                    </Col>
                    <Col>
                        <Button className='ProductButton' variant='warning' onClick={addProduct}>
                            {TextData.AddProduct}
                        </Button>

                        
                    </Col>
                </Row>
                <Row>
                <Col>
                <div className="containerStyle CommentBlock">
                    <label className="HeaderText CommentText" >{TextData.OptionalComment}</label>

                    <textarea className="Input InputAreaStyle" ref={commentTextArea}/>

                    <Button className='ProductButtonDown' ref={targetComment} variant='primary' 
                        onClick={addCommentHandleInputChange}>
                        {TextData.AddComment}
                    </Button>

                    <Overlay target={targetComment.current} show={showComment} placement="right">
                        {(props) => (
                           <Tooltip id="overlay-example" {...props}>
                            {TextData.CommentAdded}
                           </Tooltip>
                         )}
                    </Overlay>    
                </div>
                </Col>

                <Col >
                <div className="ProductListStyle">
                    <div className="HeaderText CommentText">{TextData.ProductList}</div>    
                    <ProductDeck
                        items = {myProductArray}
                        setItem = {(value) => {setCencelProduct(value)}}
                        buttonText = {TextData.Cansel} 
                        showImage = {true}
                    />

                    <div className='priceHeaderText'>
                        {orderPrice 
                            ? TextData.TotalPrice + orderPrice + '$' ?? 'price' 
                            : TextData.PleaseAddProduct}
                    </div>
                </div>
                </Col>
                </Row>
                
            </Container>
        )}
        </Container>
    )
}

export default NewOrderPage
