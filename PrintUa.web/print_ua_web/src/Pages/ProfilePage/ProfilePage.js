import { React } from "react";
import "./ProfilePage.sass";
import { Container } from "react-bootstrap";
import ProfileForm from "./Components/ProfileForm";

function ProfilePage() {

    return (
        <div className="profile-page">
            <div className="profile-container">
                <ProfileForm />
            </div>
        </div>
    );
}

export default ProfilePage;