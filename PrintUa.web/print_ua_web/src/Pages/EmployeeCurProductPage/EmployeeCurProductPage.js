import {React, useEffect, useState, useCallback} from "react";
import {useParams} from "react-router-dom";
import connection from '../../jsonData/ConnectionConfig.json'
import OrderListServiceGetAllOrders from './Services/OrderListServiceGetAllOrders';
import text from '../../jsonData/English/EmployeeProductListPage.json'
import Row from 'react-bootstrap/Row'
import BadRequest from "../../Components/BadRequest/BadRequest";
import errorText from '../../jsonData/English/BadRequest.json'
import ProductListBlock from './ComponentsEmployeeCurProductPage/ProductList'
import OrderInfoBlock from './ComponentsEmployeeCurProductPage/OrderInfo'

function EmployeeCurProductPage() {

    let { id_product } = useParams();

    const [isLoaded, setIsLoaded] = useState(false);

    const [badRequest, setBadRequest] = useState(false)

    const [data, setData] = useState()

    const [sendRequest, setSendRequest] = useState(true)

    useEffect(() => {
        OrderListServiceGetAllOrders.request(connection.ServerUrl + connection.Routes.Orders, id_product).then(response => {
            if (response.data === null) {
                setBadRequest(true)
            }
            else {
                setData(response.data)
                setIsLoaded(true)
            }
        })
      }, [sendRequest]);

        return (
            
            <div className="mx-auto text-center">
                <BadRequest show={badRequest} text={errorText.BadConnection}/>
                { 
                    !isLoaded ? null : <>                    
                        <Row>
                            <h1 className='text-center mt-3'>{text.OrderList}</h1>    
                        </Row>
                        <Row className="Body">
                            <ProductListBlock
                            incomingOrder= {data}/>
                            <OrderInfoBlock
                            setBadRequest={setBadRequest}
                            sendRequest={sendRequest}
                            setSendRequest={setSendRequest}
                            id_product={id_product}
                            incomingOrder= {data}
                            /> 
                        </Row>
                    </>
                }                
            </div>
        );
}

export default EmployeeCurProductPage;