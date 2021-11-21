import axios from "axios";

const AuthorizeService = {
    request: function (userData, url) {
        return axios({
            url: url,
            method: "POST",
            data: userData,
            headers: {
                "Content-type": "application/json; charset=UTF-8"
            }
        }).then((response) => {
            if (response.status === 202) {
                return { authorize: true, token: response.data.token, message: response.status };
            }
            else {
                return { authorize: false, token: "", message: response.status };
            }
        }).catch((error) => {
            return { authorize: false, token: "", message: error.toString() };
        });
    }
}

export default AuthorizeService;