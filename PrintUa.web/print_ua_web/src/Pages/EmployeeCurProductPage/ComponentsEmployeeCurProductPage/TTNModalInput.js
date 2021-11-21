import {React, useState} from 'react'
import { Formik } from 'formik';
import * as Yup from 'yup';
import Modal from 'react-bootstrap/Modal'
import Form from 'react-bootstrap/Form'
import CloseButton from 'react-bootstrap/CloseButton'
import AddTTNService from '../Services/AddTTNService'
import connection from '../../../jsonData/ConnectionConfig.json'
import text from '../../../jsonData/English/EmployeeProductListPage.json'
import { Button} from 'react-bootstrap'

const validateTTN = /^\d{14}$/

const SignUpSchema = Yup.object().shape({
    TTN: Yup.string()
      .min(14, "Too Short TTN. Must be 14 numbers!")
      .max(14, "Too Long TTN. Must be 14 numbers!")
      .required("TTN is required")
      .matches(
        validateTTN,
        "Invalid TTN"
      ),
  });

function TTNModalInput(props) {



    const validateTTN = (newTTN) =>{
        AddTTNService.request(connection.ServerUrl + connection.Routes.Orders, props.id_product, newTTN).then(response => {
            if (response.data === null) {
              props.setBadRequest(true)
            }
            else {
                props.toggleShowAddTTN()
                props.setSendRequest(!props.sendRequest)
                //window.location.reload();
            }
        })
    }

    return (
        <Modal show={props.addTTNState} getOpenState={props.toggleShowAddTTN} tabIndex='-1'>               
            <Modal.Header className=''>
                <Modal.Title>{text.AddTTN}</Modal.Title>
                <CloseButton variant="black" onClick={props.toggleShowAddTTN}/>
            </Modal.Header>
            <Formik
                validationSchema={SignUpSchema}
                onSubmit={(values) => validateTTN(values.TTN)}
                initialValues={{
                    TTN:""
                }}
                >
                {({
                    handleSubmit,
                    handleChange,
                    values,
                    touched,
                    errors,
                }) => (
                    <Form noValidate onSubmit={handleSubmit}>
                        <Modal.Body>
                            <Form.Group className="mb-3">
                                <Form.Control 
                                    name="TTN"
                                    value={values.TTN}  
                                    required 
                                    onChange={handleChange}
                                    placeholder='TTN' 
                                    isInvalid={ touched.TTN && errors.TTN }
                                    />
                                    <Form.Control.Feedback type='invalid'>
                                        { errors.TTN }
                                    </Form.Control.Feedback>
                            </Form.Group>
                        </Modal.Body>
                        <Modal.Footer>
                            <Button type='submit' id="EditButton">True</Button>
                            <div className="vr" />
                            <Button variant='danger' id='DiscardButton' onClick={props.toggleShowAddTTN}>NOT</Button>
                        </Modal.Footer>    
                    </Form>   
                )}
            </Formik>    
        </Modal>
    )
}

export default TTNModalInput
