import axios from "axios";

const GetCreationDataForServiceService = {
    request: async function (url) {
        return await axios({
            url: url + '/GetCreationData',
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

export default GetCreationDataForServiceService;