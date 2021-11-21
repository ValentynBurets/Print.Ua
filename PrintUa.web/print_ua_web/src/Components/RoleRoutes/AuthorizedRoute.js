import { React, useEffect, useState, createElement } from "react";
import { Route, Redirect } from "react-router-dom";
import { useAuth } from "../AuthProvider/AuthProvider";
import BadRequest from "../BadRequest/BadRequest";

function AuthorizedRoute({ role, component, ...params }) {

    const { user } = useAuth();

    const reactComponent = createElement(component);

    return (
        <Route {...params} render={() => user.auth ?
            user.role === role || role === "All" ?
                reactComponent
                : <BadRequest show={true} text={`Your role is not a ${role}`} />
            : <Redirect to="/" />} />
    );
}

export default AuthorizedRoute;