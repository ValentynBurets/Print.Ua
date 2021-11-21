import React from 'react'
import TableElement from './TableElement_new'

function Tbody(props) {

    console.log(props.bodyData.incomingOrder?.products)

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

