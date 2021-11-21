import {React} from 'react'
import text from '../../../../jsonData/English/ViewOrderPage.json'
import Button from 'react-bootstrap/Button'
import { Link } from 'react-router-dom'

function TableElement(props) {   
    function getCost(){
        return props.incomingProduct?.service.cost * props.incomingProduct?.amount;
    }
    
    return (
        <tr className="TableLine">
            <td className="TableElement">{props.incomingProduct?.service.format.name}</td>
            <td className="TableElement">{props.incomingProduct?.service.material.name}</td>
            <td className="TableElement">{props.incomingProduct?.amount}</td>
            
            <td className="TableElement">{getCost()} {text.Currency}</td>
        </tr>
    )
}

export default TableElement
