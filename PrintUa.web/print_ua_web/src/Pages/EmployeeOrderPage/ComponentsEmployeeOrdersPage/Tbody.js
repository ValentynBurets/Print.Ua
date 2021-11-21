import React from 'react'
import TableElement from './TableElement'

function Tbody(props) {
    return (
        <tbody>
            {
                props.bodyData.map((elem,index) => <TableElement 
                                                        elementData={elem} 
                                                        key={index}
                                                        toggleShow={props.toggleShow}
                                                        index={index}
                                                    /> )
            }
        </tbody>
    )
}

export default Tbody

