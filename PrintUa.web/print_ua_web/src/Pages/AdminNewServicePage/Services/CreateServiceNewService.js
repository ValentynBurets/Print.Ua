import axios from "axios";

const CreateServiceNewService = {
    request: async function (url, new_service) {
        return await axios({
            url: url + '/CreateService',
            method: "POST",
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

export default CreateServiceNewService;