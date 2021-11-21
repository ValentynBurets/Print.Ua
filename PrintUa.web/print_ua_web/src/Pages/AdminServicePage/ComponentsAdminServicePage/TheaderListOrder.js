import React from 'react'
import text from '../../../jsonData/English/AdminServiceListPage.json'

function TheaderListOrder() {
    return (
        <thead>
            <tr>
                <th>{text.Name}</th>
                <th>{text.Material}</th>
                <th>{text.Format}</th>
                <th>{text.Cost}</th>
                <th>{text.Status}</th>
                <th colSpan={2}></th>
            </tr>
        </thead>
    )
}

export default TheaderListOrder
