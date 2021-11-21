import axios from "axios";

const ServiceEditCurrentService = {
    request: async function (url, id, new_service) {
        return await axios({
            url: url + '/Edit/'+id,
            method: "PUT",
            data: new_service,
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

export default ServiceEditCurrentService;