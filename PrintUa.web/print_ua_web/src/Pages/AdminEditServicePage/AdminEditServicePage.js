import {React, useEffect, useState} from 'react'
import ServiceEditCurrentService from './Services/ServiceEditCurrentService'
import GetCreationDataForServiceService from './Services/GetCreationDataForServiceService'
import connection from '../../jsonData/ConnectionConfig.json'
import ServiceGetCurService from './Services/ServiceGetCurService'
import {useParams} from "react-router-dom";
import ServiceForm from '../AdminNewServicePage/ComponentsAdminNewServicePage/ContainerServiceForm'
import BadRequest from "../../Components/BadRequest/BadRequest";
import errorText from '../../jsonData/English/BadRequest.json'
import Container from 'react-bootstrap/Container'
import Row from 'react-bootstrap/Row'
import text from '../../jsonData/English/AdminNewServicePage.json'

function AdminEditServicePage() {

    let { id_service } = useParams();

    const[newService, setNewService] = useState({
        name:"",
        cost:0.01,
        materialName:"",
        formatName:"",
        isCanceled:false
    })

    const[defaultValue, setDefaultValue] = useState({
        name:"",
        cost:0.01,
        materialName:"",
        formatName:"",
        isCanceled:false
    })

    const changeStatusService = () => {
        setNewService((prev) => {
            return { ...prev, isCanceled: !prev.isCanceled }
        })
    }

    const setDefault = () =>{
        setNewService(defaultValue)
    }

    const [badRequest, setBadRequest] = useState(false)

    const[isGetRequest, setIsGetRequest] = useState(false)

    const[request, setRequest] = useState(false)

    const [formatsAndMaterials, setFormatsAndMaterials] = useState()
    const [isLoad, setIsLoad] = useState(false)

    useEffect(()=>{
        GetCreationDataForServiceService.request(connection.ServerUrl + connection.Routes.ProductService).then(response => {
            if (response.data === null) {
                setBadRequest(true)
            }
            else {
                setFormatsAndMaterials(Object.values(response.data))
                ServiceGetCurService.request(connection.ServerUrl + connection.Routes.ProductService, id_service).then(response => {
                    if (response.data === null) {
                    }
                    else {
                        setNewService(prev => { return { 
                            ...prev, 
                            name: Object.values(response.data)[1], 
                            cost: Object.values(response.data)[2], 
                            materialName: response.data.material.materialName, 
                            formatName: response.data.format.formatName, 
                            isCanceled: response.data.isCanceled}; })
                        setIsLoad(true)
                    }
                })
            }
        })
    },[isGetRequest])

    const createService = (value) => {
            ServiceEditCurrentService.request(connection.ServerUrl + connection.Routes.ProductService, id_service, value).then(response => {
                if (response.data === false) {
                    setRequest(response.data)
                }
                else {
                    setRequest(response.data)
                    ServiceGetCurService.request(connection.ServerUrl + connection.Routes.ProductService, id_service).then(response => {
                        if (response.data === null) {
                        }
                        else {
                            setNewService(prev => { return { 
                                ...prev, 
                                name: Object.values(response.data)[1], 
                                cost: Object.values(response.data)[2], 
                                materialName: response.data.material.materialName, 
                                formatName: response.data.format.formatName,
                                isCanceled: response.data.isCanceled}; 
                            })
                            setIsLoad(true)
                        }
                    })
                }
                
                setIsGetRequest(true)
            })
    }

    

    return (
        <>
            <BadRequest show={badRequest} text={errorText.BadConnection}/>
            { !isLoad ? null :<>
                <Row>
                    <h1 className='text-center mt-3'>{text.AddEditService}</h1>    
                </Row>
                <Container className="ListOfElem mt-3" style={{width:"50%"}}>
                    <ServiceForm
                        isGetRequest={isGetRequest}
                        setIsGetRequest={setIsGetRequest}
                        request={request}
                        newService={newService}
                        setNewService={setNewService}
                        formatsAndMaterials={formatsAndMaterials}
                        createService={createService}
                        changeStatusService={changeStatusService}
                    />
                </Container>
                </>
            }
        </>
        )
}

export default AdminEditServicePage