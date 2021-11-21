import {React} from 'react'
import { Link } from 'react-router-dom'
import '../AdminServicePage.css'
import text from '../../../jsonData/English/AdminServiceListPage.json'


function TableElement(props) {    

    return (
        <tr className='align-middle'>
            <td>{props.elementData.name}</td>
            <td>{props.elementData.material.materialName}</td>
            <td>{props.elementData.format.formatName}</td>
            <td>{props.elementData.cost}</td>
            <td>{props.elementData.isCanceled ? text.Disable : text.Active }</td>
            <td><div class="col-md-6"><Link to={`/admin/service/${props.elementData.id}`}><i class="bi fs-2 bi-pencil-square link"></i></Link></div></td>
            <td><div class="col-md-6"><i class="bi bi-trash-fill fs-2 link" onClick={()=>props.toggleShowWithNumber(props.index)}></i></div></td>
        </tr>
    )
}

export default TableElement
