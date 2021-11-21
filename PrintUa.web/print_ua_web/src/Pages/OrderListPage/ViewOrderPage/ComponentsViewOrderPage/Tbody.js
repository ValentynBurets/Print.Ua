import React from 'react'
import TableElement from './TableElement'

function Tbody(props) {
    return (
        <tbody>
             {
                props.bodyData.incomingOrder?.products.map((product) => <TableElement 
                key={product.id}    
                incomingProduct={product}
                /> )
            }
        </tbody>
    )
}

export default Tbody

