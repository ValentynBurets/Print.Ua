import "../../AuthorizeBox.sass";
import resources from "../../../../../../jsonData/English/StartPage.json";
import connection from "../../../../../../jsonData/ConnectionConfig.json";
import { React, useState } from "react";
import { useHistory } from "react-router-dom";
import { Form, Button, InputGroup } from "react-bootstrap";
import AuthorizeService from "../../../../Services/AuthorizeService";
import { useAuth } from "../../../../../../Components/AuthProvider/AuthProvider";

function RegisterForm() {

    const currentHistory = useHistory();
    const { login } = useAuth();

    const [formState, setFormState] = useState({
        name: "",
        surname: "",
        phone: "",
        email: "",
        password: "",
        passwordVisible: false,
        validated: false,
        authorizedState: false,
        authorizedMessage: "",
        errorMessage: ""
    });

    const nameValidPattern = "[а-яА-ЯёЁіІїЇєЄa-zA-Z]{2,20}$";
    const surnameValidPattern = "[а-яА-ЯёЁіІїЇєЄa-zA-Z]{2,20}$";
    const phoneValidPattern = "^\\+380[0-9]{9}$";
    const passwordValidPattern = "(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[*.!@$%^&(){}:;<>,.?~_+-=|]).{10,25}$";

    function handleSubmit(event) {
        event.preventDefault();

        setFormState(prev => { return { ...prev, validated: true } });

        const isFormValid = event.currentTarget.checkValidity();

        if (isFormValid) {
            const userData = {
                firstName: formState.name,
                lastName: formState.surname,
                phoneNumber: formState.phone,
                email: formState.email,
                password: formState.password
            }

            AuthorizeService.request(userData, connection.ServerUrl + connection.Routes.RegisterCustomer).then(response => {
                if (response.authorize === true) {
                    // Set jwt token
                    localStorage.setItem("token", response.token);

                    login();

                    currentHistory.push("/new_order");
                }
                else {
                    let error = resources.AuthorizeBox.RegisterTab.ErrorText.IncorrectData;

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

            <Form.Group className="mb-3" controlId="userName">
                <Form.Label>{resources.AuthorizeBox.RegisterTab.Labels.Name}</Form.Label>
                <InputGroup>
                    <Form.Control value={formState.name} onChange={(event) => setFormState(prev => { return { ...prev, name: event.target.value }; })}
                        className="ig-form-control" type="text" placeholder={resources.AuthorizeBox.RegisterTab.Placeholders.Name}
                        pattern={nameValidPattern} maxLength={20} required />
                    <Form.Control.Feedback type="invalid" >{resources.AuthorizeBox.RegisterTab.InvalidText.Name}</Form.Control.Feedback>
                </InputGroup>
            </Form.Group>

            <Form.Group className="mb-3" controlId="userSurname">
                <Form.Label>{resources.AuthorizeBox.RegisterTab.Labels.Surname}</Form.Label>
                <InputGroup>
                    <Form.Control value={formState.surname} onChange={(event) => setFormState(prev => { return { ...prev, surname: event.target.value }; })}
                        className="ig-form-control" type="text" placeholder={resources.AuthorizeBox.RegisterTab.Placeholders.Surname}
                        pattern={surnameValidPattern} maxLength={20} required />
                    <Form.Control.Feedback type="invalid" >{resources.AuthorizeBox.RegisterTab.InvalidText.Surname}</Form.Control.Feedback>
                </InputGroup>
            </Form.Group>

            <Form.Group className="mb-3" controlId="userPhone">
                <Form.Label>{resources.AuthorizeBox.RegisterTab.Labels.Phone}</Form.Label>
                <InputGroup>
                    <Form.Control value={formState.phone} onChange={(event) => setFormState(prev => { return { ...prev, phone: event.target.value }; })}
                        className="ig-form-control" type="text" placeholder={resources.AuthorizeBox.RegisterTab.Placeholders.Phone} pattern={phoneValidPattern}
                        maxLength={13} />
                    <Form.Control.Feedback type="invalid">{resources.AuthorizeBox.RegisterTab.InvalidText.Phone}</Form.Control.Feedback>
                </InputGroup>
            </Form.Group>

            <Form.Group className="mb-3" controlId="userEmail">
                <Form.Label>{resources.AuthorizeBox.RegisterTab.Labels.Email}</Form.Label>
                <InputGroup>
                    <Form.Control value={formState.email} onChange={(event) => setFormState(prev => { return { ...prev, email: event.target.value }; })}
                        className="ig-form-control" type="email" placeholder={resources.AuthorizeBox.RegisterTab.Placeholders.Email} maxLength={30} required />
                    <Form.Control.Feedback type="invalid">{resources.AuthorizeBox.RegisterTab.InvalidText.Email}</Form.Control.Feedback>
                </InputGroup>
            </Form.Group>

            <Form.Group className="mb-4" controlId="userPassword">
                <Form.Label>{resources.AuthorizeBox.RegisterTab.Labels.Password}</Form.Label>
                <InputGroup>
                    <Form.Control value={formState.password} onChange={(event) => setFormState(prev => { return { ...prev, password: event.target.value }; })}
                        className="ig-form-control" type={formState.passwordVisible === true ? "text" : "password"}
                        placeholder={resources.AuthorizeBox.RegisterTab.Placeholders.Password} pattern={passwordValidPattern} maxLength={30} required />
                         <i onClick={() => setFormState(prev => { return { ...prev, passwordVisible: !(prev.passwordVisible) } })} class={ formState.passwordVisible ? "bi bi-eye fs-4 eye-icon" : "bi bi-eye-slash fs-4 eye-icon"} style={{marginLeft:"10px"}}></i>
                    <Form.Control.Feedback type="invalid">{resources.AuthorizeBox.RegisterTab.InvalidText.Password}</Form.Control.Feedback>
                </InputGroup>
            </Form.Group>

            {/* <Form.Group className="mb-4" controlId="showPasswordButton">
                <Form.Text className="text-muted me-2">{resources.AuthorizeBox.RegisterTab.Labels.ShowPasswordButton}</Form.Text>
                <input value={formState.passwordVisible} onChange={() => setFormState(prev => { return { ...prev, passwordVisible: !(prev.passwordVisible) } })}
                    type="checkbox" />
            </Form.Group> */}

            <Form.Group className="text-center mt-3" controlId="submitButton">
                <Button variant="primary" type="submit" className="submit-button">{resources.AuthorizeBox.RegisterTab.ButtonText}</Button>
            </Form.Group>
        </Form>
    );
}

export default RegisterForm;