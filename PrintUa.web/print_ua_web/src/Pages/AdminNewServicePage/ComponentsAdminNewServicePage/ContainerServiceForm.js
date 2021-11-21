import React from 'react'
import RequestAnswer from './RequestAnswer'
import NewServiceForm from './NewServiceForm'
import Row from 'react-bootstrap/Row'

function ServiceForm(props) {
    return (
        <>
            <RequestAnswer
                isGetRequest={props.isGetRequest}
                setIsGetRequest={props.setIsGetRequest}
                request={props.request}
            />
            <Row>
                <NewServiceForm
                    isGetRequest={props.isGetRequest}
                    newService={props.newService}
                    setNewService={props.setNewService}
                    formatsAndMaterials={props.formatsAndMaterials}
                    createService={props.createService}
                    isCanceled = {props.newService.isCanceled}
                    changeStatusService={typeof(props.changeStatusService) !== 'undefined' ? props.changeStatusService : undefined}
                />
            </Row>
        </>
    )
}

export default ServiceForm
