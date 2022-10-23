import { useState } from "react";
import { AppConfiguration } from "read-appsettings-json";
import { getJwt } from "../Helpers";

export function AddBalance(props) {
    const [additionalBalance, setAdditionalBalance] = useState(0);

    const handleSubmit = async (e) => {
        e.preventDefault();

        const jwt = getJwt();

        const formDataForGetUser = new FormData();

        formDataForGetUser.append("jwt", jwt);

        const responseWithUserData = await fetch(AppConfiguration.Setting().GetUserByJwtUrl, {
            method: "POST",
            headers: {
                "Authorization": "Bearer " + jwt,
            },
            body: formDataForGetUser,
        });

        const user = await responseWithUserData.json();

        const formData = new FormData();

        formData.append("id", user.id);
        formData.append("email", user.email);
        formData.append("firstName", user.firstName);
        formData.append("lastName", user.lastName);
        formData.append("language", user.language);
        formData.append("timeZone", user.timeZone);
        formData.append("balance", +user.balance + +additionalBalance);

        await fetch(AppConfiguration.Setting().UpdateUserUrl, {
            method: "POST",
            headers: {
                "Authorization": "Bearer " + jwt,
            },
            body: formData,
        })

        props.history.push("/user/profile");
    }

    return (
        <form onSubmit={handleSubmit}>
            <div>
                <label for="AddBalance">Additional balance:</label>
                <br />
                <input
                    type="number"
                    name="AddBalance"
                    value={additionalBalance}
                    onChange={(e) => setAdditionalBalance(e.target.value)}
                />
            </div>
            <div>
                <input type="submit" value="Add" />
            </div>
        </form>
    );
}