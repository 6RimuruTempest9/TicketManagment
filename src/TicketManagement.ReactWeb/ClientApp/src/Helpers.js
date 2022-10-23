import { AppConfiguration } from "read-appsettings-json";
import Cookies from "universal-cookie";

export async function getRoles() {
    const jwt = getJwt();

    if (jwt) {
        const formData = new FormData();

        formData.append("jwt", jwt);

        const response = await fetch(AppConfiguration.Setting().GetUserRolesByJwtUrl, {
            method: "POST",
            body: formData,
        })

        if (response.ok) {
            return await response.json();
        }
    }

    return [];
}

export function getJwt() {
    const cookies = new Cookies();
    
    return cookies.get("JWT");
}