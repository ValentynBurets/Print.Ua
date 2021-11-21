import {React, useEffect, useState} from 'react'
import Form from 'react-bootstrap/Form'
import Row from 'react-bootstrap/Row'
import Col from 'react-bootstrap/Col'
import text from '../../../jsonData/English/AdminNewServicePage.json'
import { Formik } from 'formik';
import * as Yup from 'yup';
import Button from 'react-bootstrap/Button'
import { Link } from 'react-router-dom'
import '../AdminNewServicePage.css'

const SignUpSchema = Yup.object().shape({
    name: Yup.string()
    .min(1, "Too Short name of Service.")
    .max(40, "Too Large name of Service.")
    .required("Name of Service is required"),
    formatName: Yup.string()
    .min(1, "Too Short name of Format.")
    .max(40, "Too name of Format.")
    .required("Name of Format is required"),
    materialName: Yup.string()
    .min(1, "Too Short name of Material.")
    .max(40, "Too name of Material.")
    .required("Name of Material is required"),
    cost: Yup.number()
    .min(0.01, "Cost must be greater then 0.")
    .required("Cost is required"),
  
  });

function NewServiceForm(props) {

    const [isFormatInput, setIsFormatInput] = useState(false)
    const [isMaterialName, setIsMaterialName] = useState(false)



    const [materialBuffer, setMaterialBuffer] = useState(props.newService.materialName)
    const [formatBuffer, setFormatBuffer] = useState(props.newService.formatName)

    useEffect(()=>{
        setMaterialBuffer(props.newService.materialName)
        setFormatBuffer(props.newService.formatName)
    },[props.isGetRequest])

    return (

        <Formik
                validationSchema={SignUpSchema}
                onSubmit={(values) => props.createService(values)}
                initialValues={{
                    name: props.newService.name,
                    formatName: props.newService.formatName,
                    materialName: props.newService.materialName,
                    cost: props.newService.cost,
                    isCanceled: props.newService.isCanceled
                }}
                >
                {({
                    handleSubmit,
                    handleChange,
                    values,
                    touched,
                    errors,
                }) => (

                <Form noValidate onSubmit={handleSubmit} className="mt-3 mb-3">
                    {console.log(values.isCanceled)}
            <Form.Group as={Row} className="mb-3">
                <Col className='text-end align-self-center' md={"3"}>
                    <Form.Label>{text.ServiceName}</Form.Label>
                </Col>
                <Col md={"4"}>
                    <Form.Control 
                        name="name"
                        placeholder="Name of service" 
                        className='text-center' 
                        value={values.name} 
                        onChange={handleChange}
                        disabled={ values.isCanceled }
                        isInvalid={ errors.name && touched.name }
                        />
                        
                </Col>
                <Col md={"4"}><Form.Control.Feedback type="invalid">
                                        { errors.name }
                                    </Form.Control.Feedback></Col>
            </Form.Group>
            <Form.Group className="mb-3" as={Row}>
                <Col className='text-end align-self-center' md={"3"}>
                    <Form.Label>{text.Format}</Form.Label>
                </Col>
                <Col md={"4"}>
                    <Form.Select 
                        aria-label="Check type" 
                        className='text-center' 
                        value={values.formatName}
                        name="formatName"
                        style={{display: isFormatInput ? 'none' : 'inline'}}
                        disabled={ values.isCanceled }
                        onChange={handleChange}
                        >
                            {props.formatsAndMaterials[1].map((elem) => {return <option value={elem}>{elem}</option>})}
                    </Form.Select>
                    <Form.Control 
                        className='text-center' 
                        name="formatName"
                        value={values.formatName}
                        style={{display: !isFormatInput ? 'none' : 'inline'}}
                        disabled={values.isCanceled}
                        isInvalid={ errors.formatName && touched.formatName }
                        onChange={handleChange}
                        />
                </Col>
                <Col md={"auto"} className='align-self-center'>
                <Form.Check
                    disabled={values.isCanceled}
                    value={isFormatInput} 
                    label={"Input new Format"}
                    onChange={()=>{
                        let tmp =  values.formatName
                        values.formatName = formatBuffer 
                        setFormatBuffer(tmp)
                        setIsFormatInput(!isFormatInput)
                    }}/>
                    <Form.Control.Feedback type="invalid">{ errors.formatName }</Form.Control.Feedback>
                </Col>
            </Form.Group>
            
            <Form.Group className="mb-3" as={Row}>
                <Col className='text-end align-self-center' md={"3"}>
                    <Form.Label>{text.Material}</Form.Label>
                </Col>
                <Col md={"4"}>
                    <Form.Select 
                        aria-label="Check type" 
                        className='text-center' 
                        value={values.materialName} 
                        onChange={handleChange}
                        name="materialName"
                        style={{display: isMaterialName ? 'none' : 'inline'}}
                        disabled={values.isCanceled}
                        >
                    {props.formatsAndMaterials[0].map((elem) => {return <option value={elem}>{elem}</option>})}
                    </Form.Select>
                    <Form.Control 
                        className='text-center' 
                        style={{display: !isMaterialName ? 'none' : 'inline'}}
                        value={values.materialName} 
                        name="materialName"
                        isInvalid={ errors.materialName && touched.materialName }
                        disabled={values.isCanceled}
                        onChange={handleChange}
                            />
                </Col>
                <Col md={"auto"} className='align-self-center'>
                <Form.Check
                    value={isMaterialName} 
                    disabled={values.isCanceled}
                    label="Input new Material"
                    onChange={()=>{
                        let tmp =  values.materialName
                        values.materialName = materialBuffer 
                        setMaterialBuffer(tmp)
                        setIsMaterialName(!isMaterialName)
                    }
                    }/>
                    <Form.Control.Feedback type="invalid">{ errors.materialName }</Form.Control.Feedback>
                </Col>
            </Form.Group>
            
            <Form.Group as={Row}>
                <Col md={"3"} className='text-end align-self-center'>
                    <Form.Label>{text.Cost}</Form.Label>
                </Col>
                <Col md={"4"} >
                    <Form.Control 
                        type="number" 
                        className='text-center' 
                        min="0.01" 
                        step="0.01"
                        value={values.cost} 
                        name="cost"
                        disabled={values.isCanceled}
                        isInvalid={ errors.cost && touched.cost }
                        onChange={handleChange}/>
                </Col>
            </Form.Group>
            <Form.Group>
                <Row className='mb-3 mt-3 justify-content-center'>
                    <Col md={"3"} className=''>
                        <div class="d-grid gap-1 ">
                            <Button className="EditButton" type='submit' onClick={() => console.log(values)}>{text.Save}</Button>
                        </div>
                    </Col>
                
                {
                    typeof(props.changeStatusService) !== 'undefined' ? <Col md={"3"} className='align-self-center'>
                    <div class="d-grid gap-1 justify-content-center">
                        <Form.Check type="checkbox" className='fs-5' label={text.EnableService} name='isCanceled' checked={values.isCanceled} onChange={handleChange}/>
                    </div>
                </Col> : null
                }

                    <Col md={"3"} className=''>
                        <Link to={`/admin/service`} className="d-grid text-decoration-none">
                            <div class="d-grid gap-1">
                                <Button className="DiscardButton">{text.Cancel}</Button>
                            </div>
                        </Link>
                    </Col>
                </Row>
            </Form.Group>
        </Form>
                )}
            </Formik>    

        
    )
}

export default NewServiceForm
