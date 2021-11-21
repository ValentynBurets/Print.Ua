import {React, useEffect, useState} from 'react'
import CreateServiceNewService from './Services/CreateServiceNewService'
import GetCreationDataForServiceService from './Services/GetCreationDataForServiceService'
import connection from '../../jsonData/ConnectionConfig.json'
import ServiceForm from './ComponentsAdminNewServicePage/ContainerServiceForm'
import BadRequest from "../../Components/BadRequest/BadRequest";
import errorText from '../../jsonData/English/BadRequest.json'
import Container from 'react-bootstrap/Container'
import Row from 'react-bootstrap/Row'
import text from '../../jsonData/English/AdminNewServicePage.json'

function AdminNewServicePage() {

    const[newService, setNewService] = useState({
        name:"",
        cost:0.01,
        materialName:"",
        formatName:"",
        isCanceled:false
    })

    const [badRequest, setBadRequest] = useState(false)

    const setDefault = () =>{
        setNewService({
            name:"",
            cost:0.01,
            materialName:"",
            formatName:"",
            isCanceled:false
        })
    }

    const changeStatusService = () => {
        setNewService((prev) => {
            return { ...prev, isCanceled: !prev.isCanceled }
        })
    }

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
                setNewService(prev => { return { ...prev, materialName: Object.values(response.data)[0][0]??"", formatName: Object.values(response.data)[1][0]??""}; })
                setFormatsAndMaterials(Object.values(response.data))
                setIsLoad(true)
            }
        })
    },[isGetRequest])

    const createService = (values) => {
            CreateServiceNewService.request(connection.ServerUrl + connection.Routes.ProductService, values).then(response => {
                if (response.data === false) {
                    setRequest(response.data)
                }
                else {
                    setRequest(response.data)
                }
                setIsGetRequest(true)
            })
            
    }
    

    return (
        <>
            <BadRequest show={badRequest} text={errorText.BadConnection}/>
            { !isLoad ? null :<>
                <Row>
                    <h1 className='text-center mt-3 mb-3'>{text.AddNewService}</h1>    
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
                        setDefault={setDefault}
                    />
                </Container>
                </>
            }
        </>
        )
}

export default AdminNewServicePage
