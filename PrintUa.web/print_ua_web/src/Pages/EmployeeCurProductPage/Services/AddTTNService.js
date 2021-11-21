import axios from "axios";

const AddTTNService = {
    request: async function (url,id,newTTNstr) {
        return await axios({
            url: url + `/edit/${id}/ttn/${newTTNstr}/`,
            method: "PUT",
            headers:{
                "Authorization":"Bearer " + localStorage.getItem('token')
            }
        }).then((response) => {
            switch(response.status) {
            case 202: 
                return { data: true };
            default: 
                return { data: false };
            }
        }).catch((error) => {
            return { data: false};
        });
    }

}

export default AddTTNService;