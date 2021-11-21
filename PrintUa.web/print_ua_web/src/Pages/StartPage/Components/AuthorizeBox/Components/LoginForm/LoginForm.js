import "../../AuthorizeBox.sass";
import resources from "../../../../../../jsonData/English/StartPage.json";
import connection from "../../../../../../jsonData/ConnectionConfig.json";
import { React, useState } from "react";
import { useHistory } from "react-router-dom";
import { Form, Button, InputGroup } from "react-bootstrap";
import AuthorizeService from "../../../../Services/AuthorizeService";
import { useAuth } from "../../../../../../Components/AuthProvider/AuthProvider";

export default function LoginForm() {

    const currentHistory = useHistory();
    const { login } = useAuth();

    const [formState, setFormState] = useState({
        email: "",
        password: "",
        passwordVisible: false,
        validated: false,
        authorizedState: false,
        authorizedMessage: "",
        errorMessage: ""
    });

    const passwordValidPattern = "(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[*.!@$%^&(){}:;<>,.?~_+-=|]).{10,25}$";

    function handleSubmit(event) {
        event.preventDefault();

        setFormState(prev => { return { ...prev, validated: true } });

        const isFormValid = event.currentTarget.checkValidity();

        if (isFormValid) {
            const userData = {
                email: formState.email,
                password: formState.password
            }

            AuthorizeService.request(userData, connection.ServerUrl + connection.Routes.Login).then(response => {
                if (response.authorize === true) {
                    // Set jwt token
                    localStorage.setItem("token", response.token);

                    let receivedUser = login();

                    switch (receivedUser.role) {
                        case "Customer":
                            currentHistory.push("/new_order");
                            break;
                        case "Employee":
                            currentHistory.push("/employee_order_page");
                            break;
                        case "Admin":
                            currentHistory.push("/user_list");
                            break;
                    }
                }
                else {
                    let error = resources.AuthorizeBox.LoginTab.ErrorText.IncorrectData;

                    setFormState(prev => {
                        return {
                            ...prev,
                            authorizedState: response.authorize,
                            authorizedMessage: response.message,
                            errorMessage: error
                        }
                    });
                }
            });
        }
    }

    return (
        <Form noValidate validated={formState.validated} onSubmit={handleSubmit}>
            {/* Display this block if errorMessage not empty */}
            <div className="error-message-box" style={{ visibility: Boolean(formState.errorMessage) ? "visible" : "hidden" }}>
                {formState.errorMessage}</div>

            <Form.Group className="mb-3" controlId="userEmail">
                <Form.Label>{resources.AuthorizeBox.LoginTab.Labels.Email}</Form.Label>
                <InputGroup>
                    <Form.Control value={formState.email} onChange={(event) => setFormState(prev => { return { ...prev, email: event.target.value }; })}
                        className="ig-form-control" type="email" placeholder={resources.AuthorizeBox.LoginTab.Placeholders.Email} maxLength={30}
                        required />
                    <Form.Control.Feedback type="invalid">{resources.AuthorizeBox.LoginTab.InvalidText.Email}</Form.Control.Feedback>
                </InputGroup>
            </Form.Group>

            <Form.Group className="mb-4" controlId="userPassword">
                <Form.Label>{resources.AuthorizeBox.LoginTab.Labels.Password}</Form.Label>
                <InputGroup>
                    <Form.Control value={formState.password} onChange={(event) => setFormState(prev => { return { ...prev, password: event.target.value } })}
                        className="ig-form-control" type={formState.passwordVisible === true ? "text" : "password"}
                        placeholder={resources.AuthorizeBox.LoginTab.Placeholders.Password} pattern={passwordValidPattern} maxLength={30} required />
                        <i onClick={() => setFormState(prev => { return { ...prev, passwordVisible: !(prev.passwordVisible) } })} class={ formState.passwordVisible ? "bi bi-eye fs-4 eye-icon" : "bi bi-eye-slash fs-4 eye-icon"} style={{marginLeft:"10px"}}></i>
                    <Form.Control.Feedback type="invalid">{resources.AuthorizeBox.LoginTab.InvalidText.Password}</Form.Control.Feedback>
                </InputGroup>
            </Form.Group>

            {/* <Form.Group className="mb-4" controlId="showPasswordButton">
                <Form.Text className="text-muted me-2">{resources.AuthorizeBox.LoginTab.Labels.ShowPasswordButton}</Form.Text>
                <input value={formState.passwordVisible} onChange={() => setFormState(prev => { return { ...prev, passwordVisible: !(prev.passwordVisible) } })}
                    type="checkbox" />
            </Form.Group> */}

            <Form.Group className="text-center mt-3">
                <Button variant="primary" type="submit" className="submit-button">{resources.AuthorizeBox.LoginTab.ButtonText}</Button>
            </Form.Group>
        </Form>
    );
}