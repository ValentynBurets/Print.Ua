import React from 'react'
import text from '../../../../jsonData/English/ViewOrderPage.json'

function TheaderListOrder() {
    
    return (
        <thead>
            <tr className="TableHeader">
                <th className="TableElement" id="LeftTableHeader">{text.Format}</th>
                <th className="TableElement">{text.Material}</th>
                <th className="TableElement">{text.Amount}</th>
                
                <th className="TableElement" id="RightTableHeader">{text.Cost}</th>
            </tr>
        </thead>
    )
}

export default TheaderListOrder
