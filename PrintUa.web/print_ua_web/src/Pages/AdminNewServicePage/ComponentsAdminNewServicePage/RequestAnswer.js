import React from 'react'
import Alert from 'react-bootstrap/Alert'
import text from '../../../jsonData/English/AdminNewServicePage.json'

function RequestAnswer(props) {
    return (
        <Alert show={props.isGetRequest} dismissible onClose={() => {props.setIsGetRequest(false)}} variant={props.request ? "success" : "danger"}>
            <Alert.Heading>
                {props.request ? text.Success : text.Error} 
            </Alert.Heading>
        </Alert >
    )
}

export default RequestAnswer
