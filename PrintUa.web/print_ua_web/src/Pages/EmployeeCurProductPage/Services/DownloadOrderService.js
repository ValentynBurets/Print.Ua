import axios from "axios";

const OrderListServiceGetAllOrders = {
    request: async function (url,id) {
        return await axios({
            url: url + '/startWorking/'+id,
            method: "GET",
            headers:{
                "Authorization":"Bearer " + localStorage.getItem('token')
            }
        }).then((response) => {
            switch(response.status) {
            case 200: 
                return { data: response.data };
            default:
                
                return { data: null };
            }
        }).catch((error) => {
            return { data: null};
        });
    }

}

export default OrderListServiceGetAllOrders;