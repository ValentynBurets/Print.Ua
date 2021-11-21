import axios from 'axios'
import ConnectionConfig from '../../../jsonData/ConnectionConfig.json'

export default function GetOrders(setOrders){
    let token = localStorage.getItem('token')

    axios
        .get(`${ConnectionConfig.ServerUrl + ConnectionConfig.Routes.GetMyOrders}`, 
            {   
                headers: { 'Authorization': `Bearer ${token}` }
            }
        )
        .then((responce) => {
            var data = responce.data
            console.log(data)
            if (data != null) {
                setOrders(data)
                console.log(data)
            }
        })
        .catch((e) => {
            setOrders(null)
        })
}