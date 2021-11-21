import React from 'react'
import text from '../../../jsonData/English/EmployeeOrderListPage.json'

function TheaderListOrder() {
    return (
        <thead>
            <tr className='text-center'>
                <th>{text["№"]}</th>
                <th>{text.Date}</th>
                <th>{text.State}</th>
                <th>{text.TTN}</th>
                <th></th>
                <th></th>
                <th></th>
            </tr>
        </thead>
    )
}

export default TheaderListOrder
