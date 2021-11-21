import {React} from 'react'
import text from '../../../jsonData/English/EmployeeOrderListPage.json'
import Button from 'react-bootstrap/Button'
import { Link } from 'react-router-dom'
import '../EmployeeOrderPage.css'
import { useHistory } from "react-router-dom";

function TableElement(props) {    
    const currentHistory = useHistory();

    return (
        <tr className='align-middle text-center'>
            <td>{props.elementData.orderNumber}</td>
            <td>{props.elementData.creationDate.substring(0,props.elementData.creationDate.indexOf('.')).replace('T',' ')}</td>
            <td>{props.elementData.isCanceled ? "Canceled":props.elementData.state}</td>
            <td>{props.elementData.ttn}</td>
            <td><Button 
            onClick={() => currentHistory.push(`/cur_product/${props.elementData.id}`)}
            variant="dark" 
            disabled={props.elementData.isCanceled ? true : false}
            >{text.StartWorking}</Button></td>
            <td onClick={() => !props.elementData.isCanceled ? props.toggleShow(props.index) : null}><i className="bi bi-trash fs-1 closeButton"></i></td>
        </tr>
    )
}

export default TableElement
