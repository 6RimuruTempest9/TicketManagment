import { useState } from "react";
import { AppConfiguration } from "read-appsettings-json";
import { getJwt } from "../Helpers";

export function ChangePassword(props) {
    const [oldPassword, setOldPassword] = useState("");
    const [newPassword, setNewPassword] = useState("");

    const handleSubmit = async (e) => {
        e.preventDefault();

        const jwt = getJwt();

        const formDataForUserId = new FormData();

        formDataForUserId.append("jwt", jwt);

        const response = await fetch(AppConfiguration.Setting().GetUserIdByJwtUrl, {
            method: "POST",
            body: formDataForUserId,
        });

        const userId = await response.text();

        const formData = new FormData();

        formData.append("userId", userId);
        formData.append("oldPassword", oldPassword);
        formData.append("newPassword", newPassword);

        await fetch(AppConfiguration.Setting().ChangeUserPasswordUrl, {
            method: "POST",
            headers: {
                "Authorization": "Bearer " + jwt,
            },
            body: formData,
        })

        props.history.push("/user/profile");
    };

    return (
        <form onSubmit={handleSubmit}>
            <div>
                <label for="OldPassword">Old password: </label>
                <input
                    type="password"
                    name="OldPassword"
                    value={oldPassword}
                    onChange={(e) => setOldPassword(e.target.value)}
                />
            </div>
            <div>
                <label for="NewPassword">New password: </label>
                <input
                    type="password"
                    name="NewPassword"
                    value={newPassword}
                    onChange={(e) => setNewPassword(e.target.value)}
                />
            </div>
            <div>
                <input type="submit" value="Change" />
            </div>
        </form>
    );
}