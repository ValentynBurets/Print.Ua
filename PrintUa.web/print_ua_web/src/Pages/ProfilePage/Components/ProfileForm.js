import { React, useState, useEffect } from "react";
import "./ProfileForm.sass";
import resources from "../../../jsonData/English/StartPage.json";
import connection from "../../../jsonData/ConnectionConfig.json";
import { Container, Form, Button, InputGroup } from "react-bootstrap";
import axios from "axios";

function ProfileForm() {
    const [formState, setFormState] = useState({
        passwordVisible: false,
        confirmPassword: false,
        submitButtonActive: false,
        validated: false,
        serverMessage: "",
        requestCompleted: false
    });

    const [confirmPasswordState, setConfirmPasswordState] = useState({
        password: "",
        passwordVisible: false,
    });

    const [fieldsState, setFieldsState] = useState({
        name: "",
        surname: "",
        phoneNumber: "",
        email: "",
        password: "",
    });

    const [fieldsStateCash, setFieldsStateCash] = useState({
        name: "",
        surname: "",
        phoneNumber: "",
        email: ""
    });

    const [fieldsModifiedState, setFieldsModifiedState] = useState({
        name: false,
        surname: false,
        phoneNumber: false,
        email: false,
        password: false,
    });

    useEffect(() => {
        let profileInfo = axios({
            url: connection.ServerUrl + connection.Routes.GetProfileInfo,
            method: "GET",
            headers: {
                "Content-type": "application/json; charset=UTF-8",
                "Authorization": `Bearer ${localStorage.getItem("token")}`
            }
        });

        let profilePhone = axios({
            url: connection.ServerUrl + connection.Routes.GetProfilePhone,
            method: "GET",
            headers: {
                "Content-type": "application/json; charset=UTF-8",
                "Authorization": `Bearer ${localStorage.getItem("token")}`
            }
        });

        let profileEmail = axios({
            url: connection.ServerUrl + connection.Routes.GetProfileEmail,
            method: "GET",
            headers: {
                "Content-type": "application/json; charset=UTF-8",
                "Authorization": `Bearer ${localStorage.getItem("token")}`
            }
        });

        Promise.all([profileInfo, profilePhone, profileEmail]).then((requests) => {
            let infoData = requests[0].data;
            let phone = requests[1].data;
            let email = requests[2].data;

            setFieldsStateCash({
                name: infoData.name,
                surname: infoData.surname,
                phoneNumber: phone,
                email: email
            });

            setFieldsState(() => {
                return {
                    name: infoData.name,
                    surname: infoData.surname,
                    phoneNumber: phone,
                    email: email
                };
            });
        });
    }, []);

    useEffect(() => {
        setFieldsModifiedState(() => {
            return {
                name: fieldsState.name !== fieldsStateCash.name,
                surname: fieldsState.surname !== fieldsStateCash.surname,
                phoneNumber: fieldsState.phoneNumber !== fieldsStateCash.phoneNumber,
                email: fieldsState.email !== fieldsStateCash.email,
                password: Boolean(fieldsState.password)
            }
        });
    }, [fieldsState]);

    useEffect(() => {
        setFormState((prev) => {
            return {
                ...prev,
                submitButtonActive: Object.values(fieldsModifiedState).includes(true),
                confirmPassword: fieldsModifiedState.phoneNumber || fieldsModifiedState.email || fieldsModifiedState.password
            }
        });
        // formState.submitButtonActive = !formState.submitButtonActive;
    }, [fieldsModifiedState]);

    const nameValidPattern = "[а-яА-ЯёЁіІїЇєЄa-zA-Z]{2,20}$";
    const surnameValidPattern = "[а-яА-ЯёЁіІїЇєЄa-zA-Z]{2,20}$";
    const phoneValidPattern = "^\\+380[0-9]{9}$";
    const passwordValidPattern = "(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[*.!@$%^&(){}:;<>,.?~_+-=|]).{10,25}$";

    function checkDataChanges(update) {
        setFieldsState(prev => {
            return {
                ...prev,
                ...update
            };
        });
    }

    function handleSubmit(event) {
        event.preventDefault();

        setFormState(prev => { return { ...prev, validated: true } });

        const isFormValid = event.currentTarget.checkValidity();

        if (isFormValid) {
            let requests = [];

            let currentData = fieldsState;

            if (fieldsModifiedState.name || fieldsModifiedState.surname) {
                requests.push(
                    axios({
                        url: connection.ServerUrl + connection.Routes.UpdateProfileInfo,
                        method: "PUT",
                        data: {
                            name: currentData.name,
                            surname: currentData.surname
                        },
                        headers: {
                            "Content-type": "application/json; charset=UTF-8",
                            "Authorization": `Bearer ${localStorage.getItem("token")}`
                        }
                    })
                );
            }

            if (fieldsModifiedState.phoneNumber) {
                requests.push(
                    axios({
                        url: connection.ServerUrl + connection.Routes.UpdateProfilePhone,
                        method: "PUT",
                        data: {
                            newPhoneNumber: currentData.phoneNumber,
                            currentPassword: confirmPasswordState.password
                        },
                        headers: {
                            "Content-type": "application/json; charset=UTF-8",
                            "Authorization": `Bearer ${localStorage.getItem("token")}`
                        }
                    })
                );
            }

            if (fieldsModifiedState.email) {
                requests.push(
                    axios({
                        url: connection.ServerUrl + connection.Routes.UpdateProfileEmail,
                        method: "PUT",
                        data: {
                            newEmail: currentData.email,
                            currentPassword: confirmPasswordState.password
                        },
                        headers: {
                            "Content-type": "application/json; charset=UTF-8",
                            "Authorization": `Bearer ${localStorage.getItem("token")}`
                        }
                    })
                );
            }

            if (fieldsModifiedState.password) {
                requests.push(
                    axios({
                        url: connection.ServerUrl + connection.Routes.UpdateProfilePassword,
                        method: "PUT",
                        data: {
                            newPassword: currentData.password,
                            currentPassword: confirmPasswordState.password
                        },
                        headers: {
                            "Content-type": "application/json; charset=UTF-8",
                            "Authorization": `Bearer ${localStorage.getItem("token")}`
                        }
                    })
                );
            }

            if (requests.length > 0) {
                Promise.all(requests)
                    .then((responses) => {
                        let statusCodeArray = [];

                        responses.map((value) => statusCodeArray.push(value.status));

                        if (statusCodeArray.every((value) => value === 200)) {
                            setFieldsStateCash({
                                name: currentData.name,
                                surname: currentData.surname,
                                phoneNumber: currentData.phoneNumber,
                                email: currentData.email
                            });

                            setFieldsState(() => {
                                return {
                                    ...currentData,
                                    password: ""
                                };
                            });

                            setFormState((prev) => {
                                return {
                                    ...prev,
                                    serverMessage: "Success",
                                    requestCompleted: true
                                }
                            });
                        }
                    }).catch((error) => {
                        if (error.response) {
                            setFormState((prev) => {
                                return {
                                    ...prev,
                                    serverMessage: error.response.data,
                                    requestCompleted: false
                                }
                            });
                        }
                        else {
                            setFormState((prev) => {
                                return {
                                    ...prev,
                                    serverMessage: "Connection error",
                                    requestCompleted: false
                                }
                            });
                        }
                    });
            }
        }
    }

    return (
        <Container className="profile-form-container">
            <Form className="m-0 profile-form" noValidate validated={formState.validated} onSubmit={handleSubmit}>
                <p className="fw-bolder fs-2">Profile</p>
                <div className={`message-container ${Boolean(formState.serverMessage) ? "visible" : "hidden"}  ${formState.requestCompleted ? "successResponse" : "errorResponse"}`}>
                    {formState.serverMessage}
                </div>
                <Form.Group className="mb-3 mt-5">
                    <Form.Label>{resources.AuthorizeBox.RegisterTab.Labels.Name}</Form.Label>
                    <InputGroup>
                        <Form.Control value={fieldsState.name} onChange={(event) => checkDataChanges({ name: event.target.value })}
                            className="ig-form-control" type="text" pattern={nameValidPattern} maxLength={20} required />
                        <Form.Control.Feedback type="invalid">{resources.AuthorizeBox.RegisterTab.InvalidText.Name}</Form.Control.Feedback>
                    </InputGroup>
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>{resources.AuthorizeBox.RegisterTab.Labels.Surname}</Form.Label>
                    <InputGroup>
                        <Form.Control value={fieldsState.surname} onChange={(event) => checkDataChanges({ surname: event.target.value })}
                            className="ig-form-control" type="text" pattern={surnameValidPattern} maxLength={20} required />
                        <Form.Control.Feedback type="invalid">{resources.AuthorizeBox.RegisterTab.InvalidText.Surname}</Form.Control.Feedback>
                    </InputGroup>
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>{resources.AuthorizeBox.RegisterTab.Labels.Phone}</Form.Label>
                    <InputGroup>
                        <Form.Control value={fieldsState.phoneNumber} onChange={(event) => checkDataChanges({ phoneNumber: event.target.value })}
                            className="ig-form-control" type="text" maxLength={13} pattern={phoneValidPattern} required />
                        <Form.Control.Feedback type="invalid">{resources.AuthorizeBox.RegisterTab.InvalidText.Phone}</Form.Control.Feedback>
                    </InputGroup>
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>{resources.AuthorizeBox.RegisterTab.Labels.Email}</Form.Label>
                    <InputGroup>
                        <Form.Control value={fieldsState.email} onChange={(event) => checkDataChanges({ email: event.target.value })}
                            className="ig-form-control" type="email" maxLength={20} required />
                        <Form.Control.Feedback type="invalid">{resources.AuthorizeBox.RegisterTab.InvalidText.Email}</Form.Control.Feedback>
                    </InputGroup>
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>{resources.AuthorizeBox.RegisterTab.Labels.NewPassword}</Form.Label>
                    <InputGroup>
                        <Form.Control value={fieldsState.password} onChange={(event) => checkDataChanges({ password: event.target.value })}
                            className="ig-form-control" type={formState.passwordVisible === true ? "text" : "password"}
                            pattern={passwordValidPattern} maxLength={30} />
                        <i onClick={() => setFormState(prev => { return { ...prev, passwordVisible: !(prev.passwordVisible) } })}
                            class={formState.passwordVisible ? "bi bi-eye fs-4 eye-icon" : "bi bi-eye-slash fs-4 eye-icon"} style={{ marginLeft: "10px" }}></i>
                        <Form.Control.Feedback type="invalid">{resources.AuthorizeBox.RegisterTab.InvalidText.Password}</Form.Control.Feedback>
                    </InputGroup>
                </Form.Group>

                {formState.confirmPassword ? (
                    <div>
                        <Form.Group className="mb-1">
                            <Form.Label>*{resources.AuthorizeBox.RegisterTab.Labels.CurrentPassword}</Form.Label>
                            <InputGroup>
                                <Form.Control value={confirmPasswordState.password} onChange={(event) => setConfirmPasswordState(prev => { return { ...prev, password: event.target.value } })}
                                    className="ig-form-control" type={confirmPasswordState.passwordVisible === true ? "text" : "password"}
                                    pattern={passwordValidPattern} maxLength={30} required />
                                <i onClick={() => setConfirmPasswordState(prev => { return { ...prev, passwordVisible: !(prev.passwordVisible) } })}
                                    class={confirmPasswordState.passwordVisible ? "bi bi-eye fs-4 eye-icon" : "bi bi-eye-slash fs-4 eye-icon"} style={{ marginLeft: "10px" }}></i>
                                <Form.Control.Feedback type="invalid">{resources.AuthorizeBox.RegisterTab.InvalidText.Password}</Form.Control.Feedback>
                            </InputGroup>
                        </Form.Group>
                    </div>
                ) : null}
                <Form.Group className="mt-4 text-center">
                    <Button variant="primary" type="submit" className="submit-button" disabled={!formState.submitButtonActive}>
                        {resources.AuthorizeBox.RegisterTab.SubmitButtonText}
                    </Button>
                </Form.Group>
            </Form>
        </Container>
    );
}

export default ProfileForm;