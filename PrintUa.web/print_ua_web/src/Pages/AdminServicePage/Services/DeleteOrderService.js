import axios from "axios";

const DeleteOrderService = {
    request: async function (url,id) {
        return await axios({
            url: url + '/DisableService/'+id,
            method: "POST",
            headers:{
                "Authorization":"Bearer " + localStorage.getItem('token')
            }
        }).then((response) => {
            switch(response.status) {
            case 200: 
                return { data: true };
            default: 
                return { data: false };
            }
        }).catch((error) => {
            return { data: false};
        });
    }

}

export default DeleteOrderService;