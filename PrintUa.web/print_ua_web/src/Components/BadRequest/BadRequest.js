import React from 'react'
import Alert from 'react-bootstrap/Alert'

function BadRequest({show, text}) {
    return (
        <Alert show={show} variant="danger">
            <Alert.Heading className="text-center">
                {text} 
            </Alert.Heading>
        </Alert >
    )
}

export default BadRequest
