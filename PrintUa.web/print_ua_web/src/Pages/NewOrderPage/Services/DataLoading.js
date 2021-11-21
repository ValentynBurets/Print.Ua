import ConnectionConfig from '../../../jsonData/ConnectionConfig.json'

import axios from 'axios'

const LoadServiceData = (setServices) => {  
    axios
        .get(`${ConnectionConfig.ServerUrl + ConnectionConfig.Routes.GetService}`)
        .then((responce) => {
            var data = responce.data
            //console.log(data)
            if (data != null) {
                setServices(data)
            }
        })
        .catch((e) => {
            setServices(null)
            console.log(e)
        }
    )
}

export default LoadServiceData
