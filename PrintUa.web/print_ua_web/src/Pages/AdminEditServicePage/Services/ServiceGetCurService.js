import axios from "axios";

const ServiceGetCurService = {
    request: async function (url, id) {
        return await axios({
            url: url + '/GetById/'+id,
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

export default ServiceGetCurService;