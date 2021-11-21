import axios from 'axios'
import ConnectionConfig from '../../../../jsonData/ConnectionConfig.json'

export default function GetOrder(id, setOrder){
    let token = localStorage.getItem('token')

    axios
        .get(`${ConnectionConfig.ServerUrl + ConnectionConfig.Routes.GetOrder + id}`, 
            {   
                headers: { 'Authorization': `Bearer ${token}` }
            }
        )
        .then((responce) => {
            var data = responce.data
            console.log(data)
            if (data != null) {
                setOrder(data)
            }
        })
        .catch((e) => {
            setOrder(null)
        })
}